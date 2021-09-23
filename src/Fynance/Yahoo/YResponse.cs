namespace Fynance.Yahoo
{
	using Newtonsoft.Json;

	internal class YResponse
	{
		[JsonProperty("chart")]
		public YChartResponse Chart { get; set; }
	}
}