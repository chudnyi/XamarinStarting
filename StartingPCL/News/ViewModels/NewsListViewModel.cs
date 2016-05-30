using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Collections.Generic;
using MvvmHelpers;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using System.Reactive.Linq;

// http://www.codeproject.com/Articles/252392/Create-MVVM-Background-Tasks-with-Progress-Reporti

namespace StartingPCL
{
	public class NewsListViewModel : ViewModelBase
	{
		public INewsService NewsService { get; set; }

		public IRouter Router  { get; set; }

		public IViewModelsFactory ViewModelsFactory { get; set; }

		public IActionsFactory ActionsFactory { get; set; }


		public ObservableRangeCollection<ArticleViewModel> Articles { get; } = new ObservableRangeCollection<ArticleViewModel>();

		public Article SelectedArticle { get; set; }

//		public Action OnDisappearing{ get; private set; }

		public NewsListViewModel (IRouter router, IViewModelsFactory viewModelsFactory, IActionsFactory actionsFactory)
		{
			instanceCounter++;
			this.Title = $"NY Times #{instanceCounter}";

			if (router == null)
				throw Error.ArgumentNull (nameof (router));
			if (viewModelsFactory == null)
				throw Error.ArgumentNull (nameof (viewModelsFactory));
			if (actionsFactory == null)
				throw Error.ArgumentNull (nameof (actionsFactory));

			this.Router = router;
			this.ViewModelsFactory = viewModelsFactory;
			this.ActionsFactory = actionsFactory;
		}

	
		static int instanceCounter = 0;

		~NewsListViewModel ()
		{
			Log.Info ("[Finalize] {0}", this.GetType ().FullName);
		}

		private IDisposable storeSubscription;

		public async void OnAppearing ()
		{
			Debug.WriteLine ("[NewsListViewModel] OnAppearing...");
			base.OnAppearing ();

			storeSubscription = App.Store
				.DistinctUntilChanged (s => s.StateNews)
				.TakeUntil(Deactivated)
				.Subscribe (s => {
				var news = s.StateNews;
				this.IsBusy = news.IsBusy;
				var mvs = news.VisibleArticlesIds
						.Select (id => news.ArticlesById [id])
						.Select (model => this.ViewModelsFactory.ArticleViewModel (model));

				this.Articles.Clear ();
				this.Articles.AddRange (mvs);
					Log.Info ("reset articleViewModels: {0} Title: {1}", this.Articles.Count, this.Title);
			});


			if (this.ActionsFactory == null)
				throw Error.PropertyNull (nameof (this.ActionsFactory));

			await this.ActionsFactory.NewsFetchActionAsync (TopStoriesCategory.food).DispatchAsync ();


			Debug.WriteLine ("[NewsListViewModel] NewsFetchActionAsync complete");
//			if(this.Articles.Count == 0)
//				this.FetchAllArticlesAsync ();


		}
			
		//		async Task FetchAllArticlesAsync ()
		//		{
		//			await this.FetchArticlesAsync (TopStoriesCategory.automobiles);
		//			await this.FetchArticlesAsync (TopStoriesCategory.travel);
		//			await this.FetchArticlesAsync (TopStoriesCategory.food);
		//			await this.FetchArticlesAsync (TopStoriesCategory.sports);
		//			await this.FetchArticlesAsync (TopStoriesCategory.arts);
		//			await this.FetchArticlesAsync (TopStoriesCategory.technology);
		//			await this.FetchArticlesAsync (TopStoriesCategory.business);
		//			await this.FetchArticlesAsync (TopStoriesCategory.magazine);
		//			await this.FetchArticlesAsync (TopStoriesCategory.realestate);
		//			await this.FetchArticlesAsync (TopStoriesCategory.obituaries);
		//			await this.FetchArticlesAsync (TopStoriesCategory.fashion);
		//		}
		//
		async Task FetchArticlesAsync (TopStoriesCategory category)
		{
			if (this.IsBusy)
				return;

			this.IsBusy = true;

			try {
				Debug.WriteLine ($"[NewsListViewModel] FetchArticles {category.ToString()}...");
				var loadTask = this.NewsService.TopStories (category);

				var viewModels = await loadTask.ContinueWith (t => {
//					Debug.WriteLine ($"AppBase.IsMainThread = {AppBase.IsMainThread}");
					var objs = t.Result;
					// create view models in background
					var vms = from article in objs
					          select this.ViewModelsFactory.ArticleViewModel (article);
					
					return vms;
				});
				// TaskScheduler.FromCurrentSynchronizationContext()

				this.Articles.AddRange (viewModels);
				var count = this.Articles.Count;
				this.Title = $"NY Times ({count} articles)";

				this.IsBusy = false;

			} catch (Exception ex) {
				Debug.WriteLine (ex.ToString ());
				await this.Router.DisplayAlert ("FetchArticlesAsync", ex.Message, "OK");
			} finally {
				this.IsBusy = false;
			}
		}

		public void OnArticleSelected (ArticleViewModel articleViewModel)
		{
			if (articleViewModel == null)
				throw Error.ArgumentNull (nameof (articleViewModel));

			this.Router.routeArticleDetailsPage (articleViewModel.Model);
		}
	}

}

