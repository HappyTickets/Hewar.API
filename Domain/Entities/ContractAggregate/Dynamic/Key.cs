namespace Domain.Entities.ContractAggregate.Dynamic
{
    public class Key : SoftDeletableEntity
    {
        public string Name { get; set; }
        public DataTypes DataType { get; set; }
    }
}
