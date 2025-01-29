//using Domain.Events.Ads;
//using Domain.Events.Notifications;

//namespace Application.Ads.Events
//{
//    internal class InsuranceAdOfferMessageCreatedHandler : INotificationHandler<AdOfferMessageCreated>
//    {
//        private readonly IUnitOfWorkService _ufw;

//        public InsuranceAdOfferMessageCreatedHandler(IUnitOfWorkService ufw)
//        {
//            _ufw = ufw;
//        }

//        public async Task Handle(AdOfferMessageCreated notification, CancellationToken cancellationToken)
//        {
//            var userNotification = new Notification
//            {
//                ContentEn = "You have received a new message on your offer.",
//                ContentAr = "لقد استلمت رسالة جديدة على عرضك.",
//                IsRead = false,
//                ReferenceId = notification.InsuranceAdOfferMessage.AdOfferId,
//                ReferenceType = ReferenceTypes.AdOffer,
//                Event = NotificationEvents.AdOfferMessageCreated,
//                NotifiedOn = DateTimeOffset.UtcNow,
//                RecipientId = notification.InsuranceAdOfferMessage.AdOffer.CompanyId,
//                RecipientType = AccountTypes.Company
//            };

//            //if(notification.InsuranceAdOfferMessage.SenderType == AccountTypes.Company)
//            //{
//            userNotification.RecipientId = notification.InsuranceAdOfferMessage.AdOffer.Ad.FacilityId;
//            userNotification.RecipientType = AccountTypes.Facility;
//            //}

//            userNotification.AddDomainEvent(new NotificationCreated(userNotification));
//            _ufw.Notifications.Create(userNotification);
//            await _ufw.SaveChangesAsync();
//        }
//    }
//}
