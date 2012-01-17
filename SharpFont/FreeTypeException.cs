using System;

namespace SharpFont
{
	public class FreeTypeException : Exception
	{
		private Error error;

		public Error Error { get { return error; } }

		public FreeTypeException()
			: base()
		{
			error = Error.Ok;
		}

		public FreeTypeException(string message)
			: base(message)
		{
			error = Error.Ok;
		}

		public FreeTypeException(string message, Exception innerException)
			: base(message, innerException)
		{
			error = Error.Ok;
		}

		public FreeTypeException(Error error)
			: base(error.ToString())
		{
			this.error = error;
		}
	}
}
