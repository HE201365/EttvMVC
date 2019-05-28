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
        public User User { get; set; }
        public VideoContent VideoContent { get; set; }

        private static List<ChannelProgram> channelPrograms = new List<ChannelProgram>
        {
            new ChannelProgram{
                Id = 1,
                StartTime = new DateTime(2019, 5, 29, 00, 00, 00),
                EndTime = new DateTime(2019, 5, 29, 00, 20, 00),
                VideoContent = VideoContent.GetOne("M7lc1UVf-VE"),
                ModifiedAt = new DateTime(2019, 5, 28, 00, 00, 00),
                User = null,
            },
            new ChannelProgram{
                Id = 2,
                StartTime = new DateTime(2019, 5, 29, 00, 21, 01),
                EndTime = new DateTime(2019, 5, 29, 01, 20, 00),
                VideoContent = VideoContent.GetOne("9sWEecNUW-o"),
                ModifiedAt = new DateTime(2019, 5, 28, 00, 00, 00),
                User = null,
            },
            new ChannelProgram{
                Id = 3,
                StartTime = new DateTime(2019, 5, 29, 01, 20, 01),
                EndTime = new DateTime(2019, 5, 29, 01, 41, 30),
                VideoContent = VideoContent.GetOne("taJ60kskkns"),
                ModifiedAt = new DateTime(2019, 5, 28, 00, 00, 00),
                User = null,
            }
        };

        public static List<ChannelProgram> GetAll()
        {
            return channelPrograms;
        }

        public static ChannelProgram GetOne(int id)
        {
            return channelPrograms.Find(t => t.Id == id);
        }
    }
}