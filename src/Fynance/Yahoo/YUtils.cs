namespace Fynance.Yahoo
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Result;

	internal static class YUtils
	{
		public const string BaseUrl = "https://query1.finance.yahoo.com";

		public static DateTime DefaultDateTime => new DateTime(1970, 1, 1);

		public static string GetPeriod(Period? period = null)
		{
			switch (period)
			{
				case Period.OneDay: return "1d";
				case Period.FiveDays: return "5d";
				case Period.OneMonth: return "1mo";
				case Period.ThreeMonths: return "3mo";
				case Period.SixMonths: return "6mo";
				case Period.OneYear: return "1y";
				case Period.TwoYears: return "2y";
				case Period.FiveYears: return "5y";
				case Period.TenYears: return "10y";
				case Period.YearToDate: return "ytd";
				case Period.Max: return "max";
				default: return "1mo";
			}
		}

		public static Period GetPeriod(string period)
		{
			switch (period)
			{
				case "1d": return Period.OneDay;
				case "5d": return Period.FiveDays;
				case "1mo": return Period.OneMonth;
				case "3mo": return Period.ThreeMonths;
				case "6mo": return Period.SixMonths;
				case "1y": return Period.OneYear;
				case "2y": return Period.TwoYears;
				case "5y": return Period.FiveYears;
				case "10y": return Period.TenYears;
				case "ytd": return Period.YearToDate;
				case "max": return Period.Max;
				default: return Period.Custom;
			}
		}

		public static string GetInterval(Interval? interval = null)
		{
			switch (interval)
			{
				case Interval.OneMinute: return "1m";
				case Interval.TwoMinutes: return "2m";
				case Interval.FiveMinutes: return "5m";
				case Interval.FifteenMinutes: return "15m";
				case Interval.ThirtyMinutes: return "30m";
				case Interval.SixtyMinutes: return "60m";
				case Interval.NinetyMinutes: return "90m";
				case Interval.OneHour: return "1h";
				case Interval.OneDay: return "1d";
				case Interval.FiveDays: return "5d";
				case Interval.OneWeek: return "1wk";
				case Interval.OneMonth: return "1mo";
				case Interval.ThreeMonths: return "3mo";
				default: return "1d";
			}
		}

		public static Interval GetInterval(string interval)
		{
			switch (interval)
			{
				case "1m": return Interval.OneMinute;
				case "2m": return Interval.TwoMinutes;
				case "5m": return Interval.FiveMinutes;
				case "15m": return Interval.FifteenMinutes;
				case "30m": return Interval.ThirtyMinutes;
				case "60m": return Interval.SixtyMinutes;
				case "90m": return Interval.NinetyMinutes;
				case "1h": return Interval.OneHour;
				case "1d": return Interval.OneDay;
				case "5d": return Interval.FiveDays;
				case "1wk": return Interval.OneWeek;
				case "1mo": return Interval.OneMonth;
				case "3mo": return Interval.ThreeMonths;
			}
			throw new ArgumentOutOfRangeException("interval", "There is not interval defined for the value");
		}

		public static DateTime GetDateFromTimestamp(double timestamp)
			=> DefaultDateTime.AddSeconds(timestamp);

		public static DateTime? GetDateFromTimestamp(double? timestamp)
			=> timestamp.HasValue ? DefaultDateTime.AddSeconds(timestamp.Value) as DateTime? : null;

		public static DateTime GetDateFromTimestamp(double timestamp, TimeZoneInfo timeZone = null)
			=> timeZone != null ? GetDateFromTimestamp(timestamp).Add(timeZone.BaseUtcOffset) : GetDateFromTimestamp(timestamp);

		public static double GetTimestampFromDateTime(DateTime dateTime)
			=> dateTime.Subtract(DefaultDateTime).TotalSeconds;

		/// <summary>
		/// Prepare the splits from YResponse.
		/// </summary>
		/// <param name="resultResponse"></param>
		/// <param name="timeZone"></param>
		/// <returns></returns>
		public static FyQuote[] GetQuotes(this YResultResponse resultResponse, TimeZoneInfo timeZone = null)
		{
			var ohlc = resultResponse.Indicators.Quote.First();

			var timestamps = resultResponse.TimeStamp;
            var quotes = new List<FyQuote>();
			if (timestamps == null)
                return quotes.ToArray();

			var dateTimes = timestamps.Select(x => x.HasValue ? GetDateFromTimestamp(x.Value) : null as DateTime?).ToList();

			var adjClose = resultResponse.Indicators.AdjClose?.FirstOrDefault()?.AdjClose ?? ohlc.Close;

		
			for (int i = 0; i < dateTimes.Count; i++)
			{
				if (dateTimes[i] == null ||
					ohlc.Low[i] == null ||
					ohlc.Open[i] == null ||
					ohlc.High[i] == null ||
					ohlc.Close[i] == null ||
					adjClose[i] == null ||
					ohlc.Volume[i] == null)
					continue;

				DateTime period = dateTimes[i].Value;
				if (timeZone != null)
					period = period.Add(timeZone.BaseUtcOffset);

				quotes.Add(new FyQuote()
				{
					Period = period,
					Low = ohlc.Low[i].Value,
					Open = ohlc.Open[i].Value,
					High = ohlc.High[i].Value,
					Close = ohlc.Close[i].Value,
					AdjClose = adjClose[i].Value,
					Volume = ohlc.Volume[i].Value
				});
			}

			return quotes.ToArray();
		}

		/// <summary>
		/// Prepare the dividends from YResponse.
		/// </summary>
		/// <param name="resultResponse"></param>
		/// <param name="timeZone"></param>
		/// <returns></returns>
		public static FyDividend[] GetDividends(this YResultResponse resultResponse, TimeZoneInfo timeZone = null)
			=> resultResponse?.Events?
							.Dividends?
							.Values
							.Select(x => new FyDividend()
							{
								Date = GetDateFromTimestamp(x.Date, timeZone),
								Value = x.Amount
							}).ToArray();

		/// <summary>
		/// Prepare the splits from YResponse.
		/// </summary>
		/// <param name="resultResponse"></param>
		/// <param name="timeZone"></param>
		/// <returns></returns>
		public static FySplit[] GetSplits(this YResultResponse resultResponse, TimeZoneInfo timeZone = null)
			=> resultResponse?.Events?
						  .Splits?
						  .Values
						  .Select(x => new FySplit()
						  {
							  Date = GetDateFromTimestamp(x.Date, timeZone),
							  Denominator = x.Denominator,
							  Numberator = x.Numerator,
							  Ratio = x.SplitRatio
						  }).ToArray();

		/// <summary>
		/// Prepare a the result from YResponse deserialized object.
		/// </summary>
		/// <param name="yResponse"></param>
		/// <param name="timeZone"></param>
		/// <returns></returns>
		public static FyResult GetResult(this YResponse yResponse, TimeZoneInfo timeZone = null)
		{
			var resultResponse = yResponse?.Chart?.Result?.FirstOrDefault();

			if (resultResponse == null)
				return null;

			var meta = resultResponse.Meta;

			var result = new FyResult()
			{
				Currency = meta.Currency,
				Symbol = meta.Symbol,
				ExchangeName = meta.ExchangeName,
				InstrumentType = meta.InstrumentType,
				FirstTradeDate = GetDateFromTimestamp(meta.FirstTradeDate),
				RegularMarketTime = GetDateFromTimestamp(meta.RegularMarketTime),
				GMTOffSet = meta.GMTOffSet,
				TimeZone = meta.TimeZone,
				ExchangeTimezoneName = meta.ExchangeName,
				RegularMarketPrice = meta.RegularMarketPrice,
				ChartPreviousClose = meta.ChartPreviousClose,
				PreviousClose = meta.PreviousClose,
				Scale = meta.Scale,
				PriceHint = meta.PriceHint,
				DataGranularity = GetInterval(meta.DataGranularity),
				Range = GetPeriod(meta.Range),
				ValidRanges = meta.ValidRanges.Select(GetPeriod).ToArray(),
				Quotes = resultResponse.GetQuotes(timeZone),
				Dividends = resultResponse.GetDividends(timeZone),
				Splits = resultResponse.GetSplits(timeZone)
			};

			return result;
		}
	}
}