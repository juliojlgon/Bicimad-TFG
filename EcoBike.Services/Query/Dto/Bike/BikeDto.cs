using System;

namespace Bicimad.Services.Query.Dto.Bike
{
    public class BikeDto
    {
        public string Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsWorking { get; set; }

        public bool IsActive { get; set; }

        public bool IsBooked { get; set; }

        public string StationId { get; set; }
    }
}