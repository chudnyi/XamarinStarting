using System;
using Xamarin.Forms;

namespace StartingShared
{
	public interface IRouter
	{

		void routeSecondPage ();

		void routeNewsPage ();

	}

	public class StackNavigationRouter : IRouter
	{
		NavigationPage navigationPage { get; }

		public StackNavigationRouter (NavigationPage navigationPage)
		{
			this.navigationPage = navigationPage;
		}

		public void routeSecondPage ()
		{
			this.navigationPage.PushAsync (new SecondPage {
				Title = "Second"
			});
		}

		public void routeNewsPage ()
		{
			
			this.navigationPage.PushAsync (new NewsListPage () {
				Title = "News",
				newsService = new NYTimesNewsService ()
			});
		}
	}
}

