using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bicimad.Services.Query.Dto.Station;

namespace Bicimad.Services.Query.Interfaces
{
    public interface IStationQueryService
    {
        StationDto GetStation(string stationId);
    }
}
