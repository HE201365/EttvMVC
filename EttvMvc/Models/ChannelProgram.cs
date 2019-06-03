using System;
using System.Collections.Generic;

namespace EttvMvc.Models
{
    public class ChannelProgram
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime ModifiedAt { get; set; }
        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }
        public VideoContent VideoContent { get; set; }
        public string VideoContentVideoId { get; set; }
    }
}