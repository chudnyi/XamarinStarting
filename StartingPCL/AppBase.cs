using System;
using Xamarin.Forms;

namespace StartingPCL
{
	public class AppBase : Application
	{
		public AppBase ()
		{
		}

		public static bool IsMainThread
		{
			get { return AppBase.IsMainThreadImpl (); }
		}

		protected static Func<bool> IsMainThreadImpl;

	}
}

