using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StartingPCL.ListViewSupport;
using StartingPCL.News.Services;

namespace StartingPCL.ListView
{
    public class BigListViewModel : ViewModelBase
    {
        public ListViewItemsSource<ArticleViewModel> ItemsSource { get; }
        private ArticleViewModel LoadingItemViewModel { get; }
        private Dictionary<int, ArticleViewModel> ViewModelsCache { get; } = new Dictionary<int, ArticleViewModel>();
//        private List<Article> Articles { get;  }

        public BigListViewModel()
        {
            var newsService = new NewsServiceStatic();
            var articles = newsService.TopStoriesSync(TopStoriesCategory.home);
//            Articles = articles;
            ItemsSource = new ListViewItemsSource<ArticleViewModel>(articles.Count);
//            ItemsSource.ItemAtIndex = ItemViewModelAtIndex;
//            ItemsSource.ItemAtIndex = CreateItemViewModelAtIndex;
            ItemsSource.ItemAtIndex = index => new ArticleViewModel(articles[index]);

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

            var viewModel = new ArticleViewModel(model);
            
            Log.Info($"created item for index: {index} {viewModel.Model.Id}");
            return viewModel;
        }

    }
}