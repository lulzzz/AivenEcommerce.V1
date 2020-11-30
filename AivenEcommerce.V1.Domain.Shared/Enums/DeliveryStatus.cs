namespace AivenEcommerce.V1.Domain.Shared.Enums
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
