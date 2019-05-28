using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EttvMvc.Helps
{
    public class CustomAuthAttribute : AuthorizeAttribute
    {
        private string[] _rolesAuth;

        public CustomAuthAttribute(params string[] roles)
        {
            _rolesAuth = roles;
        }


        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (UserSession.CurrentUser == null || !_rolesAuth.Contains(UserSession.CurrentUser.Profile.Name))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary{
                    { "action", "Index" },
                    { "controller", "Home" },
                    { "Area", string.Empty }
                });
            }
        }
    }
}