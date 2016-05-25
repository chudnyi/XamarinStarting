using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Collections.Generic;
using MvvmHelpers;
using System.Linq.Expressions;
using System.Linq;

namespace StartingShared
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
			this.FetchArticlesAsync ();
		}

		async void FetchArticlesAsync ()
		{
			if (this.IsBusy)
				return;

			this.IsBusy = true;

			try {
				Debug.WriteLine ("[NewsListViewModel] FetchArticles...");
				var models = await this.NewsService.TopStories ();
				Debug.WriteLine ($"articles received: {models.Count}");

				this.SelectedArticle = null;
				var viewModels = models.Select (article => new ArticleViewModel (article));
				this.Articles.ReplaceRange (viewModels.ToList());

			} catch (Exception ex) {
				throw ex; // TODO: handle errors
			} finally {
				this.IsBusy = false;
			}


		}


	}
}

