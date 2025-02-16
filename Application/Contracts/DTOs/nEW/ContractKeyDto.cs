namespace Application.Contracts.DTOs.nEW
{
    public class ContractKeyDto
    {
        public int Id { get; set; }
        public int KeyId { get; set; }
        public string KeyName { get; set; } = "";
        public string Value { get; set; } = "Key Value";

        public DataTypes DataType { get; set; }
    }
}
