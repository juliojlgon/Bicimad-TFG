using System;

namespace Bicimad.Services.Query.Dto.Slot
{
    public class SlotDto
    {
        public bool IsWorking { get; set; }
        public bool InUse { get; set; }
        public bool IsBooked { get; set; }
        public string StationId { get; set; }
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

