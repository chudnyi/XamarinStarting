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
	public class NewsListViewModel : BaseViewModel
	{
		public INewsService NewsService { get; set; }

		public IRouter Router  { get; set; }

		public IViewModelsFactory ViewModelsFactory { get; set; }


		public ObservableRangeCollection<ArticleViewModel> Articles { get; set; } = new ObservableRangeCollection<ArticleViewModel>();

		public Article SelectedArticle { get; set; }

		public NewsListViewModel ()
		{
			this.Title = "NY Times";

			var stateNewsSeq = App.Store
				.DistinctUntilChanged (state => state.StateNews)
				.Select (state => state.StateNews);

			stateNewsSeq
				.Select (state => state.IsBusy)
				.DistinctUntilChanged ()
				.Subscribe (val => {
				this.IsBusy = val;
			});
				
			stateNewsSeq
				.DistinctUntilChanged (state => state.VisibleArticlesIds)
				.Select (state => state.VisibleArticlesIds.Select (id => state.ArticlesById [id]))
				.Select (articles => articles.Select (article => this.ViewModelsFactory.ArticleViewModel (article)))
				.Subscribe (articleViewModels => {
				
					this.Articles.Clear ();
					this.Articles.AddRange (articleViewModels);
			});
		}

		public async void OnAppearing ()
		{
			Debug.WriteLine ("[NewsListViewModel] OnAppearing...");

//			NewsActionCreator.CreateNewsFetchAction (TopStoriesCategory.food).Dispatch ();
			await NewsActionCreator.NewsFetchActionAsync (TopStoriesCategory.food).DispatchAsync ();


			Debug.WriteLine ("[NewsListViewModel] NewsFetchActionAsync complete");
//			if(this.Articles.Count == 0)
//				this.FetchAllArticlesAsync ();
		}

		async Task FetchAllArticlesAsync ()
		{
			await this.FetchArticlesAsync (TopStoriesCategory.automobiles);
			await this.FetchArticlesAsync (TopStoriesCategory.travel);
			await this.FetchArticlesAsync (TopStoriesCategory.food);
			await this.FetchArticlesAsync (TopStoriesCategory.sports);
			await this.FetchArticlesAsync (TopStoriesCategory.arts);
			await this.FetchArticlesAsync (TopStoriesCategory.technology);
			await this.FetchArticlesAsync (TopStoriesCategory.business);
			await this.FetchArticlesAsync (TopStoriesCategory.magazine);
			await this.FetchArticlesAsync (TopStoriesCategory.realestate);
			await this.FetchArticlesAsync (TopStoriesCategory.obituaries);
			await this.FetchArticlesAsync (TopStoriesCategory.fashion);
		}

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
				throw new ArgumentNullException ("articleViewModel");

			this.Router.routeArticleDetailsPage (articleViewModel.Model);
		}

	}

}

