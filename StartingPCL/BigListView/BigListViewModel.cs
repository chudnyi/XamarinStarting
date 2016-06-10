using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reactive.Linq;
using System.Threading.Tasks;
using StartingPCL.Helpers;
using StartingPCL.ListViewSupport;
using StartingPCL.News.Services;
using Xamarin.Forms;

namespace StartingPCL.ListView
{
    public class BigListViewModel : ViewModelBase
    {
        public ListViewItemsSource<ArticleViewModel> ItemsSource { get; }
        private ArticleViewModel LoadingItemViewModel { get; }
        private Dictionary<int, ArticleViewModel> ViewModelsCache { get; } = new Dictionary<int, ArticleViewModel>();
        //        private List<Article> Articles { get;  }
        public IImageService AvatarImageService;

        public BigListViewModel()
        {
            var newsService = new NewsServiceStatic();
            var articles = newsService.TopStoriesSync(TopStoriesCategory.home);
            //            Articles = articles;
            ItemsSource = new ListViewItemsSource<ArticleViewModel>(articles.Count);
            //            ItemsSource.ItemAtIndex = ItemViewModelAtIndex;
            ItemsSource.ItemAtIndex = CreateItemViewModelAtIndex;
            //            ItemsSource.ItemAtIndex = index => new ArticleViewModel(articles[index]);

            LoadingItemViewModel = new ArticleViewModel(new Article()
            {
                Url = "id_loading"
            })
            {
                IsBusy = true,
                Title = "Loading...",
            };
        }

        private ArticleViewModel ItemViewModelAtIndex(int index)
        {
            Log.Info($"=== query item for index: {index}");

            ArticleViewModel viewModel;
            if (ViewModelsCache.TryGetValue(index, out viewModel))
            {
                Log.Info($"found item in cache: {index}");
                return viewModel;
            }

            var loadingItem = LoadingItemViewModel;

            Task.Factory.StartNew(() =>
            {
                var vm = CreateItemViewModelAtIndex(index);
                ViewModelsCache[index] = vm;
                ItemsSource.UpdateItemAtIndex(vm, loadingItem, index);
            });

            return loadingItem;
        }

        private ArticleViewModel CreateItemViewModelAtIndex(int index)
        {
            Log.Info($"creating item for index: {index}");
            //            Task.Delay(1000).Wait();

            var model = new Article()
            {
                Url = $"id_{index}",
                Title = $"Article #{index}"
            };

            var imageIndex = (index % 7) + 1;
            var viewModel = new ArticleViewModel(model)
            {
                Index = index,
                BackgroundImageName = $"StartingPCL.Resources.Images.bg{imageIndex}.jpg",
                AvatarImageService = AvatarImageService
            };

            Log.Info($"created item for index: {index} {viewModel.Model.Id}");
            return viewModel;
        }

        public static IImageService ImageServiceWithMode(string mode)
        {
            switch (mode)
            {
                case "ListSingleStaticAvatar":
                    {
                        var imageSource = ImageSource.FromFile("kdrpp40.png");
                        return new AvatarImageService((name, size) => imageSource);
                    }
                case "ListOneAvatarForEachRow":
                    {
                        return new AvatarImageService((name, size) => ImageSource.FromFile("kdrpp40.png"));
                    }
                case "ListOneAvatarForEachRowAsync":
                    {
                        return new AvatarImageService((name, size) => Task<ImageSource>.Factory.StartNew(() => ImageSource.FromFile("kdrpp40.png")));
                    }
                case "ListAvatarForEachRowAsyncDelay":
                    {
                        int loadingCounter = 0;

                        return new AvatarImageService(async (name, size) =>
                        {
                            loadingCounter += 1;
                            var res = await Task<ImageSource>.Factory.StartNew(() =>
                            {

                                Task.Delay(1000).Wait();

                                return ImageSource.FromResource(name);
                            });
                            loadingCounter -= 1;

                            Log.Info("Number of loading images: {0}", loadingCounter);

                            return res;
                        });
                    }
                case "ListAvatarsUsingTransformQueue":
                    {
                        int loadingCounter = 0;

                        var queue = TransformQueue<string, string, ImageSource>.Default;

                        return new AvatarImageService(async (name, size) =>
                        {
                            loadingCounter += 1;
                            try
                            {
                                var res = await queue.EnqueueTransform(name, name,
                                            (key, input) => Task<ImageSource>.Factory.StartNew(() =>
                                            {
                                                Task.Delay(100).Wait();
                                                return ImageSource.FromResource(name);
                                            }));

                                loadingCounter -= 1;
                                Log.Info("Number of loading images: {0}", loadingCounter);

                                return res;
                            }
                            catch (Exception ex)
                            {
                                return null;
                            }

                        }, (name, size) =>
                        {
                            queue.RemoveTransform(name);
                            return true;
                        });
                    }
                case "ListAvatarsOnlineUsingTransformQueue":
                    {
                        int loadingCounter = 0;
                        return new NetworkImageService();

                        /*
                         * var queue = TransformQueue<string, string, ImageSource>.Default;
                                                return new AvatarImageService(async (name, size) =>
                                                {
                                                    loadingCounter += 1;
                                                    try
                                                    {
                                                        var res = await queue.EnqueueTransform(name, name,
                                                                    (key, input) => Task<ImageSource>.Factory.StartNew( () =>
                                                                    {
                        //                                                Task.Delay(100).Wait();
                        //                                                return ImageSource.FromResource(name);
                        //                                                var uri = new Uri("http://lorempixel.com/40/40/");

                                                                        var num = DateTime.Now.TimeOfDay.TotalMilliseconds;
                                                                        var uri = new Uri($"http://loremflickr.com/40/40/head?random={num}");


                                                                        return ImageSource.FromStream(() =>
                                                                        {
                                                                            var stream = ImageStream(uri);
                                                                            stream.Wait();
                                                                            return stream.Result;
                                                                        });
                                                                    }));

                                                        loadingCounter -= 1;
                                                        Log.Info("Number of loading images: {0}", loadingCounter);

                                                        return res;
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        return null;
                                                    }

                                                }, (name, size) =>
                                                {
                                                    queue.RemoveTransform(name);
                                                    return true;
                                                });
                        */
                    }
                default:
                    throw new ArgumentException($"Unexpected mode: {mode}", nameof(mode));
            }
        }

        private static async Task<Stream> ImageStream(Uri url)
        {
            var httpClient = App.HttpClient;
//            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(url))
                {
                    response.EnsureSuccessStatusCode();
                    var inputStream = await response.Content.ReadAsStreamAsync();
                    return inputStream;
/*
                    using (var inputStream = await response.Content.ReadAsStreamAsync())
                    {
                        //                            inputStream.CopyTo(fileStream);
//                        Image image = Image.FromStream(inputStream);
//                        pictureBox1.Image = image;

                        
                        
                    }
*/
                }
            }

        }
    }
}