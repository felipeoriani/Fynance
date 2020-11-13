using Newtonsoft.Json;

namespace Fynance.Yahoo
{
    internal class YResponse
    {
        [JsonProperty("chart")]
        public YChartResponse Chart { get; set; }
    }
}