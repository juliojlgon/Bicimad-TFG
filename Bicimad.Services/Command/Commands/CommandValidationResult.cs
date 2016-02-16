using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bicimad.Services.Command.Commands
{
    public class CommandValidationResult
    {
        public CommandValidationResult(IList<ValidationResult> violations = null)
        {
            ValidationErrors = violations ?? new List<ValidationResult>();
        }

        public IList<ValidationResult> ValidationErrors { get; private set; }

        public bool IsValid
        {
            get { return ValidationErrors.Count == 0; }
        }
    }
}

}