using System;
using System.Net.Http;
using System.Threading.Tasks;
using ModernHttpClient;
using StartingPCL.Helpers;
using Xamarin.Forms;

namespace StartingPCL.ListView
{
    public class NetworkImageService : IImageService
    {
        public Func<string, Uri> UriForImageKey;

        private TransformQueue<string, string, ImageSource> Queue { get; } = TransformQueue<string, string, ImageSource>.Default;

        public ImageSource ImageWithNameAndSize(string name, Size size)
        {
            throw new System.NotImplementedException();
        }

        public bool ImageWithNameAndSizeAsyncAllowed { get; } = true;

        public Task<ImageSource> ImageWithNameAndSizeAsync(string name, Size size)
        {
            var task = Queue.EnqueueTransform(name, name, LoadImageWithNameAsync);
            return task;
        }

        private async Task<ImageSource> LoadImageWithNameAsync(string key, string input)
        {
            //            var num = DateTime.Now.TimeOfDay.TotalMilliseconds;
            //            var uri = new Uri($"http://loremflickr.com/40/40/head?random={num}");
            //            var uri = new Uri($"http://10.3.3.1:8080/{key}");
            var uri = UriForImageKey?.Invoke(key);
            uri = uri ?? new Uri($"http://loremflickr.com/40/40/head?random={DateTime.Now.TimeOfDay.TotalMilliseconds}");

            using (var httpClient = new HttpClient(new NativeMessageHandler()))
            {
                try
                {
                    using (var response = await httpClient.GetAsync(uri))
                    {
                        //                    Log.Info($"Responce received for {key}");
                        //                    response.EnsureSuccessStatusCode();
                        var inputStream = await response.Content.ReadAsStreamAsync();
                        return ImageSource.FromStream(() => inputStream);
                    }
                }
                catch (Exception ex)
                {
                    Log.Info("Loading image error: {0}", ex);
                    return ImageSource.FromResource("StartingPCL.Resources.avatars.Images.error40.png");
                }
            }
        }

        public bool RejectImageWithNameAndSizeAsync(string name, Size size)
        {
            Queue.RemoveTransform(name);
            return false;
        }
    }
}