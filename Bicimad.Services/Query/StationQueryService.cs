using System.Collections.Generic;
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
        private IMapper _mapper;

        public StationQueryService(IRepository repository, IMapper mapper)
        {
            _repostory = repository;
            _mapper = mapper;
        }

        public StationDto GetStation(string stationId)
        {
            
            return _mapper.Map<Station, StationDto>(_repostory.Stations.FirstOrDefault(s => s.Id == stationId));
        }

        public List<StationDto> GetStations()
        {
            var stations =  _repostory.Stations.ToList();
            return stations.Select(station => _mapper.Map<Station, StationDto>(station)).ToList();
        } 
    }
}