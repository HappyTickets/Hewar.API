//using Application.Ads.Dtos;
//using AutoMapper;
//using Domain.Events.Ads;

//namespace Application.Ads.Service
//{
//    internal class InsuranceAdsService : IInsuranceAdsService
//    {
//        private readonly IUnitOfWorkService _ufw;
//        private readonly IMapper _mapper;
//        private readonly ICurrentUserService _currentUser;

//        public InsuranceAdsService(IUnitOfWorkService ufw, IMapper mapper, ICurrentUserService currentUser)
//        {
//            _ufw = ufw;
//            _mapper = mapper;
//            _currentUser = currentUser;
//        }

//        public async Task<Result<Empty>> CreateAdAsync(CreateInsuranceAdDto dto)
//        {
//            var ad = _mapper.Map<Ad>(dto);
//            ad.FacilityId = _currentUser.AccountId ?? 1;

//            _ufw.Ads.Create(ad);
//            await _ufw.SaveChangesAsync();

//            return Result<Empty>.Success(Empty.Default, SuccessCodes.AdCreated);

//        }

//        public async Task<Result<Empty>> UpdateAdAsync(UpdateAdDto dto)
//        {
//            var ad = await _ufw.Ads.GetByIdAsync(dto.Id);

//            if (ad == null)
//                return new NotFoundError();

//            _mapper.Map(dto, ad);
//            await _ufw.SaveChangesAsync();

//            return Result<Empty>.Success(Empty.Default, SuccessCodes.AdUpdated);

//        }

//        public async Task<Result<InsuranceAdDto>> GetAdByIdAsync(long id)
//        {
//            var ad = await _ufw.Ads.GetByIdAsync(id, [$"{nameof(Facility)}.{nameof(Facility.LoginDetails)}"]);

//            if (ad == null)
//                return new NotFoundError();

//            var insuranceAdDto = _mapper.Map<InsuranceAdDto>(ad);
//            return Result<InsuranceAdDto>.Success(insuranceAdDto, SuccessCodes.AdReceived);

//        }

//        public async Task<Result<InsuranceAdDto[]>> GetMyAdsAsync()
//        {
//            var userId = _currentUser.AccountId ?? 1;

//            var ads = await _ufw.Ads
//                .FilterAsync(ad => ad.FacilityId == userId, [$"{nameof(Facility)}.{nameof(Facility.LoginDetails)}"]);

//            var insurancesAdDto = _mapper.Map<InsuranceAdDto[]>(ads);
//            return Result<InsuranceAdDto[]>.Success(insurancesAdDto, SuccessCodes.MyAdReceived);

//        }

//        public async Task<Result<InsuranceAdDto[]>> GetOpenedAdsAsync()
//        {
//            var ads = await _ufw.Ads
//                .FilterAsync(ad => ad.Status == InsuranceAdStatus.Opened, [$"{nameof(Facility)}.{nameof(Facility.LoginDetails)}"]);

//            var insurancesAdDto = _mapper.Map<InsuranceAdDto[]>(ads);
//            return Result<InsuranceAdDto[]>.Success(insurancesAdDto, SuccessCodes.OpenAdsReceived);

//        }

//        public async Task<Result<Empty>> CreateOfferAsync(CreateInsuranceAdOfferDto dto)
//        {
//            var ad = await _ufw.Ads.GetByIdAsync(dto.InsuranceAdId);

//            if (ad == null)
//                return new NotFoundError();

//            if (ad.Status != InsuranceAdStatus.Opened)
//                return new ConflictError(ErrorCodes.AdNotOpened);

//            var offer = _mapper.Map<AdOffer>(dto);
//            offer.Status = RequestStatus.Pending;
//            offer.SentDate = DateTimeOffset.UtcNow;
//            offer.CompanyId = _currentUser.AccountId! ?? 1;
//            offer.Ad = ad;

//            offer.AddDomainEvent(new AdOfferCreated(offer));
//            _ufw.InsuranceAdOffers.Create(offer);
//            await _ufw.SaveChangesAsync();

//            return Result<Empty>.Success(Empty.Default, SuccessCodes.OfferCreated);

//        }

//        public async Task<Result<Empty>> AcceptOfferAsync(long offerId)
//        {
//            var offer = await _ufw.InsuranceAdOffers.GetByIdAsync(offerId);

//            if (offer == null)
//                return new NotFoundError();

//            if (offer.Status != RequestStatus.Pending)
//                return new ConflictError(ErrorCodes.AdOfferNotPending);

//            offer.Status = RequestStatus.Accepted;

//            offer.AddDomainEvent(new AdOfferAccepted(offer));
//            await _ufw.SaveChangesAsync();

//            return Result<Empty>.Success(Empty.Default, SuccessCodes.AdOfferAccepted);

//        }

//        public async Task<Result<Empty>> RejectOfferAsync(long offerId)
//        {
//            var offer = await _ufw.InsuranceAdOffers.GetByIdAsync(offerId);

//            if (offer == null)
//                return new NotFoundError();

//            if (offer.Status != RequestStatus.Pending)
//                return new ConflictError(ErrorCodes.AdOfferNotPending);

//            offer.Status = RequestStatus.Rejected;

//            offer.AddDomainEvent(new AdOfferRejected(offer));
//            await _ufw.SaveChangesAsync();

//            return Result<Empty>.Success(Empty.Default, SuccessCodes.OfferRejected);

//        }

//        public async Task<Result<Empty>> CancelOfferAsync(long offerId)
//        {
//            var offer = await _ufw.InsuranceAdOffers.GetByIdAsync(offerId, [nameof(AdOffer.Ad)]);

//            if (offer == null)
//                return new NotFoundError();

//            if (offer.Status != RequestStatus.Pending)
//                return new ConflictError(ErrorCodes.AdOfferNotPending);

//            offer.Status = RequestStatus.Cancelled;

//            offer.AddDomainEvent(new AdOfferCancelled(offer));
//            await _ufw.SaveChangesAsync();

//            return Result<Empty>.Success(Empty.Default, SuccessCodes.OfferCanceled);

//        }

//        public async Task<Result<FacilityInsuranceAdOfferDto[]>> GetMyOffersByAdIdAsFacilityAsync(long adId)
//        {
//            var userId = _currentUser.AccountId ?? 1;

//            var offers = await _ufw.InsuranceAdOffers
//                .FilterAsync(o => o.InsuranceAdId == adId && o.Ad.FacilityId == userId, [$"{nameof(Company)}.{nameof(Company.LoginDetails)}"]);

//            var facilityInsuranceAdOfferDto = _mapper.Map<FacilityInsuranceAdOfferDto[]>(offers);
//            return Result<FacilityInsuranceAdOfferDto[]>.Success(facilityInsuranceAdOfferDto,
//                SuccessCodes.MyOffersByAdIdAsFacilityReceived);

//        }


//        public async Task<Result<FacilityInsuranceAdOfferDto[]>> GetMyOffersAsFacilityAsync()
//        {
//            var userId = _currentUser.AccountId ?? 1;

//            var offers = await _ufw.InsuranceAdOffers
//                .FilterAsync(o => o.Ad.FacilityId == userId, [$"{nameof(Company)}.{nameof(Company.LoginDetails)}"]);

//            var facilityInsuranceAdOfferDto = _mapper.Map<FacilityInsuranceAdOfferDto[]>(offers);
//            return Result<FacilityInsuranceAdOfferDto[]>.Success(facilityInsuranceAdOfferDto,
//                SuccessCodes.MyOffersAsFacilityReceived);
//        }

//        public async Task<Result<CompanyInsuranceAdOfferDto[]>> GetMyOffersByAdIdAsCompanyAsync(long adId)
//        {
//            var userId = _currentUser.AccountId ?? 1;
//            var offers = await _ufw.InsuranceAdOffers
//                .FilterAsync(o => o.InsuranceAdId == adId && o.CompanyId == userId, [$"{nameof(AdOffer.Ad)}.{nameof(Facility)}.{nameof(Facility.LoginDetails)}"]);

//            var companyInsuranceAdOfferDto = _mapper.Map<CompanyInsuranceAdOfferDto[]>(offers);
//            return Result<CompanyInsuranceAdOfferDto[]>.Success(companyInsuranceAdOfferDto,
//                SuccessCodes.MyOffersByAdIdAsCompanyReceived);
//        }

//        public async Task<Result<CompanyInsuranceAdOfferDto[]>> GetMyOffersAsCompanyAsync()
//        {
//            var userId = _currentUser.AccountId ?? 1;
//            var offers = await _ufw.InsuranceAdOffers
//                .FilterAsync(o => o.CompanyId == userId, [$"{nameof(AdOffer.Ad)}.{nameof(Facility)}.{nameof(Facility.LoginDetails)}"]);



//            var companyInsuranceAdOfferDto = _mapper.Map<CompanyInsuranceAdOfferDto[]>(offers);
//            return Result<CompanyInsuranceAdOfferDto[]>.Success(companyInsuranceAdOfferDto,
//                SuccessCodes.MyOffersAsCompanyReceived);
//        }

//        public async Task<Result<Empty>> CreateOfferMessageAsync(CreateInsuranceAdOfferMessageDto dto)
//        {
//            var offer = await _ufw.InsuranceAdOffers.GetByIdAsync(dto.InsuranceAdOfferId, []);

//            if (offer == null)
//                return new NotFoundError();

//            if (offer.Ad.Status != InsuranceAdStatus.Opened)
//                return new ConflictError(ErrorCodes.AdNotOpened);

//            if (offer.Status != RequestStatus.Pending)
//                return new ConflictError(ErrorCodes.AdOfferNotPending);

//            var message = _mapper.Map<InsuranceAdOfferMessage>(dto);
//            message.SentDate = DateTimeOffset.UtcNow;
//            message.SenderId = _currentUser.AccountId!.Value;
//            message.SenderType = _currentUser.Type!.Value;
//            message.AdOffer = offer;

//            message.AddDomainEvent(new AdOfferMessageCreated(message));
//            _ufw.InsuranceAdOfferMessages.Create(message);
//            await _ufw.SaveChangesAsync();

//            return Result<Empty>.Success(Empty.Default, SuccessCodes.CreateOfferMessage);
//        }

//        public async Task<Result<InsuranceAdOfferMessageDto[]>> GetOfferMessagesAsync(long offerId)
//        {
//            var messages = await _ufw.InsuranceAdOfferMessages
//                .FilterAsync(m => m.InsuranceAdOfferId == offerId);

//            var insuranceAdOfferMessageDto = _mapper.Map<InsuranceAdOfferMessageDto[]>(messages);
//            return Result<InsuranceAdOfferMessageDto[]>.Success(insuranceAdOfferMessageDto,
//                SuccessCodes.OfferMessagesReceived);

//        }
//    }
//}
