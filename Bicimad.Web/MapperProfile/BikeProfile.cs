using AutoMapper;
using Bicimad.Core.DomainObjects;
using Bicimad.Services.Query.Dto.Bike;
using Bicimad.Services.Query.Dto.Reservation;
using Bicimad.Services.Query.Dto.Slot;

namespace Bicimad.Web.MapperProfile
{
    public class BikeProfile: Profile
    {
        protected override void Configure()
        {
            CreateMap<Bike, BikeDto>();
            CreateMap<Slot, SlotDto>();
            CreateMap<Reservation, ReservationDto>().ForMember(r => r.StationName, opt => opt.ResolveUsing<StationResolver>());
        }

        public class StationResolver : ValueResolver<Reservation, string>
        {
            protected override string ResolveCore(Reservation source)
            {
                return source.Station.StationName;
            }
        }
    }

}