namespace Domain.Entities.ChatAggregate
{
    public class Message : SoftDeletableEntity
    {
        public string Content { get; set; }
        public DateTimeOffset SentOn { get; set; }
        public virtual ICollection<Media>? Medias { get; set; }

        public EntityTypes? RepresentedEntity { get; set; }

        public long SenderId { get; set; }
        public virtual ApplicationUser Sender { get; set; }
        public long ChatId { get; set; }
    }
}
