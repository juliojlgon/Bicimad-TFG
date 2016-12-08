using System.Web.Http.Controllers;
using Bicimad.Api.Controllers;

namespace Bicimad.Api.Attributes
{
    public class AdminAuthorizeAttribute : ApiAuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var user = ((BaseController) actionContext.ControllerContext.Controller).CurrentUser;

            if (user.IsAdmin)
            {
                return;
            }

            HandleUnauthorizedRequest(actionContext);
        }
    }
}