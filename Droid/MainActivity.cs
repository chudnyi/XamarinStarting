﻿using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Starting02.Droid
{
	[Activity (Label = "Starting02.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
//			FFImageLoading.Forms.Droid.CachedImageRenderer.Init();


			global::Xamarin.Forms.Forms.Init (this, bundle);

			LoadApplication (new StartingPCL.App ());
		}
	}
}

