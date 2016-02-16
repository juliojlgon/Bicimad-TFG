using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bicimad.Services.Command.Commands
{
    internal class CommandBase
    {
        public virtual CommandValidationResult Validate()
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(this, null, null);

            Validator.TryValidateObject(this, validationContext, validationResults, true);

            return new CommandValidationResult(validationResults);
        }
    }
}