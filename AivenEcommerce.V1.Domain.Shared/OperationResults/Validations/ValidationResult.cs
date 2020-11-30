
using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Shared.OperationResults.Validations
{
    public class ValidationResult
    {
        public ValidationResult()
        {
            Messages = new List<ValidationMessage>();
        }

        public bool IsSuccess { get { return Messages.Count == 0; } }

        public ICollection<ValidationMessage> Messages { get; set; }
    }
}
