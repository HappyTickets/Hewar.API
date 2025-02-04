﻿using Application.Ads.Dtos;
using Application.Ads.Dtos.Offers;
using Application.Ads.Dtos.Post;
using AutoMapper;
using Domain.Entities.CompanyAggregate;
using Domain.Entities.FacilityAggregate;
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
            var ad = await ufw.GetRepository<Ad>().GetByIdAsync(dto.Id);

            if (ad is null)
                return new NotFoundError();

            mapper.Map(dto, ad);
            await ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.AdUpdated);

        }

        public async Task<Result<AdDto>> GetAdByIdAsync(long id)
        {
            var ad = await ufw.GetRepository<Ad>().GetByIdAsync(id, [nameof(Facility), nameof(Ad.Services)]);

            if (ad == null)
                return new NotFoundError();

            var AdDto = mapper.Map<AdDto>(ad);
            return Result<AdDto>.Success(AdDto, SuccessCodes.AdReceived);

        }

        public async Task<Result<AdDto[]>> GetMyAdsAsync()
        {
            var entityId = currentUser.EntityId ?? 1;

            var ads = await ufw.GetRepository<Ad>()
                .FilterAsync(ad => ad.FacilityId == entityId, [nameof(Facility), nameof(Ad.Services)]);

            var AdDto = mapper.Map<AdDto[]>(ads);
            return Result<AdDto[]>.Success(AdDto, SuccessCodes.MyAdReceived);

        }

        public async Task<Result<AdDto[]>> GetOpenedAdsAsync()
        {
            var ads = await ufw.GetRepository<Ad>()
                .FilterAsync(ad => ad.Status == AdStatus.Opened, [nameof(Facility), nameof(Ad.Services)]);

            var AdDto = mapper.Map<AdDto[]>(ads);
            return Result<AdDto[]>.Success(AdDto, SuccessCodes.OpenAdsReceived);

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
                .FilterAsync(o => o.AdId == adId && o.Ad.FacilityId == entityId, [nameof(Company), nameof(AdOffer.ServicesPrice)]);

            var facilityAdOfferDto = mapper.Map<FacilityAdOfferDto[]>(offers);
            return Result<FacilityAdOfferDto[]>.Success(facilityAdOfferDto,
                SuccessCodes.MyOffersByAdIdAsFacilityReceived);
        }


        public async Task<Result<FacilityAdOfferDto[]>> GetMyOffersAsFacilityAsync()
        {
            var entityId = currentUser.EntityId ?? 1;

            var offers = await ufw.GetRepository<AdOffer>()
                .FilterAsync(o => o.Ad.FacilityId == entityId, [nameof(Company), nameof(AdOffer.ServicesPrice)]);

            var facilityInsuranceAdOfferDto = mapper.Map<FacilityAdOfferDto[]>(offers);
            return Result<FacilityAdOfferDto[]>.Success(facilityInsuranceAdOfferDto,
                SuccessCodes.MyOffersAsFacilityReceived);
        }

        public async Task<Result<CompanyAdOfferDto[]>> GetMyOffersByAdIdAsCompanyAsync(long adId)
        {
            var entityId = currentUser.EntityId ?? 1;
            var offers = await ufw.GetRepository<AdOffer>()
                .FilterAsync(o => o.AdId == adId &&
                             o.CompanyId == entityId,
                             [nameof(AdOffer.Ad), $"{nameof(Ad)}.{nameof(Ad.Facility)}", nameof(AdOffer.ServicesPrice)]);

            var companyAdOfferDto = mapper.Map<CompanyAdOfferDto[]>(offers);
            return Result<CompanyAdOfferDto[]>.Success(companyAdOfferDto,
                SuccessCodes.MyOffersByAdIdAsCompanyReceived);
        }

        public async Task<Result<CompanyAdOfferDto[]>> GetMyOffersAsCompanyAsync()
        {
            var entityId = currentUser.EntityId ?? 1;
            var offers = await ufw.GetRepository<AdOffer>()
                .FilterAsync(o => o.CompanyId == entityId,
                [nameof(AdOffer.Ad), $"{nameof(Ad)}.{nameof(Ad.Facility)}", nameof(AdOffer.ServicesPrice)]);

            var companyAdOfferDto = mapper.Map<CompanyAdOfferDto[]>(offers);
            return Result<CompanyAdOfferDto[]>.Success(companyAdOfferDto,
                SuccessCodes.MyOffersAsCompanyReceived);
        }



        #endregion


    }
}
