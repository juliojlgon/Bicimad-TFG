using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using Bicimad.Core;
using Bicimad.Services.Query;
using Bicimad.Services.Query.Interfaces;
using AuthorizeAttribute = System.Web.Http.AuthorizeAttribute;

namespace Bicimad.Api.Attributes
{
    public class ApiAuthorizeAttribute : AuthorizeAttribute
    {
        private const string SecurityToken = "token"; // Name of the url parameter.
        private static readonly EFRepository Repository = new EFRepository();
        private readonly ISecurityQueryService _securityQueryService = new SecurityQueryService(Repository);

         public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)

        {

            if (AuthorizeRequest(actionContext))

            {

                return;

            }

            HandleUnauthorizedRequest(actionContext);

        }

        private bool AuthorizeRequest(HttpActionContext actionContext)

        {

            try
            {
                var request = actionContext.Request;
                IEnumerable<string> headerValues;
                request.Headers.TryGetValues("token", out headerValues);
                var token = headerValues.First();

                return _securityQueryService.IsTokenValid(token);
            }
            catch (Exception)
            {
                return false;
            }

        }
        
    }
}