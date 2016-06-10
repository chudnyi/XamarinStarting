using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using StartingPCL;
using StartingPCL.ListView;

namespace StartingPCL
{
    /// <summary>
    /// Stack navigation router and application components factory.
    /// TODO: Extract components factory logic to external class (single responsibility principe).
    /// </summary>
    public class StackNavigationRouter : IRouter
    {
        private NavigationPage navigationPage { get; set; }

        public IViewModelsFactory ViewModelsFactory { get; set; }


        public Task DisplayAlert(string title, string message, string cancel)
        {
            var page = this.navigationPage;
            return page.DisplayAlert(title, message, cancel);
        }

        public Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
        {
            var page = this.navigationPage;
            return page.DisplayAlert(title, message, accept, cancel);
        }

        public Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons)
        {
            var page = this.navigationPage;
            return page.DisplayActionSheet(title, cancel, destruction, buttons);
        }

        public Xamarin.Forms.Page mainPage()
        {
            if (navigationPage == null)
            {
                navigationPage = new NavigationPage(this.CreateMyFirstPage());
            }

            return navigationPage;
        }

        public void RouteSecondPage()
        {
            this.navigationPage.PushAsync(new SecondPage
            {
                Title = "Second"
            });
        }

        public void RouteNewsPage()
        {
            var vm = this.ViewModelsFactory.NewsListViewModel();
            this.navigationPage.PushAsync(new StartingPCL.NewsListPage(vm));
        }

        public void RouteArticleDetailsPage(Article model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            var vm = this.ViewModelsFactory.ArticleViewModel(model);
            this.navigationPage.PushAsync(new StartingPCL.ArticleDetailsPage()
            {
                ViewModel = vm
            });
        }

        private MyFirstPage CreateMyFirstPage()
        {
            return new MyFirstPage(this);
        }

        public void RouteBigList(string mode)
        {
            var vm = new BigListViewModel()
            {
                AvatarImageService = BigListViewModel.ImageServiceWithMode(mode)
            };

            var page = new BigListViewPage(vm);
            this.navigationPage.PushAsync(page);
        }
    }
}
