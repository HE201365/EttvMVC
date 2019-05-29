using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EttvMvc.Models;

namespace EttvMvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<ChannelProgram> chp = ChannelProgram.GetAll();
            return View(chp);
        }

        public JsonResult GetIndexContentJsonResult()
        {
            List<ChannelProgram> chp = ChannelProgram.GetAll();
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