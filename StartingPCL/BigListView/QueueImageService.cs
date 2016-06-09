using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StartingPCL.ListView
{
    public class QueueImageService : IImageService
    {
        public bool ImageWithNameAndSizeAsyncAllowed { get; } = true;

        public ImageSource ImageWithNameAndSize(string name, Size size)
        {
            throw new System.NotImplementedException();
        }

//        private SortedDictionary<string, ImageRequest> requests = new SortedDictionary<string, ImageRequest>();
        private List<ImageRequest> Requests = new List<ImageRequest>(30);


        public Task<ImageSource> ImageWithNameAndSizeAsync(string name, Size size)
        {
            return null;
        }

        public bool RejectImageWithNameAndSizeAsync(string name, Size size)
        {
            return false;
        }

        private ImageRequest ImageRequest(string name)
        {
            return Requests.First(request => request.Key == name);
        }
    }


    class ImageRequest
    {
        public string Key { get; set; }
        public Task<ImageSource> Task { get; set; }
        public WaitHandle OrderWaitHandle { get; set; }
        public ImageSource Result { get; set; }
    }
}