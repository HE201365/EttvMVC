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
            if (url != null)
                _contentService.Add(url);
            IEnumerable<VideoContent> videoContent = _contentService.GetAll();
            return View(videoContent);
        }


        public ActionResult TagEdit(string id)
        {
            VideoContent model = _contentService.GetByVideoId(id);
            return View(model);
        }
        // POST: Admin/Content
        [HttpPost]
        public ActionResult TagEdit(VideoContent model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _contentService.TagEdit(model);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            if (_contentService.Delete(id))
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}