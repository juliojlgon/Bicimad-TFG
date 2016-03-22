using System;

namespace Bicimad.Services.Query.Dto.Reservation
{
    public class ReservationDto
    {
        
        public bool Isbike { get; set; }

        public string UserId { get; set; }

        public string ItemId { get; set; }

        public string StationId { get; set; }

        public string Id { get; set; }

        public DateTime CreatedDate { get; set; }       
    }
}
