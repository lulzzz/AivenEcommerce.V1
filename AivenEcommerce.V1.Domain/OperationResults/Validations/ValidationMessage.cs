using System;

namespace AivenEcommerce.V1.Application.Validations
{
    public class ValidationMessage
    {
        public ValidationMessage(string? field, string message)
        {
            Field = field;
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }

        public ValidationMessage(string message) : this(null, message)
        {
    
        }

        public string? Field { get; set; }
        public string Message { get; set; }
    }
}
