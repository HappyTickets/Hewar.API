namespace Application.Contracts.DTOs.Dynamic
{
    public class ContractKeyDto
    {
        public long Id { get; set; }
        public long KeyId { get; set; }
        public string KeyName { get; set; } = "";
        public string Value { get; set; } = "Key Value";

        public DataTypes DataType { get; set; }
    }
}
