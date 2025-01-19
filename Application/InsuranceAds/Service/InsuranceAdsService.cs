using Application.InsuranceAds.Dtos;
using AutoMapper;
using Domain.Events.InsuranceAds;

namespace Application.InsuranceAds.Service
{
    internal class InsuranceAdsService: IInsuranceAdsService
    {
        private readonly IUnitOfWorkService _ufw;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;

        public InsuranceAdsService(IUnitOfWorkService ufw, IMapper mapper, ICurrentUserService currentUser)
        {
            _ufw = ufw;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<Result<Empty>> CreateAdAsync(CreateInsuranceAdDto dto)
        {
            var ad = _mapper.Map<InsuranceAd>(dto);
            ad.FacilityId = _currentUser.Id?? 11;

            _ufw.InsuranceAds.Create(ad);
            await _ufw.SaveChangesAsync();

            return Empty.Default;
        }

        public async Task<Result<Empty>> UpdateAdAsync(UpdateInsuranceAdDto dto)
        {
            var ad = await _ufw.InsuranceAds.GetByIdAsync(dto.Id);

            if (ad == null)
                return new NotFoundError();

            _mapper.Map(dto, ad);
            await _ufw.SaveChangesAsync();

            return Empty.Default;
        }

        public async Task<Result<InsuranceAdDto>> GetAdByIdAsync(long id)
        {
            var ad = await _ufw.InsuranceAds.GetByIdAsync(id, ["Facility.LoginDetails"]);

            if (ad == null)
                return new NotFoundError();

            return _mapper.Map<InsuranceAdDto>(ad);
        }

        public async Task<Result<InsuranceAdDto[]>> GetMyAdsAsync()
        {
            var userId = _currentUser.Id ?? 1; 

            var ads = await _ufw.InsuranceAds
                .FilterAsync(ad => ad.FacilityId == userId, ["Facility.LoginDetails"]);

            return _mapper.Map<InsuranceAdDto[]>(ads);
        }


        public async Task<Result<InsuranceAdDto[]>> GetOpenedAdsAsync()
        {
            var ads = await _ufw.InsuranceAds
                .FilterAsync(ad => ad.Status == InsuranceAdStatus.Opened, ["Facility.LoginDetails"]);

            return _mapper.Map<InsuranceAdDto[]>(ads);
        }

        public async Task<Result<Empty>> CreateOfferAsync(CreateInsuranceAdOfferDto dto)
        {
            var ad = await _ufw.InsuranceAds.GetByIdAsync(dto.InsuranceAdId);

            if (ad == null)
                return new NotFoundError();

            if (ad.Status != InsuranceAdStatus.Opened)
                return new ConflictError(ErrorCodes.AdNotOpened, Resource.OnlyOpenedAds);

            var offer = _mapper.Map<InsuranceAdOffer>(dto);
            offer.Status = RequestStatus.Pending;
            offer.SentDate = DateTimeOffset.UtcNow;
            offer.CompanyId = _currentUser.Id!??1;
            offer.InsuranceAd = ad;

            offer.AddDomainEvent(new InsuranceAdOfferCreated(offer));
            _ufw.InsuranceAdOffers.Create(offer);
            await _ufw.SaveChangesAsync();

            return Empty.Default;
        }

        public async Task<Result<Empty>> AcceptOfferAsync(long offerId)
        {
            var offer = await _ufw.InsuranceAdOffers.GetByIdAsync(offerId);

            if (offer == null)
                return new NotFoundError();

            if(offer.Status != RequestStatus.Pending)
                return new ConflictError(ErrorCodes.AdOfferNotPending,Resource.OnlyPendingRequests);

            offer.Status = RequestStatus.Accepted;

            offer.AddDomainEvent(new InsuranceAdOfferAccepted(offer));
            await _ufw.SaveChangesAsync();

            return Empty.Default;
        }

        public async Task<Result<Empty>> RejectOfferAsync(long offerId)
        {
            var offer = await _ufw.InsuranceAdOffers.GetByIdAsync(offerId);

            if (offer == null)
                return new NotFoundError();

            if (offer.Status != RequestStatus.Pending)
                return new ConflictError(ErrorCodes.AdOfferNotPending, Resource.OnlyPendingRequests);

            offer.Status = RequestStatus.Rejected;

            offer.AddDomainEvent(new InsuranceAdOfferRejected(offer));
            await _ufw.SaveChangesAsync();

            return Empty.Default;
        }
         
        public async Task<Result<Empty>> CancelOfferAsync(long offerId)
        {
            var offer = await _ufw.InsuranceAdOffers.GetByIdAsync(offerId, ["InsuranceAd"]);

            if (offer == null)
                return new NotFoundError();

            if (offer.Status != RequestStatus.Pending)
                return new ConflictError(ErrorCodes.AdOfferNotPending, Resource.OnlyPendingRequests);

            offer.Status = RequestStatus.Cancelled;

            offer.AddDomainEvent(new InsuranceAdOfferCancelled(offer));
            await _ufw.SaveChangesAsync();

            return Empty.Default;
        }

        public async Task<Result<FacilityInsuranceAdOfferDto[]>> GetMyOffersByAdIdAsFacilityAsync(long adId)
        {
            var userId = _currentUser.Id ?? 1; 

            var offers = await _ufw.InsuranceAdOffers
                .FilterAsync(o => o.InsuranceAdId == adId && o.InsuranceAd.FacilityId == userId, ["Company.LoginDetails"]);

            return _mapper.Map<FacilityInsuranceAdOfferDto[]>(offers);
        }


        public async Task<Result<FacilityInsuranceAdOfferDto[]>> GetMyOffersAsFacilityAsync()
        {
            var userId = _currentUser.Id ?? 1;

            var offers = await _ufw.InsuranceAdOffers
                .FilterAsync(o => o.InsuranceAd.FacilityId == userId, ["Company.LoginDetails"]);

            return _mapper.Map<FacilityInsuranceAdOfferDto[]>(offers);
        }

        public async Task<Result<CompanyInsuranceAdOfferDto[]>> GetMyOffersByAdIdAsCompanyAsync(long adId)
        {
            var userId = _currentUser.Id ?? 1;
            var offers = await _ufw.InsuranceAdOffers
                .FilterAsync(o => o.InsuranceAdId == adId && o.CompanyId == userId, ["InsuranceAd.Facility.LoginDetails"]);

            return _mapper.Map<CompanyInsuranceAdOfferDto[]>(offers);
        }
        
        public async Task<Result<CompanyInsuranceAdOfferDto[]>> GetMyOffersAsCompanyAsync()
        {
            var userId = _currentUser.Id ?? 1;
            var offers = await _ufw.InsuranceAdOffers
                .FilterAsync(o => o.CompanyId == userId , ["InsuranceAd.Facility.LoginDetails"]);

            return _mapper.Map<CompanyInsuranceAdOfferDto[]>(offers);
        }

        public async Task<Result<Empty>> CreateOfferMessageAsync(CreateInsuranceAdOfferMessageDto dto)
        {
            var offer = await _ufw.InsuranceAdOffers.GetByIdAsync(dto.InsuranceAdOfferId, ["InsuranceAd"]);

            if (offer == null)
                return new NotFoundError();

            if (offer.InsuranceAd.Status != InsuranceAdStatus.Opened)
                return new ConflictError(ErrorCodes.AdNotOpened, Resource.OnlyOpenedAds);

            if(offer.Status != RequestStatus.Pending)
                return new ConflictError(ErrorCodes.AdOfferNotPending, Resource.OnlyPendingRequests);

            var message = _mapper.Map<InsuranceAdOfferMessage>(dto);
            message.SentDate = DateTimeOffset.UtcNow;
            message.SenderId = _currentUser.Id!.Value;
            message.SenderType = _currentUser.Type!.Value;
            message.InsuranceAdOffer = offer;

            message.AddDomainEvent(new InsuranceAdOfferMessageCreated(message));
            _ufw.InsuranceAdOfferMessages.Create(message);
            await _ufw.SaveChangesAsync();

            return Empty.Default;
        }

        public async Task<Result<InsuranceAdOfferMessageDto[]>> GetOfferMessagesAsync(long offerId)
        {
            var messages = await _ufw.InsuranceAdOfferMessages
                .FilterAsync(m=>m.InsuranceAdOfferId == offerId);

            return _mapper.Map<InsuranceAdOfferMessageDto[]>(messages);
        }
    }
}
