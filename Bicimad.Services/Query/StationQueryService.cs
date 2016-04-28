using System.Collections.Generic;
using System.Linq;
using Bicimad.Core;
using Bicimad.Core.DomainObjects;
using Bicimad.Services.Query.Dto.Station;
using Bicimad.Services.Query.Interfaces;

namespace Bicimad.Services.Query
{
    public class StationQueryService : IStationQueryService
    {
        private readonly IRepository _repository;

        public StationQueryService(IRepository repository)
        {
            _repository = repository;
        }

        public StationDto GetStation(string stationId)
        {
            
            return ToDto(_repository.Stations.FirstOrDefault(s => s.Id == stationId));
        }

        public List<string> GetStationNames(List<string> stationIds)
        {
            var ids = _repository.Stations.Where(s => stationIds.Contains(s.Id)).Select(s => s.StationName).ToList();

            return ids;
        } 

        public List<StationDto> GetStations()
        {
            var stations =  _repository.Stations.ToList();
            return stations.Select(ToDto).ToList();
        }

        private static StationDto ToDto(Station station)
        {
            if (station == null) return null;

            var dto = new StationDto
            {
                BikeNum = station.BikeNum,
                CreatedDate = station.CreatedDate,
                Id = station.Id,
                ReservedSlots = station.ReservedSlots,
                Bus = station.Bus,
                FriendlyUrlStationName = station.FriendlyUrlStationName,
                Latitude = station.Latitude,
                Longitude = station.Longitude,
                Metro = station.Metro,
                StationName = station.StationName,
                FreeBikes = station.FreeBikes
            };

            return dto;
        }
    }
}