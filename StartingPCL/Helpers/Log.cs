using System;
using System.Diagnostics;

namespace StartingPCL
{
	public class Log
	{
		public static void Info (string format, params object[] args) {
			Debug.WriteLine (string.Format (format, args));
		}

	}
}

