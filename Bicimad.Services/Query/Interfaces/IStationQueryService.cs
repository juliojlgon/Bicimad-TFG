using Bicimad.Services.Query.Dto.Station;

namespace Bicimad.Services.Query.Interfaces
{
    public interface IStationQueryService
    {
        StationDto GetStation(string stationId);
    }
}