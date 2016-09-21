using System.Collections.Generic;
using System.Linq;
using Bicimad.Core;
using Bicimad.Enums;
using Bicimad.Services.Command.Commands;
using Bicimad.Services.Command.Interface;

namespace Bicimad.Services.Command
{
    public class StationCommandService : BaseCommandService, IStationCommandService
    {
        public StationCommandService(IRepository repository) : base(repository)
        {
        }

        public CommandResult SetDiscountType(List<string> stationIds, DiscountType discountType)
        {
            var commandResult = new CommandResult();

            if (stationIds == null)
            {
                commandResult.AddValidationError("You should choose at least one station");
                return commandResult;
            }
            var stations = Repository.Stations.Where(s => stationIds.Contains(s.Id)).ToList();
            foreach (var station in stations)
            {
                station.DiscType = discountType;
            }
            Repository.Commit();

            return commandResult;
        }

        public CommandResult SetDiscount(List<string> stationIds, double discount)
        {
            var commandResult = new CommandResult();

            if (stationIds == null || stationIds.Count == 0)
            {
                commandResult.AddValidationError("You should choose at least one station");
                return commandResult;
            }

            if (discount < 0)
            {
                commandResult.AddValidationError("Only use positive numbers");
                return commandResult;
            }

            var stations = Repository.Stations.Where(s => stationIds.Contains(s.Id)).ToList();

            foreach (var station in stations)
            {
                switch (station.DiscType)
                {
                    case DiscountType.Constant:
                    {
                        station.DiscConst = discount;
                        break;
                    }
                    case DiscountType.Porcentual:
                    {
                        station.DiscPorc = discount;
                        break;
                    }
                }
            }
            Repository.Commit();

            return commandResult;
        }
    }
}