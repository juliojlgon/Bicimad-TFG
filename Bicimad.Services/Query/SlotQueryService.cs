using System.Linq;
using AutoMapper;
using Bicimad.Core;
using Bicimad.Core.DomainObjects;
using Bicimad.Services.Query.Dto.Slot;
using Bicimad.Services.Query.Interfaces;

namespace Bicimad.Services.Query
{
    public class SlotQueryService: ISlotQueryService
    {

         private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public SlotQueryService(IMapper mapper, IRepository repostory)
        {
            _mapper = mapper;
            _repository = repostory;
        }

        public SlotDto GetFreeBike(string stationId)
        {
            return _mapper.Map<Slot, SlotDto>(_repository.Slots.FirstOrDefault(b => b.StationId == stationId && !b.InUse && !b.IsBooked && b.IsWorking));
        }
    }
}
