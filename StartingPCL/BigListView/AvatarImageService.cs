using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StartingPCL.ListView
{
    public delegate ImageSource ImageSourceFactoryMethod(string name, Size size);
    public delegate Task<ImageSource> ImageSourceFactoryMethodAsync(string name, Size size);



    public class AvatarImageService : IImageService
    {
        private readonly ImageSourceFactoryMethod FactoryMethod;
        private readonly ImageSourceFactoryMethodAsync FactoryMethodAsync;

        public AvatarImageService(ImageSourceFactoryMethod factoryMethod)
        {
            Debug.Assert(factoryMethod != null);
            FactoryMethod = factoryMethod;
        }

        public AvatarImageService(ImageSourceFactoryMethodAsync factoryMethodAsync)
        {
            Debug.Assert(factoryMethodAsync != null);
            FactoryMethodAsync = factoryMethodAsync;
        }

        public ImageSource ImageWithNameAndSize(string name, Size size)
        {
            return this.FactoryMethod(name, size);
        }

        public Task<ImageSource> ImageWithNameAndSizeAsync(string name, Size size)
        {
            return this.FactoryMethodAsync(name, size);
        }

        public bool ImageWithNameAndSizeAsyncAllowed => FactoryMethodAsync != null;
    }
}