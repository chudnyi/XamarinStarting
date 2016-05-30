using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace StartingPCL
{
	public partial class SecondPage : ContentPage
	{
		public SecondPage ()
		{
			InitializeComponent ();
		}

		~SecondPage ()
		{
			Log.Info ("[Finalize] {0}", this.GetType ().FullName);
		}
	}
}

