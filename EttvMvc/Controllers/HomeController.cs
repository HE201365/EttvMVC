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
            DateTime testDate = Convert.ToDateTime(TempData["myDatetimeData"]);
            ViewBag.myDatetimeBag = testDate;

            IEnumerable<ChannelProgram> chp = _channelProgramService.GetAll().Where(c => c.StartTime.Day <= testDate.Day).OrderBy(x=>x.StartTime);
            return View(chp);
        }

        public JsonResult GetIndexContentJsonResult(string now)
        {
            double ticks = double.Parse(now);
            TimeSpan time = TimeSpan.FromMilliseconds(ticks);
            DateTime myDateTime = new DateTime(1970, 1, 1) + time;
            TempData["myDatetimeData"] = myDateTime;

            IEnumerable<ChannelProgram> chp = _channelProgramService.GetAll().Where(c => c.StartTime.Day <= myDateTime.Day);
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