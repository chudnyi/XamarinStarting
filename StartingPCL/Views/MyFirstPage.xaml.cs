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
		}

		void BtnNews_Clicked (object sender, EventArgs e)
		{
			this.Router?.routeNewsPage ();
		}

		void BtnPushMe_Clicked (object sender, EventArgs e)
		{
			this.Router?.routeSecondPage ();
		}
	}
}

