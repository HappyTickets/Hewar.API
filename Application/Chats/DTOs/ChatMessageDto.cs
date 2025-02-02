namespace Application.Chats.DTOs
{
    public class ChatMessageDto
    {
        public long Id { get; set; }
        public long ChatId { get; set; }
        public string Content { get; set; }
        public ICollection<MediaDto>? Medias { get; set; }
        public DateTimeOffset SentOn { get; set; }
        public EntityTypes? RepresentedEntity { get; set; } // Company , Facility ....
        public ChatParticipantDto Sender { get; set; }
    }
}