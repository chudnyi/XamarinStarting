using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StartingPCL.Helpers;
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

            // queue indicator
            var toolbarItem = new ToolbarItem
            {
                Text = "queue",
                Order = ToolbarItemOrder.Primary
            };
            ToolbarItems.Add(toolbarItem);

            var queue = TransformQueue<string, string, ImageSource>.Default;
            queue.QueueChanged += (sender, args) =>
            {
                toolbarItem.Text = $"Queue: {args.QueueLength}";
            };
        }
    }
}
