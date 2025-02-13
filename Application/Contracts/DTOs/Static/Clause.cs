using System.Text.Json.Serialization;

namespace Application.Contracts.DTOs.Static
{
    public class Clause
    {
        public int Number { get; set; }
        public BilingualText Title { get; set; }

        public string Ar { get; set; }
        public string En { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<SubClause>? SubClauses { get; set; }
    }

    public class SubClause
    {
        public int Number { get; set; }
        public string Ar { get; set; }
        public string En { get; set; }
    }
}
