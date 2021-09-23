using System;

namespace Fynance.Result
{
	public class FyResult
	{
		public string Currency { get; set; }

		public string Symbol { get; set; }

		public string ExchangeName { get; set; }

		public string InstrumentType { get; set; }

		public DateTime? FirstTradeDate { get; set; }

		public DateTime? RegularMarketTime { get; set; }

		public int GMTOffSet { get; set; }

		public string TimeZone { get; set; }

		public string ExchangeTimezoneName { get; set; }

		public decimal RegularMarketPrice { get; set; }

		public decimal ChartPreviousClose { get; set; }

		public decimal PreviousClose { get; set; }

		public int Scale { get; set; }

		public double PriceHint { get; set; }

		public Interval DataGranularity { get; set; }

		public Period Range { get; set; }

		public Period[] ValidRanges { get; set; }

		public FyQuote[] Quotes { get; set; }

		public FyDividend[] Dividends { get; set; }

		public FySplit[] Splits { get; set; }
	}
}