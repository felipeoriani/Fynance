namespace Fynance.Yahoo
{
	using Newtonsoft.Json;

	internal class YCurrentTradingPeriodResponse
	{
		[JsonProperty("pre")]
		public YPeriodResponse Pre { get; set; }

		[JsonProperty("regular")]
		public YPeriodResponse Regular { get; set; }

		[JsonProperty("post")]
		public YPeriodResponse Post { get; set; }
	}
}