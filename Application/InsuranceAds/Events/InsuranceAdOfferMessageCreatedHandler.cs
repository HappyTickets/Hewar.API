//using Domain.Events.InsuranceAds;
//using Domain.Events.Notifications;

//namespace Application.InsuranceAds.Events
//{
//    internal class InsuranceAdOfferMessageCreatedHandler : INotificationHandler<InsuranceAdOfferMessageCreated>
//    {
//        private readonly IUnitOfWorkService _ufw;

//        public InsuranceAdOfferMessageCreatedHandler(IUnitOfWorkService ufw)
//        {
//            _ufw = ufw;
//        }

//        public async Task Handle(InsuranceAdOfferMessageCreated notification, CancellationToken cancellationToken)
//        {
//            var userNotification = new Notification
//            {
//                ContentEn = "You have received a new message on your offer.",
//                ContentAr = "لقد استلمت رسالة جديدة على عرضك.",
//                IsRead = false,
//                ReferenceId = notification.InsuranceAdOfferMessage.AdOfferId,
//                ReferenceType = ReferenceTypes.InsuranceAdOffer,
//                Event = NotificationEvents.InsuranceAdOfferMessageCreated,
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
