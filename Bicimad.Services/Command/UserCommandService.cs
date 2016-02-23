using System.Linq;
using Bicimad.Core;
using Bicimad.Core.DomainObjects;
using Bicimad.Helpers;
using Bicimad.Services.Command.Commands;
using Bicimad.Services.Command.Commands.User;
using Bicimad.Services.Command.Interface;

namespace Bicimad.Services.Command
{
    public class UserCommandService : BaseCommandService, IUserCommandService
    {
        public UserCommandService(IRepository repository) : base(repository)
        {
        }

        public CommandResult Create(CreateUserCommand command)
        {
            var commandResult = Validate(command);
            if (!commandResult.Success) return commandResult;

            if (Repository.Users.Any(u => u.UserName == command.UserName))
            {
                commandResult.AddValidationError("Ese nombre de usuario ya existe");
                return commandResult;
            }

            if (Repository.Users.Any(u => u.Email == command.Email))
            {
                commandResult.AddValidationError("Ese email ya existe");
                return commandResult;
            }

            string id = GuidHelper.GenerateId();

            Repository.Users.Add(new User
            {
                Id = id,
                CreatedDate = DateTimeHelper.SpanishNow,
                UserName = command.UserName,
                FriendlyUrlUserName = command.FriendlyUrlUserName,
                Email = command.Email,
                IsActive = command.IsActive,
                IsAdmin = command.IsAdmin,
                Avatar = !string.IsNullOrEmpty(command.Avatar) ? command.Avatar : null,
                Password = !string.IsNullOrEmpty(command.Password) ? HashHelper.Hash(command.Password) : null,
                Name = !string.IsNullOrEmpty(command.Name) ? command.Name : null,
                Surname = !string.IsNullOrEmpty(command.Surname) ? command.Surname : null
            });

            Repository.Commit();

            commandResult.ItemId = id;
            return commandResult;
        }
    }
}