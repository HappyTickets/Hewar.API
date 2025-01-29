namespace Domain.Entities.ChatAggregate
{
    public class Chat : SoftDeletableEntity
    {
        public string Content { get; set; }
        public long UserId { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}