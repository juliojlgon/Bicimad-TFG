using System.ComponentModel.DataAnnotations;
using Bicimad.Core;
using Bicimad.Services.Command.Commands;

namespace Bicimad.Services.Command
{
    public class BaseCommandService
    {
        protected readonly IRepository Repository;

        public BaseCommandService(IRepository repository)
        {
            Repository = repository;
        }

        public static CommandResult Validate(CommandBase command)
        {
            if (command == null)
            {
                return new CommandResult
                {
                    ValidationErrors = new[] {new ValidationResult("Command can not be null")}
                };
            }

            var result = new CommandResult();

            var validationResult = command.Validate();
            if (!validationResult.IsValid)
            {
                result.ValidationErrors = validationResult.ValidationErrors;
            }

            return result;
        }
    }
}