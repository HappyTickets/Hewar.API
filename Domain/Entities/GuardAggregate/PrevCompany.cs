namespace Domain.Entities.GuardAggregate
{
    public class PrevCompany
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset From { get; set; }
        public DateTimeOffset To { get; set; }
    }
}
