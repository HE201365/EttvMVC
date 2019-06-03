using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Xml;
using EttvMvc.Helps;
using EttvMvc.Models;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Newtonsoft.Json;

namespace EttvMvc.Services
{
    public class ContentService
    {
        public static readonly Regex VimeoVideoRegex = new Regex(@"vimeo\.com/(?:.*#|.*/videos/)?([0-9]+)", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        public static readonly Regex YoutubeVideoRegex = new Regex(@"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)", RegexOptions.IgnoreCase);
        public IEnumerable<VideoContent> GetAll()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44384/Api/");
            HttpResponseMessage response = client.GetAsync("VideoContent").Result;

            List<VideoContent> results = new List<VideoContent>();

            if (response.IsSuccessStatusCode)
            {
                results = JsonConvert.DeserializeObject<List<VideoContent>>(response.Content.ReadAsStringAsync().Result);
            }

            return results;
        }

        public bool Add(string url)
        {
            VideoContent model = MakeContentFromUrl(url);

            var status = false;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:44384/Api/");
                string JsonString = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(JsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync("VideoContent/", content).Result;

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

        public VideoContent GetByVideoId(string id)
        {
            return GetAll().Where(x => x.VideoId == id).SingleOrDefault();
        }

        public bool TagEdit(VideoContent model)
        {
            var status = false;
            try
            {
                if (model.VideoId != null)
                {
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri("https://localhost:44384/api/");
                    string JsonString = JsonConvert.SerializeObject(model);
                    StringContent content = new StringContent(JsonString, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PutAsync("VideoContent/" + model.VideoId, content).Result;
                    //Booking result = null;

                    if (response.IsSuccessStatusCode)
                    {
                        status = true;
                    }
                }
                return status;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        public bool Delete(string id)
        {
            var status = false;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:44384/api/");
                HttpResponseMessage response = client.DeleteAsync("VideoContent/" + id).Result;

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

        public VideoContent MakeContentFromUrl(string url)
        {
            string videoId = MakeVideoIdFromUrl(url);

            string apiKey = WebConfigurationManager.AppSettings["ApiKey"];
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = apiKey, // "AIzaSyA5RbAktplJsFLOdP8uI9s_RLEidsBTL34",
                ApplicationName = this.GetType().ToString()
            });
            var videoListRequest = youtubeService.Videos.List("snippet,contentDetails,statistics");
            videoListRequest.Id = videoId;
            var videoListResponse = videoListRequest.Execute();

            return new VideoContent
            {
                VideoId = videoListResponse.Items[0].Id.ToString(),
                Title = videoListResponse.Items[0].Snippet.Title.ToString(),
                Tag = videoListResponse.Items[0].Snippet.Tags[0].ToString(),
                Duration = convertYouTubeDuration(videoListResponse.Items[0].ContentDetails.Duration),
                Thumbnail = videoListResponse.Items[0].Snippet.Thumbnails.Default__.Url.ToString(),
                AppUserId = UserSession.CurrentUser.Id
            };
        }

        public string MakeVideoIdFromUrl(string url)
        {
            Match youtubeMatch = YoutubeVideoRegex.Match(url);
            Match vimeoMatch = VimeoVideoRegex.Match(url);
            string id = string.Empty;
            if (youtubeMatch.Success)
                id = youtubeMatch.Groups[1].Value;

            if (vimeoMatch.Success)
                id = vimeoMatch.Groups[1].Value;
            return id;
        }

        public int convertYouTubeDuration(string yt_duration)
        {
            TimeSpan youTubeDuration = XmlConvert.ToTimeSpan(yt_duration);
            int seconds = (int)youTubeDuration.TotalSeconds;
            return seconds;
        }
    }
}