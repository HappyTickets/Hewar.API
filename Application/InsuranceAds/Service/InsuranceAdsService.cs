using Application.Guards.Dtos;
using Application.InsuranceAds.Dtos;
using AutoMapper;
using Domain.Events.InsuranceAds;

namespace Application.InsuranceAds.Service
{
    internal class InsuranceAdsService : IInsuranceAdsService
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
            ad.FacilityId = _currentUser.Id ?? 1;

            _ufw.InsuranceAds.Create(ad);
            await _ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.AdCreated);

        }

        public async Task<Result<Empty>> UpdateAdAsync(UpdateInsuranceAdDto dto)
        {
            var ad = await _ufw.InsuranceAds.GetByIdAsync(dto.Id);

            if (ad == null)
                return new NotFoundError();

            _mapper.Map(dto, ad);
            await _ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.AdUpdated);

        }

        public async Task<Result<InsuranceAdDto>> GetAdByIdAsync(long id)
        {
            var ad = await _ufw.InsuranceAds.GetByIdAsync(id, ["Facility.LoginDetails"]);

            if (ad == null)
                return new NotFoundError();

            var insuranceAdDto = _mapper.Map<InsuranceAdDto>(ad);
            return Result<InsuranceAdDto>.Success(insuranceAdDto, SuccessCodes.AdReceived);

        }

        public async Task<Result<InsuranceAdDto[]>> GetMyAdsAsync()
        {
            var userId = _currentUser.Id ?? 1;

            var ads = await _ufw.InsuranceAds
                .FilterAsync(ad => ad.FacilityId == userId, ["Facility.LoginDetails"]);

            var insurancesAdDto = _mapper.Map<InsuranceAdDto[]>(ads);
            return Result<InsuranceAdDto[]>.Success(insurancesAdDto, SuccessCodes.MyAdReceived);

        }

        public async Task<Result<InsuranceAdDto[]>> GetOpenedAdsAsync()
        {
            var ads = await _ufw.InsuranceAds
                .FilterAsync(ad => ad.Status == InsuranceAdStatus.Opened, ["Facility.LoginDetails"]);

            var insurancesAdDto = _mapper.Map<InsuranceAdDto[]>(ads);
            return Result<InsuranceAdDto[]>.Success(insurancesAdDto, SuccessCodes.OpenAdsReceived);

        }

        public async Task<Result<Empty>> CreateOfferAsync(CreateInsuranceAdOfferDto dto)
        {
            var ad = await _ufw.InsuranceAds.GetByIdAsync(dto.InsuranceAdId);

            if (ad == null)
                return new NotFoundError();

            if (ad.Status != InsuranceAdStatus.Opened)
                return new ConflictError(ErrorCodes.AdNotOpened);

            var offer = _mapper.Map<InsuranceAdOffer>(dto);
            offer.Status = RequestStatus.Pending;
            offer.SentDate = DateTimeOffset.UtcNow;
            offer.CompanyId = _currentUser.Id! ?? 1;
            offer.InsuranceAd = ad;

            offer.AddDomainEvent(new InsuranceAdOfferCreated(offer));
            _ufw.InsuranceAdOffers.Create(offer);
            await _ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.OfferCreated);

        }

        public async Task<Result<Empty>> AcceptOfferAsync(long offerId)
        {
            var offer = await _ufw.InsuranceAdOffers.GetByIdAsync(offerId);

            if (offer == null)
                return new NotFoundError();

            if (offer.Status != RequestStatus.Pending)
                return new ConflictError(ErrorCodes.AdOfferNotPending);

            offer.Status = RequestStatus.Accepted;

            offer.AddDomainEvent(new InsuranceAdOfferAccepted(offer));
            await _ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.AdOfferAccepted);

        }

        public async Task<Result<Empty>> RejectOfferAsync(long offerId)
        {
            var offer = await _ufw.InsuranceAdOffers.GetByIdAsync(offerId);

            if (offer == null)
                return new NotFoundError();

            if (offer.Status != RequestStatus.Pending)
                return new ConflictError(ErrorCodes.AdOfferNotPending);

            offer.Status = RequestStatus.Rejected;

            offer.AddDomainEvent(new InsuranceAdOfferRejected(offer));
            await _ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.OfferRejected);

        }

        public async Task<Result<Empty>> CancelOfferAsync(long offerId)
        {
            var offer = await _ufw.InsuranceAdOffers.GetByIdAsync(offerId, ["InsuranceAd"]);

            if (offer == null)
                return new NotFoundError();

            if (offer.Status != RequestStatus.Pending)
                return new ConflictError(ErrorCodes.AdOfferNotPending);

            offer.Status = RequestStatus.Cancelled;

            offer.AddDomainEvent(new InsuranceAdOfferCancelled(offer));
            await _ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.OfferCanceled);

        }

        public async Task<Result<FacilityInsuranceAdOfferDto[]>> GetMyOffersByAdIdAsFacilityAsync(long adId)
        {
            var userId = _currentUser.Id ?? 1;

            var offers = await _ufw.InsuranceAdOffers
                .FilterAsync(o => o.InsuranceAdId == adId && o.InsuranceAd.FacilityId == userId, ["Company.LoginDetails"]);

            var facilityInsuranceAdOfferDto = _mapper.Map<FacilityInsuranceAdOfferDto[]>(offers);
            return Result<FacilityInsuranceAdOfferDto[]>.Success(facilityInsuranceAdOfferDto,
                SuccessCodes.MyOffersByAdIdAsFacilityReceived);

        }


        public async Task<Result<FacilityInsuranceAdOfferDto[]>> GetMyOffersAsFacilityAsync()
        {
            var userId = _currentUser.Id ?? 1;

            var offers = await _ufw.InsuranceAdOffers
                .FilterAsync(o => o.InsuranceAd.FacilityId == userId, ["Company.LoginDetails"]);

            var facilityInsuranceAdOfferDto = _mapper.Map<FacilityInsuranceAdOfferDto[]>(offers);
            return Result<FacilityInsuranceAdOfferDto[]>.Success(facilityInsuranceAdOfferDto,
                SuccessCodes.MyOffersAsFacilityReceived);
        }

        public async Task<Result<CompanyInsuranceAdOfferDto[]>> GetMyOffersByAdIdAsCompanyAsync(long adId)
        {
            var userId = _currentUser.Id ?? 1;
            var offers = await _ufw.InsuranceAdOffers
                .FilterAsync(o => o.InsuranceAdId == adId && o.CompanyId == userId, ["InsuranceAd.Facility.LoginDetails"]);

            var companyInsuranceAdOfferDto = _mapper.Map<CompanyInsuranceAdOfferDto[]>(offers);
            return Result<CompanyInsuranceAdOfferDto[]>.Success(companyInsuranceAdOfferDto,
                SuccessCodes.MyOffersByAdIdAsCompanyReceived);
        }

        public async Task<Result<CompanyInsuranceAdOfferDto[]>> GetMyOffersAsCompanyAsync()
        {
            var userId = _currentUser.Id ?? 1;
            var offers = await _ufw.InsuranceAdOffers
                .FilterAsync(o => o.CompanyId == userId, ["InsuranceAd.Facility.LoginDetails"]);

            var companyInsuranceAdOfferDto = _mapper.Map<CompanyInsuranceAdOfferDto[]>(offers);
            return Result<CompanyInsuranceAdOfferDto[]>.Success(companyInsuranceAdOfferDto,
                SuccessCodes.MyOffersAsCompanyReceived);
        }

        public async Task<Result<Empty>> CreateOfferMessageAsync(CreateInsuranceAdOfferMessageDto dto)
        {
            var offer = await _ufw.InsuranceAdOffers.GetByIdAsync(dto.InsuranceAdOfferId, ["InsuranceAd"]);

            if (offer == null)
                return new NotFoundError();

            if (offer.InsuranceAd.Status != InsuranceAdStatus.Opened)
                return new ConflictError(ErrorCodes.AdNotOpened);

            if (offer.Status != RequestStatus.Pending)
                return new ConflictError(ErrorCodes.AdOfferNotPending);

            var message = _mapper.Map<InsuranceAdOfferMessage>(dto);
            message.SentDate = DateTimeOffset.UtcNow;
            message.SenderId = _currentUser.Id!.Value;
            message.SenderType = _currentUser.Type!.Value;
            message.InsuranceAdOffer = offer;

            message.AddDomainEvent(new InsuranceAdOfferMessageCreated(message));
            _ufw.InsuranceAdOfferMessages.Create(message);
            await _ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.CreateOfferMessage);
        }

        public async Task<Result<InsuranceAdOfferMessageDto[]>> GetOfferMessagesAsync(long offerId)
        {
            var messages = await _ufw.InsuranceAdOfferMessages
                .FilterAsync(m => m.InsuranceAdOfferId == offerId);

            var insuranceAdOfferMessageDto = _mapper.Map<InsuranceAdOfferMessageDto[]>(messages);
            return Result<InsuranceAdOfferMessageDto[]>.Success(insuranceAdOfferMessageDto,
                SuccessCodes.OfferMessagesReceived);

        }
    }
}
