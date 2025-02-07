using Application.Chats.DTOs;
using AutoMapper;
using Domain.Entities.ChatAggregate;
using Domain.Events.Ads;
using Domain.Events.PriceRequests;

namespace Application.Chats.Service
{
    public class ChatService(IUnitOfWorkService ufw, ICurrentUserService currentUser, IMapper mapper) : IChatService
    {

        public async Task<Result<Empty>> SendPriceRequestMessageAsync(CreateChatMessageDto dto)
            => await SendChatMessageAsync(dto, (message, chat, audienceId, audienceType) =>
                new PriceRequestMessageCreated(message, chat.RelatedEntityId, audienceId, audienceType));

        public async Task<Result<Empty>> SendPriceOfferMessageAsync(CreateChatMessageDto dto)
            => await SendChatMessageAsync(dto, (message, chat, audienceId, audienceType) =>
                new PriceOfferMessageCreated(message, chat.RelatedEntityId, audienceId, audienceType));

        public async Task<Result<Empty>> SendAdOfferMessageAsync(CreateChatMessageDto dto)
            => await SendChatMessageAsync(dto, (message, chat, audienceId, audienceType) =>
                new AdOfferMessageCreated(message, chat.RelatedEntityId, audienceId, audienceType));

        private async Task<Result<Empty>> SendChatMessageAsync(CreateChatMessageDto dto,
            Func<Message, Chat, long, EntityTypes, DomainEvent> createDomainEvent)
        {
            var chat = await ufw.GetRepository<Chat>().GetByIdAsync(dto.ChatId);
            var validationResult = ValidateChat(chat);
            if (validationResult is not null)
                return validationResult;

            var message = CreateMessage(dto, chat.Id);

            chat.Messages.Add(message);

            var (audienceId, audienceType) = GetAudienceInfo(message.RepresentedEntity!.Value, chat);

            var domainEvent = createDomainEvent(message, chat, audienceId, audienceType);
            message.AddDomainEvent(domainEvent);

            await ufw.SaveChangesAsync();
            return Result<Empty>.Success(Empty.Default, SuccessCodes.CreateRequestMessage);
        }

        private static Result<Empty>? ValidateChat(Chat? chat)
        {
            if (chat is null)
                return new NotFoundError();

            if (chat.Status == ChatStatus.Closed)
                return new ConflictError(ErrorCodes.ChatClosed);

            return null;
        }

        private Message CreateMessage(CreateChatMessageDto dto, long chatId)
        {
            return new Message
            {
                SentOn = DateTimeOffset.UtcNow,
                SenderId = currentUser.UserId ?? 1,
                RepresentedEntity = currentUser.EntityType ?? null,
                Content = dto.Content,
                ChatId = chatId,
                Medias = dto.Medias != null ? mapper.Map<Media[]>(dto.Medias) : null
            };
        }



        public async Task<Result<long>> InitialzePriceRequestChatAsync(long priceRequestId)
        {
            var pr = await ufw.GetRepository<PriceRequest>().GetByIdAsync(priceRequestId);
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
            await ufw.SaveChangesAsync();
            return Result<long>.Success(pr.Chat.Id, SuccessCodes.ChatInitialized);
        }
        public async Task<Result<long>> InitialzePriceOfferChatAsync(long priceOfferId)
        {
            var po = await ufw.GetRepository<PriceOffer>().GetByIdAsync(priceOfferId, [nameof(PriceOffer.PriceRequest)]);

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
            await ufw.SaveChangesAsync();
            return Result<long>.Success(po.Chat.Id, SuccessCodes.ChatInitialized);
        }

        #region Ads Chat
        public async Task<Result<long>> InitialzeAdOfferChatAsync(long adOfferId)
        {
            var adOffer = await ufw.GetRepository<AdOffer>().GetByIdAsync(adOfferId, [nameof(AdOffer.Ad)]);

            if (adOffer is null)
                return new NotFoundError();

            if (adOffer.Status != RequestStatus.Pending)
                return new ConflictError(ErrorCodes.AdOfferNotPending);

            if (adOffer.ChatId.HasValue)
                return new ConflictError(ErrorCodes.ChatAlreadyExist);

            adOffer.Chat = new Chat
            {
                EntityAudienceId = adOffer.Ad.FacilityId,
                EntityIssuerId = adOffer.CompanyId,
                RelatedEntityId = adOffer.Id,
                RelatedEntityType = ChatEntityType.AdOffer
            };
            await ufw.SaveChangesAsync();
            return Result<long>.Success(adOffer.Chat.Id, SuccessCodes.ChatInitialized);
        }


        private Tuple<long, EntityTypes> GetAudienceInfo(EntityTypes messageSenderType, Chat chat)
        {
            if (messageSenderType == chat.IssuerType)
                return new(chat.EntityAudienceId, chat.AudienceType);
            else
                return new(chat.EntityIssuerId, chat.IssuerType);
        }
        #endregion

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

    }
}
