namespace Domain.Enums
{
    public enum NotificationEvents
    {
        // price requests
        PriceRequestCreated = 1,
        PriceRequestAccepted = 2,
        PriceRequestRejected = 3,

        // tickets
        TicketCreated = 4,
        TicketClosed = 5,
        TicketMessageCreated = 6,

        // insurance ads
        InsuranceAdOfferCreated = 7,
        InsuranceAdOfferAccepted = 8,
        InsuranceAdOfferRejected = 9,
        InsuranceAdOfferMessageCreated = 10,
    }
}
