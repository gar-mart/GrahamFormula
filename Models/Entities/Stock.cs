using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrahamFormula.Models.Financials;
using Newtonsoft.Json;

namespace GrahamFormula.Models.Entities
{
    public class Stock
	{

		public Stock() { }
		public Stock(string symbol)
		{
			Symbol = symbol;
		}

		public string? Symbol { get; set; }
		public string? Name { get; set; }



		[JsonProperty("Global Quote")]
		public GlobalQuote? StockQuote { get; set; }

		public List<EPS>? EPSList { get; set; }

		public EPS? EPSCurrent => EPSList?.LastOrDefault();

		public List<AnnualReports>? AnnualReports { get; set; }

		public StockOverview? StockOverview { get; set; }
		public string GenerateSummary()
		{
			var summary = new StringBuilder();
			summary.AppendLine($"Stock Summary for {Name} ({Symbol})");
			summary.AppendLine($"--------------------------------");

			if (StockQuote != null)
			{
				summary.AppendLine($"Current Price: {StockQuote.Price}");
				summary.AppendLine($"Day's High: {StockQuote.High}");
				summary.AppendLine($"Day's Low: {StockQuote.Low}");
				summary.AppendLine($"Volume: {StockQuote.Volume}");
				summary.AppendLine($"Change: {StockQuote.Change} ({StockQuote.ChangePercent})");
				summary.AppendLine();
			}

			var latestAnnualReport = AnnualReports.OrderByDescending(r => r.FiscalDateEnding).FirstOrDefault();
			if (latestAnnualReport != null)
			{
				summary.AppendLine($"Latest Fiscal Year Ending: {latestAnnualReport.FiscalDateEnding}");
				summary.AppendLine($"Total Assets: {latestAnnualReport.TotalAssets:N0}");
				summary.AppendLine($"Total Liabilities: {latestAnnualReport.TotalLiabilities:N0}");
				summary.AppendLine($"Total Shareholder Equity: {latestAnnualReport.TotalShareholderEquity:N0}");
				summary.AppendLine($"Revenue: {latestAnnualReport.Revenue:N0}");
				summary.AppendLine($"Net Income: {latestAnnualReport.NetIncome:N0}");


			}

			summary.AppendLine($"Earnings Per Share (EPS): {EPSCurrent.Value}");
			summary.AppendLine();

			if (StockOverview != null)
			{
				summary.AppendLine($"Exchange: {StockOverview.Exchange} | Sector/Industry: {StockOverview.Sector} / {StockOverview.Industry}");
				summary.AppendLine($"Market Cap: {StockOverview.MarketCapitalization:N0} | Beta: {StockOverview.Beta:N2}");
				summary.AppendLine($"P/E Ratio: {StockOverview.PERatio:N2} | PEG Ratio: {StockOverview.PEGRatio:N2} | Book Value: {StockOverview.BookValue:N2}");
				summary.AppendLine($"Dividend Yield: {StockOverview.DividendYield:P2} | Revenue TTM: {StockOverview.RevenueTTM:N0}");
				summary.AppendLine($"Profit Margin: {StockOverview.ProfitMargin:P2} | Operating Margin TTM: {StockOverview.OperatingMarginTTM:P2}");
				summary.AppendLine($"Analyst Target Price: {StockOverview.AnalystTargetPrice:C} | Price Range 52W: {StockOverview.WeekLow52:C} - {StockOverview.WeekHigh52:C}");
				summary.AppendLine();

			}
			return summary.ToString();
		}

	}

}






