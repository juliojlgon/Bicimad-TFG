using Bicimad.Models.Station;
using Bicimad.Services.Query.Dto.Station;

namespace Bicimad.Mappers
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
                BikeNum = dto.BikeNum,
                Bus = dto.Bus,
                FriendlyUrlStationName = dto.FriendlyUrlStationName,
                IsBikeBooked = isBikeBook,
                IsSlotBooked = isSlotBook,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                Metro = dto.Metro,
                ReservedSlots = dto.ReservedSlots,
                StationName = dto.StationName,
                AvailSlots = dto.BikeNum - (dto.FreeBikes + dto.ReservedSlots),
                DiscConst = dto.DiscConst,
                DiscPorc = dto.DiscPorc,
                DiscType = dto.DiscType
            };
            return model;
        }
    }
}