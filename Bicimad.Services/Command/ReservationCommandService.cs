

using System.Collections.Generic;
using System.Linq;
using Bicimad.Core;
using Bicimad.Core.DomainObjects;
using Bicimad.Helpers;
using Bicimad.Services.Command.Commands;
using Bicimad.Services.Command.Interface;

namespace Bicimad.Services.Command
{
    public class ReservationCommandService : BaseCommandService, IReservationCommandService
    {
        public ReservationCommandService(IRepository repository)
            : base(repository)
        {
        }

        public CommandResult BookItem(string userId, string stationId, string itemId, bool isBike)
        {

            var commandResult = new CommandResult();

            var station = Repository.Stations.First(s => s.Id == stationId);

            if (isBike && station.FreeBikes == 0)
            {
                commandResult.AddValidationError("No hay bicicletas disponibles en la estación seleccionada.");
                return commandResult;
            }
            var availSlots = station.BikeNum - (station.FreeBikes + station.ReservedSlots);
            if (!isBike && availSlots == 0)
            {
                commandResult.AddValidationError("No hay anclajes disponibles en la estación seleccionada.");
                return commandResult;
            }

            var userHistorical = Repository.UserHistories.Where(us => us.UserId == userId);

            if (userHistorical.Any(us =>!us.Finished && isBike))
            {
                commandResult.AddValidationError("No puedes coger más de una bicicleta a la vez");
                return commandResult;
            }

            if (!isBike && userHistorical.All(us => us.Finished))
            {

                commandResult.AddValidationError("No puedes reservar un anclaje sin tener una bicicleta.");
                return commandResult;
            }

            if (isBike && Repository.Reservations.Any(r => r.IsBike && r.UserId == userId))
            {
                var reserv = Repository.Reservations.First(r => isBike && r.UserId == userId);
                RemoveReservationItem(stationId, reserv);
            }

            if (!isBike && Repository.Reservations.Any(r => !r.IsBike && r.UserId == userId))
            {
                var reserv = Repository.Reservations.First(r => !isBike && r.UserId == userId);
                RemoveReservationItem(stationId, reserv);
            }


            //TODO: Buscar si hay mas errores de validación.

            var id = GuidHelper.GenerateId();

            //Mark selected item as booked & add or delete from the station variable.
            if (isBike)
            {
                var bike = Repository.Bikes.First(b => b.Id == itemId);
                bike.IsBooked = true;
            }
            else
            {
                var slot = Repository.Slots.First(s => s.Id == itemId);
                slot.IsBooked = true;
                var resSlots = station.ReservedSlots + 1;
                station.ReservedSlots = resSlots;
            }


            //Add the action to the database.
            Repository.Reservations.Add(new Reservation
            {
                Id = id,
                IsBike = isBike,
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

        public CommandResult RemoveReservation(string id, string stationId)
        {
            var commandResult = new CommandResult();

            var reservation = Repository.Reservations.FirstOrDefault(s => s.UserId == id && s.StationId == stationId);

            if (reservation == null)
            {
                commandResult.AddValidationError("La reserva no es válida");
                return commandResult;
            }

            RemoveReservationItem(stationId, reservation);

            return commandResult;
        }

        private void RemoveReservationItem(string stationId, Reservation reservation)
        {
            if (reservation.IsBike)
            {
                var bike = Repository.Bikes.First(b => b.Id == reservation.ItemId);
                bike.IsBooked = false;
                Repository.Stations.First(s => s.Id == stationId).FreeBikes++;
            }
            else
            {
                var slot = Repository.Slots.First(s => s.Id == reservation.ItemId);
                slot.IsBooked = false;
                Repository.Stations.First(s => s.Id == stationId).ReservedSlots--;
            }

            Repository.Reservations.Remove(reservation);

            Repository.Commit();
        }

        public CommandResult RemoveReservations(List<string> ids)
        {
            var commandResult = new CommandResult();

            var reservations = ids.Select(id => Repository.Reservations.FirstOrDefault(r => r.UserId == id)).ToList();

            if (reservations.Any(reservation => reservation == null))
            {
                commandResult.AddValidationError("Hay reservas no válidas");
                return commandResult;
            }

            Repository.Reservations.RemoveRange(reservations);
            return commandResult;
        }
    }
}
