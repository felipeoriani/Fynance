using Newtonsoft.Json;
using System.Collections.Generic;

namespace Fynance.Yahoo
{
    internal class YQuoteResponse
    {
        [JsonProperty("open")]
        public IList<double?> Open { get; set; }

        [JsonProperty("high")]
        public IList<double?> High { get; set; }

        [JsonProperty("low")]
        public IList<double?> Low { get; set; }

        [JsonProperty("close")]
        public IList<double?> Close { get; set; }

        [JsonProperty("volume")]
        public IList<double?> Volume { get; set; }
    }
}