using Newtonsoft.Json;
using System.Collections.Generic;

namespace Fynance.Yahoo
{
    internal class YIndicatorAdjCloseResponse
    {
        [JsonProperty("adjclose")]
        public IList<double?> AdjClose { get; set; }
    }
}
