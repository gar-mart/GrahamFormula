using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Reflection.Metadata;
using System.Collections.Generic;
using GrahamFormula.Models.Financials;

namespace GrahamFormula.Services

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

		public string LastRequestedUrl { get; private set; } // Store the last requested URL



		public async Task<Stock> GetStockDataAsync(string symbol)
		{
			Stock stock = new Stock();
			string url;
			// Relative URL should not include the base address, HttpClient will combine them
			url = $"query?function=GLOBAL_QUOTE&symbol={symbol}&apikey={_apiKey}";

			HttpResponseMessage response = null;
			string content = "";

			try
			{
				response = await _httpClient.GetAsync(url);
				content = await response.Content.ReadAsStringAsync();

				// Check if the response status code indicates success
				if (!response.IsSuccessStatusCode)
				{
					// Log the error and the content of the response
					Console.WriteLine($"Error fetching stock data: {response.StatusCode}");
					Console.WriteLine($"Response content: {content}");
					return null; // Handle the error appropriately
				}
			}
			catch (HttpRequestException e)
			{
				// Log the exception details
				Console.WriteLine($"HttpRequestException caught: {e.Message}");
				return null; // Handle the exception appropriately
			}

			// If the response was successful, parse the JSON
			var stockData1 = JsonConvert.DeserializeObject<Stock>(content);
			stock.StockQuote = stockData1.StockQuote;

			url = $"query?function=BALANCE_SHEET&symbol={symbol}&apikey={_apiKey}";

			response = await _httpClient.GetAsync(url);
			content = await response.Content.ReadAsStringAsync();

			var stockData2 = JsonConvert.DeserializeObject<Stock>(content);
			stock.AnnualReports = stockData2.AnnualReports;
			return stock;
		}




		public async Task<List<EPS>> GetEPSListAsync(string symbol)
		{
			List<EPS> EPSList = new List<EPS>();

			var url = $"query?function=EARNINGS&symbol={symbol}&apikey={_apiKey}";
			HttpResponseMessage response = null;
			string content = "";

			try
			{
				response = await _httpClient.GetAsync(url);
				content = await response.Content.ReadAsStringAsync();

				if (!response.IsSuccessStatusCode)
				{
					return null;
				}
			}
			catch (HttpRequestException e)
			{
				return null;
			}
			dynamic data = JsonConvert.DeserializeObject(content);

			foreach (var item in data.annualEarnings)
			{
				DateTime fiscalYear = DateTime.Parse((string)item.fiscalDateEnding); // Parses the fiscalDateEnding string to DateTime
				decimal epsValue = decimal.Parse((string)item.reportedEPS); // Parses the reportedEPS string to decimal

				EPSList.Add(new EPS { FiscalYear = fiscalYear, Value = epsValue });
			}
			return EPSList;
		}


	}
}