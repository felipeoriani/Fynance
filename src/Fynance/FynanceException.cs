namespace Fynance
{
	using System;
	using System.Net;

	public class FynanceException : Exception
	{
		public string Code { get; set; }

		public string Symbol { get; set; }

		public Period? Period { get; set; }

		public Interval? Interval { get; set; }

		public HttpStatusCode StatusCode { get; set; }

		public FynanceException(string code, string message)
			: this(code, message, null) { }

		public FynanceException(string code, string message, Exception innerException)
			: base($"{code}: {message}", innerException)
		{
			Code = code;
		}
	}
}