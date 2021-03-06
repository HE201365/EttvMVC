﻿using System;
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
            //client.BaseAddress = new Uri("https://localhost:44384/Api/");
            client.BaseAddress = new Uri("https://ettv.azurewebsites.net/api/");
            //client.BaseAddress = new Uri("http://localhost:5000/api/");
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
            var status = false;
            // if startTime is earlier than DateTime.Now ??
            if ( GetAll().Any(x => x.StartTime.TrimSeconds() < starTime.TrimSeconds() && x.EndTime.TrimSeconds() > starTime.TrimSeconds()))
            {
                return status;
            }
            else
            {
                VideoContent currentContent = _contentService.GetAll().Where(v => v.VideoId == videoId).SingleOrDefault();
                ChannelProgram cp = new ChannelProgram
                {
                    StartTime = starTime,
                    EndTime = starTime.AddMilliseconds(currentContent.Duration),
                    AppUserId = UserSession.CurrentUser.Id,
                    VideoContentVideoId = videoId
                };
                try
                {
                    HttpClient client = new HttpClient();
                    //client.BaseAddress = new Uri("https://localhost:44384/Api/");
                    client.BaseAddress = new Uri("https://ettv.azurewebsites.net/api/");
                    //client.BaseAddress = new Uri("http://localhost:5000/api/");
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
                    Console.WriteLine(ex);
                    return false;
                }
            }
        }

        public bool Delete(int id)
        {
            var status = false;
            try
            {
                HttpClient client = new HttpClient();
                //client.BaseAddress = new Uri("https://localhost:44384/api/");
                client.BaseAddress = new Uri("https://ettv.azurewebsites.net/api/");
                //client.BaseAddress = new Uri("http://localhost:5000/api/");
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