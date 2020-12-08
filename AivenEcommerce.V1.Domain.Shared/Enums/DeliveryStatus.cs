namespace AivenEcommerce.V1.Domain.Shared.Enums
{
    public enum DeliveryStatus : short
    {
        Created,
        Pending,
        Delivered,
        Going,
        NoAddressFound,
        ProductIncident,
        IncorrectCode,
        BuyerNotFound
    }
}
