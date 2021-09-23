namespace Fynance.Result
{
	using System;

	/// <summary>
	/// Representation of a slipt event.
	/// </summary>
	public class FySplit
	{
		public DateTime Date { get; set; }

		public decimal Numberator { get; set; }

		public decimal Denominator { get; set; }

		public string Ratio { get; set; }
	}
}