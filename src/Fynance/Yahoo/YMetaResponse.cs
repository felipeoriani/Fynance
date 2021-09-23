namespace Fynance.Yahoo
{
	using Newtonsoft.Json;
	using System.Collections.Generic;

	internal class YMetaResponse
	{
		[JsonProperty("currency")]
		public string Currency { get; set; }

		[JsonProperty("symbol")]
		public string Symbol { get; set; }

		[JsonProperty("exchangeName")]
		public string ExchangeName { get; set; }

		[JsonProperty("instrumentType")]
		public string InstrumentType { get; set; }

		[JsonProperty("firstTradeDate")]
		public int? FirstTradeDate { get; set; }

		[JsonProperty("regularMarketTime")]
		public int RegularMarketTime { get; set; }

		[JsonProperty("gmtoffset")]
		public int GMTOffSet { get; set; }

		[JsonProperty("timezone")]
		public string TimeZone { get; set; }

		[JsonProperty("exchangeTimezoneName")]
		public string ExchangeTimezoneName { get; set; }

		[JsonProperty("regularMarketPrice")]
		public decimal RegularMarketPrice { get; set; }

		[JsonProperty("chartPreviousClose")]
		public decimal ChartPreviousClose { get; set; }

		[JsonProperty("previousClose")]
		public decimal PreviousClose { get; set; }

		[JsonProperty("scale")]
		public int Scale { get; set; }

		[JsonProperty("priceHint")]
		public double PriceHint { get; set; }

		[JsonProperty("currentTradingPeriod")]
		public YCurrentTradingPeriodResponse CurrentTradingPeriod { get; set; }

		[JsonProperty("tradingPeriods")]
		public IList<IList<YPeriodResponse>> TradingPeriods { get; set; }

		[JsonProperty("dataGranularity")]
		public string DataGranularity { get; set; }

		[JsonProperty("range")]
		public string Range { get; set; }

		[JsonProperty("validRanges")]
		public IList<string> ValidRanges { get; set; }
	}
}