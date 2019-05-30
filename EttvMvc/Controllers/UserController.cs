using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using EttvMvc.Helps;
using EttvMvc.Models;
using EttvMvc.Services;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

namespace EttvMvc.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View(new UserViewModel());
        }

        [HttpPost]
        public ActionResult Index(UserViewModel viewModel)
        {
            UserService us = new UserService();

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            if (us.LoginService(viewModel))
            {
                if (UserSession.CurrentUser.Profile.Name == "Admin") { return RedirectToAction("Index", "Home", new { area = "Admin" }); }
                if (UserSession.CurrentUser.Profile.Name == "Volunteer") { return RedirectToAction("Index", "Channel", new { area = "Admin" }); }
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");

            #region Without service

            //try
            //{
            //    HttpClient client = new HttpClient();
            //    client.BaseAddress = new Uri("https://localhost:44384/api/user/");
            //    string jsonString = JsonConvert.SerializeObject(viewModel);
            //    StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            //    User results = new User();

            //    HttpResponseMessage response = client.PostAsync("Login/", content).Result;
            //    if (response.IsSuccessStatusCode)
            //    {
            //        results = JsonConvert.DeserializeObject<User>(response.Content.ReadAsStringAsync().Result);
            //        if (results != null)
            //        {
            //            UserSession.CurrentUser = results;
            //            if(results.Profile.Name=="Admin") { return RedirectToAction("Index", "Home", new { area = "Admin" });} 
            //            if(results.Profile.Name== "Volunteer") { return RedirectToAction("Index", "Channel", new { area = "Admin" }); }
            //            return RedirectToAction("Index", "Home");
            //        }

            //    }

            //    return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return RedirectToAction("Index");
            //}

            #endregion

        }

        public ActionResult Logoff()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}
