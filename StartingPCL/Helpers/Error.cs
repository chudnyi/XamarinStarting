using System;

namespace StartingPCL
{
	internal static class Error
	{
		internal static Exception ArgumentNull (string parameterName)
		{
			return new ArgumentNullException (parameterName);
		}

		internal static Exception ArgumentOutOfRange (string parameterName)
		{
			return new ArgumentOutOfRangeException (parameterName);
		}

		internal static Exception Argument (string parameterName, string format, params object[] args)
		{
			return new ArgumentException (string.Format (format, args), parameterName);
		}
	}
}

