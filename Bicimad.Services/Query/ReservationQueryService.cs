using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Bicimad.Core;
using Bicimad.Core.DomainObjects;
using Bicimad.Services.Query.Dto.Reservation;
using Bicimad.Services.Query.Interfaces;
using Bicimad.Services.Query.Queries;

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

        public List<ReservationDto> GetReservations(ref ReservationQuery query)
        {
            var userId = query.Id;
            var reservations = _repository.Reservations.Where(r => r.UserId == userId);

            query.OutTotalCount = reservations.Count();

            reservations = reservations.OrderByDescending(uh => uh.CreatedDate);

            return query.PageSize == 0
                ? reservations.ToList().Select(_mapper.Map<Reservation, ReservationDto>).ToList()
                : reservations.ToList().Skip(query.PageIndex * query.PageSize).Take(query.PageSize).Select(_mapper.Map<Reservation, ReservationDto>).ToList();
            
        }

        public ReservationDto GetReservation(string userId, string stationId, bool isBike)
        {
            return _mapper.Map<Reservation, ReservationDto>(_repository.Reservations.FirstOrDefault(r => r.UserId == userId && r.StationId == stationId && r.IsBike == isBike));
        }


    }
}
