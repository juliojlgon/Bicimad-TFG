using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bicimad.Services.Query.Dto.History;

namespace Bicimad.Services.Query.Interfaces
{
    public interface IUserHistoryQueryService
    {
        List<UserHistoryDto> GetHistorial(string userId);
        UserHistoryDto GetUserHistory(string userId);
    }
}
