using Application.Ads.Dtos;
using Application.Ads.Events;
using AutoMapper;
using Domain.Entities.ChatAggregate;
using Domain.Entities.CompanyAggregate;
using Domain.Entities.FacilityAggregate;
using Domain.Events.Ads;
using Domain.Events.Tickets;

namespace Application.Ads.Service
{
    internal class AdsService : IAdsService
    {
        private readonly IUnitOfWorkService _ufw;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;

        public AdsService(IUnitOfWorkService ufw, IMapper mapper, ICurrentUserService currentUser)
        {
            _ufw = ufw;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<Result<Empty>> CreateAdAsync(CreateAdDto dto)
        {
            var ad = _mapper.Map<Ad>(dto);
            ad.FacilityId = _currentUser.UserId ?? 1;

            _ufw.GetRepository<Ad>().Create(ad);
            await _ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.AdCreated);

        }

        public async Task<Result<Empty>> UpdateAdAsync(UpdateAdDto dto)
        {
            var ad = await _ufw.GetRepository<Ad>().GetByIdAsync(dto.Id);

            if (ad == null)
                return new NotFoundError();

            _mapper.Map(dto, ad);
            await _ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.AdUpdated);

        }

        public async Task<Result<AdDto>> GetAdByIdAsync(long id)
        {
            var ad = await _ufw.GetRepository<Ad>().GetByIdAsync(id, [$"{nameof(Facility)}"]);

            if (ad == null)
                return new NotFoundError();

            var AdDto = _mapper.Map<AdDto>(ad);
            return Result<AdDto>.Success(AdDto, SuccessCodes.AdReceived);

        }

        public async Task<Result<AdDto[]>> GetMyAdsAsync()
        {
            var userId = _currentUser.UserId ?? 1;

            var ads = await _ufw.GetRepository<Ad>()
                .FilterAsync(ad => ad.FacilityId == userId, [$"{nameof(Facility)}"]);

            var AdDto = _mapper.Map<AdDto[]>(ads);
            return Result<AdDto[]>.Success(AdDto, SuccessCodes.MyAdReceived);

        }

        public async Task<Result<AdDto[]>> GetOpenedAdsAsync()
        {
            var ads = await _ufw.GetRepository<Ad>()
                .FilterAsync(ad => ad.Status == AdStatus.Opened, [$"{nameof(Facility)}"]);

            var AdDto = _mapper.Map<AdDto[]>(ads);
            return Result<AdDto[]>.Success(AdDto, SuccessCodes.OpenAdsReceived);

        }

        public async Task<Result<Empty>> CreateOfferAsync(CreateAdOfferDto dto)
        {
            var ad = await _ufw.GetRepository<Ad>().GetByIdAsync(dto.AdId);

            if (ad == null)
                return new NotFoundError();

            if (ad.Status != AdStatus.Closed)
                return new ConflictError(ErrorCodes.AdNotExists);

            var offer = _mapper.Map<AdOffer>(dto);
            offer.Status = RequestStatus.Pending;
            offer.SentDate = DateTimeOffset.UtcNow;
            offer.CompanyId = _currentUser.UserId! ?? 1;
            offer.AdId = dto.AdId;

            offer.AddDomainEvent(new AdOfferCreated(offer));
            ad.AdOffers.Add(offer);
            await _ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.AdOfferAccepted);

        }

        public async Task<Result<Empty>> AcceptOfferAsync(long offerId)
        {
            var offer = await _ufw.GetRepository<AdOffer>().GetByIdAsync(offerId);

            if (offer == null)
                return new NotFoundError();

            if (offer.Status != RequestStatus.Pending)
                return new ConflictError(ErrorCodes.AdOfferNotPending);

            offer.Status = RequestStatus.Approved;

            offer.AddDomainEvent(new AdOfferAccepted(offer));
            await _ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.AdOfferAccepted);

        }

        public async Task<Result<Empty>> RejectOfferAsync(long offerId)
        {
            var offer = await _ufw.GetRepository<AdOffer>().GetByIdAsync(offerId);

            if (offer == null)
                return new NotFoundError();

            if (offer.Status != RequestStatus.Pending)
                return new ConflictError(ErrorCodes.AdOfferNotPending);

            offer.Status = RequestStatus.Rejected;

            offer.AddDomainEvent(new AdOfferRejected(offer));
            await _ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.OfferRejected);

        }

        public async Task<Result<Empty>> CancelOfferAsync(long offerId)
        {
            var offer = await _ufw.GetRepository<AdOffer>().GetByIdAsync(offerId, [nameof(AdOffer.Ad)]);

            if (offer == null)
                return new NotFoundError();

            if (offer.Status != RequestStatus.Pending)
                return new ConflictError(ErrorCodes.AdOfferNotPending);

            offer.Status = RequestStatus.Cancelled;

            offer.AddDomainEvent(new AdOfferCancelled(offer));
            await _ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.OfferCanceled);

        }

        public async Task<Result<FacilityAdOfferDto[]>> GetMyOffersByAdIdAsFacilityAsync(long adId)
        {
            var userId = _currentUser.UserId ?? 1;

            var offers = await _ufw.GetRepository<AdOffer>()
                .FilterAsync(o => o.AdId == adId && o.Ad.FacilityId == userId, [$"{nameof(Company)}"]);

            var facilityAdOfferDto = _mapper.Map<FacilityAdOfferDto[]>(offers);
            return Result<FacilityAdOfferDto[]>.Success(facilityAdOfferDto,
                SuccessCodes.MyOffersByAdIdAsFacilityReceived);

        }


        public async Task<Result<FacilityAdOfferDto[]>> GetMyOffersAsFacilityAsync()
        {
            var userId = _currentUser.UserId ?? 1;

            var offers = await _ufw.GetRepository<AdOffer>()
                .FilterAsync(o => o.Ad.FacilityId == userId, [$"{nameof(Company)}"]);

            var facilityInsuranceAdOfferDto = _mapper.Map<FacilityAdOfferDto[]>(offers);
            return Result<FacilityAdOfferDto[]>.Success(facilityInsuranceAdOfferDto,
                SuccessCodes.MyOffersAsFacilityReceived);
        }

        public async Task<Result<CompanyAdOfferDto[]>> GetMyOffersByAdIdAsCompanyAsync(long adId)
        {
            var userId = _currentUser.UserId ?? 1;
            var offers = await _ufw.GetRepository<AdOffer>()
                .FilterAsync(o => o.AdId == adId && o.CompanyId == userId, [$"{nameof(AdOffer.Ad)}"]);

            var companyInsuranceAdOfferDto = _mapper.Map<CompanyAdOfferDto[]>(offers);
            return Result<CompanyAdOfferDto[]>.Success(companyInsuranceAdOfferDto,
                SuccessCodes.MyOffersByAdIdAsCompanyReceived);
        }

        public async Task<Result<CompanyAdOfferDto[]>> GetMyOffersAsCompanyAsync()
        {
            var userId = _currentUser.UserId ?? 1;
            var offers = await _ufw.GetRepository<AdOffer>()
                .FilterAsync(o => o.CompanyId == userId, [$"{nameof(AdOffer.Ad)}"]);



            var companyInsuranceAdOfferDto = _mapper.Map<CompanyAdOfferDto[]>(offers);
            return Result<CompanyAdOfferDto[]>.Success(companyInsuranceAdOfferDto,
                SuccessCodes.MyOffersAsCompanyReceived);
        }

        public async Task<Result<Empty>> CreateOfferMessageAsync(CreateAdOfferMessageDto dto)
        {
            var offer = await _ufw.GetRepository<AdOffer>().GetByIdAsync(dto.AdOfferId, []);

            if (offer == null)
                return new NotFoundError();

            if (offer.Ad.Status != AdStatus.Opened)
                return new ConflictError(ErrorCodes.AdNotOpened);

            if (offer.Status != RequestStatus.Pending)
                return new ConflictError(ErrorCodes.AdOfferNotPending);

            var message = _mapper.Map<Message>(dto);
            message.CreatedOn = DateTimeOffset.UtcNow;
            message.SenderId = _currentUser.UserId!.Value;

            offer.Chat.Messages.Add(message);
            //offer.AddDomainEvent(new TicketMessageCreated(message));
            await _ufw.SaveChangesAsync();

            return  Result<Empty>.Success(Empty.Default, SuccessCodes.CreateOfferMessage);
        }

        public async Task<Result<AdOfferMessageDto[]>> GetOfferMessagesAsync(long offerId)
        {
            var messages = await _ufw.GetRepository<AdOffer>()
                .FilterAsync(m => m.Id == offerId, [$"{nameof(AdOffer.Chat.Messages)}"]);

            var AdOfferMessageDto = _mapper.Map<AdOfferMessageDto[]>(messages);
            return Result<AdOfferMessageDto[]>.Success(AdOfferMessageDto,
                SuccessCodes.OfferMessagesReceived);

        }

        
    }
}
