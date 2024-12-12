namespace Application.PriceRequests.Dtos
{
    public class PriceRequestResponseDto
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public DateTimeOffset RespondedDate { get; set; }
    }
}
