using Newtonsoft.Json;

namespace Fynance.Yahoo
{
    internal class YErrorResponse
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}