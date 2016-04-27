using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;
using Bicimad.Api.Models.Account;

namespace Bicimad.Api.Controllers
{
    public class BaseController : ApiController
    {
        public UserLoggedModel CurrentUser;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            IEnumerable<string> headerValues;
            if (controllerContext.Request.Headers.TryGetValues("token", out headerValues))
            {
                var token = headerValues.First();

                // Base64 decode the string, obtaining the token:username:timeStamp.
                var key = Encoding.UTF8.GetString(Convert.FromBase64String(token));

                // Split the parts.
                var parts = key.Split(':');
                if (parts.Length == 4)
                {
                    // Get the hash message, username, and timestamp.
                    var hash = parts[0];
                    var userId = parts[1];
                    var username = parts[2];
                    var ticks = long.Parse(parts[3]);


                    CurrentUser = new UserLoggedModel
                    {
                        Id = userId,
                        Name = username
                    };
                }
            }


            base.Initialize(controllerContext);
        }
    }
}