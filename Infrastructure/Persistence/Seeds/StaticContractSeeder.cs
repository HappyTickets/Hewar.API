using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Infrastructure.Persistence.Seeds.Contract
{
    internal static class StaticContractSeeder
    {
        public static ModelBuilder SeedStaticContract(this ModelBuilder builder)
        {
            // Seed StaticContract data
            // ,//var staticContractData = ReadJsonData<StaticContract>("StaticContractData.json");
            //   builder.Entity<StaticContract>().HasData(staticContractData);

            // Seed StaticClause data
            //   var staticClausesData = ReadJsonData<StaticClause[]>("StaticClauses.json");
            //     builder.Entity<StaticClause>().HasData(staticClausesData);

            return builder;
        }

        private static T ReadJsonData<T>(string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "JsonFiles", "Contract", fileName);
            var jsonData = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(jsonData);
        }
    }


}
