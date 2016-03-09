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

        public CommandResult Create(TakeBikeCommand command)
        {
            //TODO: Probablemente haya que expandir el UserHistories para que contenga tambien la estacion de destino.
            var commandResult = Validate(command);
            if (!commandResult.Success) return commandResult;

            if (Repository.UserHistories.Any(us => us.UserId == command.UserId))
            {
                commandResult.AddValidationError("No puedes coger más de una bicicleta a la vez");
                return commandResult;
            }

            //TODO: Buscar si hay mas errores de validación.

            var id = GuidHelper.GenerateId();

            Repository.UserHistories.Add(new UserHistory
            {
                BikeId = command.BikeId,
                UserId = command.UserId,
                StationId = command.StationId
            });

            Repository.Commit();

            commandResult.ItemId = id;
            return commandResult;
        }
    }
}