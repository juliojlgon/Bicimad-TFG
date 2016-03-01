using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bicimad.Services.Query.Dto.Bike;
using Bicimad.Services.Query.Interfaces;
using Bicimad.Web.Models.Home;

namespace Bicimad.Web.Controllers
{
    public partial class HomeController : BaseController
    {
        private readonly IStationQueryService _stationQueryService;
        private readonly IBikeQueryService _bikeQueryService;
 
        public HomeController(IStationQueryService stationQueryService, IBikeQueryService bikeQueryService)
        {
            _stationQueryService = stationQueryService;
            _bikeQueryService = bikeQueryService;
        }

        public virtual ActionResult Index()
        {
            //TODO: Añadir un filtro a las estaciones, para pasarles todo desde ahí y hacer un solo metodo.
            var bikes = _bikeQueryService.GetBikes();
            //TODO: Arreglar esto.
            var model = new BikeStatsModel
            {
                BrokenBikes = bikes.Count(b => !b.IsWorking),
                FreeBikes = bikes.Count(b => !b.IsActive && !b.IsBooked),
                TotalBikes = bikes.Count
            };

            //TODO: MIRAR QUE DATOS DEVOLVER. Poder acceder tanto a la reserva como a los datos de estaciones y bicis en uso.
            return View(MVC.Home.Views.Index,model);
        }

        public virtual ActionResult About()
        {

            _stationQueryService.GetStations();
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public virtual ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
        [HttpPost]
        public virtual ActionResult FillMap()
        {
            var stations =_stationQueryService.GetStations();
            var jsonStation = Json(stations);
            return jsonStation;
        }

    }
}