using System.Linq;
using System.Web.Mvc;
using Bicimad.Services.Command.Interface;
using Bicimad.Services.Query.Interfaces;

namespace Bicimad.Web.Areas.Admin.Controllers
{
    public partial class StationsController : AdminBaseController
    {

        private readonly IStationCommandService _stationCommandService;
        private readonly IStationQueryService _stationQueryService;

        public StationsController(IStationQueryService stationQueryService, IStationCommandService stationCommandService)
        {
            _stationQueryService = stationQueryService;
            _stationCommandService = stationCommandService;
        }

        // GET: Admin/Station
        public virtual ActionResult Index()
        {

            var stations = _stationQueryService.GetStations().ToList();
            return View(MVC.Admin.Stations.Views.Index,stations);
        }
    }
}