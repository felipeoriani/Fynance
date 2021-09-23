namespace Fynance.Yahoo
{
	using Newtonsoft.Json;

	internal class YErrorResponse
	{
		[JsonProperty("code")]
		public string Code { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }
	}
}