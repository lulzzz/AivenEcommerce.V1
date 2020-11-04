namespace AivenEcommerce.V1.Domain.Entities
{
    public class BasketItemDetail
    {
        public int Id { get; set; }
        public int BasketItemId { get; set; }
        public int ProductDetailId { get; set; }
    }
}
