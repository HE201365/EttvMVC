using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EttvMvc.Models;

namespace EttvMvc.Helps.ViewModels
{
    public class ChannelPageViewModel
    {
       public IEnumerable<ChannelProgram> ChannelPrograms { get; set; }
       public IEnumerable<VideoContent> VideoContents { get; set; }
    }
}