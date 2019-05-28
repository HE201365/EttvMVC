using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EttvMvc.Helps;

namespace EttvMvc.Areas.Admin.Controllers
{
    [CustomAuth("Admin")]
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}