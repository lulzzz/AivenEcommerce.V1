
using AivenEcommerce.V1.Domain.Enums;

namespace AivenEcommerce.V1.Domain.Entities
{
    public class Basket
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public BasketStatus Status { get; set; }
    }
}
