using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bicimad.Enums;
using Bicimad.Helpers;

namespace Bicimad.Web.Areas.Admin.Models
{
    public class PriceModel
    {
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
        public double DiscConst { get; set; }
        [Display(ResourceType = typeof (Resources.Resources), Name = "PriceModel_DiscConst_Porcentual")]
        public double DiscPorc { get; set; }


    }
}