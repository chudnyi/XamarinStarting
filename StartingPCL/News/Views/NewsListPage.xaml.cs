using System;
using System.Collections.Generic;
using Xamarin.Forms;

//using Refit;

namespace StartingPCL
{
	public partial class NewsListPage : ContentPage
	{
		public NewsListViewModel ViewModel { 
			get { 
				return (NewsListViewModel)this.BindingContext;
			}
			set { 
				this.BindingContext = value;
			}
		}

		public NewsListPage ()
		{
			InitializeComponent ();
			this.listView.ItemTemplate = new DataTemplate (typeof(ArticleRowViewCell));

			this.listView.ItemSelected += (sender, e) => {
				if(e.SelectedItem == null) return;

				this.ViewModel.OnArticleSelected((ArticleViewModel)e.SelectedItem);

				((ListView)sender).SelectedItem = null; // deselect row
			};
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			System.Diagnostics.Debug.WriteLine ("OnAppearing...");

			this.ViewModel?.OnAppearing ();
		}

	}
}

