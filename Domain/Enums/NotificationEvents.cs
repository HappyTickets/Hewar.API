namespace Domain.Enums
{
    public enum NotificationEvents
    {
        // price requests
        PriceRequestCreated = 1,
        PriceRequestAccepted = 2,
        PriceRequestRejected = 3,
        PriceRequestCancelled = 4,
        PriceRequestMessageCreated = 5,
        // Price offers
        PriceOfferCreated = 20,
        PriceOfferAccepted = 21,
        PriceOfferRejected = 22,
        PriceOfferCancelled = 23,
        PriceOfferMessageCreated = 24,

        // tickets
        TicketCreated = 50,
        TicketClosed = 51,
        TicketMessageCreated = 52,

        // insurance ads
        AdOfferCreated = 100,
        AdOfferAccepted = 101,
        AdOfferRejected = 102,
        AdOfferCancelled = 103,
        AdOfferMessageCreated = 104,
    }
}
