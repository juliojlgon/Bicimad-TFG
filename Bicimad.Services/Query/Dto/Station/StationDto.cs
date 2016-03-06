using System;

namespace Bicimad.Services.Query.Dto.Station
{
     public class StationDto
    {
        public string Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public string BikeNum { get; set; }

        public string FreeBikes { get; set; }

        public string StationName { get; set; }

        public string FriendlyUrlStationName { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string Metro { get; set; }

        public string Bus { get; set; }

        public string ReservedSlots { get; set; }

    }
}