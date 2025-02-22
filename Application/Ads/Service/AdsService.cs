using Application.Ads.Dtos;
using Application.Ads.Dtos.Offers;
using Application.Ads.Dtos.Post;
using AutoMapper;
using Domain.Events.Ads;

namespace Application.Ads.Service
{
    internal class AdsService(IUnitOfWorkService ufw, IMapper mapper, ICurrentUserService currentUser) : IAdsService
    {
        public async Task<Result<Empty>> CreateAdAsync(CreateAdDto dto)
        {
            var ad = mapper.Map<Ad>(dto);
            ad.FacilityId = currentUser.EntityId ?? 1;
            ad.Status = AdStatus.Opened;
            ad.DatePosted = DateTimeOffset.UtcNow;

            ufw.GetRepository<Ad>().Create(ad);
            await ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.AdCreated);

        }

        public async Task<Result<Empty>> UpdateAdAsync(UpdateAdDto dto)
        {
            var ad = await ufw.GetRepository<Ad>().GetByIdAsync(dto.Id,
                [nameof(Ad.Services), nameof(Ad.OtherServices)]);

            if (ad is null)
                return new NotFoundError();

            mapper.Map(dto, ad);

            await ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.AdUpdated);
        }

        public async Task<Result<Empty>> ChangeAdStatusAsync(long id, AdStatus status)
        {
            var ad = await ufw.GetRepository<Ad>().GetByIdAsync(id);

            if (ad is null)
                return new NotFoundError();
            ad.Status = status;
            await ufw.SaveChangesAsync();
            return Result<Empty>.Success(Empty.Default, SuccessCodes.AdUpdated);

        }

        public async Task<Result<AdDto>> GetAdByIdAsync(long id)
        {
            var ad = await ufw.GetRepository<Ad>().FirstOrDefaultAsync<AdDto>(a => a.Id == id);

            if (ad is null)
                return new NotFoundError();

            return Result<AdDto>.Success(ad, SuccessCodes.AdReceived);

        }

        public async Task<Result<AdDto[]>> GetMyAdsAsync()
        {
            var entityId = currentUser.EntityId ?? 1;

            var ads = await ufw.GetRepository<Ad>().FilterAsync<AdDto>(ad => ad.FacilityId == entityId);

            return Result<AdDto[]>.Success(ads.ToArray(), SuccessCodes.MyAdReceived);

        }

        public async Task<Result<AdDto[]>> GetOpenedAdsAsync()
        {
            var ads = await ufw.GetRepository<Ad>()
                .FilterAsync<AdDto>(ad => ad.Status == AdStatus.Opened);

            return Result<AdDto[]>.Success(ads.ToArray(), SuccessCodes.OpenAdsReceived);

        }

        #region  Offers Operations
        public async Task<Result<Empty>> CreateOfferAsync(CreateAdOfferDto dto)
        {
            var ad = await ufw.GetRepository<Ad>().GetByIdAsync(dto.AdId);

            if (ad is null)
                return new NotFoundError(ErrorCodes.AdNotExists);

            if (ad.Status == AdStatus.Closed)
                return new ConflictError(ErrorCodes.AdClosed);

            var offer = mapper.Map<AdOffer>(dto);
            offer.Status = RequestStatus.Pending;
            offer.SentDate = DateTimeOffset.UtcNow;
            offer.CompanyId = currentUser.EntityId ?? 1;
            offer.AdId = dto.AdId;

            offer.AddDomainEvent(new AdOfferCreated(offer));
            await ufw.GetRepository<AdOffer>().CreateAsync(offer);
            await ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.AdOfferAccepted);

        }
        public async Task<Result<Empty>> UpdateAdOfferAsync(UpdateAdOfferDto dto)
        {
            var offer = await ufw.GetRepository<AdOffer>().GetByIdAsync(dto.Id,
                [nameof(AdOffer.ServicesCost), nameof(AdOffer.OtherServicesCost), nameof(AdOffer.CompanyServicesCost)]);

            if (offer is null)
                return new NotFoundError();

            if (offer.Status != RequestStatus.Pending)
                return new ConflictError(ErrorCodes.AdOfferNotPending);

            mapper.Map(dto, offer);

            await ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.AdOfferUpdated);
        }
        public async Task<Result<Empty>> AcceptOfferAsync(long offerId)
        {
            var offer = await ufw.GetRepository<AdOffer>().GetByIdAsync(offerId, [nameof(AdOffer.Ad)]);

            if (offer is null)
                return new NotFoundError();

            if (offer.Status != RequestStatus.Pending)
                return new ConflictError(ErrorCodes.AdOfferNotPending);

            offer.Status = RequestStatus.Approved;
            offer.Ad.Status = AdStatus.Closed;

            offer.AddDomainEvent(new AdOfferAccepted(offer));
            await ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.AdOfferAccepted);

        }

        public async Task<Result<Empty>> RejectOfferAsync(long offerId)
        {
            var offer = await ufw.GetRepository<AdOffer>().GetByIdAsync(offerId);

            if (offer == null)
                return new NotFoundError();

            if (offer.Status != RequestStatus.Pending)
                return new ConflictError(ErrorCodes.AdOfferNotPending);

            offer.Status = RequestStatus.Rejected;

            offer.AddDomainEvent(new AdOfferRejected(offer));
            await ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.OfferRejected);

        }

        public async Task<Result<Empty>> CancelOfferAsync(long offerId)
        {
            var offer = await ufw.GetRepository<AdOffer>().GetByIdAsync(offerId,
                [nameof(AdOffer.Ad)]);

            if (offer is null)
                return new NotFoundError();

            if (offer.Status != RequestStatus.Pending)
                return new ConflictError(ErrorCodes.AdOfferNotPending);

            offer.Status = RequestStatus.Cancelled;

            offer.AddDomainEvent(new AdOfferCancelled(offer));
            await ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.OfferCanceled);

        }

        public async Task<Result<FacilityAdOfferDto[]>> GetMyOffersByAdIdAsFacilityAsync(long adId)
        {
            var entityId = currentUser.EntityId ?? 1;

            var offers = await ufw.GetRepository<AdOffer>()
                .FilterAsync<FacilityAdOfferDto>(o => o.AdId == adId && o.Ad.FacilityId == entityId);

            return Result<FacilityAdOfferDto[]>.Success(offers.ToArray(), SuccessCodes.MyOffersByAdIdAsFacilityReceived);
        }


        public async Task<Result<FacilityAdOfferDto[]>> GetMyOffersAsFacilityAsync()
        {
            var entityId = currentUser.EntityId ?? 1;

            var offers = await ufw.GetRepository<AdOffer>()
                .FilterAsync<FacilityAdOfferDto>(o => o.Ad.FacilityId == entityId);

            return Result<FacilityAdOfferDto[]>.Success(offers.ToArray(),
                SuccessCodes.MyOffersAsFacilityReceived);
        }

        public async Task<Result<CompanyAdOfferDto[]>> GetMyOffersByAdIdAsCompanyAsync(long adId)
        {
            var entityId = currentUser.EntityId;
            var offers = await ufw.GetRepository<AdOffer>()
                .FilterAsync<CompanyAdOfferDto>(o => o.AdId == adId && o.CompanyId == entityId);

            return Result<CompanyAdOfferDto[]>.Success(offers.ToArray(), SuccessCodes.MyOffersByAdIdAsCompanyReceived);
        }

        public async Task<Result<CompanyAdOfferDto[]>> GetMyOffersAsCompanyAsync()
        {
            var entityId = currentUser.EntityId ?? 1;
            var offers = await ufw.GetRepository<AdOffer>()
                .FilterAsync<CompanyAdOfferDto>(o => o.CompanyId == entityId);

            return Result<CompanyAdOfferDto[]>.Success(offers.ToArray(), SuccessCodes.MyOffersAsCompanyReceived);
        }

        public async Task<Result<Empty>> DeleteAdAsync(long id)
        {
            var ad = await ufw.GetRepository<Ad>().GetByIdAsync(id);

            if (ad is null)
                return new NotFoundError();

            ufw.GetRepository<Ad>().HardDelete(ad);
            await ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.Deleted);
        }
        public async Task<Result<Empty>> HideOfferAsync(long adOfferId)
        {
            var currentEntityType = currentUser.EntityType;

            var adOffer = await ufw.GetRepository<AdOffer>()
                .FirstOrDefaultAsync(ao => ao.Id == adOfferId);

            if (adOffer is null)
                return new NotFoundError(ErrorCodes.PriceOfferNotExists);

            if (currentEntityType == EntityTypes.Facility)
                adOffer.IsFacilityHidden = true;
            else if (currentEntityType == EntityTypes.Company)
                adOffer.IsCompanyHidden = true;

            await ufw.SaveChangesAsync();
            return Result<Empty>.Success(Empty.Default, SuccessCodes.AdOfferHidden);
        }

        public async Task<Result<Empty>> ShowOfferAsync(long adOfferId)
        {
            var currentEntityType = currentUser.EntityType;

            var adOffer = await ufw.GetRepository<AdOffer>()
                .FirstOrDefaultAsync(ao => ao.Id == adOfferId, ignoreQueryFilters: true);

            if (adOffer is null)
                return new NotFoundError(ErrorCodes.PriceOfferNotExists);

            if (currentEntityType == EntityTypes.Facility)
                adOffer.IsFacilityHidden = false;

            else if (currentEntityType == EntityTypes.Company)
                adOffer.IsCompanyHidden = false;

            await ufw.SaveChangesAsync();
            return Result<Empty>.Success(Empty.Default, SuccessCodes.AdOfferShown);
        }




        #endregion
    }
}
