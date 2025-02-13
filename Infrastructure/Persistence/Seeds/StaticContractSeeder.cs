using Domain.Entities.ContractJson;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Seeds.Contract
{
    internal static class StaticContractSeeder
    {
        public static ModelBuilder SeedStaticContract(this ModelBuilder builder)
        {
            var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "JsonFiles/Contract", "staticContractJson.json");
            var jsonData = File.ReadAllText(jsonFilePath);

            var staticContract = new StaticContractTemplate { Id = 1, JsonData = jsonData };

            builder.Entity<StaticContractTemplate>().HasData(staticContract);

            return builder;
        }
    }

}
