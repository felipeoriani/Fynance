using System;
using System.Collections.Generic;
using System.Linq;
using Fynance.Yahoo;
using Fynance.Result;

namespace Fynance.Yahoo
{
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
            }

            throw new ArgumentOutOfRangeException("period", "There is not period defined for the value");
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
            var ohlc = resultResponse.indicators.quote.First();

            var timestamps = resultResponse.timestamp;
            var dateTimes = timestamps.Select(x => x.HasValue ? GetDateFromTimestamp(x.Value) : null as DateTime?).ToList();

            var adjClose = resultResponse.indicators.adjclose?.FirstOrDefault()?.adjclose ?? ohlc.close;

            var quotes = new List<FyQuote>();
            for (int i = 0; i < dateTimes.Count; i++)
            {
                if (dateTimes[i] == null ||
                    ohlc.low[i] == null ||
                    ohlc.open[i] == null ||
                    ohlc.high[i] == null ||
                    ohlc.close[i] == null ||
                    adjClose[i] == null ||
                    ohlc.volume[i] == null)
                    continue;

                DateTime period = dateTimes[i].Value;
                if (timeZone != null)
                    period = period.Add(timeZone.BaseUtcOffset);

                quotes.Add(new FyQuote()
                {
                    Period = period,
                    Low = ohlc.low[i].Value,
                    Open = ohlc.open[i].Value,
                    High = ohlc.high[i].Value,
                    Close = ohlc.close[i].Value,
                    AdjClose = adjClose[i].Value,
                    Volume = ohlc.volume[i].Value
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
        => resultResponse?.events?
                          .dividends?
                          .Values
                          .Select(x => new FyDividend()
                          {
                              Date = GetDateFromTimestamp(x.date, timeZone),
                              Value = x.amount
                          }).ToArray();

        /// <summary>
        /// Prepare the splits from YResponse.
        /// </summary>
        /// <param name="resultResponse"></param>
        /// <param name="timeZone"></param>
        /// <returns></returns>
        public static FySplit[] GetSplits(this YResultResponse resultResponse, TimeZoneInfo timeZone = null)
        => resultResponse?.events?
                          .splits?
                          .Values
                          .Select(x => new FySplit()
                          {
                              Date = GetDateFromTimestamp(x.date, timeZone),
                              Denominator = x.denominator,
                              Numberator = x.numerator,
                              Ratio = x.splitRatio
                          }).ToArray();

        /// <summary>
        /// Prepare a the result from YResponse deserialized object.
        /// </summary>
        /// <param name="yResponse"></param>
        /// <param name="timeZone"></param>
        /// <returns></returns>
        public static FyResult GetResult(this YResponse yResponse, TimeZoneInfo timeZone = null)
        {
            var resultResponse = yResponse?.chart?.result?.FirstOrDefault();

            if (resultResponse == null)
                return null;

            var meta = resultResponse.meta;

            var result = new FyResult()
            {
                Currency = meta.currency,
                Symbol = meta.symbol,
                ExchangeName = meta.exchangeName,
                InstrumentType = meta.instrumentType,
                FirstTradeDate = GetDateFromTimestamp(meta.firstTradeDate),
                RegularMarketTime = GetDateFromTimestamp(meta.regularMarketTime),
                GMTOffSet = meta.gmtoffset,
                TimeZone = meta.timezone,
                ExchangeTimezoneName = meta.exchangeName,
                RegularMarketPrice = meta.regularMarketPrice,
                ChartPreviousClose = meta.chartPreviousClose,
                PreviousClose = meta.previousClose,
                Scale = meta.scale,
                PriceHint = meta.priceHint,
                DataGranularity = GetInterval(meta.dataGranularity),
                Range = GetPeriod(meta.range),
                ValidRanges = meta.validRanges.Select(GetPeriod).ToArray(),
                Quotes = resultResponse.GetQuotes(timeZone),
                Dividends = resultResponse.GetDividends(timeZone),
                Splits = resultResponse.GetSplits(timeZone)
            };

            return result;
        }
    }
}