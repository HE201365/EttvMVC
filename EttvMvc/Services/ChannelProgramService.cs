using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using EttvMvc.Helps;
using EttvMvc.Models;
using Newtonsoft.Json;

namespace EttvMvc.Services
{
    public class ChannelProgramService
    {
        private readonly ContentService _contentService;

        public ChannelProgramService()
        {
            _contentService = new ContentService();
        }

        public IEnumerable<ChannelProgram> GetAll()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44384/Api/");
            HttpResponseMessage response = client.GetAsync("ChannelProgram").Result;

            List<ChannelProgram> results = new List<ChannelProgram>();

            if (response.IsSuccessStatusCode)
            {
                results = JsonConvert.DeserializeObject<List<ChannelProgram>>(response.Content.ReadAsStringAsync().Result);
            }

            return results;
        }

        public bool AddProgram(string videoId, DateTime starTime)
        {
            VideoContent currentContent = _contentService.GetAll().Where(v => v.VideoId == videoId).SingleOrDefault();
            ChannelProgram cp = new ChannelProgram
            {
                StartTime = starTime,
                EndTime = starTime.AddMilliseconds(currentContent.Duration),
                AppUserId = UserSession.CurrentUser.Id,
                VideoContentVideoId = videoId
            };
            var status = false;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:44384/Api/");
                string JsonString = JsonConvert.SerializeObject(cp);
                StringContent content = new StringContent(JsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync("channelprogram/", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    status = true;
                }

                return status;
            }
            catch (Exception ex)
            {
                //TODO loging ....
                Console.WriteLine(ex);
                return false;
            }
        }

        public bool Delete(int id)
        {
            var status = false;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:44384/api/");
                HttpResponseMessage response = client.DeleteAsync("channelprogram/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    status = true;
                }

                return status;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return status;
            }
        }
    }
}