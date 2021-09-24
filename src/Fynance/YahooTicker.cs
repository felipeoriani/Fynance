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
	public class YahooTicker : Ticker
	{
		#region [ctor]

		public YahooTicker() { }

		public YahooTicker(string symbol)
			: base(symbol)
		{
		}

		#endregion

		#region [Methods]

		public override async Task<FyResult> GetAsync()
		{
			var queryString = GetQueryStringParameters();

			var url = $"{YUtils.BaseUrl}/v8/finance/chart/{Symbol}?{queryString}";

			HttpResponseMessage response = await GetResponse(url).ConfigureAwait(false);

			string responseBody = null;
			if (response.IsSuccessStatusCode)
				responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
			
			YResponse yResponse = null;

			if (!string.IsNullOrWhiteSpace(responseBody))
				yResponse = JsonConvert.DeserializeObject<YResponse>(responseBody);

			if (!response.IsSuccessStatusCode || yResponse == null)
			{
				var error = yResponse?.Chart?.Error;

				string code = "Fynance.Yahoo";
				string message = "This result was not possible to get from Yahoo Finance.";

				if (error != null)
				{
					code = error.Code;
					message = error.Description;
				}

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
				Result = yResponse.GetResult(this.TimeZone);
			}
			catch (Exception ex)
			{
				throw new FynanceException("Fynance.Yahoo", "An error occurred while trying to fetch the results. Please, check the InnerException for more details.", ex);
			}

			return Result;
		}

		private async Task<HttpResponseMessage> GetResponse(string url)
		{
			var handler = new HttpClientHandler();
			handler.UseProxy = false;
			using (var http = new HttpClient(handler))
			{
				return await http.GetAsync(url).ConfigureAwait(false);
			}
		}

		private string GetQueryStringParameters()
		{
			var queryStringParameters = new Dictionary<string, object>();

			// If there are definitions for 'StartDate' or 'FinishDate' then use it as arguments.
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

				var period1 = (long)YUtils.GetTimestampFromDateTime(StartDate.Value);
				var period2 = (long)YUtils.GetTimestampFromDateTime(FinishDate.Value);

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

			if (Dividends) events.Add("div");
			if (Splits) events.Add("splits");

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