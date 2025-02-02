using Application.Chats.DTOs;
using Application.PriceRequests.Dtos.Offers;
using Application.PriceRequests.Dtos.Requests;
using AutoMapper;
using Domain.Entities.ChatAggregate;
using Domain.Entities.CompanyAggregate;
using Domain.Entities.FacilityAggregate;
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

            priceRequest.RequestStatus = RequestStatus.Pending;
            priceRequest.FacilityId = _currentUser.EntityId ?? 1;

            priceRequest.AddDomainEvent(new PriceRequestCreated(priceRequest));
            await _ufw.GetRepository<PriceRequest>().CreateAsync(priceRequest);
            await _ufw.SaveChangesAsync();

            var id = priceRequest.Id;
            return Result<long>.Success(id, SuccessCodes.PriceRequestCreated);

        }

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

        public async Task<Result<FacilityPriceRequestDto[]>> GetMyRequestsAsFacilityAsync()
        {
            var entityId = _currentUser.EntityId ?? 1;

            var priceRequests = await _ufw.GetRepository<PriceRequest>()
                .FilterAsync(pr => pr.FacilityId == entityId, [$"{nameof(Company)}", $"{nameof(PriceRequest.Offer)}.{nameof(PriceRequest.Offer.Services)}"]);

            var facilityPriceRequestDto = _mapper.Map<FacilityPriceRequestDto[]>(priceRequests);
            return Result<FacilityPriceRequestDto[]>.Success(facilityPriceRequestDto,
                SuccessCodes.GetMyRequestsAsFacility);


        }

        public async Task<Result<CompanyPriceRequestDto[]>> GetMyRequestsAsCompanyAsync()
        {
            var entityId = _currentUser.EntityId ?? 1;

            var priceRequests = await _ufw.GetRepository<PriceRequest>()
                .FilterAsync(pr => pr.CompanyId == entityId, [$"{nameof(Facility)}",
                    $"{nameof(PriceRequest.Offer)}.{nameof(PriceRequest.Offer.Services)}"]);

            var companyPriceRequestDto = _mapper.Map<CompanyPriceRequestDto[]>(priceRequests);
            return Result<CompanyPriceRequestDto[]>.Success(companyPriceRequestDto,
                SuccessCodes.GetMyRequestsAsCompany);
        }

        public async Task<Result<Empty>> AcceptRequestAsync(CreatePriceOfferDto dto)
        {
            var request = await _ufw.GetRepository<PriceRequest>().GetByIdAsync(dto.PriceRequestId);

            if (request is null)
                return new NotFoundError();

            if (request.RequestStatus != RequestStatus.Pending)
                return new ConflictError(ErrorCodes.PriceRequestNotPending);

            var offer = _mapper.Map<PriceOffer>(dto);
            offer.OfferStatus = RequestStatus.Pending;

            request.RequestStatus = RequestStatus.Approved;
            request.Offer = offer;

            request.AddDomainEvent(new PriceRequestAccepted(request));
            await _ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.PriceRequestApproved);

        }

        public async Task<Result<Empty>> RejectRequestAsync(long priceRequestId)
        {
            var request = await _ufw.GetRepository<PriceRequest>().GetByIdAsync(priceRequestId);

            if (request is null)
                return new NotFoundError();

            if (request.RequestStatus != RequestStatus.Pending)
                return new ConflictError(ErrorCodes.PriceRequestNotPending);

            // mark request rejected
            request.RequestStatus = RequestStatus.Rejected;

            request.AddDomainEvent(new PriceRequestRejected(request));
            await _ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.PriceRequestRejected);
        }

        public async Task<Result<Empty>> CancelRequestAsync(long priceRequestId)
        {
            var request = await _ufw.GetRepository<PriceRequest>().GetByIdAsync(priceRequestId);

            if (request is null)
                return new NotFoundError();

            if (request.RequestStatus != RequestStatus.Pending)
                return new ConflictError(ErrorCodes.PriceRequestNotPending);

            // mark request rejected
            request.RequestStatus = RequestStatus.Cancelled;

            request.AddDomainEvent(new PriceRequestCancelled(request));
            await _ufw.SaveChangesAsync();


            return Result<Empty>.Success(Empty.Default, SuccessCodes.CancelRequest);

        }

        public async Task<Result<Empty>> CreateRequestMessageAsync(CreateChatMessageDto dto)
        {
            var chat = await _ufw.GetRepository<Chat>().GetByIdAsync(dto.ChatId);

            if (chat is null)
                return new NotFoundError();


            if (chat.Status == ChatStatus.Closed)
                return new ConflictError(ErrorCodes.ChatClosed);


            var message = new Message
            {
                SentOn = DateTimeOffset.UtcNow,
                SenderId = _currentUser.UserId ?? 1,
                RepresentedEntity = _currentUser.EntityType ?? null,
                Content = dto.Content,
                ChatId = chat.Id
            };
            if (dto.Medias != null)
            {
                var medias = _mapper.Map<Media[]>(dto.Medias);
                message.Medias = medias;
            }

            chat.Messages.Add(message);
            message.AddDomainEvent(new PriceRequestMessageCreated(message, chat.EntityAudienceId, chat.EntityIssuerId));
            await _ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.CreateRequestMessage);
        }

        public async Task<Result<ChatMessageDto[]>> GetChatMessagesAsync(long chatId)
        {
            // TO DO : Retrive the latest 10 Messages 
            var isChatExist = await _ufw.GetRepository<Chat>().AnyAsync(ch => ch.Id == chatId);
            if (!isChatExist)
                return new NotFoundError();

            var messages = await _ufw.GetRepository<Message>()
                .FilterAsync(msg => msg.ChatId == chatId, [nameof(Message.Sender)]);

            var messagesDto = _mapper.Map<ChatMessageDto[]>(messages);

            return Result<ChatMessageDto[]>.Success(messagesDto, SuccessCodes.GetRequestMessages);

        }

        public async Task<Result<long>> InitialzePriceRequestChatAsync(long priceRequestId)
        {
            var pr = await _ufw.GetRepository<PriceRequest>().GetByIdAsync(priceRequestId);
            if (pr is null)
                return new NotFoundError();

            if (pr.RequestStatus != RequestStatus.Pending)
                return new ConflictError(ErrorCodes.PriceRequestNotPending);

            if (pr.ChatId.HasValue)
                return new ConflictError(ErrorCodes.ChatAlreadyExist);

            pr.Chat = new Chat
            {
                EntityAudienceId = pr.CompanyId,
                EntityIssuerId = pr.FacilityId,
                RelatedEntityId = pr.Id,
                RelatedEntityType = ChatEntityType.PriceRequest
            };
            await _ufw.SaveChangesAsync();
            return Result<long>.Success(pr.Chat.Id, SuccessCodes.ChatInitialized);
        }

        public async Task<Result<long>> InitialzeOfferChatAsync(long offerId)
        {
            var po = await _ufw.GetRepository<PriceOffer>().GetByIdAsync(offerId, [nameof(PriceOffer.PriceRequest)]);

            if (po is null)
                return new NotFoundError();

            if (po.OfferStatus != RequestStatus.Pending)
                return new ConflictError(ErrorCodes.PriceOfferNotPending);

            if (po.ChatId.HasValue)
                return new ConflictError(ErrorCodes.ChatAlreadyExist);

            po.Chat = new Chat
            {
                EntityAudienceId = po.PriceRequest.FacilityId,
                EntityIssuerId = po.PriceRequest.CompanyId,
                RelatedEntityId = po.Id,
                RelatedEntityType = ChatEntityType.PriceOffer
            };
            await _ufw.SaveChangesAsync();
            return Result<long>.Success(po.Chat.Id, SuccessCodes.ChatInitialized);
        }

    }
}
