namespace Domain.Entities
{
    public class Policy : SoftDeletableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Details { get; set; }
    }
}
