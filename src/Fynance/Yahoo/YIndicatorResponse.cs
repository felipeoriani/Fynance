namespace Fynance.Yahoo
{
	using Newtonsoft.Json;
	using System.Collections.Generic;

	internal class YIndicatorResponse
	{
		[JsonProperty("quote")]
		public IList<YQuoteResponse> Quote { get; set; }

		[JsonProperty("adjclose")]
		public IList<YIndicatorAdjCloseResponse> AdjClose { get; set; }
	}
}