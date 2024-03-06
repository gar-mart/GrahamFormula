using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GrahamFormula.Models.Financials
{

    public class AnnualReports
    {
        [JsonProperty("fiscalDateEnding")]
        public string? FiscalDateEnding { get; set; }

        [JsonProperty("reportedCurrency")]
        public string? ReportedCurrency { get; set; }

        [JsonProperty("totalAssets")]
        public string? TotalAssets { get; set; }

        [JsonProperty("totalLiabilities")]
        public string? TotalLiabilities { get; set; }

        [JsonProperty("totalShareholderEquity")]
        public string? TotalShareholderEquity { get; set; }

        [JsonProperty("totalCurrentAssets")]
        public string? TotalCurrentAssets { get; set; }

        [JsonProperty("totalCurrentLiabilities")]
        public string? TotalCurrentLiabilities { get; set; }

        [JsonProperty("longTermDebt")]
        public string? LongTermDebt { get; set; }

        [JsonProperty("cashAndCashEquivalentsAtCarryingValue")]
        public string? CashAndEquivalents { get; set; }

        [JsonProperty("revenue")]
        public string? Revenue { get; set; }

        [JsonProperty("netIncome")]
        public string? NetIncome { get; set; }

    }

}
