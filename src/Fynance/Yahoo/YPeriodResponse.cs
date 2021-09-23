namespace Fynance.Yahoo
{
	using Newtonsoft.Json;

	internal class YPeriodResponse
	{
		[JsonProperty("timezone")]
		public string TimeZone { get; set; }

		[JsonProperty("start")]
		public int Start { get; set; }

		[JsonProperty("end")]
		public int End { get; set; }

		[JsonProperty("gmtoffset")]
		public int GTMOffSet { get; set; }
	}
}