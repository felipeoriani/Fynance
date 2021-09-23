namespace Fynance.Yahoo
{
	using Newtonsoft.Json;

	internal class YDividendResponse
	{
		[JsonProperty("amount")]
		public decimal Amount { get; set; }

		[JsonProperty("date")]
		public double Date { get; set; }
	}
}