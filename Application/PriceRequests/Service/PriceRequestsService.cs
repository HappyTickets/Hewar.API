using Application.PriceRequests.Dtos;
using AutoMapper;
using Domain.Events.PriceRequests;

namespace Application.PriceRequests.Service
{
    internal class PriceRequestsService : IPriceRequestsService
    {
        private readonly IUnitOfWorkService _ufw;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;

        public PriceRequestsService(IUnitOfWorkService ufw, IMapper mapper, ICurrentUserService currentUser)
        {
            _ufw = ufw;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<Result<long>> CreateRequestAsync(CreatePriceRequestDto dto)
        {
            var priceRequest = _mapper.Map<PriceRequest>(dto);

            priceRequest.Status = RequestStatus.Pending;
            priceRequest.FacilityId = _currentUser.AccountId ?? 1;

            priceRequest.AddDomainEvent(new PriceRequestCreated(priceRequest));
            _ufw.PriceRequests.Create(priceRequest);
            await _ufw.SaveChangesAsync();

            var id = priceRequest.Id;
            return Result<long>.Success(id, SuccessCodes.PriceRequestCreated);

        }

        public async Task<Result<Empty>> CreateRequestFacilityDetailsAsync(CreatePriceRequestFacilityDetailsDto dto)
        {
            if (!await _ufw.PriceRequests.AnyAsync(pr => pr.Id == dto.PriceRequestId && pr.Status == RequestStatus.Pending))
                return new ConflictError(ErrorCodes.PriceRequestNotPending);

            var details = _mapper.Map<PriceRequestFacilityDetails>(dto);

            _ufw.PriceRequestFacilityDetails.Create(details);
            await _ufw.SaveChangesAsync();


            return Result<Empty>.Success(Empty.Default, SuccessCodes.CreateRequestFacilityDetails);

        }

        public async Task<Result<Empty>> UpdateRequestFacilityDetailsAsync(long facilityDetailsId, UpdatePriceRequestFacilityDetailsDto dto)
        {
            var details = await _ufw.PriceRequestFacilityDetails
                .GetByIdAsync(facilityDetailsId);

            if (details == null)
                return new NotFoundError();

            if (!await _ufw.PriceRequests.AnyAsync(pr => pr.Id == details.PriceRequestId && pr.Status == RequestStatus.Pending))
                return new ConflictError(ErrorCodes.PriceRequestNotPending);

            _mapper.Map(dto, details);
            await _ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.UpdateRequestFacilityDetails);

        }

        public async Task<Result<PriceRequestFacilityDetailsDto>> GetRequestFacilityDetailsAsync(long priceRequestId)
        {
            var facilityDetails = await _ufw.PriceRequestFacilityDetails
                .FirstOrDefaultAsync(d => d.PriceRequestId == priceRequestId);

            var priceRequestFacilityDetailsDto = _mapper.Map<PriceRequestFacilityDetailsDto>(facilityDetails);
            return Result<PriceRequestFacilityDetailsDto>.Success(priceRequestFacilityDetailsDto,
                SuccessCodes.GetRequestFacilityDetails);

        }

        public async Task<Result<FacilityPriceRequestDto[]>> GetMyRequestsAsFacilityAsync()
        {
            var accId = _currentUser.AccountId ?? 1;
            var priceRequests = await _ufw.PriceRequests
                .FilterAsync(pr => pr.FacilityId == accId, ["Company.LoginDetails", "Offer"]);

            var facilityPriceRequestDto = _mapper.Map<FacilityPriceRequestDto[]>(priceRequests);
            return Result<FacilityPriceRequestDto[]>.Success(facilityPriceRequestDto,
                SuccessCodes.GetMyRequestsAsFacility);


        }

        public async Task<Result<CompanyPriceRequestDto[]>> GetMyRequestsAsCompanyAsync()
        {
            var accId = _currentUser.AccountId ?? 1;

            var priceRequests = await _ufw.PriceRequests
                .FilterAsync(pr => pr.CompanyId == accId, ["Facility.LoginDetails", "Offer"]);

            var companyPriceRequestDto = _mapper.Map<CompanyPriceRequestDto[]>(priceRequests);
            return Result<CompanyPriceRequestDto[]>.Success(companyPriceRequestDto,
                SuccessCodes.GetMyRequestsAsCompany);
        }

        public async Task<Result<Empty>> AcceptRequestAsync(CreatePriceRequestOfferDto dto)
        {
            var request = await _ufw.PriceRequests.GetByIdAsync(dto.PriceRequestId);

            if (request == null)
                return new NotFoundError();

            if (request.Status != RequestStatus.Pending)
                return new ConflictError(ErrorCodes.PriceRequestNotPending);

            var offer = _mapper.Map<PriceRequestOffer>(dto);
            offer.RespondedDate = DateTimeOffset.UtcNow;

            // mark request accepted and create offer
            request.Status = RequestStatus.Accepted;
            _ufw.PriceRequestOffers.Create(offer);

            request.AddDomainEvent(new PriceRequestAccepted(request));
            await _ufw.SaveChangesAsync();


            return Result<Empty>.Success(Empty.Default, SuccessCodes.PriceRequestApproved);

        }

        public async Task<Result<Empty>> RejectRequestAsync(long priceRequestId)
        {
            var request = await _ufw.PriceRequests.GetByIdAsync(priceRequestId);

            if (request == null)
                return new NotFoundError();

            if (request.Status != RequestStatus.Pending)
                return new ConflictError(ErrorCodes.PriceRequestNotPending);

            // mark request rejected
            request.Status = RequestStatus.Rejected;

            request.AddDomainEvent(new PriceRequestRejected(request));
            await _ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.PriceRequestRejected);
        }

        public async Task<Result<Empty>> CancelRequestAsync(long priceRequestId)
        {
            var request = await _ufw.PriceRequests.GetByIdAsync(priceRequestId);

            if (request == null)
                return new NotFoundError();

            if (request.Status != RequestStatus.Pending)
                return new ConflictError(ErrorCodes.PriceRequestNotPending);

            // mark request rejected
            request.Status = RequestStatus.Cancelled;

            request.AddDomainEvent(new PriceRequestCancelled(request));
            await _ufw.SaveChangesAsync();


            return Result<Empty>.Success(Empty.Default, SuccessCodes.CancelRequest);

        }

        public async Task<Result<Empty>> CreateRequestMessageAsync(CreatePriceRequestMessageDto dto)
        {
            var request = await _ufw.PriceRequests.GetByIdAsync(dto.PriceRequestId);

            if (request == null)
                return new NotFoundError();

            if (request.Status != RequestStatus.Pending)
                return new ConflictError(ErrorCodes.PriceRequestNotPending);

            var message = _mapper.Map<PriceRequestMessage>(dto);
            message.SentDate = DateTimeOffset.UtcNow;
            message.SenderId = _currentUser.AccountId ?? 1;
            message.SenderType = _currentUser.Type ?? AccountTypes.Facility;
            message.PriceRequest = request;

            message.AddDomainEvent(new PriceRequestMessageCreated(message));
            _ufw.PriceRequestMessages.Create(message);
            await _ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.CreateRequestMessage);
        }

        public async Task<Result<PriceRequestMessageDto[]>> GetRequestMessagesAsync(long requestId)
        {
            var messages = await _ufw.PriceRequestMessages
                .FilterAsync(m => m.PriceRequestId == requestId);

            var priceRequestMessageDto = _mapper.Map<PriceRequestMessageDto[]>(messages);
            return Result<PriceRequestMessageDto[]>.Success(priceRequestMessageDto,
                SuccessCodes.GetRequestMessages);

        }
    }
}
