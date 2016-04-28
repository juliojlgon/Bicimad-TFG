using System.Linq;
using Bicimad.Core;
using Bicimad.Core.DomainObjects;
using Bicimad.Services.Query.Dto.Slot;
using Bicimad.Services.Query.Dto.Station;
using Bicimad.Services.Query.Interfaces;

namespace Bicimad.Services.Query
{
    public class SlotQueryService: ISlotQueryService
    {

         private readonly IRepository _repository;

        public SlotQueryService(IRepository repostory)
        {
            _repository = repostory;
        }

        public SlotDto GetFreeSlot(string stationId)
        {
            return toDto(_repository.Slots.FirstOrDefault(b => b.StationId == stationId && !b.InUse && !b.IsBooked && b.IsWorking));
        }

        private static SlotDto toDto(Slot slot)
        {
            if (slot == null) return null;

            var dto = new SlotDto
            {
                Id = slot.Id,
                IsWorking = slot.IsWorking,
                CreatedDate = slot.CreatedDate,
                IsBooked = slot.IsBooked,
                InUse = slot.InUse,
                StationId = slot.StationId
            };

            return dto;
        }
    }
}
