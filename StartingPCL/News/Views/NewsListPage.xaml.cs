using System;
using System.Collections.Generic;
using Xamarin.Forms;

//using Refit;

namespace StartingPCL
{
	public partial class NewsListPage : ContentPage
	{
		NewsListViewModel ViewModel { get; }

		public NewsListPage (NewsListViewModel viewModel)
		{
			InitializeComponent ();

			ViewModel = viewModel;
			this.BindingContext = viewModel;

			this.listView.ItemTemplate = new DataTemplate (typeof(ArticleRowViewCell));
			this.listView.ItemsSource = viewModel.Articles;


			this.listView.ItemSelected += (sender, e) => {
				if (e.SelectedItem == null)
					return;

				viewModel.OnArticleSelected ((ArticleViewModel)e.SelectedItem);

				((ListView)sender).SelectedItem = null; // deselect row
			};


			var showNewBtn = new ToolbarItem {
				Text = "Show New",
				Order = ToolbarItemOrder.Primary,
				Command = viewModel.ShowNewArticlesCommand
			};
//			showNewBtn.SetBinding (ToolbarItem.TextProperty, nameof (viewModel.NumberOfNewArticlesText));

			ToolbarItems.Add (showNewBtn);



			ToolbarItems.Add (new ToolbarItem {
				Text = "Load more",
				Order = ToolbarItemOrder.Primary,
				Command = viewModel.LoadMoreCommand
			});
		}

		private void DebugAction ()
		{
			Log.Info ("DebugAction");

			var itemsSource = this.listView.ItemsSource;
			var articles = this.ViewModel.Articles;

			Log.Info ("break");
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			this.ViewModel?.OnAppearing ();
		}

		protected override void OnDisappearing ()
		{
			this.ViewModel?.OnDisappearing ();
			base.OnDisappearing ();
		}

	}
}

