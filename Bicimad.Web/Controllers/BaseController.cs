using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Bicimad.Web.Models.AccountModels;

namespace Bicimad.Web.Controllers
{
    public partial class BaseController : Controller
    {
        public UserLoggedModel CurrentUser;

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            try
            {
                if (requestContext.HttpContext.User.Identity.IsAuthenticated)
                { 
                    var userCookieValues = requestContext.HttpContext.User.Identity.Name;
                    var userCookieValuesArray = userCookieValues.Split(';');

                    var id = userCookieValuesArray[0];
                    var email = userCookieValuesArray[1];
                    var name = userCookieValuesArray[2];
                    var avatar = userCookieValuesArray[3];
                    var friendlyName = userCookieValuesArray[4];
                    var isAdmin = Convert.ToBoolean(userCookieValuesArray[5]);

                    CurrentUser = new UserLoggedModel
                    {
                        Id = ViewBag.UserId = id,
                        Email = ViewBag.UserEmail = email,
                        Name = ViewBag.UserName = name,
                        Avatar = ViewBag.UserAvatar = avatar,
                        FriendlyUrlName = ViewBag.FriendlyName = friendlyName,
                        IsAdmin = ViewBag.UserIsAdmin = isAdmin
                    };
                }
                else
                {
                    ClearUserViewBag();
                }
            }
            catch (Exception err)
            {
                ClearUserViewBag();
                FormsAuthentication.SignOut();
            }

            return base.BeginExecute(requestContext, callback, state);
        }

        private void ClearUserViewBag()
        {
            ViewBag.UserId =
                ViewBag.UserEmail = ViewBag.UserName = ViewBag.UserAvatar = ViewBag.FriendlyName = string.Empty;
            ViewBag.UserIsSocial = ViewBag.UserIsAdmin = ViewBag.UserIsRoot = false;
        }
    }
}

