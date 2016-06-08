using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StartingPCL.ListView
{
    public class AvatarImageService : IImageService
    {
        private readonly ImageSourceFactoryMethod FactoryMethod;

        public AvatarImageService(ImageSourceFactoryMethod factoryMethod, bool asyncAllowed)
        {
            Debug.Assert(factoryMethod != null);

            FactoryMethod = factoryMethod;
            ImageWithNameAndSizeAsyncAllowed = asyncAllowed;
        }

        public ImageSource ImageWithNameAndSize(string name, Size size)
        {
            return this.FactoryMethod(name, size);
        }

        public Task<ImageSource> ImageWithNameAndSizeAsync(string name, Size size)
        {
            if(!ImageWithNameAndSizeAsyncAllowed)
                throw new InvalidOperationException($"Not support async method");

            return Task<ImageSource>.Factory.StartNew(() => ImageWithNameAndSize(name, size));
        }

        public bool ImageWithNameAndSizeAsyncAllowed { get; }
    }
}