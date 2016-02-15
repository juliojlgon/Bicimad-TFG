using System.Linq;
using AutoMapper;
using Bicimad.Core;
using Bicimad.Core.DomainObjects;
using Bicimad.Services.Query.Dto.Station;
using Bicimad.Services.Query.Interfaces;

namespace Bicimad.Services.Query
{
    public class StationQueryService : IStationQueryService
    {
        private readonly IRepository _repostory;

        public StationQueryService(IRepository repository)
        {
            _repostory = repository;
        }

        public StationDto GetStation(string stationId)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Station, StationDto>());
            var mapper = config.CreateMapper();
            return mapper.Map<StationDto>(_repostory.Stations.FirstOrDefault(s => s.Id == stationId));
        }
    }
}