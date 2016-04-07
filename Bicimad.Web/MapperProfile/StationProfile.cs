using AutoMapper;
using Bicimad.Core.DomainObjects;
using Bicimad.Services.Query.Dto.Bike;
using Bicimad.Services.Query.Dto.History;
using Bicimad.Services.Query.Dto.Station;
using Bicimad.Services.Query.Dto.User;
using Bicimad.Web.Areas.User.Models;

namespace Bicimad.Web.MapperProfile
{
    public class StationProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Station, StationDto>();
            CreateMap<User, UserDto>();
            CreateMap<UserHistory, UserHistoryDto>().ForMember(u => u.ArrivalStationUserName, opt => opt.ResolveUsing<ArrivalStationResolver>())
                .ForMember(u => u.DepartureStationUserName, opt => opt.ResolveUsing<DepartureStationResolver>());
            CreateMap<UserHistoryDto, UserHistoriesModel>();
            CreateMap<Bike, BikeDto>();
        }
    }

    public class ArrivalStationResolver : ValueResolver<UserHistory, string>
    {
        protected override string ResolveCore(UserHistory userHistory)
        {
            return !string.IsNullOrEmpty(userHistory.ArrivalStationId) ? userHistory.ArrivalStation.StationName : "-";
        }
    }

    public class DepartureStationResolver : ValueResolver<UserHistory, string>
    {
        protected override string ResolveCore(UserHistory userHistory)
        {
            return userHistory.DepartureStation.StationName;
        }
    }
}