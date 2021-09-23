namespace Fynance.Result
{
	using System;

	/// <summary>
	/// Prices in a period of time.
	/// </summary>
	public class FyQuote
	{
		/// <summary>
		/// DateTime of the period.
		/// </summary>
		public DateTime Period { get; set; }

		/// <summary>
		/// Low price of the period.
		/// </summary>
		public decimal Low { get; set; }

		/// <summary>
		/// Close price of the period.
		/// </summary>
		public decimal Close { get; set; }

		/// <summary>
		/// Open price of the period.
		/// </summary>
		public decimal Open { get; set; }

		/// <summary>
		/// High price of the period.
		/// </summary>
		public decimal High { get; set; }

		/// <summary>
		/// AdjClose price of the period.
		/// </summary>
		public decimal AdjClose { get; set; }

		/// <summary>
		/// Volume of the period.
		/// </summary>
		public decimal Volume { get; set; }
	}
}