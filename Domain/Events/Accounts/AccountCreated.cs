namespace Domain.Events.Accounts
{
    public record AccountCreated(ApplicationUser User) : INotification;

}
