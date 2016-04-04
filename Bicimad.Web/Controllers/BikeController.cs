using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bicimad.Services.Command.Commands.Bike;
using Bicimad.Services.Command.Interface;
using Bicimad.Services.Query.Dto.Bike;
using Bicimad.Services.Query.Interfaces;
using Bicimad.Web.Models.Home;

namespace Bicimad.Web.Controllers
{
    [Authorize]
    public partial class BikeController : BaseController
    {

        private readonly IBikeCommandService _bikeCommandService;
        private readonly IBikeQueryService _bikeQueryService;
        private readonly IReservationCommandService _reservationCommandService;
        private readonly IReservationQueryService _reservationQueryService;
        private readonly IUserHistoryQueryService _userHistoryQueryService;

        public BikeController(IBikeCommandService bikeCommandService, IBikeQueryService bikeQueryService, IReservationCommandService reservationCommandService, IUserHistoryQueryService userHistoryQueryService, IReservationQueryService reservationQueryService)
        {
            _bikeCommandService = bikeCommandService;
            _bikeQueryService = bikeQueryService;
            _reservationCommandService = reservationCommandService;
            _userHistoryQueryService = userHistoryQueryService;
            _reservationQueryService = reservationQueryService;
        }

        public virtual ActionResult Index()
        {
            //TODO: Añadir un filtro a las estaciones, para pasarles todo desde ahí y hacer un solo metodo.
            var bikes = _bikeQueryService.GetBikes();

            var model = new BikeStatsModel
            {
                BrokenBikes = bikes.Count(b => !b.IsWorking),
                FreeBikes = bikes.Count(b => !b.IsActive && !b.IsBooked),
                ActiveBikes = bikes.Count(b => b.IsActive || b.IsBooked)
            };

            return View(MVC.Manage.Views.Bike.Index, model);
        }

        public virtual ActionResult ReturnIndex()
        {
            //TODO: Añadir un filtro a las estaciones, para pasarles todo desde ahí y hacer un solo metodo.
            var bikes = _bikeQueryService.GetBikes();

            var model = new BikeStatsModel
            {
                BrokenBikes = bikes.Count(b => !b.IsWorking),
                FreeBikes = bikes.Count(b => !b.IsActive && !b.IsBooked),
                ActiveBikes = bikes.Count(b => b.IsActive || b.IsBooked)
            };

            return View(MVC.Manage.Views.Bike.ReturnBike, model);
        }

        [HttpPost]
        public virtual ActionResult TakeBike(string userId, string stationId)
        {
            if (stationId == null)
            {
                return new JsonResult
                {
                    Data = new { Success = false, BikeId = "", Error = "Estación no valida" }
                };
            }

            string bikeId;

            var reservation = _reservationQueryService.GetReservation(userId, stationId, true);
            if (reservation != null)
            {
                bikeId = reservation.ItemId;
                _reservationCommandService.RemoveReservation(userId, stationId);
            }
            else
            {

                var bike = _bikeQueryService.GetFreeBike(stationId);
                if (bike == null)
                {
                    return new JsonResult
                    {
                        Data = new { Success = false, BikeId = "", Error = "No hay bicicletas disponibles" }
                    };
                }
                bikeId = bike.Id;
            }

            var action = _bikeCommandService.TakeBike(userId, stationId, bikeId);

            if (action.ItemId != null)
            {
                return new JsonResult
                {
                    Data = new { Success = true, BikeId = bikeId, Error = "" }
                };
            }

            return new JsonResult
            {
                Data = new { Success = false, BikeId = "", Error = action.ValidationErrors.First().ErrorMessage }
            };
        }

        [HttpPost]
        public virtual ActionResult ReturnBike(string userId, string stationId)
        {
            if (stationId == null)
            {
                return new JsonResult
                {
                    Data = new { Success = false, Error = "Estación no valida" }
                };
            }

            var reservation = _reservationQueryService.GetReservation(userId, stationId, false);
            if (reservation != null)
            {
                 _reservationCommandService.RemoveReservation(userId, stationId);
            }

            var action = _bikeCommandService.ReturnBike(userId, stationId);

            if (action.ItemId != null)
            {
                return new JsonResult
                {
                    Data = new { Success = true, Error = "" }
                };
            }

            return new JsonResult
            {
                Data = new { Success = false, BikeId = "", Error = action.ValidationErrors.First().ErrorMessage }
            };
        }

        [HttpPost]
        public virtual ActionResult BookBike(string userId, string stationId)
        {
            if (stationId == "")
            {
                return new JsonResult
                {
                    Data = new { Success = false, BikeId = "", Error = "Estación no valida" }
                };
            }

            var bike = _bikeQueryService.GetFreeBike(stationId);

            if (bike == null)
            {
                return new JsonResult
                {
                    Data = new { Success = false, BikeId = "", Error = "No hay bicicletas disponibles" }
                };
            }

            var action = _reservationCommandService.BookItem(userId, stationId, bike.Id, true);

            if (action.ItemId != null)
            {
                return new JsonResult
                {
                    Data = new { Success = true, BikeId = bike.Id, Error = "" }
                };
            }

            return new JsonResult
            {
                Data = new { Success = false, BikeId = "", Error = action.ValidationErrors.First().ErrorMessage }
            };
        }

        [HttpPost]
        public virtual ActionResult RemoveBikeReservation(string userId, string stationId)
        {
            if (stationId == null)
            {
                return new JsonResult
                {
                    Data = new { Success = false, Error = "Estación no valida" }
                };
            }


            var action = _reservationCommandService.RemoveReservation(userId, stationId);

            if (action.ItemId != null)
            {
                return new JsonResult
                {
                    Data = new { Success = true, Error = "" }
                };
            }

            return new JsonResult
            {
                Data = new { Success = false, Error = action.ValidationErrors.First().ErrorMessage }
            };
        }

        public virtual ActionResult InformBrokenBike(string bikeId)
        {
            if (bikeId == null)
            {
                return new JsonResult
                {
                    Data = new { Success = false, Error = "Bicicleta no válida" }
                };
            }
            var action = _bikeCommandService.InformBrokenBike(bikeId);

            if (action == null)
            {
                return new JsonResult
                {
                    Data = new { Success = false, Error = "No se pudo enviar el aviso" }
                };
            }

            return new JsonResult
                {
                    Data = new { Success = true, Error = "" }
                };
        }
    }




}