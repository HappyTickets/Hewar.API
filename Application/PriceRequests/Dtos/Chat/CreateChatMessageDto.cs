﻿namespace Application.PriceRequests.Dtos.Chat
{
    public class CreateChatMessageDto
    {
        public string Content { get; set; }
        public IEnumerable<MediaDto>? Medias { get; set; }
        public long ChatId { get; set; }
    }
}