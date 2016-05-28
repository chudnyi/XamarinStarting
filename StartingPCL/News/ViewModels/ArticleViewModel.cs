using System;
using MvvmHelpers;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Diagnostics;
using System.Windows.Input;

namespace StartingPCL
{
	public class ArticleViewModel : BaseViewModel
	{
		public Article Model { get; private set; }

		public string Url { get; }
		public string TimeText { get; }

		public ArticleViewModel (Article article)
		{
			this.Model = article;

			this.Title = article.Title;
			this.Subtitle = article.Abstract;
			this.Url = article.Url;
//			this.TimeText = article.PublishedDate.ToString ("f");
			this.TimeText = DateTime.Now.ToString ("f");
		}

		private UriImageSource listRowImageSource;

		public ImageSource ListRowImageSource {
			get { 
				if (this.listRowImageSource == null) {
					var images = this.Model.Multimedia;
					Debug.WriteLine ($"Article multimedia: {images.Count}");

					var image = images.Count != 0 ? images [0] : null;
					if (image != null) {
						var imageUrl = new Uri (image.Url);
//						this.listRowImageSource = ImageSource.FromUri (imageUrl);
						listRowImageSource = new UriImageSource { 
							CachingEnabled = true, 
							Uri = imageUrl,
							CacheValidity = new TimeSpan (1, 0, 0, 0)
						};
					}
				}
				return this.listRowImageSource;
			}
		}

		FormattedString textForListViewCell;

		public FormattedString TextForListViewCell {
			get { 
				if (textForListViewCell == null) {
					textForListViewCell = new FormattedString ();
					textForListViewCell.Spans.Add (new Span { Text = Title, FontSize = 18, ForegroundColor=Color.FromHex("#f35e20") });
					textForListViewCell.Spans.Add (new Span { Text = Title, FontSize = 12, ForegroundColor=Color.FromHex("#503026") });
				}

				return textForListViewCell;
			}
		}

		public void WillUnbindFromListViewCell ()
		{
			Debug.WriteLine ($"[PrepareForReuseInListViewCell]...");

			if (this.listRowImageSource != null) {
				this.listRowImageSource.Cancel ();
			}
		}
	}
}
