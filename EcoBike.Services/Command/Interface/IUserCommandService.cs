using Bicimad.Services.Command.Commands;
using Bicimad.Services.Command.Commands.User;

namespace Bicimad.Services.Command.Interface
{
    public interface IUserCommandService
    {
        CommandResult Create(CreateUserCommand command);
    }
}