namespace Domain.Enums
{
    public enum SuccessCodes
    {

        // shared
        //None = 0,
        OperationSuccessful = 1,
        Created = 2,
        Updated = 3,
        Deleted = 4,
        Authorized = 5,
        SoftDeleted = 6,
        HardDeleted = 7,
        fileUploaded = 8,

        // account
        EmailConfirmed = 100,
        PasswordReset = 101,
        UserRegistered = 102,
        EmailConfirmation = 103,
        EmailConfirmationMessage = 104,
        EmailChangedSuccessfully = 105,
        PasswordResetMessage = 106,
        LoggedInSuccessfully = 107,
        LoggedoutSuccessfully = 108,
        UserReceived = 109,

        // roles
        RoleAssigned = 200,
        RoleUnassigned = 201,
        RoleCreated = 202,
        RoleDeleted = 203,
        RoleUpdated = 204,
        RoleReceived = 205,
        RolesReceived = 206,
        RoleExists = 207,
        UserRemovedFromRole = 208,

        // tickets
        TicketCreated = 300,
        TicketClosed = 301,
        CreateTicketMessage = 302,
        GetMyReceivedTickets = 303,
        GetMySentTickets = 304,
        GetTicketMessages = 305,


        // insurance ads
        AdCreated = 400,
        AdUpdated = 401,
        AdPublished = 402,
        AdOfferAccepted = 403,
        MyAdReceived = 404,
        OpenAdsReceived = 405,
        OfferCreated = 406,
        OfferAccepted = 407,
        AdReceived = 408,
        AdsReceived = 409,
        OfferRejected = 410,
        OfferCanceled = 411,
        MyOffersByAdIdAsFacilityReceived = 412,
        MyOffersAsFacilityReceived = 413,
        MyOffersByAdIdAsCompanyReceived = 414,
        MyOffersAsCompanyReceived = 415,
        CreateOfferMessage = 416,
        OfferMessagesReceived = 417,

        // price requests
        PriceRequestCreated = 500,
        PriceRequestApproved = 501,
        CreateRequestFacilityDetails = 502,
        UpdateRequestFacilityDetails = 503,
        GetRequestFacilityDetails = 504,
        GetMyRequestsAsFacility = 505,
        GetMyRequestsAsCompany = 506,
        PriceRequestRejected = 507,
        CancelRequest = 508,
        CreateRequestMessage = 509,
        GetRequestMessages = 510,

        //tokens
        RefreshToken = 600,

        //companies
        CompanyCreated = 700,
        CompanyUpdated = 701,
        CompanyReceived = 702,
        CompaniesReceived = 703,
        CompanySoftDeleted = 704,
        CompanyHardDeleted = 705,

        //facilities
        FacilityCreated = 800,
        FacilityUpdated = 801,
        FacilityReceived = 802,
        FacilitiesReceived = 803,
        FacilitySoftDeleted = 804,
        FacilityHardDeleted = 805,

        //guards
        GuardCreated = 900,
        GuardUpdated = 901,
        GuardReceived = 902,
        GuardsReceived = 903,
        GuardSoftDeleted = 904,
        GuardHardDeleted = 905,

        //notification
        MarkAsRead = 950,
        NotificationsReceived = 951,
        CountUnRead = 952,

    }
}
