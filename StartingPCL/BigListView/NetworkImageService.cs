using System;
using System.IO;
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
        public bool ResizeAllowed { get; set; }

        private TransformQueue<string, string, ImageSource> Queue { get; } = TransformQueue<string, string, ImageSource>.Default;

        public ImageSource ImageWithNameAndSize(string name, Size size)
        {
            throw new System.NotImplementedException();
        }

        public bool ImageWithNameAndSizeAsyncAllowed { get; } = true;

        public Task<ImageSource> ImageWithNameAndSizeAsync(string name, Size size)
        {
            //            var task = Queue.EnqueueTransform(name, name, (key, input) => LoadImageWithNameAsync(key, input, size));
            var task = Queue.EnqueueTransform(name, name, LoadImageWithNameAsync);
            return task;
        }

        private async Task<ImageSource> LoadImageWithNameAsync(string key, string input)
        {
            Size size = new Size(40, 40);
            //            var num = DateTime.Now.TimeOfDay.TotalMilliseconds;
            //            var uri = new Uri($"http://loremflickr.com/40/40/head?random={num}");
            //            var uri = new Uri($"http://10.3.3.1:8080/{key}");
            var uri = UriForImageKey?.Invoke(key);
            uri = uri ?? new Uri($"http://loremflickr.com/40/40/head?random={DateTime.Now.TimeOfDay.TotalMilliseconds}");
            var resizeAllowed = ResizeAllowed;

            byte[] bytes;

            try
            {
                using (var httpClient = new HttpClient(new NativeMessageHandler()))
                {
                    using (var response = await httpClient.GetAsync(uri))
                    {
                        bytes = await response.Content.ReadAsByteArrayAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Info("Loading image error: {0}", ex);
                return ImageSource.FromResource("StartingPCL.Resources.avatars.Images.error40.png");
            }

            if (bytes != null)
            {
                if (resizeAllowed)
                {
                    var resizedImage = App.ImageResizer.ResizeImage(bytes, (float)size.Width, (float)size.Height);
                    var stream = new MemoryStream(resizedImage);
                    return ImageSource.FromStream(() => stream);
                }
                else
                {
                    var stream = new MemoryStream(bytes);
                    return ImageSource.FromStream(() => stream);
                }
            }

            return null;
        }

        public bool RejectImageWithNameAndSizeAsync(string name, Size size)
        {
            Queue.RemoveTransform(name);
            return false;
        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}