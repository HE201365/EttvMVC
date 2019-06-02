using System.Collections.Generic;

namespace EttvMvc.Models
{
    public class VideoContent
    {
        public string VideoId { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public string Tag { get; set; }
        public string Thumbnail { get; set; }
        public int AppUserId { get; set; }

        //private static List<VideoContent> videoContents = new List<VideoContent>
        //{
        //    new VideoContent{
        //        VideoId = "M7lc1UVf-VE",
        //        Duration = 1344000, 
        //        Thumbnail = "https://i.ytimg.com/vi/M7lc1UVf-VE/default.jpg",
        //        Tag = "youtubedataapi", 
        //        Title = "YouTube Developers Live: Embedded Web Player Customization",
        //        AppUser = null
        //    },
        //    new VideoContent{
        //        VideoId = "taJ60kskkns",
        //        Duration = 107000, 
        //        Thumbnail = "https://i.ytimg.com/vi/taJ60kskkns/default.jpg",
        //        Tag = "Big blue live", 
        //        Title = "Humpback Whales having a laugh - Big Blue Live: Episode 1 - BBC One",
        //        AppUser = null
        //    },
        //    new VideoContent{
        //        VideoId = "9sWEecNUW-o",
        //        Duration = 4055000, 
        //        Thumbnail = "https://i.ytimg.com/vi/9sWEecNUW-o/default.jpg",
        //        Tag = "Big blue live", 
        //        Title = "Code your own YouTube app: YouTube API + HTML + CSS + JavaScript (full tutorial)",
        //        AppUser = null
        //    }
        //};

        //public static List<VideoContent> GetAll()
        //{
        //    return videoContents;
        //}
        //public static VideoContent GetOne(string id)
        //{
        //    return videoContents.Find(t => t.VideoId == id);
        //}
    }
}