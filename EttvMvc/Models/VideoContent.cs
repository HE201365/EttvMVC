using System.Collections.Generic;

namespace EttvMvc.Models
{
    public class VideoContent
    {
        public string VideoId { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        public string tag { get; set; }
        public string Thumbnail { get; set; }

        private static List<VideoContent> videoContents = new List<VideoContent>
        {
            new VideoContent{
                VideoId = "M7lc1UVf-VE",
                Description = "On this week's show, Jeff Posnick covers everything you need to know about using player parameters to customize the YouTube iframe-embedded player.", 
                Duration = 1344000, 
                Thumbnail = "https://i.ytimg.com/vi/M7lc1UVf-VE/default.jpg",
                tag = "youtubedataapi", 
                Title = "YouTube Developers Live: Embedded Web Player Customization"
            },
            new VideoContent{
                VideoId = "taJ60kskkns",
                Description = "SUBSCRIBE to the OFFICIAL BBC YouTube channel: https://bit.ly/2IXqEInLAUNCH BBC iPlayer to access Live TV and Box Sets: https://bbc.in/2J18jYJProgramme website: http://bbc.in/1KF88dm Are these Humpback Wales just showing off?#bbc", 
                Duration = 107000, 
                Thumbnail = "https://i.ytimg.com/vi/taJ60kskkns/default.jpg",
                tag = "Big blue live", 
                Title = "Humpback Whales having a laugh - Big Blue Live: Episode 1 - BBC One"
            },
            new VideoContent{
                VideoId = "9sWEecNUW-o",
                Description = "Create a dynamic YouTube playlist app using HTML, CSS, Javascript and jQuery.🔗The Completed App - https://codepen.io/Middi/pen/QQrOdB🔗 Thumbnail image - https://i.ytimg.com/vi/qxWrnhZEuRU/mqdefault.jpg🔗YouTube Logo - https://github.com/Middi/youtube-api/blob/master/images/logo.png🎥Check out Richard's YouTube channel - https://www.youtube.com/channel/UCimIdsDPn0mE03Cb7C6aR8Q--Learn to code for free and get a developer job: https://www.freecodecamp.comRead hundreds of articles on programming: https://medium.freecodecamp.comAnd subscribe for new videos on technology every day: https://youtube.com/subscription_center?add_user=freecodecamp", 
                Duration = 4055000, 
                Thumbnail = "https://i.ytimg.com/vi/9sWEecNUW-o/default.jpg",
                tag = "Big blue live", 
                Title = "Code your own YouTube app: YouTube API + HTML + CSS + JavaScript (full tutorial)"
            }
        };

        public static List<VideoContent> GetAll()
        {
            return videoContents;
        }
        public static VideoContent GetOne(string id)
        {
            return videoContents.Find(t => t.VideoId == id);
        }
    }
}