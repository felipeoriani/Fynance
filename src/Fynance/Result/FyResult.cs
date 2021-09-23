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
        
        public void Clear()
        {
            this.Currency = null;

            this.Symbol = null;

            this.ExchangeName = null;

            this.InstrumentType = null;

            this.FirstTradeDate = null;

            this.RegularMarketTime = null;

            this.GMTOffSet = new int();

            this.TimeZone = null;

            this.ExchangeTimezoneName = null;

            this.RegularMarketPrice = new double();

            this.ChartPreviousClose = new double();

            this.PreviousClose = new double();

            this.Scale = new int();

            this.PriceHint = new double();

            this.DataGranularity = new Interval();

            this.Range = new Period();

            this.ValidRanges = null;

            this.Quotes = null;

            this.Dividends = null;

            this.Splits = null;
        }
    }
}
