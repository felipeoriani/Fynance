namespace Fynance.Yahoo
{
	using Newtonsoft.Json;
	using System.Collections.Generic;

	internal class YChartResponse
	{
		[JsonProperty("result")]
		public IList<YResultResponse> Result { get; set; }

		[JsonProperty("error")]
		public YErrorResponse Error { get; set; }
	}
}