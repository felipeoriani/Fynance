using Newtonsoft.Json;
using System.Collections.Generic;

namespace Fynance.Yahoo
{
    internal class YResultResponse
    {
        [JsonProperty("meta")]
        public YMetaResponse Meta { get; set; }

        [JsonProperty("timestamp")] 
        public IList<double?> TimeStamp { get; set; }

        [JsonProperty("indicators")] 
        public YIndicatorResponse Indicators { get; set; }

        [JsonProperty("events")] 
        public YEventResponse Events { get; set; }
    }
}