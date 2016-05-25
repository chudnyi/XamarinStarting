using System;

using Xamarin.Forms;
//using static System.Math;


namespace StartingShared
{
	public class App : Application
	{
		IRouter router { get; set;}

		public App ()
		{
			MyFirstPage startPage = new MyFirstPage ();

			var navPage = new NavigationPage (startPage);
			this.router = new StackNavigationRouter (navPage);
			startPage.router = this.router;

			// The root page of your application
			MainPage = navPage;
			MainPage.BackgroundColor = Color.FromRgb (240, 240, 250);
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

