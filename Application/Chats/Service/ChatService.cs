using Application.Chats.DTOs;
using AutoMapper;
using Domain.Entities.ChatAggregate;
using Domain.Events.Ads;
using Domain.Events.PriceRequests;

namespace Application.Chats.Service
{
    public class ChatService(IUnitOfWorkService ufw, ICurrentUserService currentUser, IMapper mapper) : IChatService
    {
        #region Price Requests/Offers
        private async Task<Result<Empty>> SendPriceRequestOrOfferMessageAsync(CreateChatMessageDto dto)
        {
            var chat = await ufw.GetRepository<Chat>().GetByIdAsync(dto.ChatId);

            if (chat is null)
                return new NotFoundError();


            if (chat.Status == ChatStatus.Closed)
                return new ConflictError(ErrorCodes.ChatClosed);


            var message = new Message
            {
                SentOn = DateTimeOffset.UtcNow,
                SenderId = currentUser.UserId ?? 1,
                RepresentedEntity = currentUser.EntityType ?? null,
                Content = dto.Content,
                ChatId = chat.Id
            };
            if (dto.Medias != null)
            {
                var medias = mapper.Map<Media[]>(dto.Medias);
                message.Medias = medias;
            }

            chat.Messages.Add(message);
            message.AddDomainEvent(new PriceRequestMessageCreated(message, chat.EntityAudienceId, chat.EntityIssuerId));
            await ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.CreateRequestMessage);
        }



        public async Task<Result<Empty>> SendPriceRequestMessageAsync(CreateChatMessageDto dto)
            => await SendPriceRequestOrOfferMessageAsync(dto);
        public async Task<Result<Empty>> SendPriceOfferMessageAsync(CreateChatMessageDto dto)
          => await SendPriceRequestOrOfferMessageAsync(dto);
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
        #endregion

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

        public async Task<Result<Empty>> SendAdOfferMessageAsync(CreateChatMessageDto dto)
        {
            var chat = await ufw.GetRepository<Chat>().GetByIdAsync(dto.ChatId);
            if (chat is null)
                return new NotFoundError();

            if (chat.Status == ChatStatus.Closed)
                return new ConflictError(ErrorCodes.ChatClosed);

            var message = new Message
            {
                SentOn = DateTimeOffset.UtcNow,
                SenderId = currentUser.UserId ?? 1,
                RepresentedEntity = currentUser.EntityType ?? null,
                Content = dto.Content,
                ChatId = chat.Id
            };

            if (dto.Medias != null)
            {
                var medias = mapper.Map<Media[]>(dto.Medias);
                message.Medias = medias;
            }
            chat.Messages.Add(message);
            message.AddDomainEvent(new AdOfferMessageCreated(message,
            chat.EntityIssuerId, chat.EntityAudienceId));
            await ufw.SaveChangesAsync();
            return Result<Empty>.Success(Empty.Default, SuccessCodes.CreateOfferMessage);

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
