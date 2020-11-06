using System;

namespace AivenEcommerce.V1.Domain.Entities
{
    public class Label
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public string ColorHex { get; set; }
    }
}
