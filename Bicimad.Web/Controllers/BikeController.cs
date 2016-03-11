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
        private readonly IStationQueryService _stationQueryService;

        public BikeController(IBikeCommandService bikeCommandService, IBikeQueryService bikeQueryService, IStationQueryService stationQueryService)
        {
            _bikeCommandService = bikeCommandService;
            _bikeQueryService = bikeQueryService;
            _stationQueryService = stationQueryService;
        }

        public virtual ActionResult Index()
        {
            //TODO: Añadir un filtro a las estaciones, para pasarles todo desde ahí y hacer un solo metodo.
            var bikes = _bikeQueryService.GetBikes();
            
            var model = new BikeStatsModel
            {
                BrokenBikes = bikes.Count(b => !b.IsWorking),
                FreeBikes = bikes.Count(b => !b.IsActive && !b.IsBooked),
                ActiveBikes = bikes.Count(b => b.IsActive && b.IsBooked)
            };

            return View(MVC.Manage.Views.Bike.Index, model);
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

            var bike = _bikeQueryService.GetFreeBike(stationId);

            if (bike == null)
            {
                return new JsonResult
                {
                    Data = new { Success = false, BikeId = "" , Error = "No hay bicicletas disponibles"}
                };
            }

            var action = _bikeCommandService.TakeBike(userId,stationId,bike.Id);

            if (action.ItemId != null)
            {
                return new JsonResult
                {
                    Data = new {Success = true, BikeId = bike.Id, Error=""}
                };
            }

            return new JsonResult
            {
                Data = new { Success = false, BikeId = "", Error =  action.ValidationErrors }
            };
        }

        
        

        //public virtual ActionResult LeaveBike()
        //{
            
        //}

        //public virtual ActionResult BrokenBike()
        //{
            
        //}
    }
}