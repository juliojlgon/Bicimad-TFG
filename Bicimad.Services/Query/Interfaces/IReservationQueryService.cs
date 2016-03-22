using System.Collections.Generic;
using Bicimad.Services.Query.Dto.Reservation;

namespace Bicimad.Services.Query.Interfaces
{
    public interface IReservationQueryService
    {
        List<ReservationDto> GetReservations(string userId);
    }
}