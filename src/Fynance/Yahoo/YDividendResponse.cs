using Newtonsoft.Json;

namespace Fynance.Yahoo
{
	internal class YDividendResponse
	{
		[JsonProperty("amount")]
		public decimal Amount { get; set; }

		[JsonProperty("date")]
		public double Date { get; set; }
	}
}