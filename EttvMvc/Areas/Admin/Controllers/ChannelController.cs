using System;
using System.Collections.Generic;
using System.Web.Mvc;
using EttvMvc.Helps;
using EttvMvc.Helps.ViewModels;
using EttvMvc.Models;
using EttvMvc.Services;

namespace EttvMvc.Areas.Admin.Controllers
{
    public class ChannelController : Controller
    {
        private readonly ChannelProgramService _channelProgramService;
        private readonly ContentService _contentService;

        public ChannelController()
        {
            _channelProgramService = new ChannelProgramService();
            _contentService = new ContentService();
        }

        [CustomAuth("Admin", "Volunteer")]
        // GET: Admin/Channel
        public ActionResult Index()
        {
            IEnumerable<ChannelProgram> channelPrograms = _channelProgramService.GetAll();
            IEnumerable<VideoContent> videoContents = _contentService.GetAll();

            ChannelPageViewModel channelPageViewModel = new ChannelPageViewModel
            {
                ChannelPrograms = channelPrograms,
                VideoContents = videoContents
            };
            
            return View(channelPageViewModel);
        }

        [CustomAuth("Admin", "Volunteer")]
        public ActionResult AddProgram()
        {
            return View(new ChannelProgram());
        }
        // POST: Admin/Channel
        [HttpPost]
        public ActionResult AddProgram(ChannelProgram model)
        {
            DateTime start = model.StartTime;
            var id = Url.RequestContext.RouteData.Values["id"].ToString();
            _channelProgramService.AddProgram(id, start);

            IEnumerable<ChannelProgram> channelPrograms = _channelProgramService.GetAll();
            IEnumerable<VideoContent> videoContents = _contentService.GetAll();

            ChannelPageViewModel channelPageViewModel = new ChannelPageViewModel
            {
                ChannelPrograms = channelPrograms,
                VideoContents = videoContents
            };
            return RedirectToAction("Index",channelPageViewModel);
        }
    }
}