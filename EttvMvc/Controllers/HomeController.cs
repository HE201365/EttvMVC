using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EttvMvc.Models;
using EttvMvc.Services;

namespace EttvMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ChannelProgramService _channelProgramService;

        public HomeController()
        {
            _channelProgramService = new ChannelProgramService();
        }
        public ActionResult Index()
        {
            IEnumerable<ChannelProgram> chp = _channelProgramService.GetAll().Where(c => c.StartTime.Day <= DateTime.Now.Day);
            return View(chp);
        }

        public JsonResult GetIndexContentJsonResult()
        {
            IEnumerable<ChannelProgram> chp = _channelProgramService.GetAll().Where(c => c.StartTime.Day <= DateTime.Now.Day);
            return new JsonResult{Data = chp, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}