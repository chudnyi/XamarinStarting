using System;
using Autofac;

namespace StartingPCL
{
	internal class ViewModelsFactoryImpl : IViewModelsFactory
	{
		public IContainer Container  { get; set;}

		public ArticleViewModel ArticleViewModel (Article model)
		{
			if (Container == null)
				throw Error.PropertyNull (nameof (Container));
			
			return Container.Resolve<ArticleViewModel>(
				new PositionalParameter(0, model)
			);
		}

		public NewsListViewModel NewsListViewModel ()
		{
			if (Container == null)
				throw Error.PropertyNull (nameof (Container));
			
			return Container.Resolve<NewsListViewModel> ();
		}
	}
}

