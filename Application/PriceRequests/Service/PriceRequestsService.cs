using Application.Chats.DTOs;
using Application.PriceRequests.Dtos;
using AutoMapper;
using Domain.Entities.ChatAggregate;
using Domain.Events.PriceRequests;

namespace Application.PriceRequests.Service
{
    internal class PriceRequestsService(IUnitOfWorkService ufw, IMapper mapper, ICurrentUserService currentUser) : IPriceRequestsService
    {



        #region Facility Details
        //public async Task<Result<Empty>> CreateRequestFacilityDetailsAsync(CreatePriceRequestFacilityDetailsDto dto)
        //{
        //    if (!await _ufw.PriceRequests.AnyAsync(pr => pr.Id == dto.PriceRequestId && pr.Status == RequestStatus.Pending))
        //        return new ConflictError(ErrorCodes.PriceRequestNotPending);

        //    var details = _mapper.Map<PriceRequestFacilityDetails>(dto);

        //    _ufw.PriceRequestFacilityDetails.Create(details);
        //    await _ufw.SaveChangesAsync();


        //    return Result<Empty>.Success(Empty.Default, SuccessCodes.CreateRequestFacilityDetails);

        //}

        //public async Task<Result<Empty>> UpdateRequestFacilityDetailsAsync(long facilityDetailsId, UpdatePriceRequestFacilityDetailsDto dto)
        //{
        //    var details = await _ufw.PriceRequestFacilityDetails
        //        .GetByIdAsync(facilityDetailsId);

        //    if (details == null)
        //        return new NotFoundError();

        //    if (!await _ufw.PriceRequests.AnyAsync(pr => pr.Id == details.PriceRequestId && pr.Status == RequestStatus.Pending))
        //        return new ConflictError(ErrorCodes.PriceRequestNotPending);

        //    _mapper.Map(dto, details);
        //    await _ufw.SaveChangesAsync();

        //    return Result<Empty>.Success(Empty.Default, SuccessCodes.UpdateRequestFacilityDetails);

        //}

        //public async Task<Result<PriceRequestFacilityDetailsDto>> GetRequestFacilityDetailsAsync(long priceRequestId)
        //{
        //    var facilityDetails = await _ufw.PriceRequestFacilityDetails
        //        .FirstOrDefaultAsync(d => d.PriceRequestId == priceRequestId);

        //    var priceRequestFacilityDetailsDto = _mapper.Map<PriceRequestFacilityDetailsDto>(facilityDetails);
        //    return Result<PriceRequestFacilityDetailsDto>.Success(priceRequestFacilityDetailsDto,
        //        SuccessCodes.GetRequestFacilityDetails);

        //} 
        #endregion

        public async Task<Result<FacilityPriceRequestDto[]>> GetMyFacilityRequestsAsync()
        {
            var entityId = currentUser.EntityId ?? 1;

            var priceRequests = await ufw.GetRepository<PriceRequest>()
                .FilterAsync<FacilityPriceRequestDto>(pr => pr.FacilityId == entityId);

            return Result<FacilityPriceRequestDto[]>.Success(priceRequests.ToArray(), SuccessCodes.GetMyRequestsAsFacility);


        }

        public async Task<Result<CompanyPriceRequestDto[]>> GetMyCompanyRequestsAsync()
        {
            var entityId = currentUser.EntityId ?? 1;

            var priceRequests = await ufw.GetRepository<PriceRequest>()
                .FilterAsync<CompanyPriceRequestDto>(pr => pr.CompanyId == entityId);

            return Result<CompanyPriceRequestDto[]>.Success(priceRequests.ToArray(),
                SuccessCodes.GetMyRequestsAsCompany);
        }



        public async Task<Result<Empty>> RejectRequestAsync(long priceRequestId)
        {
            var request = await ufw.GetRepository<PriceRequest>().GetByIdAsync(priceRequestId);

            if (request is null)
                return new NotFoundError();

            if (request.RequestStatus != RequestStatus.Pending)
                return new ConflictError(ErrorCodes.PriceRequestNotPending);

            // mark request rejected
            request.RequestStatus = RequestStatus.Rejected;

            request.AddDomainEvent(new PriceRequestRejected(request));
            await ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.PriceRequestRejected);
        }

        public async Task<Result<Empty>> CancelRequestAsync(long priceRequestId)
        {
            var request = await ufw.GetRepository<PriceRequest>().GetByIdAsync(priceRequestId);

            if (request is null)
                return new NotFoundError();

            if (request.RequestStatus != RequestStatus.Pending)
                return new ConflictError(ErrorCodes.PriceRequestNotPending);

            // mark request rejected
            request.RequestStatus = RequestStatus.Cancelled;

            request.AddDomainEvent(new PriceRequestCancelled(request));
            await ufw.SaveChangesAsync();


            return Result<Empty>.Success(Empty.Default, SuccessCodes.CancelRequest);

        }


        public async Task<Result<ChatMessageDto[]>> GetChatMessagesAsync(long chatId)
        {
            // TO DO : Retrive the latest 10 Messages 
            var isChatExist = await ufw.GetRepository<Chat>().AnyAsync(ch => ch.Id == chatId);
            if (!isChatExist)
                return new NotFoundError();

            var messages = await ufw.GetRepository<Message>()
                .FilterAsync(msg => msg.ChatId == chatId, [nameof(Message.Sender)]);

            var messagesDto = mapper.Map<ChatMessageDto[]>(messages);

            return Result<ChatMessageDto[]>.Success(messagesDto, SuccessCodes.GetRequestMessages);

        }




        public async Task<Result<long>> CreateRequestAsync(CreatePriceRequestDto dto)
        {
            var priceRequest = mapper.Map<PriceRequest>(dto);

            priceRequest.RequestStatus = RequestStatus.Pending;
            priceRequest.FacilityId = currentUser.EntityId ?? 1;

            priceRequest.AddDomainEvent(new PriceRequestCreated(priceRequest));
            await ufw.GetRepository<PriceRequest>().CreateAsync(priceRequest);
            await ufw.SaveChangesAsync();

            return Result<long>.Success(priceRequest.Id, SuccessCodes.PriceRequestCreated);

        }

        public async Task<Result<Empty>> UpdateRequestAsync(UpdatePriceRequestDto dto)
        {
            var request = await ufw.GetRepository<PriceRequest>()
                .GetByIdAsync(dto.PriceRequestId,
                [nameof(PriceOffer.Services), nameof(PriceOffer.OtherServices)]);

            if (request is null)
                return new NotFoundError(ErrorCodes.PriceRequestNotExists);

            if (request.RequestStatus != RequestStatus.Pending)
                return new ConflictError(ErrorCodes.PriceRequestNotPending);

            mapper.Map(dto, request);


            await ufw.SaveChangesAsync();
            return Result<Empty>.Success(Empty.Default, SuccessCodes.Updated);
        }

        public async Task<Result<GetPriceRequestDto>> GetByIdAsync(long priceRequestId)
        {
            var priceRequest = await ufw.GetRepository<PriceRequest>().FirstOrDefaultAsync<GetPriceRequestDto>(pr => pr.Id == priceRequestId);

            if (priceRequest is null)
                return new NotFoundError(ErrorCodes.PriceRequestNotExists);

            return Result<GetPriceRequestDto>.Success(priceRequest, SuccessCodes.OperationSuccessful);
        }
    }
}
