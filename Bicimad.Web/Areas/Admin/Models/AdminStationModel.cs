using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Bicimad.Enums;
using Bicimad.Helpers;
using Bicimad.Services.Query.Dto.Station;

namespace Bicimad.Web.Areas.Admin.Models
{
    public class AdminStationModel
    {
        public List<StationDto> Stations { get; set; }

        [Display(Name = "Base Price")]
        public double? BasePrice { get; set; }

        public int TotalCount { get; set; }

        [Display(ResourceType = typeof (Resources.Resources), Name = "Type")]
        [Required]
        public DiscountType DiscountType { get; set; }

        public List<SelectListItem> DiscountTypes
        {
            get
            {
                var list = new List<SelectListItem>();
                var values = EnumHelper.GetValues<DiscountType>();

                list.AddRange(values.Select(value => new SelectListItem
                {
                    Text = value.ToString(),
                    Value = value.ToString(),
                    Selected = DiscountType == value
                }));

                return list;
            }
        }

        [Display(ResourceType = typeof (Resources.Resources), Name = "Money")]
        [RegularExpression("(?:\\d*\\.)?\\d+", ErrorMessage = "Invalid input. only positive numbers")]
        public string DiscConst { get; set; }

        [Display(ResourceType = typeof (Resources.Resources), Name = "PriceModel_DiscConst_Porcentual")]
        [RegularExpression("(?:\\d*\\.)?\\d+", ErrorMessage = "Invalid input. only positive numbers")]
        public string DiscPorc { get; set; }
    }
}