using System;
using System.Web.Mvc;
using Bicimad.Core;
using Bicimad.Services.Query;
using Bicimad.Services.Query.Interfaces;

namespace Bicimad.Api.Attributes
{
    public class ApiAuthorizeAttribute : AuthorizeAttribute
    {
        private const string SecurityToken = "token"; // Name of the url parameter.
        private static readonly EFRepository Repository = new EFRepository();
        private readonly ISecurityQueryService _securityQueryService = new SecurityQueryService(Repository);

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (Authorize(filterContext))
            {
                return;
            }
            filterContext.Result = new HttpUnauthorizedResult();
        }

        private bool Authorize(ControllerContext actionContext)
        {
            try
            {
                var request = actionContext.RequestContext.HttpContext.Request;
                var token = request.Params[SecurityToken];

                return _securityQueryService.IsTokenValid(token);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}