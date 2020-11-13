using Newtonsoft.Json;

namespace Fynance.Yahoo
{
    internal class YDividendResponse
    {
        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("date")]
        public double Date { get; set; }
    }
}