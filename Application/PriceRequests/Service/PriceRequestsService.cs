using Application.PriceRequests.Dtos;
using AutoMapper;
using Domain.Entities.PriceRequestAggregates;
using Domain.Events.PriceRequests;

namespace Application.PriceRequests.Service
{
    internal class PriceRequestsService: IPriceRequestsService
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
            priceRequest.FacilityId = _currentUser.Id!.Value;

            priceRequest.AddDomainEvent(new PriceRequestCreated(priceRequest));
            _ufw.PriceRequests.Create(priceRequest);
            await _ufw.SaveChangesAsync();

            return priceRequest.Id;
        }

        public async Task<Result<Empty>> CreateRequestFacilityDetailsAsync(CreatePriceRequestFacilityDetailsDto dto)
        {
            if (!await _ufw.PriceRequests.AnyAsync(pr => pr.Id == dto.PriceRequestId && pr.Status == RequestStatus.Pending))
                return new ConflictException(Resource.OnlyPendingRequests);

            var details = _mapper.Map<PriceRequestFacilityDetails>(dto);

            _ufw.PriceRequestFacilityDetails.Create(details);
            await _ufw.SaveChangesAsync();

            return Empty.Default;
        }

        public async Task<Result<Empty>> UpdateRequestFacilityDetailsAsync(long facilityDetailsId, UpdatePriceRequestFacilityDetailsDto dto)
        {
            var details = await _ufw.PriceRequestFacilityDetails
                .GetByIdAsync(facilityDetailsId);

            if (details == null)
                return new NotFoundException();

            if (!await _ufw.PriceRequests.AnyAsync(pr => pr.Id == details.PriceRequestId && pr.Status == RequestStatus.Pending))
                return new ConflictException(Resource.OnlyPendingRequests);

            _mapper.Map(dto, details);
            await _ufw.SaveChangesAsync();

            return Empty.Default;
        }

        public async Task<Result<PriceRequestFacilityDetailsDto>> GetRequestFacilityDetailsAsync(long priceRequestId)
        {
            var facilityDetails = await _ufw.PriceRequestFacilityDetails
                .FirstOrDefaultAsync(d => d.PriceRequestId == priceRequestId);

            return _mapper.Map<PriceRequestFacilityDetailsDto>(facilityDetails);
        }

        public async Task<Result<FacilityPriceRequestDto[]>> GetMyRequestsAsFacilityAsync()
        {
            var priceRequests = await _ufw.PriceRequests
                .FilterAsync(pr => pr.FacilityId == _currentUser.Id, ["Company.LoginDetails", "Offer"]);

            return _mapper.Map<FacilityPriceRequestDto[]>(priceRequests);
        } 
        
        public async Task<Result<CompanyPriceRequestDto[]>> GetMyRequestsAsCompanyAsync()
        {
            var priceRequests = await _ufw.PriceRequests
                .FilterAsync(pr => pr.CompanyId == _currentUser.Id, ["Facility.LoginDetails", "Offer"]);

            return _mapper.Map<CompanyPriceRequestDto[]>(priceRequests);
        }

        public async Task<Result<Empty>> AcceptRequestAsync(CreatePriceRequestOfferDto dto)
        {
            var request = await _ufw.PriceRequests.GetByIdAsync(dto.PriceRequestId);

            if (request == null)
                return new NotFoundException();

            if (request.Status != RequestStatus.Pending)
                return new ConflictException(Resource.OnlyPendingRequests);

            var offer = _mapper.Map<PriceRequestOffer>(dto);
            offer.RespondedDate = DateTimeOffset.UtcNow;

            // mark request accepted and create offer
            request.Status = RequestStatus.Accepted;
            _ufw.PriceRequestOffers.Create(offer);

            request.AddDomainEvent(new PriceRequestAccepted(request));
            await _ufw.SaveChangesAsync();

            return Empty.Default;
        }

        public async Task<Result<Empty>> RejectRequestAsync(long priceRequestId)
        {
            var request = await _ufw.PriceRequests.GetByIdAsync(priceRequestId);

            if (request == null)
                return new NotFoundException();

            if (request.Status != RequestStatus.Pending)
                return new ConflictException(Resource.OnlyPendingRequests);

            // mark request rejected
            request.Status = RequestStatus.Rejected;

            request.AddDomainEvent(new PriceRequestRejected(request));
            await _ufw.SaveChangesAsync();

            return Empty.Default;
        }

        public async Task<Result<Empty>> CreateRequestMessageAsync(CreatePriceRequestMessageDto dto)
        {
            var request = await _ufw.PriceRequests.GetByIdAsync(dto.PriceRequestId);

            if (request == null)
                return new NotFoundException();

            if(request.Status != RequestStatus.Pending)
                return new ConflictException(Resource.OnlyPendingRequests);

            var message = _mapper.Map<PriceRequestMessage>(dto);
            message.SentDate = DateTimeOffset.UtcNow;
            message.SenderId = _currentUser.Id!.Value;
            message.SenderType = _currentUser.Type!.Value;
            message.PriceRequest = request;

            message.AddDomainEvent(new PriceRequestMessageCreated(message));
            _ufw.PriceRequestMessages.Create(message);
            await _ufw.SaveChangesAsync();

            return Empty.Default;
        }

        public async Task<Result<PriceRequestMessageDto[]>> GetRequestMessagesAsync(long requestId)
        {
            var messages = await _ufw.PriceRequestMessages
                .FilterAsync(m=>m.PriceRequestId == requestId);

            return _mapper.Map<PriceRequestMessageDto[]>(messages);
        }
    }
}
