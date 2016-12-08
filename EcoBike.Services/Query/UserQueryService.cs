using System.Linq;
using Bicimad.Core;
using Bicimad.Core.DomainObjects;
using Bicimad.Helpers;
using Bicimad.Services.Query.Dto.User;
using Bicimad.Services.Query.Interfaces;

namespace Bicimad.Services.Query
{
    public class UserQueryService : IUserQueryService
    {
        private readonly IRepository _repository;

        public UserQueryService(IRepository repository)
        {
            _repository = repository;
        }

        public bool TryLogin(string userNameOrEmail, string password, out UserLoginDto outUser)
        {
            var lowerName = userNameOrEmail.ToLower();

            var user =
                _repository.Users.FirstOrDefault(
                    u => u.Email.ToLower() == lowerName || u.UserName.ToLower() == lowerName);

            if (user != null && HashHelper.CheckHash(password, user.Password))
            {
                return GetLoginResult(user, out outUser);
            }

            return GetLoginResult(null, out outUser);
        }

        public UserDto GetUser(string userId)
        {
            return ToDto(_repository.Users.FirstOrDefault(u => u.Id == userId));
        }

        public bool ExistsEmail(string email)
        {
            return _repository.Users.Any(u => u.Email == email);
        }

        public bool ExistsUserName(string userName)
        {
            return _repository.Users.Any(u => u.UserName == userName);
        }

        private static bool GetLoginResult(User user, out UserLoginDto outUser)
        {
            outUser = null;

            if (user == null || !user.IsActive)
            {
                return false;
            }

            outUser = new UserLoginDto
            {
                Avatar = user.Avatar,
                Email = user.Email,
                Id = user.Id,
                IsAdmin = user.IsAdmin,
                UserName = user.UserName,
                FriendlyUrlUserName = user.FriendlyUrlUserName
            };

            return true;
        }

        private static UserDto ToDto(User user)
        {
            if (user == null) return null;

            var dto = new UserDto
            {
                CreatedDate = user.CreatedDate,
                Id = user.Id,
                IsActive = user.IsActive,
                Avatar = user.Avatar,
                Email = user.Email,
                FriendlyUrlUserName = user.FriendlyUrlUserName,
                UserName = user.UserName,
                Name = user.UserName,
                IsAdmin = user.IsAdmin,
                Password = user.Password
            };

            return dto;
        }
    }
}