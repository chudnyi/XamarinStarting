using System;

namespace StartingPCL
{
	/// <summary>
	/// View models factory.
	/// </summary>
	public interface IViewModelsFactory
	{
		ArticleViewModel ArticleViewModel (Article model);

		NewsListViewModel NewsListViewModel ();
	}
}

