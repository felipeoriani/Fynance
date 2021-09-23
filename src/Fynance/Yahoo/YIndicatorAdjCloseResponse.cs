namespace Fynance.Yahoo
{
	using Newtonsoft.Json;
	using System.Collections.Generic;

	internal class YIndicatorAdjCloseResponse
	{
		[JsonProperty("adjclose")]
		public IList<decimal?> AdjClose { get; set; }
	}
}