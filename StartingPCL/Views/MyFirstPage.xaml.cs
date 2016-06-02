using System;
using System.Collections.Generic;
using Xamarin.Forms;
using StartingPCL;

namespace StartingPCL
{
	public partial class MyFirstPage : ContentPage
	{
		public IRouter Router { get; set; }

		public MyFirstPage ()
		{
			InitializeComponent ();

			this.btnPushMe.Clicked += BtnPushMe_Clicked;
			this.btnNews.Clicked += BtnNews_Clicked;

		    this.BigListButton.Clicked += (sender, args) =>
		    {
		        this.Router?.RouteBigList();
		    };
		}

		void BtnNews_Clicked (object sender, EventArgs e)
		{
			this.Router?.RouteNewsPage ();
		}

		void BtnPushMe_Clicked (object sender, EventArgs e)
		{
			this.Router?.RouteSecondPage ();
		}
	}
}

