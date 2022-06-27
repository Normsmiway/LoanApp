using System.Text.Json.Serialization;

namespace DlmCredit.Infrastructure.ThirdpartyApis
{
    public class IncomeResult
    {
        [JsonPropertyName("average_income")]
        public int AverageIncome { get; set; }

        [JsonPropertyName("monthly_income")]
        public int MonthlyIncome { get; set; }

        [JsonPropertyName("yearly_income")]
        public int YearlyIncome { get; set; }

        [JsonPropertyName("income_sources")]
        public int IncomeSources { get; set; }
    }
}
