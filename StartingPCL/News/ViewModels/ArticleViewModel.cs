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

		public Uri Url { get; }
		public string TimeText { get; }
		public string BodyText { get; }
		public string SectionText { get; }
		public string SubsectionText { get; }
		public ICommand ReadMoreCommand { get;}

		public ArticleViewModel (Article article)
		{
			this.Model = article;

			this.Title = article.Title;
			this.Subtitle = article.Abstract;
			Uri uri;
			if (Uri.TryCreate (article.Url, UriKind.Absolute, out uri))
				this.Url = uri;
			this.TimeText = DateTime.Now.ToString ("f"); // fake published date
			this.BodyText = article.Abstract;

			this.SectionText = article.Section != null ? string.Format("Section: {0}", article.Section) : null;
			this.SubsectionText = article.Subsection != null ? string.Format("Subsection: {0}", article.Subsection) : null;

			this.ReadMoreCommand = new Command (OnReadMore);
		}

		private Uri imageUri;
		public Uri ImageUri {
			get
			{
			    return null; // disable images

				if (imageUri == null) {
					var images = this.Model.Multimedia;
					var image = images != null && images.Count != 0 ? images [0] : null;
					if (image != null) {
						imageUri = new Uri (image.Url);
					}
				}
				return imageUri;
			}
		}

		public bool IsImageVisible {
			get {
				return this.ImageUri != null;
			}
		}

		private UriImageSource listRowImageSource;

		public ImageSource ListRowImageSource {
			get { 
				if (this.ImageUri != null) {
					listRowImageSource = new UriImageSource { 
						CachingEnabled = true, 
						Uri = this.ImageUri,
						CacheValidity = new TimeSpan (1, 0, 0, 0)
					};
				}
				return this.listRowImageSource;
			}
		}

		public ImageSource DetailsImageSource {
			get {
				return this.ListRowImageSource;
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

//				this.listRowImageSource.Cancel ();



			}
		}

		private void OnReadMore() {

			if(this.Url != null) {
				Debug.WriteLine ($"Openin URL: {this.Url} ...");
				Device.OpenUri(this.Url);
			}

		}
	}
}
