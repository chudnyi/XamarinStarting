using System;
using MvvmHelpers;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Diagnostics;

namespace StartingShared
{
	public class ArticleViewModel : BaseViewModel
	{
		private Article Model { get; set; }

		public ArticleViewModel (Article article)
		{
			this.Model = article;

			this.Title = article.Title;
			this.Subtitle = article.Abstract;


		}

		private ImageSource listRowImageSource;

		public ImageSource ListRowImageSource {
			get { 
				if (this.listRowImageSource == null) {
					var images = this.Model.Multimedia;
					Debug.WriteLine ($"Article multimedia: {images.Count}");

					var image = images.Count != 0 ? images [0] : null;
					if (image != null) {
						var imageUrl = new Uri (image.Url);
						this.listRowImageSource = ImageSource.FromUri (imageUrl);
					}
				}
				return this.listRowImageSource;
			}
		}
	}
}
