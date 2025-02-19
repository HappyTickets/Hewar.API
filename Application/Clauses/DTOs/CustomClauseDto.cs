namespace Application.Clauses.DTOs
{
    public class CustomClauseDto
    {
        public long Id { get; set; }
        public long ContractId { get; set; }
        public EntityTypes? AuthorType { get; set; }
        public string HtmlContentAr { get; set; } = "Temp Data";
        public string HtmlContentEn { get; set; } = "Temp Data";
    }
}
