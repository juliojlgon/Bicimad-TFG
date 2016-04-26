using System;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Bicimad.Api.Models.Account;

namespace Bicimad.Api.Controllers
{
    public partial class BaseController : ApiController
    {
        public UserLoggedModel CurrentUser;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            //try
            //{
            //    if (ControllerContext.Request.Headers.Contains("token"))
            //    {
            //        var token = controllerContext.Request.Headers.GetValues("token");

            //        var id = userCookieValuesArray[0];
            //        var email = userCookieValuesArray[1];
            //        var name = userCookieValuesArray[2];
            //        var avatar = userCookieValuesArray[3];
            //        var friendlyName = userCookieValuesArray[4];
            //        var isAdmin = Convert.ToBoolean(userCookieValuesArray[5]);

            //        CurrentUser = new UserLoggedModel
            //        {
            //            Id = ViewBag.UserId = id,
            //            Email = ViewBag.UserEmail = email,
            //            Name = ViewBag.UserName = name,
            //            Avatar = ViewBag.UserAvatar = avatar,
            //            FriendlyUrlName = ViewBag.FriendlyName = friendlyName,
            //            IsAdmin = ViewBag.UserIsAdmin = isAdmin
            //        };
            //    }
            //    else
            //    {
            //        ClearUserViewBag();
            //    }
            //}
            //catch (Exception err)
            //{
            //    ClearUserViewBag();
            //    FormsAuthentication.SignOut();
            //}

            base.Initialize(controllerContext);
        }
        
    }
}

