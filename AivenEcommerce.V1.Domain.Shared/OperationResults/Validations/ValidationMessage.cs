
using System;

namespace AivenEcommerce.V1.Domain.Shared.OperationResults.Validations
{
    public class ValidationMessage
    {
        public ValidationMessage(string field, string message)
        {
            Field = field;
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }

        public ValidationMessage(string message) : this(null, message)
        {

        }

        public ValidationMessage()
        {
        }

        public string Field { get; set; }
        public string Message { get; set; }
    }
}
