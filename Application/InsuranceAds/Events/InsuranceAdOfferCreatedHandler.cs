﻿using Domain.Events.InsuranceAds;
using Domain.Events.Notifications;

namespace Application.InsuranceAds.Events
{
    internal class InsuranceAdOfferCreatedHandler : INotificationHandler<InsuranceAdOfferCreated>
    {
        private readonly IUnitOfWorkService _ufw;

        public InsuranceAdOfferCreatedHandler(IUnitOfWorkService ufw)
        {
            _ufw = ufw;
        }

        public async Task Handle(InsuranceAdOfferCreated notification, CancellationToken cancellationToken)
        {
            var userNotification = new Notification
            {
                ContentEn = "You have received a new offer.",
                ContentAr = "لقد استلمت عرض جديد.",
                IsRead = false,
                ReferenceId = notification.InsuranceAdOffer.Id,
                ReferenceType = ReferenceTypes.InsuranceAdOffer,
                Event = NotificationEvents.InsuranceAdOfferCreated,
                NotifiedOn = DateTimeOffset.UtcNow,
                RecipientId = notification.InsuranceAdOffer.Ad.FacilityId,
                //RecipientType = AccountTypes.Facility
            };

            userNotification.AddDomainEvent(new NotificationCreated(userNotification));
            _ufw.GetRepository<Notification>().Create(userNotification);
            await _ufw.SaveChangesAsync();
        }
    }
}
