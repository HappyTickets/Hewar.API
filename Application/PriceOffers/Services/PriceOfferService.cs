using Application.PriceOffers.Dtos;
using AutoMapper;
using Domain.Events.PriceRequests;

namespace Application.PriceOffers.Services
{
    public class PriceOfferService(IUnitOfWorkService ufw, IMapper mapper, ICurrentUserService currentUser) : IPriceOfferService
    {

        public async Task<Result<long>> CreateOfferAsync(CreatePriceOfferDto dto)
        {
            var request = await ufw.GetRepository<PriceRequest>().GetByIdAsync(dto.PriceRequestId, [nameof(PriceRequest.Offers)]);

            if (request is null)
                return new NotFoundError();

            if (request.RequestStatus != RequestStatus.Approved && request.RequestStatus != RequestStatus.Pending)
                return new ConflictError(ErrorCodes.PriceRequestNotPending);

            var offer = mapper.Map<PriceOffer>(dto);
            offer.OfferStatus = RequestStatus.Pending;

            request.RequestStatus = RequestStatus.Approved;

            request.Offers ??= new List<PriceOffer>();

            request.Offers.Add(offer);

            request.AddDomainEvent(new PriceRequestAccepted(request));
            await ufw.SaveChangesAsync();

            return Result<long>.Success(offer.Id, SuccessCodes.PriceRequestApproved);
        }


        public async Task<Result<Empty>> UpdateOfferAsync(UpdatePriceOfferDto dto)
        {
            // Retrieve the existing offer
            var offer = await ufw.GetRepository<PriceOffer>()
                .GetByIdAsync(dto.PriceOfferId,
                [nameof(PriceOffer.Services), nameof(PriceOffer.OtherServices)]);

            if (offer is null)
                return new NotFoundError();

            if (offer.OfferStatus != RequestStatus.Pending)
                return new ConflictError(ErrorCodes.PriceOfferNotPending);

            mapper.Map(dto, offer);


            await ufw.SaveChangesAsync();
            return Result<Empty>.Success(Empty.Default, SuccessCodes.Updated);
        }

        public async Task<Result<Empty>> CancelOfferAsync(long offerId)
        {
            var offer = await ufw.GetRepository<PriceOffer>().GetByIdAsync(offerId);
            if (offer is null)
                return new NotFoundError();

            if (offer.OfferStatus != RequestStatus.Pending)
                return new ConflictError(ErrorCodes.PriceOfferNotPending);

            offer.OfferStatus = RequestStatus.Cancelled;

            await ufw.SaveChangesAsync();
            return Result<Empty>.Success(Empty.Default, SuccessCodes.OfferCanceled);
        }

        public async Task<Result<Empty>> RejectOfferAsync(long offerId)
        {
            var offer = await ufw.GetRepository<PriceOffer>().GetByIdAsync(offerId);
            if (offer is null)
                return new NotFoundError();

            if (offer.OfferStatus != RequestStatus.Pending)
                return new ConflictError(ErrorCodes.PriceOfferNotPending);

            offer.OfferStatus = RequestStatus.Rejected;

            await ufw.SaveChangesAsync();
            return Result<Empty>.Success(Empty.Default, SuccessCodes.OfferRejected);
        }

        public async Task<Result<Empty>> AcceptOfferAsync(long offerId)
        {
            var offer = await ufw.GetRepository<PriceOffer>().GetByIdAsync(offerId,
                [nameof(PriceOffer.PriceRequest)]);

            if (offer is null)
                return new NotFoundError();

            if (offer.OfferStatus != RequestStatus.Pending)
                return new ConflictError(ErrorCodes.PriceOfferNotPending);

            offer.OfferStatus = RequestStatus.Approved;
            offer.PriceRequest.RequestStatus = RequestStatus.Completed; // no offers again

            await ufw.SaveChangesAsync();
            return Result<Empty>.Success(Empty.Default, SuccessCodes.OfferAccepted);
        }


        public async Task<Result<GetPriceOfferDetailedDto[]>> GetMyCompanyOffersAsync()
        {
            var companyId = currentUser.EntityId;
            var offers = await ufw.GetRepository<PriceOffer>()
                .FilterAsync(o => o.PriceRequest.CompanyId == companyId,
                [nameof(PriceOffer.PriceRequest),
                 nameof(PriceOffer.Services), nameof(PriceOffer.OtherServices)]);

            return MapAndReturnSuccess(offers);
        }


        public async Task<Result<GetOffersForRequest>> GetMyCompanyOffersByRequestIdAsync(long requestId)
        {
            var companyId = currentUser.EntityId;

            var priceRequest = await ufw.GetRepository<PriceRequest>()
                .FirstOrDefaultAsync(
                    o => o.Id == requestId && o.CompanyId == companyId,
                    [
                nameof(PriceRequest.Offers),
                $"{nameof(PriceRequest.Offers)}.{nameof(PriceOffer.Services)}",
                $"{nameof(PriceRequest.Offers)}.{nameof(PriceOffer.OtherServices)}"
                    ]);

            var dto = mapper.Map<GetOffersForRequest>(priceRequest);

            return Result<GetOffersForRequest>.Success(dto, SuccessCodes.OperationSuccessful);
        }

        public async Task<Result<GetPriceOfferDetailedDto[]>> GetMyFacilityOffersAsync()
        {
            var facilityId = currentUser.EntityId;
            var offers = await ufw.GetRepository<PriceOffer>()
                .FilterAsync(o => o.PriceRequest.FacilityId == facilityId,
              [nameof(PriceOffer.PriceRequest),
                 nameof(PriceOffer.Services), nameof(PriceOffer.OtherServices)]);
            return MapAndReturnSuccess(offers);
        }

        public async Task<Result<GetOffersForRequest>> GetMyFacilityOffersByRequestIdAsync(long requestId)
        {
            var facilityId = currentUser.EntityId;

            var priceRequest = await ufw.GetRepository<PriceRequest>()
                .FirstOrDefaultAsync(
                    o => o.Id == requestId && o.FacilityId == facilityId,
                    [
                nameof(PriceRequest.Offers),
                $"{nameof(PriceRequest.Offers)}.{nameof(PriceOffer.Services)}",
                $"{nameof(PriceRequest.Offers)}.{nameof(PriceOffer.OtherServices)}"
                    ]);

            var dto = mapper.Map<GetOffersForRequest>(priceRequest);

            return Result<GetOffersForRequest>.Success(dto, SuccessCodes.OperationSuccessful);
        }

        private Result<GetPriceOfferDetailedDto[]> MapAndReturnSuccess(IEnumerable<PriceOffer> offers)
        {
            var dtoOffers = mapper.Map<GetPriceOfferDetailedDto[]>(offers);
            return Result<GetPriceOfferDetailedDto[]>.Success(dtoOffers, SuccessCodes.OperationSuccessful);
        }


    }
}
