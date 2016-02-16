using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bicimad.Services.Command.Commands
{
    class CommandBase
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
