using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bicimad.Enums;
using Bicimad.Services.Command.Commands;

namespace Bicimad.Services.Command.Interface
{
    public interface IStationCommandService
    {
        CommandResult SetDiscountType(List<string> stationIds, DiscountType discountType);
        CommandResult SetDiscounts(List<string> stationIds, DiscountType discountType, double discountConst, double discountPorc);
    }
}
