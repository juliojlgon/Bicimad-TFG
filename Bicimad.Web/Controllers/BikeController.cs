using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bicimad.Services.Command.Interface;
using Bicimad.Services.Query.Interfaces;
using Bicimad.Web.Models.Home;

namespace Bicimad.Web.Controllers
{
    public partial class BikeController : BaseController
    {

        private readonly IBikeCommandService _bikeCommandService;
        private readonly IBikeQueryService _bikeQueryService;

        public BikeController(IBikeCommandService bikeCommandService, IBikeQueryService bikeQueryService)
        {
            _bikeCommandService = bikeCommandService;
            _bikeQueryService = bikeQueryService;
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

        //public virtual ActionResult TakeBike()
        //{
            
        //}

        //public virtual ActionResult LeaveBike()
        //{
            
        //}

        //public virtual ActionResult BrokenBike()
        //{
            
        //}
    }
}