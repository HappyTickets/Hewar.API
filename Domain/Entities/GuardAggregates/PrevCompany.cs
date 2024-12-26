namespace Domain.Entities.GuardAggregates
{
    public class PrevCompany
    {
        public string Name { get; set; }
        public DateTimeOffset From { get; set; }
        public DateTimeOffset To { get; set; }
    }
}
