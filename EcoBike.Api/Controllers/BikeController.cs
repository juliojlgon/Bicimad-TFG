using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using Bicimad.Api.Attributes;
using Bicimad.Models.Home;
using Bicimad.Services.Command.Interface;
using Bicimad.Services.Query.Interfaces;

namespace Bicimad.Api.Controllers
{
    /// <summary>
    /// Requires an AuthToken.
    /// It has all the methods required for managing bikes.
    /// </summary>
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

        /// <summary>
        /// Requires an AuthToken.
        /// It will return information usefull to show while returning objects. 
        /// </summary>
        [HttpGet, ResponseType(typeof(BikeStatsModel))]
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

        /// <summary>
        /// Requires an AuthToken.
        /// Method for taking a Bike.
        /// </summary>
        /// <param name="userId">The actual userId</param>
        /// <param name="stationId">The stationId of the station where the user is taking the bike.</param>
        /// <response code="200"> {Success = true; BikeId=xxxxxxxxxxxxx; error = ""} </response>
        /// <response code="400"> {Success = false; BikeId=""; error = error message} </response>
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

        /// <summary>
        /// Requires an AuthToken.
        /// Method for return a Bike.
        /// </summary>
        /// <param name="userId">The actual userId</param>
        /// <param name="stationId">The stationId of the station where the user is taking the bike.</param>
        /// <response code="200"> {Success = true; error = ""} </response>
        /// <response code="400"> {Success = false; BikeId=""; error = error message} </response>
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

        /// <summary>
        /// Requires an AuthToken.
        /// Method for book a Bike.
        /// </summary>
        /// <param name="userId">The actual userId</param>
        /// <param name="stationId">The stationId of the station where the user is taking the bike.</param>
        /// <response code="200"> {Success = true; BikeId=xxxxxxxxxxxxx; error = ""} </response>
        /// <response code="400"> {Success = false; BikeId=""; error = error message} </response>
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

        /// <summary>
        /// Requires an AuthToken.
        /// Method for remove a Bike reservation.
        /// </summary>
        /// <param name="userId">The actual userId</param>
        /// <param name="stationId">The stationId of the station where the user is taking the bike.</param>
        /// <returns>A Json object with a bool value</returns>
        /// <response code="200"> {Success = true; error = ""} </response>
        /// <response code="400"> {Success = false;  error = error message} </response>
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

        /// <summary>
        /// Requires an AuthToken.
        /// Method for set a bike as broken.
        /// </summary>
        /// <param name="bikeId">The bikeId you wanna set as broken.</param>
        /// <returns>Json with a bool.</returns>
        /// <response code="200"> {Success = true; error = ""} </response>
        /// <response code="400"> {Success = false;  error = error message} </response>
        public virtual IHttpActionResult InformBrokenBike(string bikeId)
        {
            if (bikeId == null)
            {
                return Json(new {Success = false, Error = "Bike not valid"});
            }
            var action = _bikeCommandService.InformBrokenBike(bikeId);

            if (action == null)
            {
                return Json(new {Success = false, Error = "There was a problem updating bike status"});
            }

            return Json(new {Success = true, Error = ""});
        }
    }
}