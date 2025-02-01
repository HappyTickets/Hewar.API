namespace Application.PriceRequests.Dtos.Chat
{
    public class ChatMessageDto
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public ICollection<MediaDto>? Medias { get; set; }
        public DateTimeOffset SentOn { get; set; }
        public EntityTypes? RepresentedEntity { get; set; }
        public long SenderId { get; set; }
        public ChatParticipantDto Sender { get; set; }
    }
}