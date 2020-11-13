using Newtonsoft.Json;

namespace Fynance.Yahoo
{
    internal class YSplitResponse
    {
        [JsonProperty("date")] 
        public double Date { get; set; }

        [JsonProperty("numerator")]
        public double Numerator { get; set; }

        [JsonProperty("denominator")] 
        public double Denominator { get; set; }

        [JsonProperty("splitRatio")]
        public string SplitRatio { get; set; }
    }
}