using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace StartingShared
{
	public partial class MyFirstPage : ContentPage
	{
		public IRouter router { get; set; }

		public MyFirstPage ()
		{
			InitializeComponent ();

			this.btnPushMe.Clicked += BtnPushMe_Clicked;
			this.btnNews.Clicked += BtnNews_Clicked;
		}

		void BtnNews_Clicked (object sender, EventArgs e)
		{
			this.router?.routeNewsPage ();
		}

		void BtnPushMe_Clicked (object sender, EventArgs e)
		{
			this.router?.routeSecondPage ();
		}
	}
}

