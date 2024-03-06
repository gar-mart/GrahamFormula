using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrahamFormula.Models.Financials
{
    public class StockOverview
    {

        [JsonProperty("Exchange")]
        public string? Exchange { get; set; }

        [JsonProperty("Sector")]
        public string? Sector { get; set; }

        [JsonProperty("Industry")]
        public string? Industry { get; set; }

        [JsonProperty("MarketCapitalization")]
        public string? MarketCapitalization { get; set; }

        //  Earnings before interest, taxes, depreciation, and amortization
        [JsonProperty("EBITDA")]
        public string? EBITDA { get; set; }

        [JsonProperty("PERatio")]
        public string? PERatio { get; set; }

        [JsonProperty("PEGRatio")]
        public string? PEGRatio { get; set; }

        [JsonProperty("BookValue")]
        public string? BookValue { get; set; }

        [JsonProperty("DividendPerShare")]
        public string? DividendPerShare { get; set; }

        [JsonProperty("DividendYield")]
        public string? DividendYield { get; set; }

        [JsonProperty("RevenueTTM")]
        public string? RevenueTTM { get; set; }

        [JsonProperty("ProfitMargin")]
        public string? ProfitMargin { get; set; }

        // TTM - Trailing Twelve Months 
        [JsonProperty("OperatingMarginTTM")]
        public string? OperatingMarginTTM { get; set; }

        [JsonProperty("ReturnOnAssetsTTM")]
        public string? ReturnOnAssetsTTM { get; set; }

        [JsonProperty("ReturnOnEquityTTM")]
        public string? ReturnOnEquityTTM { get; set; }

        [JsonProperty("AnalystTargetPrice")]
        public string? AnalystTargetPrice { get; set; }

        [JsonProperty("Beta")]
        public string? Beta { get; set; }

        [JsonProperty("52WeekHigh")]
        public string? WeekHigh52 { get; set; }

        [JsonProperty("52WeekLow")]
        public string? WeekLow52 { get; set; }

    }

}
