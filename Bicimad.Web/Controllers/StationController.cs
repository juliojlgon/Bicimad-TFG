

using System.Linq;
using System.Web.Mvc;
using Bicimad.Enums;
using Bicimad.Services.Command.Interface;
using Bicimad.Services.Query;
using Bicimad.Services.Query.Interfaces;
using Bicimad.Web.Extensions;

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
                TempData.SetMessage("Estación no valida", MessageType.Error);
                return new JsonResult
                {
                    Data = new { Success = false, BikeId = "", Error = "Estación no valida" }
                };
            }

            var slot = _slotQueryService.GetFreeSlot(stationId);

            if (slot == null)
            {
                TempData.SetMessage("No hay anclajes disponibles", MessageType.Error);
                return new JsonResult
                {
                    Data = new { Success = false, BikeId = "", Error = "No hay anclajes disponibles" }
                };
                
            }

            var action = _reservationCommandService.BookItem(userId, stationId, slot.Id, false);

            if (action.ItemId != null)
            {
                TempData.SetMessage("Has reservado el anclaje" + slot.Id,MessageType.Success);
                return new JsonResult
                {
                    Data = new { Success = true, BikeId = slot.Id, Error = "" }
                };
                
            }

            TempData.SetMessage(action.ValidationErrors.First().ErrorMessage, MessageType.Error);
            return new JsonResult
            {
                Data = new { Success = false, BikeId = "", Error = action.ValidationErrors.First().ErrorMessage }
            };
            
            
        }

        
        public virtual ActionResult RemoveSlotReservation(string userId, string stationId)
        {
            if (stationId == null)
            {
                TempData.SetMessage("Estación no valida", MessageType.Error);
                return RedirectToAction(MVC.User.Home.ActiveRerservations());
                //return new JsonResult
                //{
                //    Data = new { Success = false, Error = "Estación no valida" }
                //};
            }

            
            var action = _reservationCommandService.RemoveReservation(userId, stationId);

            if (action.Success)
            {
                TempData.SetMessage("Reserva Eliminada", MessageType.Error);
                return RedirectToAction(MVC.User.Home.ActiveRerservations());
                //return new JsonResult
                //{
                //    Data = new { Success = true, Error = "" }
                //};
            }

            TempData.SetMessage(action.ValidationErrors.First().ErrorMessage, MessageType.Error);
            return RedirectToAction(MVC.User.Home.ActiveRerservations());
            //return new JsonResult
            //{
            //    Data = new { Success = false, Error = action.ValidationErrors.First().ErrorMessage }
            //};
        }
    }
}