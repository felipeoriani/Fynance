using System;
using System.Threading.Tasks;

namespace Fynance
{
    using Result;

    /// <summary>
    /// An object of this class must represent a ticker (symbol) for a security. 
    /// This is an abstract fluent implementation to define how to get data from the stock market.
    /// By default an instance of Ticker will use Period of one month, interval of days and TimeZoneInfo.Local.
    /// </summary>
    public abstract class Ticker
    {
        #region [Properties]

        /// <summary>
        /// Security symbol.
        /// </summary>
        public string Symbol { get; protected set; }

        /// <summary>
        /// Period of data. The default value is <c>Period.OneMonth</c>.
        /// </summary>
        public Period Period { get; protected set; }

        /// <summary>
        /// Interval of data. The default value is <c>Interval.OneDay</c>.
        /// </summary>
        public Interval Interval { get; protected set; }

        /// <summary>
        /// StartDate of data.
        /// </summary>
        public DateTime? StartDate { get; protected set; }

        /// <summary>
        /// FinishDate of data.
        /// </summary>
        public DateTime? FinishDate { get; protected set; }

        /// <summary>
        /// TimeZone to convert the dateTime received data. if there is no definition, it does not change the value.
        /// </summary>
        public TimeZoneInfo TimeZone { get; protected set; }

        /// <summary>
        /// It contain the dividends events.
        /// </summary>
        public bool Dividends { get; protected set; }

        /// <summary>
        /// It contain the split events.
        /// </summary>
        public bool Splits { get; protected set; }

        /// <summary>
        /// It represents the last valid result after the Get/GetAsync method is invoked.
        /// </summary>
        public FyResult Result { get; protected set; }

        #endregion

        protected Ticker() { }

        protected Ticker(string symbol)
        {
            this.SetSymbol(symbol)
                .SetPeriod(Period.OneMonth)
                .SetInterval(Interval.OneDay);
        }            

        #region [Methods]

        public virtual Ticker SetSymbol(string ticker)
        {
            this.Symbol = ticker;
            return this;
        }

        public virtual Ticker SetPeriod(Period period)
        {
            this.Period = period;
            return this;
        }

        public virtual Ticker SetInterval(Interval interval)
        {
            this.Interval = interval;
            return this;
        }

        public virtual Ticker SetInterval(DateTime startDate, DateTime finishDate)
            => SetStartDate(startDate).SetFinishDate(finishDate);

        public virtual Ticker SetStartDate(DateTime startDate)
        {
            this.StartDate = startDate;
            return this;
        }

        public virtual Ticker SetFinishDate(DateTime finishDate)
        {
            this.FinishDate = finishDate;
            return this;
        }

        public virtual Ticker SetLocalTimeZone() 
            => SetTimeZone(TimeZoneInfo.Local);

        public virtual Ticker SetTimeZone(TimeZoneInfo timeZone)
        {
            this.TimeZone = timeZone;
            return this;
        }

        public virtual Ticker SetDividends(bool dividends) 
        {
            this.Dividends = dividends;
            return this;
        }

        public virtual Ticker SetSplits(bool splits)
        {
            this.Splits = splits;
            return this;
        }

        public virtual Ticker SetEvents(bool events) 
            => SetDividends(events).SetSplits(events);

        public FyResult Get() => GetAsync().Result;

        public abstract Task<FyResult> GetAsync();

        #endregion

        public static Ticker Build() => new YahooTicker();

        public static Ticker Build(string ticker) => new YahooTicker(ticker);

        public static Ticker BuildYahoo(string ticker) => new YahooTicker(ticker);
    }
}