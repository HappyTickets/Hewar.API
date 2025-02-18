namespace Domain.Entities.ContractAggregate.Static
{
    public class StaticClause : SoftDeletableEntity
    {
        public string TitleAr { get; set; }
        public string TitleEn { get; set; }

        public string ContentAr { get; set; }
        public string ContentEn { get; set; }
    }
}
