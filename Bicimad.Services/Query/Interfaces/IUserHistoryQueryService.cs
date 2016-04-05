using System.Collections.Generic;
using Bicimad.Services.Query.Dto.History;
using Bicimad.Services.Query.Queries;

namespace Bicimad.Services.Query.Interfaces
{
    public interface IUserHistoryQueryService
    {
        List<UserHistoryDto> GetHistorial(ref UserHistoryQuery quer);
        UserHistoryDto GetUserHistory(string userId);
    }
}
