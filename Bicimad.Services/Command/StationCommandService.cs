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

        public CommandResult SetDiscounts(List<string> stationIds, DiscountType discountType, double discountConst, double discountPorc)
        {
            var commandResult = new CommandResult();

            if (stationIds == null || stationIds.Count == 0)
            {
                commandResult.AddValidationError("You should choose at least one station");
                return commandResult;
            }

            if (discountConst < 0)
            {
                commandResult.AddValidationError("Only use positive numbers");
                return commandResult;
            }
            if (discountPorc < 0 || discountPorc > 100)
            {
                commandResult.AddValidationError("It has to be a number between 0 and 100.");
                return commandResult;
            }

            var stations = Repository.Stations.Where(s => stationIds.Contains(s.Id)).ToList();

            foreach (var station in stations)
            {
                station.DiscType = discountType;
                switch (station.DiscType)
                {
                    case DiscountType.Constant:
                    {
                        station.DiscConst = discountConst;
                        break;
                    }
                    case DiscountType.Porcentual:
                    {
                        station.DiscPorc = discountPorc;
                        break;
                    }
                }
            }
            Repository.Commit();

            return commandResult;
        }
    }
}