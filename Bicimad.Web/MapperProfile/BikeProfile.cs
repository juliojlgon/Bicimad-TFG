using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Bicimad.Core.DomainObjects;
using Bicimad.Services.Query.Dto.Bike;

namespace Bicimad.Web.MapperProfile
{
    public class BikeProfile: Profile
    {
        protected override void Configure()
        {
            CreateMap<Bike, BikeDto>();
        }
    }

}