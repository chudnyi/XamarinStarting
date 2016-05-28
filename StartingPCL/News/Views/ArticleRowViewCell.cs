using System;
using Xamarin.Forms;
using System.Diagnostics;
using System.Windows.Input;


namespace StartingPCL
{
	delegate void OnBindingContextChanged ();

	public class ArticleRowViewCell : ViewCell
	{
		Image image = null;
		Label title = null;
		Label subtitle = null;
		Label timeLabel = null;


		public ArticleRowViewCell ()
		{
			// Performance optimization
			// https://developer.xamarin.com/guides/xamarin-forms/user-interface/listview/performance/#RecycleElement
			// no xaml, no bindings

			image = new Image ();
			//			image = new FFImageLoading.Forms.CachedImage ();

			title = new Label () { 
				TextColor = new OnPlatform<Color> () {
					iOS = Color.FromHex ("#f35e20")
				},
				LineBreakMode = LineBreakMode.TailTruncation
			};
			subtitle = new Label () { 
				TextColor = new OnPlatform<Color> () {
					iOS = Color.FromHex ("#503026")
				},
				LineBreakMode = LineBreakMode.TailTruncation,
				FontSize = 12
			};

			timeLabel = new Label () { 
				TextColor = Color.FromHex ("#B7B3B4"),
				LineBreakMode = LineBreakMode.NoWrap,
				FontSize = 12,
				HorizontalOptions = LayoutOptions.StartAndExpand,
			};

			var bottomGrid = new Grid () {
				ColumnDefinitions = {
					new ColumnDefinition () { Width = new GridLength (100, GridUnitType.Absolute) },
					new ColumnDefinition () { Width = new GridLength (1, GridUnitType.Auto) },
				}
			};
			bottomGrid.Children.Add (timeLabel);
			bottomGrid.Children.Add (subtitle, 1, 0);


			View = new StackLayout () {
				Padding = new Thickness (4),
				Orientation = StackOrientation.Horizontal,
				VerticalOptions = LayoutOptions.Center,
				Children = {
					image,
					new StackLayout () {
						Orientation = StackOrientation.Vertical,
						HorizontalOptions = LayoutOptions.StartAndExpand,
						VerticalOptions = LayoutOptions.Center,
						Children = {
							title, 		
							bottomGrid
						}
					}
				}
			};
		}

		protected override void OnBindingContextChanged ()
		{
			base.OnBindingContextChanged ();

			// TODO: !!!
			// https://github.com/luberda-molinet/FFImageLoading/wiki/Xamarin.Forms-Advanced


			// Performance optimization
			// https://developer.xamarin.com/guides/xamarin-forms/user-interface/listview/performance/#RecycleElement
			var viewModel = BindingContext as ArticleViewModel;

			image.Source = viewModel?.ListRowImageSource;
			title.Text = viewModel?.Title;
			subtitle.Text = viewModel?.Subtitle;
			timeLabel.Text = viewModel?.TimeText;
		}

		protected override void OnPropertyChanging (string propertyName)
		{
			if (propertyName == "BindingContext") {
				var viewModel = BindingContext as ArticleViewModel;
				viewModel?.WillUnbindFromListViewCell ();
			}

			base.OnPropertyChanging (propertyName);
		}
	}
}

