using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using EttvMvc.Helps;
using EttvMvc.Models;
using Newtonsoft.Json;

namespace EttvMvc.Services
{
    public class UserService
    {
        public bool LoginService(UserViewModel viewModel)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:44384/api/user/");
                string jsonString = JsonConvert.SerializeObject(viewModel);
                StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                User results = new User();

                HttpResponseMessage response = client.PostAsync("Login/", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    results = JsonConvert.DeserializeObject<User>(response.Content.ReadAsStringAsync().Result);
                    if (results != null)
                    {
                        UserSession.CurrentUser = results;
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}