using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using StartingPCL;

namespace StartingShared
{
	public class StackNavigationRouter : IRouter
	{
		NavigationPage navigationPage { get; }

		public StackNavigationRouter (NavigationPage navigationPage)
		{
			this.navigationPage = navigationPage;
		}

		public Task DisplayAlert (string title, string message, string cancel)
		{
			var page = this.navigationPage;
			return page.DisplayAlert (title, message, cancel);
		}

		public Task<bool> DisplayAlert (string title, string message, string accept, string cancel)
		{
			var page = this.navigationPage;
			return page.DisplayAlert (title, message, accept, cancel);
		}

		public Task<string> DisplayActionSheet (string title, string cancel, string destruction, params string[] buttons)
		{
			var page = this.navigationPage;
			return page.DisplayActionSheet (title, cancel, destruction, buttons);
		}


		public void routeSecondPage ()
		{
			this.navigationPage.PushAsync (new SecondPage {
				Title = "Second"
			});
		}

		public void routeNewsPage ()
		{
			NewsListViewModel viewModel = new NewsListViewModel {
				NewsService = new NYTimesNewsService (),
				Router = this
			};
			
			this.navigationPage.PushAsync (new StartingPCL.NewsListPage () {
				ViewModel = viewModel
			});
		}

		public void routeArticleDetailsPage ()
		{
			throw new NotImplementedException ();
		}
	}
}

