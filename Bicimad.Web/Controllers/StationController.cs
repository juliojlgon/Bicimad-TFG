using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bicimad.Services.Query;

namespace Bicimad.Web.Controllers
{
    public partial class StationController : Controller
    {
        private readonly StationQueryService _stationQueryService;

        public StationController(StationQueryService stationQueryService)
        {
            _stationQueryService = stationQueryService;
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
    }
}