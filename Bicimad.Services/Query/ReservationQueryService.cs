using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bicimad.Core;
using Bicimad.Core.DomainObjects;
using Bicimad.Services.Command.Interface;
using Bicimad.Services.Query.Dto.Reservation;
using Bicimad.Services.Query.Interfaces;

namespace Bicimad.Services.Query
{
    public class ReservationQueryService : IReservationQueryService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public ReservationQueryService(IMapper mapper, IRepository repostory)
        {
            _mapper = mapper;
            _repository = repostory;
        }

        public List<ReservationDto> GetReservations(string userId)
        {
            return _repository.Reservations.Where(r => r.UserId == userId).Select(r => _mapper.Map<Reservation, ReservationDto>(r)).ToList();
        }

        public ReservationDto GetReservation(string userId, string stationId, bool isBike)
        {
            return _mapper.Map<Reservation, ReservationDto>(_repository.Reservations.FirstOrDefault(r => r.UserId == userId && r.StationId == stationId && r.Isbike == isBike));
        }
    }
}
