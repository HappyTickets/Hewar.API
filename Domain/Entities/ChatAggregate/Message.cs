namespace Domain.Entities.ChatAggregate
{
    public class Message : SoftDeletableEntity
    {
        public string Content { get; set; }

        public virtual ICollection<Media>? Medias { get; set; }

        public long SenderId { get; set; }
        public ApplicationUser User { get; set; }
        public DateTimeOffset SentOn { get; set; }
    }
}
