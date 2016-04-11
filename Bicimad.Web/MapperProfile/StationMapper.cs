using Bicimad.Services.Query.Dto.Station;
using Bicimad.Web.Models.Station;

namespace Bicimad.Web.MapperProfile
{
    public static class StationMapper
    {
        public static MapReservStationModel ToModel(this StationDto dto, bool isBikeBook, bool isSlotBook)
        {
            var model = new MapReservStationModel
            {
                Id = dto.Id,
                CreatedDate = dto.CreatedDate,
                FreeBikes = dto.FreeBikes,
                BikeNum = dto.FreeBikes,
                Bus = dto.Bus,
                FriendlyUrlStationName = dto.FriendlyUrlStationName,
                IsBikeBooked = isBikeBook,
                IsSlotBooked = isSlotBook,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                Metro = dto.Metro,
                ReservedSlots = dto.ReservedSlots,
                StationName = dto.StationName
            };
            return model;
        }
    }
}