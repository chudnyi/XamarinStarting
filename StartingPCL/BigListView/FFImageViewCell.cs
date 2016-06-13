using System;
using FFImageLoading.Forms;
using Xamarin.Forms;

namespace StartingPCL.BigListView
{
    public class FFImageViewCell : ViewCell
    {
        private CachedImage avatarImage;

        public FFImageViewCell()
        {
            
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var viewModel = this.BindingContext as ArticleViewModel;
            if (viewModel == null) return;

            if (avatarImage == null)
                avatarImage = this.FindByName<CachedImage>("avatarImage");

            avatarImage.Source = viewModel.AvatarImageUri.AbsoluteUri;
        }

    }
}