namespace Fynance.Yahoo
{
	using Newtonsoft.Json;
	using System.Collections.Generic;

	internal class YEventResponse
	{
		[JsonProperty("dividends")]
		public Dictionary<double, YDividendResponse> Dividends { get; set; }

		[JsonProperty("splits")]
		public Dictionary<double, YSplitResponse> Splits { get; set; }
	}
}