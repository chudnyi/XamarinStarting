using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using StartingPCL;
using StartingPCL.Views;

namespace StartingPCL
{
	public partial class MyFirstPage : ContentPage
	{
		public IRouter Router { get; }

	    


        public MyFirstPage (IRouter router)
        {
            this.Router = router;


            this.BindingContext = new MyFirstPageViewModel()
            {
                Router = router
            };

            InitializeComponent ();
        }


	    void BtnNews_Clicked (object sender, EventArgs e)
		{
			this.Router?.RouteNewsPage ();
		}

	}
}

