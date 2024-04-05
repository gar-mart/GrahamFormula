# Overview

This application is designed to perform stock analysis based on Benjamin Graham's value investing principles. It allows users to analyze stocks for intrinsic value, margin of safety, dividend history, and other financial metrics to make informed investment decisions.
Features

- Intrinsic Value Calculation: Estimates the true value of a stock based on earnings, growth, and other fundamental data.
- Margin of Safety Assessment: Evaluates whether a stock's market price offers a sufficient margin of safety compared to its intrinsic value.
- Dividend History Analysis: Reviews the consistency and growth of a company's dividend payouts over time.
- Financial Health Checks: Analyzes balance sheet strength, debt levels, and liquidity ratios.
- Stock Comparison: Compares multiple stocks based on key financial metrics.
- Portfolio Diversification Analysis: Ensures a well-balanced and diversified investment portfolio.
- etc.

# Usage
## Initializing the Alpha Vantage Service

First you need to create an instance of AlphaVantageService by passing in your Alpha Vantage API key. This service will provide you with methods to fetch stock data.


```csharp
string apiKey = "YOUR_API_KEY"; // Replace with your actual API key
var alphaVantageService = new AlphaVantageService(apiKey);
```

## Initializing and fetching data for a Stock

```csharp
string symbol = "TICKER_SYMBOL"; // Replace with am actual ticker symbol for a company
Stock stock = alphaVantageService.GetStockDataAsync(symbol);
```

Once you have Stocks initialized, you can use the functionality provided by SecurityManager class for:

- Dividend Analysis: Evaluate the consistency and growth of dividend payments to assess a company's financial health and shareholder return policies.
- Intrinsic Value Assessment: Calculate the intrinsic value of a security based on its earnings, growth prospects, and financial stability.
- Margin of Safety Calculation: Determine if the current market price of a security provides a significant margin of safety compared to its intrinsic value.
- Financial Ratios and Metrics: Compute critical financial ratios such as P/E (Price-to-Earnings), P/B (Price-to-Book), debt-to-equity, and current ratios that Graham emphasized for selecting stocks.
- Historical Earnings Growth: Calculate the historical growth rate of a company's earnings to evaluate its track record of profitability and growth.
- Debt Analysis: Assess a company's debt levels to ensure that it is not overleveraged, maintaining a conservative debt-to-equity ratio.
- Stock Categorization: Classify stocks as either defensive or enterprising based on their financial metrics, size, and earnings stability to suit different investment strategies.
- Portfolio Diversification: Evaluate and recommend diversification strategies to reduce the risk of the investment portfolio.
- etc.



