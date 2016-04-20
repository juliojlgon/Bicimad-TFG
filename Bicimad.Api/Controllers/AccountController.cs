using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using Bicimad.Api.Models.Account;
using Bicimad.Services.Command.Commands.User;
using Bicimad.Services.Command.Interface;
using Bicimad.Services.Query.Dto.User;
using Bicimad.Services.Query.Interfaces;

namespace Bicimad.Api.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserCommandService _userCommandService;
        private readonly IUserQueryService _userQueryService;

        public AccountController(IUserQueryService userQueryService, IUserCommandService userCommandService)
        {
            _userQueryService = userQueryService;
            _userCommandService = userCommandService;
        }
        

        [HttpPost]
        public virtual ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                UserLoginDto userDto;
                var loginResult = _userQueryService.TryLogin(model.UserNameOrEmail, model.Password, out userDto);
                if (loginResult)
                {
                    LogUser(userDto, model.RememberMe);
                    
                    //TODO: REDIRIGIR A LA PAGINA ADECUADA
                    return RedirectToAction(MVC.Home.Index());
                }
            }

            ViewBag.ReturnUrl = returnUrl;
            return System.Web.UI.WebControls.View(MVC.Account.Views.Login, model);
        }

        private void LogUser(UserLoginDto userDto, bool remember)
        {
            CurrentUser = new UserLoggedModel
            {
                Id = userDto.Id,
                Email = userDto.Email,
                Avatar = userDto.Avatar,
                Name = userDto.UserName,
                IsAdmin = userDto.IsAdmin,
                FriendlyUrlName = userDto.FriendlyUrlUserName
            };

            FormsAuthentication.SetAuthCookie(CurrentUser.SerializeForCookie(), remember);
        }

        public virtual ActionResult Register()
        {
            return System.Web.UI.WebControls.View(MVC.Account.Views.Register, new RegisterModel());
        }

        [HttpPost]
        public virtual ActionResult Register(string from, RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var createUserResult = _userCommandService.Create(new CreateUserCommand
                {
                    UserName = model.UserName,
                    FriendlyUrlUserName = model.UserName.Sanitize(),
                    Email = model.Email,
                    Password = model.Password,
                    IsActive = true
                });

                if (createUserResult.Success)
                {
                    return RedirectToAction(MVC.Account.Login());
                }
            }

            return System.Web.UI.WebControls.View(MVC.Account.Views.Register, model);
        }

        public virtual ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction(MVC.Home.Index());
        }

        /// <summary>
        ///     For valide register form.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public virtual ActionResult ValidateEmailUnique(string email)
        {
            var exists = _userQueryService.ExistsEmail(email);
            return Json(!exists, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///     For validate register Form.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public virtual ActionResult ValidateUserNameUnique(string userName)
        {
            var exists = _userQueryService.ExistsUserName(userName);
            return Json(!exists, JsonRequestBehavior.AllowGet);
        }
    }
}