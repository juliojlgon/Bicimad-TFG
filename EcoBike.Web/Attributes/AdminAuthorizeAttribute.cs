using System.Web.Mvc;
using Bicimad.Web.Controllers;

namespace Bicimad.Web.Attributes
{
    public class AdminAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var user = ((BaseController) filterContext.Controller).CurrentUser;
                if (user.IsAdmin)
                {
                    return;
                }
            }

            filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}