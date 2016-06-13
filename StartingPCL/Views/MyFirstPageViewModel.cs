using System.Windows.Input;
using Xamarin.Forms;

namespace StartingPCL.Views
{
    public class MyFirstPageViewModel
    {
        public IRouter Router { get; set; }
        public ICommand BigListCommand { get; }
        public ICommand FFImageLoadingCommand { get; }

        public MyFirstPageViewModel()
        {
            BigListCommand = new Command(BigListCommandExecute);
            FFImageLoadingCommand = new Command(FFImageLoadingCommandExecute);
        }

        private void BigListCommandExecute(object o)
        {
            this.Router?.RouteBigList((string)o);
        }
        private void FFImageLoadingCommandExecute(object o)
        {
            this.Router?.RouteFFImageLoading((string)o);
        }

    }
}