using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bicimad.Services.Command.Commands;

namespace Bicimad.Services.Command.Interface
{
    public interface IStationCommandService
    {
        CommandResult BookItem(string userId, string stationId, string itemId, bool type);
    }
}
