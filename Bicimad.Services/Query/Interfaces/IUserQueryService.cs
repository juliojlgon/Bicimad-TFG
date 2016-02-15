using System.Collections.Generic;
using Bicimad.Services.Query.Dto.User;

namespace Bicimad.Services.Query.Interfaces
{
    public interface IUserQueryService
    {
        List<UserDto> GetUser(string userId);
    }
}