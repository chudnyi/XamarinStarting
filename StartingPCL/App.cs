using System;
using Xamarin.Forms;

namespace StartingPCL
{
	public class App : Application
	{
		//		static int mainThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
		public static Redux.IStore<State> Store { get;} = new Redux.Store<State> (Reducer.Execute, new State ());


		IRouter router { get; set; }

		AppSetup appBuilder { get; set; }

		public App ()
		{
//			AppBase.IsMainThreadImpl = () => {
//				return System.Threading.Thread.CurrentThread.ManagedThreadId == mainThreadId;
//			};



			Store.Subscribe (s => {
				Log.Info ("[Store] changed: {0}", s);
			});

			Store.Subscribe (s => {
				Log.Info ("[Store] (2) changed: {0}", s);
			});



			this.appBuilder = new AppSetup ();
			this.router = this.appBuilder.Router;

			// The root page of your application
			MainPage = this.router.mainPage ();
			MainPage.BackgroundColor = Color.FromRgb (240, 240, 250);

//			router.routeNewsPage ();
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

