using Newtonsoft.Json;
using System.Collections.Generic;

namespace Fynance.Yahoo
{
	internal class YQuoteResponse
	{
		[JsonProperty("open")]
		public IList<decimal?> Open { get; set; }

		[JsonProperty("high")]
		public IList<decimal?> High { get; set; }

		[JsonProperty("low")]
		public IList<decimal?> Low { get; set; }

		[JsonProperty("close")]
		public IList<decimal?> Close { get; set; }

		[JsonProperty("volume")]
		public IList<decimal?> Volume { get; set; }
	}
}