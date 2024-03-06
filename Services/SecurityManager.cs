using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrahamFormula.Models;
using GrahamFormula.Models.Entities;
using GrahamFormula.Models.Financials;

namespace GrahamFormula.Services
{
	public class SecurityManager
	{
		public static decimal EpsGrowthRate(Stock stock)
		{
			var epsList = stock.EPSList;
			if (epsList == null || epsList.Count < 2)
			{
				throw new ArgumentException();
			}

			decimal epsBegin = epsList.First().Value;
			decimal epsEnd = epsList.Last().Value;
			int yearsBetween = epsList.Last().FiscalYear.Year - epsList.First().FiscalYear.Year;

			double growthRate = Math.Pow((double)(epsEnd / epsBegin), 1.0 / yearsBetween) - 1;
			return (decimal)growthRate;
		}






		// Assess the Margin Of Safety using Benjamin Graham's methods.
		public static decimal MarginOfSafety(Stock stock)
		{

			decimal intrinsicValue = IntrinsicValue(stock);
			decimal currentMarketPrice = decimal.Parse(stock.StockQuote.Price);


			// Calcuate Margin Of Safety using Graham's formula.
			decimal marginOfSafety = (intrinsicValue - currentMarketPrice) / intrinsicValue;
			return marginOfSafety * 100; // Convert to percentage
		}



		// Calculate the Intrinsic Value of the stock using Benjamin Graham's methods.
		// Intrinsic Value = EPS × (8.5 + 2g)  
		// EPS represents current Earnings Per Share
		// 8.5 represents the P/E ratio of a stock with zero growth. 
		// g represents the growth rate.


		public static decimal IntrinsicValue(Stock stock)
		{

			decimal eps = stock.EPSCurrent.Value;
			decimal g = EpsGrowthRate(stock);

			// turn growth rate from percentage into decimal
			g = g / 100;

			// Calculate the intrinsic value using Graham's formula
			decimal intrinsicValue = eps * (8.5m + (2 * g));
			return intrinsicValue;
		}


	}
}
