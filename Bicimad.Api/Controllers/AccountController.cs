using System.Web.Http;
using Bicimad.Api.Models.Account;
using Bicimad.Helpers;
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
        

        [System.Web.Mvc.HttpPost]
        public virtual IHttpActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                UserLoginDto userDto;
                var loginResult = _userQueryService.TryLogin(model.UserNameOrEmail, model.Password, out userDto);
                if (loginResult)
                {
                    var token = LogUser(userDto, model.RememberMe);
                    
                    return Json(token);
                }
            }

            return NotFound();
        }

        private string LogUser(UserLoginDto userDto, bool remember)
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

            return HashHelper.GenerateToken(userDto.UserName, userDto.Password);


        }
       
        [System.Web.Mvc.HttpPost]
        public virtual IHttpActionResult Register(string from, RegisterModel model)
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
                    return Ok();
                }
            }

            return BadRequest();
        }

        public virtual IHttpActionResult LogOut()
        {
            //Borrar el Token y regenerarlo en el siguiente login.
            return Ok();
        }

        /// <summary>
        ///     For valide register form.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public virtual IHttpActionResult ValidateEmailUnique(string email)
        {
            var exists = _userQueryService.ExistsEmail(email);
            return Json(!exists);
        }

        /// <summary>
        ///     For validate register Form.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public virtual IHttpActionResult ValidateUserNameUnique(string userName)
        {
            var exists = _userQueryService.ExistsUserName(userName);
            return Json(!exists);
        }
    }
}