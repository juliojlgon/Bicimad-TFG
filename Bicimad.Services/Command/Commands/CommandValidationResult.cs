using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bicimad.Services.Command.Commands
{
    public class CommandValidationResult
        {
            public IList<ValidationResult> ValidationErrors { get; private set; }

            public CommandValidationResult(IList<ValidationResult> violations = null)
            {
                ValidationErrors = violations ?? new List<ValidationResult>();
            }

            public bool IsValid
            {
                get { return ValidationErrors.Count == 0; }
            }
        }
    }
}
