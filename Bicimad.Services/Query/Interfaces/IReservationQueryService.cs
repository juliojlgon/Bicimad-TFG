using System.Collections.Generic;
using Bicimad.Services.Query.Dto.Reservation;
using Bicimad.Services.Query.Queries;

namespace Bicimad.Services.Query.Interfaces
{
    public interface IReservationQueryService
    {
        List<ReservationDto> GetReservations(ref ReservationQuery query);

        /// <summary>
        /// Looks into the DB for a Reservation with parameters supplied.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="stationId"></param>
        /// <param name="isBike"></param>
        /// <returns>Reservation if it exist</returns>
        ReservationDto GetReservation(string userId, string stationId, bool isBike);
    }
}