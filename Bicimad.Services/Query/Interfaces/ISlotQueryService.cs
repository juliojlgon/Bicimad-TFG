using Bicimad.Services.Query.Dto.Slot;

namespace Bicimad.Services.Query.Interfaces
{
    public interface ISlotQueryService
    {
        SlotDto GetFreeBike(string stationId);
    }
}
