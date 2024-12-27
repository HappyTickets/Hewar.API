﻿namespace Application.PriceRequests.Dtos
{
    public class PriceRequestMessageDto
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public ICollection<MediaDto> Medias { get; set; }
        public DateTimeOffset SentDate { get; set; }
        public long SenderId { get; set; }
        public AccountTypes SenderType { get; set; }
    }
}
