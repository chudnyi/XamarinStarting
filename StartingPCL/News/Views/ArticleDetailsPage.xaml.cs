using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace StartingPCL
{
	public partial class ArticleDetailsPage : ContentPage
	{
		public ArticleDetailsPage ()
		{
			InitializeComponent ();
		}

		public ArticleViewModel ViewModel { 
			get { 
				return (ArticleViewModel)this.BindingContext;
			}
			set { 
				this.BindingContext = value;
			}
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			var model = this.ViewModel.Model;
			System.Diagnostics.Debug.WriteLine ($"Article details: {model}");
		}
	}
}

