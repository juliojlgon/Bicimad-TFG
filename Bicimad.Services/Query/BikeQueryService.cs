using System.Collections.Generic;
using System.Linq;
using Bicimad.Core;
using Bicimad.Core.DomainObjects;
using Bicimad.Services.Query.Dto.Bike;
using Bicimad.Services.Query.Interfaces;

namespace Bicimad.Services.Query
{
    public class BikeQueryService: IBikeQueryService
    {
        private readonly IRepository _repository;

        public BikeQueryService(IRepository repostory)
        {
            _repository = repostory;
        }

        public List<BikeDto> GetBikes()
        {
            var bikes = _repository.Bikes.ToList();
            return bikes.Select(ToDto).ToList();
        }

        public List<BikeDto> GetBikesByStationId(string id)
        {
            return _repository.Bikes.Where(b => b.StationId == id).Select(ToDto).ToList();
        }

        public List<BikeDto> GetBikesByStationNameList(string name)
        {
            return _repository.Bikes.Where(b => b.Station.StationName == name).Select(ToDto).ToList(); 
        }

        /// <summary>
        /// Returns the first availiable bike of the station or null in case there is no availiable bike.
        /// </summary>
        /// <param name="stationId"></param>
        /// <returns>BikeDto or Null</returns>
        public BikeDto GetFreeBike(string stationId)
        {
            return ToDto(_repository.Bikes.FirstOrDefault(b => b.StationId == stationId && !b.IsActive && !b.IsBooked && b.IsWorking));
        }

        private static BikeDto ToDto(Bike bike)
        {
            if (bike == null) return null;

            var dto = new BikeDto
            {
                Id = bike.Id,
                IsWorking = bike.IsWorking,
                CreatedDate = bike.CreatedDate,
                IsBooked = bike.IsBooked,
                IsActive = bike.IsActive,
                StationId = bike.StationId
            };

            return dto;
        }
    }
}
