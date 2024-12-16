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

        // users
        CreateUser = 50,
        UpdateUser = 51,
        DeleteUser = 52,
        ViewUsers = 53,

        // PriceRquests
        CreatePriceRequest = 100,
        UpdatePriceRequest = 101,
        AcceptPriceRequest = 102,
        RejectPriceRequest = 103,
        ViewPriceRequests = 104,

        // tickets
        CreateTicket = 150,
        ViewTickets = 151,
        CloseTicket = 152,
        CreateTicketMessage = 153,
        ViewTicketMessages = 154,

    }
}
