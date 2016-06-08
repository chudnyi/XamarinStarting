using System;
using System.Threading.Tasks;

namespace StartingPCL
{
	public interface IRouter
	{
		Task DisplayAlert (string title, string message, string cancel);

		Task<bool> DisplayAlert (string title, string message, string accept, string cancel);

		Task<string> DisplayActionSheet (string title, string cancel, string destruction, params string[] buttons);


		Xamarin.Forms.Page mainPage ();

		void RouteSecondPage ();

		void RouteNewsPage ();

		void RouteArticleDetailsPage (Article model);

        void RouteBigList(string mode);
    }
}

