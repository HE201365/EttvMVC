using System.Collections.Generic;
using System.Web.Mvc;
using EttvMvc.Models;
using EttvMvc.Services;

namespace EttvMvc.Areas.Admin.Controllers
{
    public class ContentController : Controller
    {
        private readonly ContentService _contentService;

        public ContentController()
        {
            _contentService = new ContentService();
        }

        // GET: Admin/Content
        public ActionResult Index()
        {
            IEnumerable<VideoContent> videoContent = _contentService.GetAll();
            return View(videoContent);
        }

        // POST: Admin/Content
        [HttpPost]
        public ActionResult Index(string url)
        {
            _contentService.Add(url);
            IEnumerable<VideoContent> videoContent = _contentService.GetAll();
            return View(videoContent);
        }
    }
}