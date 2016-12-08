using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bicimad.Mappers;
using Bicimad.Models.Home;
using Bicimad.Models.Station;
using Bicimad.Services.Query.Interfaces;
using Bicimad.Services.Query.Queries;

namespace Bicimad.Web.Controllers
{
    public partial class HomeController : BaseController
    {
        private readonly IBikeQueryService _bikeQueryService;
        private readonly IStationQueryService _stationQueryService;
        private readonly IReservationQueryService _reservationQueryService;

        public HomeController(IStationQueryService stationQueryService, IBikeQueryService bikeQueryService, IReservationQueryService reservationQueryService)
        {
            _stationQueryService = stationQueryService;
            _bikeQueryService = bikeQueryService;
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

            //TODO: MIRAR QUE DATOS DEVOLVER. Poder acceder tanto a la reserva como a los datos de estaciones y bicis en uso.
            return View(MVC.Home.Views.Index, model);
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
            var stations = _stationQueryService.GetStations();
            var mapModel = new List<MapReservStationModel>();
            if (User.Identity.IsAuthenticated)
            {
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

            }
            else
            {
                mapModel = stations.Select(u => u.ToModel(false, false)).ToList();
            }
            
            var jsonStationModel = Json(mapModel);
            return jsonStationModel;
        }

        
    }
}