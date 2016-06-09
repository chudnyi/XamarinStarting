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
        private readonly Func<string, Size, bool> RejectMethodAsync;

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

        public AvatarImageService(ImageSourceFactoryMethodAsync factoryMethodAsync, Func<string, Size, bool> rejectMethodAsync)
        {
            Debug.Assert(factoryMethodAsync != null);
            FactoryMethodAsync = factoryMethodAsync;
            RejectMethodAsync = rejectMethodAsync;
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

        public bool RejectImageWithNameAndSizeAsync(string name, Size size)
        {
            return RejectMethodAsync(name, size);
        }
    }
}