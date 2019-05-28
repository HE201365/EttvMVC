using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EttvMvc.Models;

namespace EttvMvc.Helps
{
    public static class UserSession
    {

        public static User CurrentUser
        {
            get { return (User)HttpContext.Current.Session["CurrentUser"]; }
            set { HttpContext.Current.Session["CurrentUser"] = value; }
        }
    }
}