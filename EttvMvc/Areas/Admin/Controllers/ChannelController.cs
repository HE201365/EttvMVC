using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EttvMvc.Helps;

namespace EttvMvc.Areas.Admin.Controllers
{
    public class ChannelController : Controller
    {
        [CustomAuth("Admin", "Volunteer")]
        // GET: Admin/Channel
        public ActionResult Index()
        {
            return View();
        }
    }
}