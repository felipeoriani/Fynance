using Newtonsoft.Json;
using System.Collections.Generic;

namespace Fynance.Yahoo
{
	internal class YIndicatorResponse
	{
		[JsonProperty("quote")]
		public IList<YQuoteResponse> Quote { get; set; }

		[JsonProperty("adjclose")]
		public IList<YIndicatorAdjCloseResponse> AdjClose { get; set; }
	}
}