using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bicimad.Core;
using Bicimad.Core.DomainObjects;
using Bicimad.Helpers;
using Bicimad.Services.Command.Commands;
using Bicimad.Services.Command.Interface;

namespace Bicimad.Services.Command
{
    public class StationCommandService: BaseCommandService, IStationCommandService
    {
        public StationCommandService(IRepository repository) : base(repository)
        {
        }

        public CommandResult BookItem(string userId, string stationId, string itemId, bool type)
        {

            var commandResult = new CommandResult();

            
            if (Repository.UserHistories.Any(us => us.UserId == userId && !us.Finished))
            {
                commandResult.AddValidationError("No puedes coger más de una bicicleta a la vez");
                return commandResult;
            }

            //TODO: Buscar si hay mas errores de validación.

            var id = GuidHelper.GenerateId();

            //Mark selected item as booked & add or delete from the station variable.
            if (type)
            {
                var bike = Repository.Bikes.First(b => b.Id == itemId);
                bike.IsBooked = true;
                var station = Repository.Stations.First(s => s.Id == stationId);
                var freeB = int.Parse(station.FreeBikes) - 1;
                station.FreeBikes = freeB.ToString();
            }
            else
            {
                var slot = Repository.Slots.First(s => s.Id == itemId);
                slot.IsBooked = true;
                var station = Repository.Stations.First(s => s.Id == stationId);
                var resSlots = int.Parse(station.ReservedSlots) + 1;
                station.ReservedSlots = resSlots.ToString();
            }
            

            //Add the action to the database.
            Repository.Reservations.Add(new Reservation
            {
                Id = id,
                Isbike = type,
                CreatedDate = DateTimeHelper.SpanishNow,
                ItemId = itemId,
                UserId = userId,
                StationId = stationId
            });

            //Commit the changes to the database.
            Repository.Commit();

            commandResult.ItemId = id;
            return commandResult;
        }
    }
}
