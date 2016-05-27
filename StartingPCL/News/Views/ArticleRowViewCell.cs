using System;
using Xamarin.Forms;
using System.Diagnostics;

namespace StartingPCL
{
	delegate void OnBindingContextChanged();

	public class ArticleRowViewCell : ImageCell
	{
		

		public ArticleRowViewCell ()
		{
		}

		protected override void OnBindingContextChanged ()
		{
			base.OnBindingContextChanged ();

			var viewModel = (ArticleViewModel)this.BindingContext;
			var url = viewModel?.Url ?? "url not set";
			Debug.WriteLine ($"OnBindingContextChanged: ${url}");

		}	

		protected override void OnPropertyChanging (string propertyName)
		{
			if(propertyName == "BindingContext") {
				var viewModel = (ArticleViewModel)this.BindingContext;
				var url = viewModel?.Url ?? "url not set";
				Debug.WriteLine ($"changing BindingContext: ${url}");
			}

			base.OnPropertyChanging (propertyName);
		}
	}
}

