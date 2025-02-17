using Application.Contracts.DTOs.Dynamic;
using Domain.Entities.ContractAggregate.Dynamic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Seeds
{
    internal static class KeySeeder
    {
        public static ModelBuilder SeedKeys(this ModelBuilder builder)
        {
            var properties = typeof(ContractFieldsDto).GetProperties();
            var keys = new List<Key>();
            int index = 1;

            foreach (var property in properties)
            {
                var key = new Key
                {
                    Id = index++,
                    Name = property.Name,
                    DataType = GetDataType(property.PropertyType)
                };

                keys.Add(key);
            }

            builder.Entity<Key>().HasData(keys);
            return builder;
        }

        private static DataTypes GetDataType(Type type)
        {
            if (type == typeof(int) || type == typeof(long) || type == typeof(decimal) || type == typeof(double) || type == typeof(float))
            {
                return DataTypes.Number;
            }
            else if (type == typeof(DateTime) || type == typeof(DateOnly))
            {
                return DataTypes.Date;
            }
            else
            {
                return DataTypes.String;
            }
        }




    }
}
