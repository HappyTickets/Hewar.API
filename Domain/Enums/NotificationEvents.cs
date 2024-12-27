namespace Domain.Enums
{
    public enum NotificationEvents
    {
        // price requests
        PriceRequestCreated = 1,
        PriceRequestAccepted = 2,
        PriceRequestRejected = 3,
        PriceRequestMessageCreated = 4,

        // tickets
        TicketCreated = 50,
        TicketClosed = 51,
        TicketMessageCreated = 52,

        // insurance ads
        InsuranceAdOfferCreated = 100,
        InsuranceAdOfferAccepted = 101,
        InsuranceAdOfferRejected = 102,
        InsuranceAdOfferMessageCreated = 103,
    }
}
