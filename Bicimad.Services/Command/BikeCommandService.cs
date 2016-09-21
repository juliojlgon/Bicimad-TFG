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
            if (Repository.UserHistories.Any(us => us.UserId == userId && !us.Finished))
            {
                commandResult.AddValidationError("No puedes coger más de una bicicleta a la vez");
                return commandResult;
            }

            
            var reservation = Repository.Reservations.FirstOrDefault(r => r.Id == userId && r.StationId == stationId);
            if (reservation != null)
            {
                bikeId = reservation.ItemId;
            }

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
                TotalDiscount = (station.DiscType == DiscountType.Constant)?string.Format("{0}€", station.DiscConst):string.Format("{0}%", station.DiscPorc),
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

            var transaction = Repository.UserHistories.FirstOrDefault(u => u.UserId == userId && !u.Finished);

            if (transaction == null)
            {
                commandResult.AddValidationError("Usuario no es valido");
                return commandResult;
            }

            var bike = transaction.Bike;
            var station = Repository.Stations.First(s => s.Id == arrivalStationId);

            if (transaction.Finished)
            {
                commandResult.AddValidationError("No Puedes entregar la bicicleta dos veces");
                return commandResult;
            }
            
            var availSlots = station.BikeNum - (station.FreeBikes +station.ReservedSlots);
            if (availSlots == 0)
            {
                commandResult.AddValidationError("No hay ningún anclaje disponible");
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
            var totalprice = BicimadMetadata.BasePrice*(transaction.CreatedDate.Hour - DateTimeHelper.SpanishNow.Hour);
            
            //if same type, apply the highest one.
            if (transaction.DepartureStation.DiscType == station.DiscType)
            {
                if (transaction.DepartureStation.DiscType == DiscountType.Constant)
                {
                    var totalDiscount = (station.DiscConst > transaction.DepartureStation.DiscConst)
                        ? station.DiscConst
                        : transaction.DepartureStation.DiscConst;
                    transaction.FinalPrice = totalprice - totalDiscount;
                }
                else
                {
                    var totalDiscount = (station.DiscPorc > transaction.DepartureStation.DiscPorc)
                        ? station.DiscPorc
                        : transaction.DepartureStation.DiscPorc;
                    transaction.FinalPrice = totalprice*(1 - totalDiscount);
                }

            }
            else
            {
                if (transaction.DepartureStation.DiscType == DiscountType.Constant)
                {
                    transaction.FinalPrice = (totalprice - transaction.DepartureStation.DiscConst)*
                                             (1 - station.DiscPorc);
                }
                else
                {
                    transaction.FinalPrice = (totalprice - station.DiscConst)*
                                             (1 - transaction.DepartureStation.DiscPorc);               
                }
            }


            transaction.TotalDiscount = string.Format("{0}", totalprice - transaction.FinalPrice);            
            

            //Add the action to the database.
            transaction.ArrivalStationId = arrivalStationId;
            //Close the action
            transaction.Finished = true;

            //Commit the changes to the database.
            Repository.Commit();

            commandResult.ItemId = id;
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