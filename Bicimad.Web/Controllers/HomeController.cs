using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bicimad.Services.Query.Interfaces;

namespace Bicimad.Web.Controllers
{
    public partial class HomeController : BaseController
    {
        private readonly IStationQueryService _stationQueryService;
 
        public HomeController(IStationQueryService stationQueryService)
        {
            _stationQueryService = stationQueryService;
        }

        public virtual ActionResult Index()
        {
            var stations = _stationQueryService.GetStations();
            return View();
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

        public virtual ActionResult FillMap()
        {
            var stations =_stationQueryService.GetStations();
            var jsonStation = Json(stations);
            return jsonStation;
        }
    }
}