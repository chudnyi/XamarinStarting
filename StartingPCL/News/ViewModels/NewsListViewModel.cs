using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Collections.Generic;
using MvvmHelpers;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;

namespace StartingPCL
{
	public class NewsListViewModel : BaseViewModel
	{
		public INewsService NewsService { get; set; }

		public IRouter Router  { get; set; }

		public ObservableRangeCollection<ArticleViewModel> Articles { get; set; } = new ObservableRangeCollection<ArticleViewModel>();

		public Article SelectedArticle { get; set; }

		public NewsListViewModel ()
		{
			this.Title = "NY Times";
		}

		public void OnAppearing ()
		{
			Debug.WriteLine ("[NewsListViewModel] OnAppearing...");
//			await this.FetchArticlesAsync (TopStoriesCategory.automobiles);
//			await this.FetchArticlesAsync (TopStoriesCategory.travel);
//			await this.FetchArticlesAsync (TopStoriesCategory.food);
//			this.FetchArticlesAsync (TopStoriesCategory.sports);
//			this.FetchArticlesAsync (TopStoriesCategory.arts);
//			this.FetchArticlesAsync (TopStoriesCategory.technology);
			this.FetchAllArticlesAsync ();
		}

		async Task FetchAllArticlesAsync ()
		{
			await this.FetchArticlesAsync (TopStoriesCategory.automobiles);
			await this.FetchArticlesAsync (TopStoriesCategory.travel);
			await this.FetchArticlesAsync (TopStoriesCategory.food);
			await this.FetchArticlesAsync (TopStoriesCategory.sports);
			await this.FetchArticlesAsync (TopStoriesCategory.arts);
			await this.FetchArticlesAsync (TopStoriesCategory.technology);
		}

		async Task FetchArticlesAsync (TopStoriesCategory category = TopStoriesCategory.home)
		{
			if (this.IsBusy)
				return;

			this.IsBusy = true;

			try {
				Debug.WriteLine ($"[NewsListViewModel] FetchArticles {category.ToString()}...");
				var models = await this.NewsService.TopStories (category);
				Debug.WriteLine ($"articles received: {models.Count}");

//				this.SelectedArticle = null;
//				var viewModels = models.Select (article => new ArticleViewModel (article)).ToList ();
				var viewModels = from article in models
				                 select new ArticleViewModel (article);
//				var viewModels = new List<ArticleViewModel> { new ArticleViewModel (models[0]) };
//				var viewModels = new List<ArticleViewModel>();
//				var viewModels = models.ConvertAll (article => new ArticleViewModel (article));
//				this.Articles.ReplaceRange (viewModels);
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


	}
}

