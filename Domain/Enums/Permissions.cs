namespace Domain.Enums
{
    public enum Permissions
    {
        // roles
        CreateRole = 1,
        UpdateRole = 2,
        DeleteRole = 3,
        ViewRoles = 4,
        AssignUserToRole = 5,
        UnassignUserToRole = 6,
        ViewUsers = 7,

        // companies
        CreateCompany = 50,
        UpdateCompany = 51,
        DeleteCompany = 52,
        ViewCompanies = 53,

        CreateCompanyService = 54,
        UpdateCompanyService = 55,
        DeleteCompanyService = 56,
        ViewCompaniesService = 57,

        // facilities
        CreateFacility = 100,
        UpdateFacility = 101,
        DeleteFacility = 102,
        ViewFacilities = 103,

        // guards
        CreateGuard = 150,
        UpdateGuard = 151,
        DeleteGuard = 152,
        ViewGuards = 153,

        // Ads
        CreateAd = 200,
        UpdateAd = 201,
        DeleteAd = 202,
        ViewAds = 203,

        // Ads Offers
        CreateAdOffer = 250,
        UpdateAdOffer = 251,
        HideAdOffer = 252,
        ShowAdOffer = 253,
        AcceptAdOffer = 254,
        RejectAdOffer = 255,
        CancelAdOffer = 256,

        // Contracts
        CreateContract = 300,
        UpdateContract = 301,
        ViewContracts = 302,
        SignContract = 303,

        // Chats
        CreatePriceRequestChat = 350,
        CreatePriceOfferChat = 351,
        CreateAdOfferChat = 352,
        SendPriceRequestMessage = 3543,
        SendPriceOfferMessage = 354,
        SendAdOfferMessage = 355,
        ViewChatMessages = 356,


        // Price Requests
        CreatePriceRequest = 400,
        UpdatePriceRequest = 401,
        RejectPriceRequest = 402,
        CancelPriceRequest = 403,
        ViewPriceRequests = 404,
        HidePriceRequest = 405,
        ShowPriceRequest = 406,

        // Price Offers
        CreatePriceOffer = 450,
        UpdatePriceOffer = 451,
        HidePriceOffer = 452,
        ShowPriceOffer = 453,
        AcceptPriceOffer = 454,
        RejectPriceOffer = 455,
        CancelPriceOffer = 456,
        ViewPriceOffers = 457,

        // Tickets
        CreateTicket = 500,
        CreateTicketMessage = 501,
        ViewReceivedTickets = 502,
        ViewSentTickets = 503,
        ViewTicketMessages = 504,
        CloseTicket = 505,

        // Custom Clauses 
        CreateCustomClause = 550,
        UpdateCustomClause = 551,
        DeleteCustomClause = 552,
        ViewCustomClauses = 553,

        // Schedule Entries
        CreateScheduleEntry = 600,
        UpdateScheduleEntry = 601,
        DeleteScheduleEntry = 602,
        ViewScheduleEntries = 603,

        // Security Certificates
        CreateSecurityCertificate = 650,
        UpdateSecurityCertificate = 651,
        DeleteSecurityCertificate = 652,
        ViewSecurityCertificate = 653,
        RejectSecurityCertificate = 654,
        ApproveSecurityCertificate = 655,


        // Hewar Services 
        CreateHewarService = 700,
        UpdateHewarService = 701,
        DeleteHewarService = 702,
        ViewHewarServices = 703,
    }
}
