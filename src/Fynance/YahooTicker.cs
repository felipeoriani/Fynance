namespace Fynance
{
	using Newtonsoft.Json;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Net.Http;
	using System.Threading.Tasks;
	using Yahoo;
	using Result;

	/// <summary>
	/// Implementation of Ticker for Yahoo Finance.
	/// </summary>
	public class YahooTicker : Ticker, IDisposable
	{
		#region [ctor]

		public YahooTicker() 
		{
		}

		public YahooTicker(string symbol)
			: base(symbol)
		{
		}

		public YahooTicker(HttpClient client)
		{
			Client = client;
		}

		public YahooTicker(string symbol, HttpClient client)
			: base(symbol)
		{
			Client = client;
		}

		#endregion

		#region [Methods]

		/// <inheritdoc />
		public override async Task<FyResult> GetAsync()
		{
			// Get the query string argumentrs for the yahoo finance route.
			var queryString = GetQueryStringParameters();

			// Build the full route.
			var url = $"{YUtils.BaseUrl}/v8/finance/chart/{Symbol}?{queryString}";

			// Get the http response from the http call on the given route.
			HttpResponseMessage response = await GetResponse(url).ConfigureAwait(false);

			string responseBody = null;
			YResponse yResponse = null;

			// Read all the content whe the request succeed.
			if (response.IsSuccessStatusCode)
				responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

			// Deserialize all the content
			if (!string.IsNullOrWhiteSpace(responseBody))
				yResponse = JsonConvert.DeserializeObject<YResponse>(responseBody);

			// When the http request does not succeed
			var error = yResponse?.Chart?.Error;
			if (!response.IsSuccessStatusCode || yResponse == null || error != null)
			{
				string code = "Fynance.Yahoo";
				string message = "This result was not possible to get from Yahoo Finance.";

				if (error != null)
				{
					code = error.Code;
					message = error.Description;
				}

				// Throw an exception for the current error
				throw new FynanceException(code, message)
				{
					Symbol = this.Symbol,
					Period = this.Period,
					Interval = this.Interval,
					StatusCode = response.StatusCode
				};
			}

			try
			{
				// Once the request was performed fine, the results are prepared based on the TimeZone.
				Result = yResponse.GetResult(TimeZone);

				// Define the splits.
				if (Splits)
				{
					Result.Splits = Result.Splits ?? new FySplit[0];
				}

				// Define the dividends.
				if (Dividends)
				{
					Result.Dividends = Result.Dividends ?? new FyDividend[0];
				}
			}
			catch (Exception ex)
			{
				throw new FynanceException("Fynance.Yahoo", "An error occurred while trying to fetch the results. Please, check the InnerException for more details.", ex);
			}

			return Result;
		}

		/// <summary>
		/// Get a http response message from the client instance.
		/// </summary>
		/// <param name="url">Url to invoke.</param>
		/// <returns>An instance of HttpResponseMessage.</returns>
		private async Task<HttpResponseMessage> GetResponse(string url)
		{
			if (Client == null)
			{
				Client = new HttpClient(new HttpClientHandler { UseProxy = false });
                 	        Client.DefaultRequestHeaders.Add("User-Agent", "Fynance");
			}

			return await Client.GetAsync(url).ConfigureAwait(false);
		}

		/// <summary>
		/// Prepare the query string arguments for Yahoo Finance request.
		/// </summary>
		/// <returns>An url with all containing arguments.</returns>
		private string GetQueryStringParameters()
		{
			var queryStringParameters = new Dictionary<string, object>();

			// When there are definitions for 'StartDate' or 'FinishDate' then use it as arguments.
			if (StartDate != null || FinishDate != null)
			{
				// Set default timestamp for 'StartDate' when it is not defined.
				if (StartDate == null)
					StartDate = YUtils.DefaultDateTime;

				// Set current datetime for' FinishDate' when it is not defined.
				if (FinishDate == null)
					FinishDate = DateTime.Now;

				// Validate the Start/Finish interval.
				if (StartDate > FinishDate)
					throw new ArgumentOutOfRangeException("StartDate", "The StartDate can not be greater than FinishDate.");

				var period1 = (long) YUtils.GetTimestampFromDateTime(StartDate.Value);
				var period2 = (long) YUtils.GetTimestampFromDateTime(FinishDate.Value);

				// YahooFinance expect two parameters called 'period1' and 'period1' as timeStamps values.
				queryStringParameters.Add(nameof(period1), period1);
				queryStringParameters.Add(nameof(period2), period2);
			}
			else
			{
				// When there is no definition for Start/Finish dates.
				// We can must use the Period property which is available on the enumerator YPeriod.
				// Get the valida format for periods.
				var range = YUtils.GetPeriod(Period);

				// Use the range parameter.
				queryStringParameters.Add(nameof(range), range);
			}

			// Add the interval based on Interval property.
			var interval = YUtils.GetInterval(Interval == Interval.ThirtyMinutes ? Interval.FifteenMinutes : Interval);

			// Use the interval parameter.
			queryStringParameters.Add(nameof(interval), interval);

			var events = new List<string>();

			// Define events for dividends and splits.
			if (Dividends) events.Add("div");
			if (Splits) events.Add("splits");

			// set the argumento for events when defined.
			if (events.Any())
			{
				queryStringParameters.Add("events", string.Join(",", events));
			}

			// build the queryString parameters.
			var queryString = string.Join("&", queryStringParameters.Select(x => $"{x.Key}={x.Value}"));

			return queryString;
		}

		#endregion
	}
}
