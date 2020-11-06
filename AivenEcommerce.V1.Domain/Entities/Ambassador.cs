using System;

namespace AivenEcommerce.V1.Domain.Entities
{
    public class Ambassador
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Code { get; set; }
        public Uri PayPalMeLink { get; set; }
    }
}
