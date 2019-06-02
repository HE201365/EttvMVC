using System.Web.Mvc;
using EttvMvc.Helps;
using EttvMvc.Models;
using EttvMvc.Services;

namespace EttvMvc.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _us;

        public UserController()
        {
            _us = new UserService();
        }

        // GET: AppUser
        public ActionResult Index()
        {
            return View(new UserViewModel());
        }

        [HttpPost]
        public ActionResult Index(UserViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            if (_us.LoginService(viewModel))
            {
                if (UserSession.CurrentUser.Profile.Name == "Admin") { return RedirectToAction("Index", "Channel", new { area = "Admin" }); }
                if (UserSession.CurrentUser.Profile.Name == "Volunteer") { return RedirectToAction("Index", "Channel", new { area = "Admin" }); }
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");

            #region Without service

            //try
            //{
            //    HttpClient client = new HttpClient();
            //    client.BaseAddress = new Uri("https://localhost:44384/api/user/");
            //    string jsonString = JsonConvert.SerializeObject(viewModel);
            //    StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            //    AppUser results = new AppUser();

            //    HttpResponseMessage response = client.PostAsync("Login/", content).Result;
            //    if (response.IsSuccessStatusCode)
            //    {
            //        results = JsonConvert.DeserializeObject<AppUser>(response.Content.ReadAsStringAsync().Result);
            //        if (results != null)
            //        {
            //            UserSession.CurrentAppUser = results;
            //            if(results.Profile.Name=="Admin") { return RedirectToAction("Index", "Home", new { area = "Admin" });} 
            //            if(results.Profile.Name== "Volunteer") { return RedirectToAction("Index", "Channel", new { area = "Admin" }); }
            //            return RedirectToAction("Index", "Home");
            //        }

            //    }

            //    return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return RedirectToAction("Index");
            //}

            #endregion

        }

        public ActionResult Logoff()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}
