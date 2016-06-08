using System.Diagnostics;
using Xamarin.Forms;

namespace StartingPCL.ListView
{
    public class BigListCell : ViewCell
    {
        private ImageResourceExtension ImageResource { get; } = new ImageResourceExtension();
        private Image avatarImage;

        public BigListCell()
        {
        }



        protected override async void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            var viewModel = this.BindingContext as ArticleViewModel;
            if (viewModel == null) return;

            if(avatarImage == null)
                avatarImage = this.FindByName<Image>("avatarImage");

            if (!viewModel.AvatarImageService.ImageWithNameAndSizeAsyncAllowed)
            {
                var imgSrc = viewModel.AvatarImageService.ImageWithNameAndSize(viewModel.AvatarImageName, avatarImage.Bounds.Size);
                avatarImage.Source = imgSrc;
            }
            else
            {
                var imgSrc = await viewModel.AvatarImageService.ImageWithNameAndSizeAsync(viewModel.AvatarImageName, avatarImage.Bounds.Size);
                if (this.BindingContext == viewModel)
                {
                    avatarImage.Source = imgSrc;
                }

            }
        }
    }
}