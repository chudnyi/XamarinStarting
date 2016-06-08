using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace StartingPCL.ListView
{
    public partial class BigListViewPage : ContentPage
    {
        public BigListViewModel ViewModel { get; set; }


        public BigListViewPage(BigListViewModel viewModel)
        {
            
            InitializeComponent();

            ViewModel = viewModel;
            this.BindingContext = viewModel;
        }
    }
}
