namespace Fynance
{
	using System;
	using System.Net.Http;
	using System.Threading;
	using System.Threading.Tasks;
	using Result;

	/// <summary>
	/// An object of this class must represent a ticker (symbol) for a security. 
	/// This is an abstract fluent implementation to define how to get data from the stock market.
	/// By default an instance of Ticker will use Period of one month, interval of days and TimeZoneInfo.Local.
	/// </summary>
	public abstract class Ticker : IDisposable
	{
		#region Properties

		protected HttpClient Client { get; set; }

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

		/// <summary>
		/// It contains the information about the user-agent http request header. The default value is fynance, but you can set your own application.
		/// </summary>
		public string UserAgent { get; protected set; } = "fynance";

		#endregion

		#region Construtors

		protected Ticker() { }

		protected Ticker(string symbol)
		{
			this.SetSymbol(symbol)
				.SetPeriod(Period.OneMonth)
				.SetInterval(Interval.OneDay);
		}

		#endregion

		#region Methods

		/// <summary>
		/// Set a symbol (ticker) of a security.
		/// </summary>
		/// <param name="ticker">Symbol.</param>
		/// <returns>The instance itself.</returns>
		public virtual Ticker SetSymbol(string ticker)
		{
			this.Symbol = ticker;
			return this;
		}

		/// <summary>
		/// Set a Period to get data.
		/// </summary>
		/// <param name="period">Period.</param>
		/// <returns>The instance itself.</returns>
		public virtual Ticker SetPeriod(Period period)
		{
			this.Period = period;
			this.StartDate = this.FinishDate = null;
			return this;
		}

		/// <summary>
		/// Set the interval of the data.
		/// </summary>
		/// <param name="interval">Interval.</param>
		/// <returns>The instance itself.</returns>
		public virtual Ticker SetInterval(Interval interval)
		{
			this.Interval = interval;
			return this;
		}

		/// <summary>
		/// Set interval between two dates.
		/// </summary>
		/// <param name="startDate">Start date.</param>
		/// <param name="finishDate">Finish date.</param>
		/// <returns>The instance itself.</returns>
		public virtual Ticker SetInterval(DateTime startDate, DateTime finishDate)
			=> SetStartDate(startDate).SetFinishDate(finishDate);

		/// <summary>
		/// Set the start date. 
		/// </summary>
		/// <param name="startDate">Start date.</param>
		/// <returns>The instance itself.</returns>
		public virtual Ticker SetStartDate(DateTime startDate)
		{
			this.Period = Period.Custom;
			this.StartDate = startDate;
			return this;
		}

		/// <summary>
		/// Set the finish date.
		/// </summary>
		/// <param name="finishDate">Finish date.</param>
		/// <returns>The instance itself.</returns>
		public virtual Ticker SetFinishDate(DateTime finishDate)
		{
			this.Period = Period.Custom;
			this.FinishDate = finishDate;
			return this;
		}

		/// <summary>
		/// Set the local timezone.
		/// </summary>
		/// <returns>The instance itself.</returns>
		public virtual Ticker SetLocalTimeZone()
			=> SetTimeZone(TimeZoneInfo.Local);

		/// <summary>
		/// Set a timezone to convert the date/time output.
		/// </summary>
		/// <param name="timeZone">Timezone</param>
		/// <returns>The instance itself.</returns>
		public virtual Ticker SetTimeZone(TimeZoneInfo timeZone)
		{
			this.TimeZone = timeZone;
			return this;
		}

		/// <summary>
		/// Set if the response should hold dividends.
		/// </summary>
		/// <param name="dividends">Dividends flag.</param>
		/// <returns>The instance itself.</returns>
		public virtual Ticker SetDividends(bool dividends)
		{
			this.Dividends = dividends;
			return this;
		}

		/// <summary>
		/// Set if the response should hold splits.
		/// </summary>
		/// <param name="splits">Splits flag.</param>
		/// <returns>The instance itself.</returns>
		public virtual Ticker SetSplits(bool splits)
		{
			this.Splits = splits;
			return this;
		}

		/// <summary>
		/// Set if the response should hold events (Dividends and Splits).
		/// </summary>
		/// <param name="events">Events flags</param>
		/// <returns>The instance itself.</returns>
		public virtual Ticker SetEvents(bool events)
			=> SetDividends(events).SetSplits(events);

		public virtual Ticker SetHttpClient(HttpClient client)
		{
			if (Client != null)
				Client.Dispose();

			Client = client;

			return this;
		}

		public virtual Ticker SetUserAgent(string userAgent)
		{
			this.UserAgent = userAgent;
			return this;
		}

		/// <summary>
		/// Sync implementation to get the results from the predefined settings.
		/// </summary>
		/// <returns>An instance of FyResult containing the result.</returns>
		public FyResult Get()
			=> _taskFactory.StartNew(GetAsync).Unwrap().GetAwaiter().GetResult();

		/// <summary>
		/// Async implementation to get a result from the predefined settings.
		/// </summary>
		/// <returns>An instance of FyResult containing the result.</returns>
		public abstract Task<FyResult> GetAsync();

		/// <inheritdoc />
		public void Dispose()
		{
			if (Client != null)
				Client.Dispose();
		}

		#endregion

		#region Static

		/// <summary>
		/// Build an instance of the Ticker.
		/// Currently this package just support <see cref="YahooTicker"/> implementation.
		/// </summary>
		/// <returns>An instance of the <see cref="Ticker"/>.</returns>
		public static Ticker Build() => new YahooTicker();

		/// <summary>
		/// Build an instance of the Ticker for a given symbol.
		/// Currently this package just support <see cref="YahooTicker"/> implementation.
		/// </summary>
		/// <param name="symbol">Symbol</param>
		/// <returns>An instance of the <see cref="Ticker"/>.</returns>
		public static Ticker Build(string symbol) => new YahooTicker(symbol);

		/// <summary>
		/// Build an instance of the <see cref="YahooTicker"/> for a given symbol.
		/// </summary>
		/// <param name="symbol"></param>
		/// <returns>An instance of the <see cref="Ticker"/>.</returns>
		public static Ticker BuildYahoo(string symbol) => new YahooTicker(symbol);

		private static readonly TaskFactory _taskFactory = new TaskFactory(CancellationToken.None,
			TaskCreationOptions.None,
			TaskContinuationOptions.None,
			TaskScheduler.Default);

		#endregion
	}
}