using Newtonsoft.Json;
using System.Collections.Generic;

namespace Fynance.Yahoo
{
	internal class YEventResponse
	{
		[JsonProperty("dividends")]
		public Dictionary<double, YDividendResponse> Dividends { get; set; }

		[JsonProperty("splits")]
		public Dictionary<double, YSplitResponse> Splits { get; set; }
	}
}