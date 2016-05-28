using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using StartingPCL;

namespace StartingShared
{
	/// <summary>
	/// Stack navigation router and application components factory.
	/// TODO: Extract components factory logic to external class (single responsibility principe).
	/// </summary>
	public class StackNavigationRouter : IRouter, IViewModelsFactory
	{
		private NavigationPage navigationPage { get; set; }

		//		public StackNavigationRouter (NavigationPage navigationPage)
		//		{
		//			this.navigationPage = navigationPage;
		//		}

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

		public Xamarin.Forms.Page mainPage ()
		{
			if (navigationPage == null) {
				navigationPage = new NavigationPage (this.CreateMyFirstPage());
			}

			return navigationPage;
		}

		public void routeSecondPage ()
		{
			this.navigationPage.PushAsync (new SecondPage {
				Title = "Second"
			});
		}

		public void routeNewsPage ()
		{
			this.navigationPage.PushAsync (new StartingPCL.NewsListPage () {
				ViewModel = this.NewsListViewModel ()
			});
		}

		public void routeArticleDetailsPage (Article model)
		{
			if (model == null)
				throw new ArgumentNullException ("model");
			
			this.navigationPage.PushAsync (new StartingPCL.ArticleDetailsPage () {
				ViewModel = this.ArticleViewModel (model)
			});
		}

		private MyFirstPage CreateMyFirstPage() {
			return new MyFirstPage() {
				Router = this
			};
		}

		public ArticleViewModel ArticleViewModel (Article model)
		{
			if (model == null)
				throw new ArgumentNullException ("model");

			return new ArticleViewModel (model) {
			};
		}

		public NewsListViewModel NewsListViewModel ()
		{
			return new NewsListViewModel {
				NewsService = new NYTimesNewsService (),
				Router = this,
				ViewModelsFactory = this
			};
		}
	}
}

