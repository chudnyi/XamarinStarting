using System;
using FFImageLoading.Forms;
using Xamarin.Forms;

namespace StartingPCL.BigListView
{
    public class FFImageViewCell : ViewCell
    {
        private CachedImage avatarImage;
        private CachedImage avatarImage2;
        private CachedImage avatarImage3;

        public FFImageViewCell()
        {

        }

        public CachedImage AvatarImage
        {
            get
            {
                if (avatarImage == null)
                    avatarImage = this.FindByName<CachedImage>("avatarImage");

                return avatarImage;
            }
        }

        public CachedImage AvatarImage2
        {
            get
            {
                if (avatarImage2 == null)
                    avatarImage2 = this.FindByName<CachedImage>("avatarImage2");

                return avatarImage2;
            }
        }

        public CachedImage AvatarImage3
        {
            get
            {
                if (avatarImage3 == null)
                    avatarImage3 = this.FindByName<CachedImage>("avatarImage3");

                return avatarImage3;
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var viewModel = this.BindingContext as ArticleViewModel;
            if (viewModel == null) return;

            AvatarImage.Source = viewModel.AvatarImageUriWithIndex(viewModel.Index * 3 + 0);
            AvatarImage2.Source = viewModel.AvatarImageUriWithIndex(viewModel.Index * 3 + 1);
            AvatarImage3.Source = viewModel.AvatarImageUriWithIndex(viewModel.Index * 3 + 2);

        }

    }
}