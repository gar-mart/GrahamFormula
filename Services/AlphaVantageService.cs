using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GrahamFormula.Models;
using System.Reflection.Metadata;
using System.Collections.Generic;
using GrahamFormula.Models.Entities;
using GrahamFormula.Models.Financials;

namespace GrahamFormula.AlphaVintage
{
	public class AlphaVantageService
	{
		private readonly HttpClient _httpClient;
		private readonly string _apiKey;

		public AlphaVantageService(string apiKey)
		{
			_httpClient = new HttpClient();
			_httpClient.BaseAddress = new Uri("https://www.alphavantage.co/");
			_apiKey = apiKey;

		}


		public async Task<Stock> GetStockDataAsync(string symbol)
		{
			Stock stock = new Stock();
			stock.Symbol = symbol;

			// Fetch Global Quote
			try
			{
				stock.StockQuote = await GetGlobalQuoteAsync(symbol);
				stock.Name = stock.StockQuote.Name; // Assign the stock Name
			}
			catch (Exception)
			{
				stock.StockQuote = null;
			}

			// Fetch Balance Sheet
			try
			{
				stock.AnnualReports = await GetBalanceSheetAsync(symbol);
			}
			catch (Exception)
			{
				stock.AnnualReports = null;
			}

			// Fetch Stock Overview
			try
			{
				stock.StockOverview = await GetStockOverviewAsync(symbol);
			}
			catch (Exception)
			{
				stock.StockOverview = null;
			}

			// Fetch EPS List
			try
			{
				stock.EPSList = await GetEPSListAsync(symbol);
			}
			catch (Exception)
			{
				stock.EPSList = null;
			}


			return stock;
		}



		// Get API response based on url
		private async Task<string> GetApiResponseAsync(string url)
		{
			HttpResponseMessage response = await _httpClient.GetAsync(url);
			if (!response.IsSuccessStatusCode)
			{
				throw new HttpRequestException($"Error fetching data: {response.StatusCode}");
			}
			return await response.Content.ReadAsStringAsync();
		}

		// Fetch Global Quote
		private async Task<GlobalQuote> GetGlobalQuoteAsync(string symbol)
		{
			string url = $"query?function=GLOBAL_QUOTE&symbol={symbol}&apikey={_apiKey}";
			string content = await GetApiResponseAsync(url);
			var stockData = JsonConvert.DeserializeObject<Stock>(content);
			return stockData.StockQuote;
		}
		// Fetch Balance Sheet
		private async Task<List<AnnualReports>> GetBalanceSheetAsync(string symbol)
		{
			string url = $"query?function=BALANCE_SHEET&symbol={symbol}&apikey={_apiKey}";
			string content = await GetApiResponseAsync(url);
			var stockData = JsonConvert.DeserializeObject<Stock>(content);
			return stockData.AnnualReports;
		}

		// Fetch Stock Overview
		private async Task<StockOverview> GetStockOverviewAsync(string symbol)
		{
			string url = $"query?function=OVERVIEW&symbol={symbol}&apikey={_apiKey}";
			string content = await GetApiResponseAsync(url);
			var stockData = JsonConvert.DeserializeObject<Stock>(content);
			return stockData.StockOverview;
		}

		// Fetch EPS History
		private async Task<List<EPS>> GetEPSListAsync(string symbol)
		{
			List<EPS> EPSList = new List<EPS>();
			string url = $"query?function=EARNINGS&symbol={symbol}&apikey={_apiKey}";

			try
			{
				string content = await GetApiResponseAsync(url);
				dynamic data = JsonConvert.DeserializeObject(content);

				foreach (var item in data.annualEarnings)
				{
					DateTime fiscalYear = DateTime.Parse((string)item.fiscalDateEnding);
					decimal epsValue = decimal.Parse((string)item.reportedEPS);
					EPSList.Add(new EPS { FiscalYear = fiscalYear, Value = epsValue });
				}
			}
			catch (HttpRequestException e)
			{
				return null;
			}

			return EPSList;
		}

	}
}