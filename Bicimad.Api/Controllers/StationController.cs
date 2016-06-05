﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.UI.WebControls;
using Bicimad.Api.Attributes;
using Bicimad.Mappers;
using Bicimad.Models.Station;
using Bicimad.Services.Command.Interface;
using Bicimad.Services.Query;
using Bicimad.Services.Query.Dto.History;
using Bicimad.Services.Query.Interfaces;
using Bicimad.Services.Query.Queries;

namespace Bicimad.Api.Controllers
{
    [ApiAuthorize]
    public class StationController : BaseController
    {
        private readonly IReservationCommandService _reservationCommandService;
        private readonly IReservationQueryService _reservationQueryService;
        private readonly ISlotQueryService _slotQueryService;
        private readonly StationQueryService _stationQueryService;
        private readonly UserHistoryQueryService _userStoryQueryService;
        

        public StationController(StationQueryService stationQueryService,
            IReservationCommandService reservationCommandService, ISlotQueryService slotQueryService,
            IReservationQueryService reservationQueryService, UserHistoryQueryService userStoryQueryService)
        {
            _stationQueryService = stationQueryService;
            _reservationCommandService = reservationCommandService;
            _slotQueryService = slotQueryService;
            _reservationQueryService = reservationQueryService;
            _userStoryQueryService = userStoryQueryService;
        }

        [HttpPost]
        public virtual IHttpActionResult GetStationByIdJson(string id)
        {
            var station = _stationQueryService.GetStation(id);
            var jsonStation = Json(station);
            return jsonStation;
        }

        [HttpPost]
        public virtual IHttpActionResult BookSlot(string userId, string stationId)
        {
            if (stationId == null)
            {
                return Json(new {Success = false, BikeId = "", Error = "Estación no valida"});
            }

            var slot = _slotQueryService.GetFreeSlot(stationId);

            if (slot == null)
            {
                return Json(new {Success = false, BikeId = "", Error = "No hay anclajes disponibles"});
            }

            var action = _reservationCommandService.BookItem(userId, stationId, slot.Id, false);

            if (action.ItemId != null)
            {
                return Json(new {Success = true, BikeId = slot.Id, Error = ""});
            }

            return Json(new {Success = false, BikeId = "", Error = action.ValidationErrors.First().ErrorMessage});
        }

        public virtual IHttpActionResult FillMap()
        {
            var stations = _stationQueryService.GetStations();
            var mapModel = new List<MapReservStationModel>();
            
                var query = new ReservationQuery {Id = CurrentUser.Id};
                var reservations = _reservationQueryService.GetReservations(ref query);
                //As we can just have two active as much at the same time.
                var bikeReservation = reservations.FirstOrDefault(r => r.Isbike);
                var slotReservation = reservations.FirstOrDefault(r => !r.Isbike);

                foreach (var station in stations)
                {
                    var bike = false;
                    var slot = false;
                    if (bikeReservation != null && station.Id == bikeReservation.StationId)
                        bike = true;

                    if (slotReservation != null && station.Id == slotReservation.StationId)
                        slot = true;
                    mapModel.Add(station.ToModel(bike, slot));
                }

            var jsonStationModel = Json(mapModel);
            return jsonStationModel;
        }

        public virtual IHttpActionResult GetHistory()
        {

            var query = new UserHistoryQuery
            {
                Id = CurrentUser.Id
            };

            var userHistorical = _userStoryQueryService.GetHistorial(ref query);

            return Json(userHistorical);
        }

        public virtual IHttpActionResult GetActiveRerservations()
        {

            
           var query = new ReservationQuery
            {
                Id = CurrentUser.Id,
            };

            var reservationHistorical = _reservationQueryService.GetReservations(ref query);

            return Json(reservationHistorical);
        }

        [HttpPost]
        public virtual IHttpActionResult RemoveSlotReservation(string userId, string stationId)
        {
            if (stationId == null)
            {
                return Json(new {Success = false, Error = "Estación no valida"});
            }


            var action = _reservationCommandService.RemoveReservation(userId, stationId);

            if (action.Success)
            {
                return Json(new {Success = true, Error = ""});
            }

            return Json(new {Success = false, Error = action.ValidationErrors.First().ErrorMessage});
        }
    }
}