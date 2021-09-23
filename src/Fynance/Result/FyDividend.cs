namespace Fynance.Result
{
	using System;

	/// <summary>
	/// Dividend instance.
	/// </summary>
	public class FyDividend
	{
		/// <summary>
		/// When the dividend was defined.
		/// </summary>
		public DateTime Date { get; set; }

		/// <summary>
		/// Dividend value.
		/// </summary>
		public decimal Value { get; set; }
	}
}