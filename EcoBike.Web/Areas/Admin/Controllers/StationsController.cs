using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Bicimad.Enums;
using Bicimad.Helpers;
using Bicimad.Services.Command.Commands.MetaConfig;
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
        private readonly IMetaConfigCommandService _metaConfigCommandService;
        private readonly IMetaConfigQueryService _metaconfigQueryService;

        public StationsController(IStationQueryService stationQueryService, IStationCommandService stationCommandService, IMetaConfigCommandService metaConfigCommandService, IMetaConfigQueryService metaconfigQueryService)
        {
            _stationQueryService = stationQueryService;
            _stationCommandService = stationCommandService;
            _metaConfigCommandService = metaConfigCommandService;
            _metaconfigQueryService = metaconfigQueryService;
        }

        // GET: Admin/Station
        public virtual ActionResult Index()
        {
            var stations = _stationQueryService.GetStations().ToList();
            var metaBasePrice = _metaconfigQueryService.Get(MetaConfigKey.BasePrice).FirstOrDefault();
            var baseprice = metaBasePrice != null ? metaBasePrice.MetaValue : "5";
            

            var model = new AdminStationModel
            {
                BasePrice = double.Parse(baseprice, NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo),
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
                basePrice = 5;

            var command = new CreateMetaConfigCommand
            {
                MetaKey = MetaConfigKey.BasePrice,
                DeleteExistentKeys = true,
                MetaValue = basePrice.Value.ToString(CultureInfo.InvariantCulture)
            };

            var result = _metaConfigCommandService.Create(command);

            if (result.Success)
            {
                TempData.SetMessage("Base price changed.", MessageType.Success);
            }
            else
            {
                TempData.SetMessage("There was a problem changing the price", MessageType.Error);
            }
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