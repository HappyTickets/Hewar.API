using Application.PriceRequests.Dtos;
using AutoMapper;

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

            _ufw.PriceRequests.Create(priceRequest);
            await _ufw.SaveChangesAsync();

            return priceRequest.Id;
        }

        public async Task<Result<Empty>> CreateFacilityDetailsAsync(CreatePriceRequestFacilityDetailsDto dto)
        {
            if (!await _ufw.PriceRequests.AnyAsync(pr => pr.Id == dto.PriceRequestId && pr.Status == RequestStatus.Pending))
                return new ConflictException(Resource.OnlyPendingRequests);

            var details = _mapper.Map<PriceRequestFacilityDetails>(dto);

            _ufw.PriceRequestFacilityDetails.Create(details);
            await _ufw.SaveChangesAsync();

            return Empty.Default;
        }

        public async Task<Result<Empty>> UpdateFacilityDetailsAsync(long facilityDetailsId, UpdatePriceRequestFacilityDetailsDto dto)
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

        public async Task<Result<PriceRequestFacilityDetailsDto>> GetFacilityDetailsAsync(long priceRequestId)
        {
            var facilityDetails = await _ufw.PriceRequestFacilityDetails
                .FirstOrDefaultAsync(d => d.PriceRequestId == priceRequestId);

            return _mapper.Map<PriceRequestFacilityDetailsDto>(facilityDetails);
        }

        public async Task<Result<FacilityPriceRequestDto[]>> GetRequestForFacilityAsync()
        {
            var priceRequests = await _ufw.PriceRequests
                .FilterAsync(pr => pr.FacilityId == _currentUser.Id, ["Company.LoginDetails", "Response"]);

            return _mapper.Map<FacilityPriceRequestDto[]>(priceRequests);
        } 
        
        public async Task<Result<CompanyPriceRequestDto[]>> GetRequestForCompanyAsync()
        {
            var priceRequests = await _ufw.PriceRequests
                .FilterAsync(pr => pr.CompanyId == _currentUser.Id, ["Facility.LoginDetails", "Response"]);

            return _mapper.Map<CompanyPriceRequestDto[]>(priceRequests);
        }

        public async Task<Result<Empty>> AcceptRequestAsync(CreatePriceRequestResponseDto dto)
        {
            var request = await _ufw.PriceRequests.GetByIdAsync(dto.PriceRequestId);

            if (request == null)
                return new NotFoundException();

            if (request.Status != RequestStatus.Pending)
                return new ConflictException(Resource.OnlyPendingRequests);

            var response = _mapper.Map<PriceRequestResponse>(dto);
            response.RespondedDate = DateTimeOffset.UtcNow;

            // mark request accepted and create response
            request.Status = RequestStatus.Accepted;
            _ufw.PriceRequestResponses.Create(response);
           
            // mark request tickets as closed
            await MarkAllRequestTicketsAsClosedAsync(request.Id);
            
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

            // mark request tickets as closed
            await MarkAllRequestTicketsAsClosedAsync(request.Id);

            await _ufw.SaveChangesAsync();

            return Empty.Default;
        }

        private async Task MarkAllRequestTicketsAsClosedAsync(long priceRequestId)
        {
            var tickets = await _ufw.Tickets
                .FilterAsync(t => t.PriceRequestId == priceRequestId && t.Status == TicketStatus.Opened);

            foreach(var ticket in tickets)
            {
                ticket.Status = TicketStatus.Closed;
                ticket.ClosedDate = DateTimeOffset.UtcNow;
            }
        }
    }
}
