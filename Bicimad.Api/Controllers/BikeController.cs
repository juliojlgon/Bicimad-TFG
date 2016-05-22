using System.Linq;
using System.Web.Http;
using Bicimad.Api.Attributes;
using Bicimad.Models.Home;
using Bicimad.Services.Command.Interface;
using Bicimad.Services.Query.Interfaces;

namespace Bicimad.Api.Controllers
{
    [ApiAuthorize]
    public class BikeController : BaseController
    {
        private readonly IBikeCommandService _bikeCommandService;
        private readonly IBikeQueryService _bikeQueryService;
        private readonly IReservationCommandService _reservationCommandService;
        private readonly IReservationQueryService _reservationQueryService;

        public BikeController(IBikeCommandService bikeCommandService, IBikeQueryService bikeQueryService,
            IReservationCommandService reservationCommandService,
            IReservationQueryService reservationQueryService)
        {
            _bikeCommandService = bikeCommandService;
            _bikeQueryService = bikeQueryService;
            _reservationCommandService = reservationCommandService;
            _reservationQueryService = reservationQueryService;
        }

        public virtual IHttpActionResult ReturnIndex()
        {
            //TODO: Añadir un filtro a las estaciones, para pasarles todo desde ahí y hacer un solo metodo.
            var bikes = _bikeQueryService.GetBikes();

            var model = new BikeStatsModel
            {
                BrokenBikes = bikes.Count(b => !b.IsWorking),
                FreeBikes = bikes.Count(b => !b.IsActive && !b.IsBooked),
                ActiveBikes = bikes.Count(b => b.IsActive || b.IsBooked)
            };

            return Json(model);
        }

        [HttpPost]
        public virtual IHttpActionResult TakeBike(string userId, string stationId)
        {
            if (stationId == null)
            {
                return Json(new {Success = false, BikeId = "", Error = "Estación no valida"});
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
                    return Json(new {Success = false, BikeId = "", Error = "No hay bicicletas disponibles"});
                }
                bikeId = bike.Id;
            }

            var action = _bikeCommandService.TakeBike(userId, stationId, bikeId);

            if (action.ItemId != null)
            {
                return Json(new {Success = true, BikeId = bikeId, Error = ""});
            }

            return Json(new {Success = false, BikeId = "", Error = action.ValidationErrors.First().ErrorMessage});
        }

        [HttpPost]
        public virtual IHttpActionResult ReturnBike(string userId, string stationId)
        {
            if (stationId == null)
            {
                return Json(new {Success = false, Error = "Estación no valida"});
            }

            var reservation = _reservationQueryService.GetReservation(userId, stationId, false);
            if (reservation != null)
            {
                _reservationCommandService.RemoveReservation(userId, stationId);
            }

            var action = _bikeCommandService.ReturnBike(userId, stationId);

            if (action.ItemId != null)
            {
                return Json(new {Success = true, Error = ""});
            }

            return Json(new {Success = false, BikeId = "", Error = action.ValidationErrors.First().ErrorMessage});
        }

        [HttpPost]
        public virtual IHttpActionResult BookBike(string userId, string stationId)
        {
            if (stationId == "")
            {
                return Json(new {Success = false, BikeId = "", Error = "Estación no valida"});
            }

            var bike = _bikeQueryService.GetFreeBike(stationId);

            if (bike == null)
            {
                return Json(new {Success = false, BikeId = "", Error = "No hay bicicletas disponibles"});
            }

            var action = _reservationCommandService.BookItem(userId, stationId, bike.Id, true);

            if (action.ItemId != null)
            {
                return Json(new {Success = true, BikeId = bike.Id, Error = ""});
            }

            return Json(new {Success = false, BikeId = "", Error = action.ValidationErrors.First().ErrorMessage});
        }

        public virtual IHttpActionResult RemoveBikeReservation(string userId, string stationId)
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

        public virtual IHttpActionResult InformBrokenBike(string bikeId)
        {
            if (bikeId == null)
            {
                return Json(new {Success = false, Error = "Bicicleta no válida"});
            }
            var action = _bikeCommandService.InformBrokenBike(bikeId);

            if (action == null)
            {
                return Json(new {Success = false, Error = "No se pudo enviar el aviso"});
            }

            return Json(new {Success = true, Error = ""});
        }
    }
}