using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Bicimad.Core.DomainObjects;
using Bicimad.Services.Query.Dto.Station;
using Bicimad.Services.Query.Dto.User;

namespace Bicimad.Web.MapperProfile
{
    public class StationProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Station, StationDto>();
            CreateMap<User, UserDto>();
        }
    }
}