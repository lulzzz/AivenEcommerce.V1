namespace AivenEcommerce.V1.Domain.Enums
{
    public enum DeliveryStatus : short
    {
        Created,
        Delivered,
        Going,
        NoAddressFound,
        ProductIncident,
        IncorrectCode,
        BuyerNotFound
    }
}
