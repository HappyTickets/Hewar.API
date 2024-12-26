namespace Application.Guards.Dtos
{
    public class PrevCompanyDto
    {
        public string Name { get; set; }
        public DateTimeOffset From { get; set; }
        public DateTimeOffset To { get; set; }
    }
}
