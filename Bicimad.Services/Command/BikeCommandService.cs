using System.Linq;
using Bicimad.Core;
using Bicimad.Core.DomainObjects;
using Bicimad.Helpers;
using Bicimad.Services.Command.Commands;
using Bicimad.Services.Command.Commands.Bike;
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
            
            //TODO: Aqui habrá que comprobar por el IsFinished de la tabla también
            if (Repository.UserHistories.Any(us => us.UserId == userId && !us.Finished))
            {
                commandResult.AddValidationError("No puedes coger más de una bicicleta a la vez");
                return commandResult;
            }

            //TODO: Buscar si hay mas errores de validación.

            var id = GuidHelper.GenerateId();

            //Mark selected bike as active.
            var bike = Repository.Bikes.First(b => b.Id == bikeId);
            bike.IsActive = true;

            var station = Repository.Stations.First(s => s.Id == stationId);
            var freeB = int.Parse(station.FreeBikes) - 1;
            
            station.FreeBikes = freeB.ToString();

            //Add the action to the database.
            Repository.UserHistories.Add(new UserHistory
            {
                Id = id,
                CreatedDate = DateTimeHelper.SpanishNow,
                BikeId = bikeId,
                UserId = userId,
                DepartureStationId = stationId,
                Finished = false
            });

            //Commit the changes to the database.
            Repository.Commit();

            commandResult.ItemId = id;
            return commandResult;
        }

        public CommandResult ReturnBike(UserHistory transaction, string arrivalStationId)
        {
            var commandResult = new CommandResult();

            var bike = transaction.Bike;
            var station = Repository.Stations.First(s => s.Id == arrivalStationId);

            if (transaction.Finished)
            {
                commandResult.AddValidationError("No Puedes entrgar la bicicleta dos veces");
            }

            var id = GuidHelper.GenerateId();

            //Mark selected bike as not active.
            //TODO: Comprobar si es true antes?
            bike.IsActive = false;

            //Add the bike to the station
            var freeB = int.Parse(station.FreeBikes) + 1;
            station.FreeBikes = freeB.ToString();

            //Add the action to the database.
            transaction.ArrivalStationId = arrivalStationId;

            //Commit the changes to the database.
            Repository.Commit();

            commandResult.ItemId = id;
            return commandResult;
        }
    }
}