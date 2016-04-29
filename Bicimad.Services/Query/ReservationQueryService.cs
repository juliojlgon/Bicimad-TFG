using System.Collections.Generic;
using System.Linq;
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

        public ReservationQueryService(IRepository repostory)
        {
            _repository = repostory;
        }

        public List<ReservationDto> GetReservations(ref ReservationQuery query)
        {
            var userId = query.Id;
            var reservations = _repository.Reservations.Where(r => r.UserId == userId);

            query.OutTotalCount = reservations.Count();

            reservations = reservations.OrderByDescending(uh => uh.CreatedDate);

            return query.PageSize == 0
                ? reservations.ToList().Select(ToDto).ToList()
                : reservations.ToList().Skip(query.PageIndex * query.PageSize).Take(query.PageSize).Select(ToDto).ToList();
            
        }

        public ReservationDto GetReservation(string userId, string stationId, bool isBike)
        {
            return ToDto(_repository.Reservations.FirstOrDefault(r => r.UserId == userId && r.StationId == stationId && r.IsBike == isBike));
        }

        private static ReservationDto ToDto(Reservation reservation)
        {
            if (reservation == null) return null;

            return new ReservationDto
            {
                CreatedDate = reservation.CreatedDate,
                Id = reservation.Id,
                StationId = reservation.StationId,
                StationName = reservation.Station.StationName,
                Isbike = reservation.IsBike,
                UserId = reservation.UserId,
                ItemId = reservation.ItemId
            };

        }


    }
}
