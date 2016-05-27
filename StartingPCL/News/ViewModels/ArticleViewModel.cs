using System;
using MvvmHelpers;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Diagnostics;

namespace StartingPCL
{
	public class ArticleViewModel : BaseViewModel
	{
		private Article Model { get; set; }
		public string Url { get;}

		public ArticleViewModel (Article article)
		{
			this.Model = article;

			this.Title = article.Title;
			this.Subtitle = article.Abstract;
			this.Url = article.Url;
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
	}
}
