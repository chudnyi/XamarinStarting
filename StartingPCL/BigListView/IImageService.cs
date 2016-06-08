using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StartingPCL.ListView
{
    public interface IImageService
    {
        ImageSource ImageWithNameAndSize(string name, Size size);

        Task<ImageSource> ImageWithNameAndSizeAsync(string name, Size size);

        bool ImageWithNameAndSizeAsyncAllowed { get; }
    
    }
}