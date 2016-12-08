using Bicimad.Core.DomainObjects;
using Bicimad.Services.Command.Commands;

namespace Bicimad.Services.Command.Interface
{
    public interface IBikeCommandService
    {
        CommandResult TakeBike(string userId, string stationId, string bikeId);
        CommandResult ReturnBike(string userId, string arrivalStationId);
        CommandResult InformBrokenBike(string bikeId);
    }
}