using System.Globalization;
using System.Linq;
using Bicimad.Core;
using Bicimad.Core.DomainObjects;
using Bicimad.Enums;
using Bicimad.Helpers;
using Bicimad.Services.Command.Commands;
using Bicimad.Services.Command.Interface;

namespace Bicimad.Services.Command
{
    public class BikeCommandService : BaseCommandService, IBikeCommandService
    {
        public BikeCommandService(IRepository repository) : base(repository)
        {
        }

        public CommandResult TakeBike(string userId, string stationId, string bikeId)
        {
            var commandResult = new CommandResult();
            if (Repository.UserHistories.Any(us => (us.UserId == userId) && !us.Finished))
            {
                commandResult.AddValidationError("You can only take a bike at a time");
                return commandResult;
            }


            var reservation = Repository.Reservations.FirstOrDefault(r => (r.Id == userId) && (r.StationId == stationId));
            if (reservation != null)
                bikeId = reservation.ItemId;

            //TODO: Buscar si hay mas errores de validación.

            var id = GuidHelper.GenerateId();

            //Mark selected bike as active.
            var bike = Repository.Bikes.First(b => b.Id == bikeId);
            bike.IsActive = true;

            var station = Repository.Stations.First(s => s.Id == stationId);
            var freeB = station.FreeBikes - 1;

            station.FreeBikes = freeB;

            //Add the action to the database.
            Repository.UserHistories.Add(new UserHistory
            {
                Id = id,
                CreatedDate = DateTimeHelper.SpanishNow,
                BikeId = bikeId,
                UserId = userId,
                DepartureStationId = stationId,
                TotalDiscount = station.DiscType == DiscountType.Constant ? string.Format("{0}€", station.DiscConst) : string.Format("{0}%", station.DiscPorc),
                Finished = false
            });

            //Commit the changes to the database.
            Repository.Commit();

            commandResult.ItemId = id;
            return commandResult;
        }

        public CommandResult ReturnBike(string userId, string arrivalStationId)
        {
            var commandResult = new CommandResult();

            var transaction = Repository.UserHistories.FirstOrDefault(u => (u.UserId == userId) && !u.Finished);

            if (transaction == null)
            {
                commandResult.AddValidationError("You have to take a bike first.");
                return commandResult;
            }

            var bike = transaction.Bike;
            var station = Repository.Stations.First(s => s.Id == arrivalStationId);

            if (transaction.Finished)
            {
                commandResult.AddValidationError("You can't return the bike two times.");
                return commandResult;
            }

            var availSlots = station.BikeNum - (station.FreeBikes + station.ReservedSlots);
            if (availSlots == 0)
            {
                commandResult.AddValidationError("No slot availiable");
                return commandResult;
            }

            //Si tiene un anclaje reservado?

            //TODO: Tener en cuenta la reserva de slot

            var id = GuidHelper.GenerateId();

            //Mark selected bike as not active.
            //TODO: Comprobar si es true antes?
            bike.IsActive = false;

            //Add the bike to the station
            var freeB = station.FreeBikes + 1;
            station.FreeBikes = freeB;

            //Update the price and discount
            var metaBasePrice = Repository.MetaConfigs.Where(c => c.MetaKey == MetaConfigKey.BasePrice).Select(c => c.MetaValue).First();
            var basePrice = double.Parse(metaBasePrice, NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo);
            var span = DateTimeHelper.SpanishNow - transaction.CreatedDate;
            var hours = span.TotalHours;
            var totalprice = basePrice*hours;

            //if same type, apply the highest one.
            if (transaction.DepartureStation.DiscType == station.DiscType)
            {
                if (transaction.DepartureStation.DiscType == DiscountType.Constant)
                {
                    var totalDiscount = station.DiscConst > transaction.DepartureStation.DiscConst
                        ? station.DiscConst
                        : transaction.DepartureStation.DiscConst;
                    transaction.FinalPrice = totalprice - totalDiscount;
                }
                else
                {
                    var totalDiscount = station.DiscPorc > transaction.DepartureStation.DiscPorc
                        ? station.DiscPorc
                        : transaction.DepartureStation.DiscPorc;
                    transaction.FinalPrice = totalprice*((100 - totalDiscount)/100);
                }
            }
            else
            {
                if (transaction.DepartureStation.DiscType == DiscountType.Constant)
                    transaction.FinalPrice = (totalprice - transaction.DepartureStation.DiscConst)*
                                             ((100 - station.DiscPorc)/100);
                else
                    transaction.FinalPrice = (totalprice - station.DiscConst)*
                                             ((100 - transaction.DepartureStation.DiscPorc)/100);
            }


            transaction.TotalDiscount = string.Format("{0:0.##}", totalprice - transaction.FinalPrice);


            //Add the action to the database.
            transaction.ArrivalStationId = arrivalStationId;
            //Close the action
            transaction.Finished = true;

            //Commit the changes to the database.
            Repository.Commit();

            commandResult.ItemId = id;
            return commandResult;
        }

        public CommandResult RemoveBikesFromStation(int numBikes, string friendlyUrlStationName)
        {
            var commandResult = new CommandResult();

            if (numBikes <= 0)
            {
                commandResult.AddValidationError("The number cant be 0 or less than 0");
                return commandResult;
            }

            if (string.IsNullOrEmpty(friendlyUrlStationName))
            {
                commandResult.AddValidationError("The Station title can't be empty");
                return commandResult;
            }

            var station = Repository.Stations.FirstOrDefault(x => x.FriendlyUrlStationName == friendlyUrlStationName);

            if (station == null)
            {
                commandResult.AddValidationError("The station title is not valid");
                return commandResult;
            }

            var bikes = Repository.Bikes.Where(x => (x.StationId == station.Id) && !x.IsBooked && !x.IsActive).ToList();
            var slots = Repository.Slots.Where(x => (x.StationId == station.Id) && !x.IsBooked).ToList();

            var bikesToRemove = bikes.Take(numBikes).ToList();
            var slotsToChange = slots.Take(numBikes).ToList();

            foreach (var slot in slotsToChange)
                slot.InUse = false;

            Repository.Bikes.RemoveRange(bikesToRemove);

            station.FreeBikes -= numBikes;

            Repository.Commit();

            return commandResult;
        }

        public CommandResult InformBrokenBike(string bikeId)
        {
            var commandResult = new CommandResult();

            var bike = Repository.Bikes.First(b => b.Id == bikeId);

            //TODO: mirar que acciones se harían al poner que ser averiara la bici.

            //Change status to broken
            bike.IsWorking = false;

            //Commit change to database
            Repository.Commit();

            return commandResult;
        }
    }
}