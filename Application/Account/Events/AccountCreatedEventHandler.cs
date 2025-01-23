using Application.AccountManagement.Dtos.Email;
using Application.AccountManagement.Service.Interfaces;
using Domain.Events.Accounts;

namespace Application.Account.Events
{
    internal class AccountCreatedEventHandler(IEmailConfirmationService emailConfirmationService) : INotificationHandler<AccountCreated>
    {
        public async Task Handle(AccountCreated notification, CancellationToken cancellationToken)
        {
            if (notification.User.EmailConfirmed || notification.User.Email is null)
            {
                return;
            }
            await emailConfirmationService.SendEmailConfirmationAsync(new SendEmailConfirmationRequest { Email = notification.User.Email });
        }
    }
}
