using System;
using Bicimad.Enums;

namespace Bicimad.Services.Query.Dto.Station
{
     public class StationDto
    {
        public string Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public int BikeNum { get; set; }

        public int FreeBikes { get; set; }

        public string StationName { get; set; }

        public string FriendlyUrlStationName { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string Metro { get; set; }

        public string Bus { get; set; }

        public int ReservedSlots { get; set; }

        public double DiscPorc { get; set; }

        public double DiscConst { get; set; }

        public DiscountType DiscType { get; set; }

    }
}