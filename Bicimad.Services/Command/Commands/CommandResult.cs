using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Bicimad.Services.Command.Commands
{
    public class CommandResult
    {
        public CommandResult()
        {
            ValidationErrors = new List<ValidationResult>();
        }

        public CommandResult(string errorMessage)
            : this()
        {
            ValidationErrors.Add(new ValidationResult(errorMessage));
        }

        public IList<ValidationResult> ValidationErrors { get; set; }
        public string ItemId { get; set; }

        public virtual bool Success
        {
            get { return ValidationErrors == null || ValidationErrors.Count == 0; }
        }

        public string FirstErrorMessage
        {
            get
            {
                var result = string.Empty;
                if (ValidationErrors != null && ValidationErrors.Any())
                {
                    result = ValidationErrors.First().ErrorMessage;
                }
                return result;
            }
        }

        public void AddValidationError(ValidationResult validationResult)
        {
            ValidationErrors.Add(validationResult);
        }

        public void AddValidationError(string errorMessage)
        {
            ValidationErrors.Add(new ValidationResult(errorMessage));
        }

        public void AddValidationError(string errorMessage, IEnumerable<string> memberNames)
        {
            ValidationErrors.Add(new ValidationResult(errorMessage, memberNames));
        }
    }
}