using System;
using System.Collections.Generic;
using Xamarin.Forms;
//using Refit;

namespace StartingShared
{
	public partial class NewsListPage : ContentPage
	{
		public INewsService newsService { get; set;}

		public NewsListPage ()
		{
			InitializeComponent ();

		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			System.Diagnostics.Debug.WriteLine("OnAppearing");

			this.FetchArticles ();
		}

		async void FetchArticles() {
			var result = await this.newsService.TopStories ();
//			this.articlesCountLabel.Text = $"Articles status: {result.status}";

			System.Diagnostics.Debug.WriteLine($"articles received: {result.Count}");
		}
	}
}

