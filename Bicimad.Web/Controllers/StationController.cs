

using System.Linq;
using System.Web.Mvc;
using Bicimad.Services.Command.Interface;
using Bicimad.Services.Query;
using Bicimad.Services.Query.Interfaces;

namespace Bicimad.Web.Controllers
{
    public partial class StationController : Controller
    {
        private readonly StationQueryService _stationQueryService;
        private readonly IReservationCommandService _reservationCommandService;
        private readonly ISlotQueryService _slotQueryService;

        public StationController(StationQueryService stationQueryService, IReservationCommandService reservationCommandService, ISlotQueryService slotQueryService)
        {
            _stationQueryService = stationQueryService;
            _reservationCommandService = reservationCommandService;
            _slotQueryService = slotQueryService;
        }

        // GET: Station
        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult GetStationByIdJson(string id)
        {
            var station = _stationQueryService.GetStation(id);
            var jsonStation = Json(station);
            return jsonStation;
        }

        [HttpPost]
        public virtual ActionResult BookSlot(string userId, string stationId)
        {
            if (stationId == null)
            {
                return new JsonResult
                {
                    Data = new { Success = false, BikeId = "", Error = "Estación no valida" }
                };
            }

            var slot = _slotQueryService.GetFreeBike(stationId);

            if (slot == null)
            {
                return new JsonResult
                {
                    Data = new { Success = false, BikeId = "", Error = "No hay bicicletas disponibles" }
                };
            }

            var action = _reservationCommandService.BookItem(userId, stationId, slot.Id,true);

            if (action.ItemId != null)
            {
                return new JsonResult
                {
                    Data = new { Success = true, BikeId = slot.Id, Error = "" }
                };
            }

            return new JsonResult
            {
                Data = new { Success = false, BikeId = "", Error = action.ValidationErrors.First().ErrorMessage }
            };
        }

        [HttpPost]
        public virtual ActionResult RemoveSlotReservation(string userId, string stationId)
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
    }
}