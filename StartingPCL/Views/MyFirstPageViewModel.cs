using System.Windows.Input;
using Xamarin.Forms;

namespace StartingPCL.Views
{
    public class MyFirstPageViewModel
    {
        public IRouter Router { get; set; }
        public ICommand BigListCommand { get; }

        public MyFirstPageViewModel()
        {
            BigListCommand = new Command(BigListCommandExecute);
        }

        private void BigListCommandExecute(object o)
        {
            this.Router?.RouteBigList((string)o);
        }

    }
}