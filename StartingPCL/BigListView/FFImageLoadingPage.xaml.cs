using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;
using FFImageLoading;
using FFImageLoading.Cache;
using StartingPCL.ListView;
using Xamarin.Forms;

namespace StartingPCL.BigListView
{
    public partial class FFImageLoadingPage : ContentPage
    {
        public BigListViewModel ViewModel { get; }

        public FFImageLoadingPage(BigListViewModel viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;
            this.BindingContext = viewModel;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            ImageService.Instance.InvalidateCacheAsync(CacheType.All)
                .ToObservable()
                .Subscribe(unit => Log.Info($"Invalidate FFImage cache."));

        }
    }
}
