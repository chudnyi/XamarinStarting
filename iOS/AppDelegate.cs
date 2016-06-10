using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using StartingPCL;
using StartingPCL.Helpers;
using UIKit;

namespace Starting02.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
//			FFImageLoading.Forms.Touch.CachedImageRenderer.Init();
			global::Xamarin.Forms.Forms.Init ();

            App.ImageResizer = new ImageResizer();
            LoadApplication (new StartingPCL.App ());

			return base.FinishedLaunching (app, options);
		}
	}
}

