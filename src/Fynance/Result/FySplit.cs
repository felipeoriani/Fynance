namespace Fynance.Result
{
	using System;

	/// <summary>
	/// Representation of a slipt event.
	/// </summary>
	public class FySplit
	{
		/// <summary>
		/// Split date.
		/// </summary>
		public DateTime Date { get; set; }

		/// <summary>
		/// Split numerator.
		/// </summary>
		public decimal Numberator { get; set; }

		/// <summary>
		/// Split denominator.
		/// </summary>
		public decimal Denominator { get; set; }

		/// <summary>
		/// Split ratio.
		/// </summary>
		public string Ratio { get; set; }
	}
}