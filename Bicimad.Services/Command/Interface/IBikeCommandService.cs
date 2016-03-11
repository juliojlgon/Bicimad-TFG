using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bicimad.Services.Command.Commands;
using Bicimad.Services.Command.Commands.Bike;

namespace Bicimad.Services.Command.Interface
{
    public  interface IBikeCommandService
    {
        CommandResult TakeBike(string userId, string stationId, string bikeId);
    }
}
