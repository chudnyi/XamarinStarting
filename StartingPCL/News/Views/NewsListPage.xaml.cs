﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;

//using Refit;

namespace StartingPCL
{
	public partial class NewsListPage : ContentPage
	{
		public NewsListViewModel ViewModel { 
			get { 
				return (NewsListViewModel)this.BindingContext;
			}
			set { 
				this.BindingContext = value;
			}
		}

		public NewsListPage ()
		{
			InitializeComponent ();

			// this.listView.IsPullToRefreshEnabled = true;
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			System.Diagnostics.Debug.WriteLine ("OnAppearing...");

			this.ViewModel?.OnAppearing ();
		}

	}
}

