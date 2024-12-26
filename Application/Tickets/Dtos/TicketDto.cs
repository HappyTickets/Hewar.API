namespace Application.Tickets.Dtos
{
    public class TicketDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTimeOffset OpenedDate { get; set; }
        public DateTimeOffset? ClosedDate { get; set; }
        public TicketStatus Status { get; set; }
    }
}
