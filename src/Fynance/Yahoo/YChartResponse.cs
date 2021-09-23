using Newtonsoft.Json;
using System.Collections.Generic;

namespace Fynance.Yahoo
{
	internal class YChartResponse
	{
		[JsonProperty("result")]
		public IList<YResultResponse> Result { get; set; }

		[JsonProperty("error")]
		public YErrorResponse Error { get; set; }
	}
}