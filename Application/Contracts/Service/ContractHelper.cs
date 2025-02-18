using Application.Contracts.DTOs.Dynamic;
using Domain.Entities.ContractAggregate.Dynamic;

namespace Application.Contracts.Service
{
    public static class ContractHelper
    {
        public static async Task<ContractFieldsDto> MapContractFieldsToDto(ICollection<ContractKey> contractKeys, Dictionary<string, Key> keys)
        {
            var contractFieldsDto = new ContractFieldsDto();

            foreach (var contractKey in contractKeys)
            {
                if (keys.TryGetValue(contractKey.Key.Name, out var key))
                {
                    var property = typeof(ContractFieldsDto).GetProperty(key.Name);
                    if (property != null && property.CanWrite)
                    {
                        object? value = null;
                        if (property.PropertyType == typeof(DateTime))
                        {
                            if (DateTime.TryParse(contractKey.Value, out var dateValue))
                            {
                                value = dateValue;
                            }
                        }
                        else if (property.PropertyType == typeof(int))
                        {
                            if (int.TryParse(contractKey.Value, out var intValue))
                            {
                                value = intValue;
                            }
                        }
                        else
                        {
                            value = contractKey.Value;
                        }

                        property.SetValue(contractFieldsDto, value);
                    }
                }
            }

            return contractFieldsDto;
        }

        public static async Task<List<ContractKey>> MapContractFields(ContractFieldsDto fields, Dictionary<string, Key> keys)
        {
            var contractKeys = new List<ContractKey>();
            var properties = typeof(ContractFieldsDto).GetProperties();

            foreach (var property in properties)
            {
                if (keys.TryGetValue(property.Name, out var key))
                {
                    var value = property.GetValue(fields)?.ToString() ?? string.Empty;
                    var contractKey = new ContractKey
                    {
                        KeyId = key.Id,
                        Value = value
                    };
                    contractKeys.Add(contractKey);
                }
            }

            return contractKeys;
        }
    }
}
