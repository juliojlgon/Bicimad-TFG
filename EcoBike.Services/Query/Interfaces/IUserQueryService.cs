using System.Collections.Generic;
using Bicimad.Services.Query.Dto.User;

namespace Bicimad.Services.Query.Interfaces
{
    public interface IUserQueryService
    {
        UserDto GetUser(string userId);
        bool ExistsEmail(string email);
        bool ExistsUserName(string userName);
        bool TryLogin(string userNameOrEmail, string password, out UserLoginDto userDto);
    }
}