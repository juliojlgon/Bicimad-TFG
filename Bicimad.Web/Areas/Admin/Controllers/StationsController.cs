using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Bicimad.Enums;
using Bicimad.Helpers;
using Bicimad.Services.Command.Interface;
using Bicimad.Services.Query.Interfaces;
using Bicimad.Web.Areas.Admin.Models;
using Bicimad.Web.Extensions;

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

            var model = new AdminStationModel
            {
                BasePrice = BicimadMetadata.BasePrice,
                Stations = stations,
                TotalCount = stations.Count,
                DiscConst = "0",
                DiscPorc = "0",
                DiscountType = DiscountType.Constant
            };
            return View(MVC.Admin.Stations.Views.Index, model);
        }

        public virtual ActionResult SetBasePrice(double? basePrice)
        {
            if (!basePrice.HasValue)
                BicimadMetadata.BasePrice = 5;
            else
                BicimadMetadata.BasePrice = basePrice.Value;

            TempData.SetMessage("Base price changed.", MessageType.Success);

            return RedirectToAction(MVC.Admin.Stations.Index());
        }

        [HttpPost]
        public virtual ActionResult SetDiscounts(List<string> stationIds, DiscountType discountType, string discConst,
            string discPorc)
        {
            if (discConst == null) discConst = "0";
            if (discPorc == null) discPorc = "0";
            var c = double.Parse(discConst, NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo);
            var p = double.Parse(discPorc, NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo);

            var result = _stationCommandService.SetDiscounts(stationIds, discountType, c, p);

            if (result.Success)
            {
                TempData.SetMessage("Stations updated.", MessageType.Success);
                return RedirectToAction(MVC.Admin.Stations.Index());
            }

            TempData.SetMessage(result.FirstErrorMessage, MessageType.Error);
            return RedirectToAction(MVC.Admin.Stations.Index());
        }
    }
}