using System;
using Autofac;

namespace StartingPCL
{
	internal class AppSetup
	{
		private IContainer container;

		public AppSetup ()
		{
			var builder = new ContainerBuilder ();
			builder
				.RegisterType<NYTimesNewsService> ()
				.As<INewsService> ();

			builder
				.Register (e => new StackNavigationRouter () {
				ViewModelsFactory = e.Resolve<IViewModelsFactory> ()
			})
				.As<IRouter> ()
				.SingleInstance ();

			builder
				.Register (e => new ViewModelsFactoryImpl ())
				.As<IViewModelsFactory> ()
				.SingleInstance ();

			builder
				.Register<ArticleViewModel> ((e, p) => new ArticleViewModel (p.Positional<Article> (0)));

			builder
				.Register<ActionsFactoryImpl> (e => new ActionsFactoryImpl ())
				.As<IActionsFactory> ()
				.SingleInstance ();

//			builder
//				.Register (e => new NewsListViewModel () {
//				NewsService = e.Resolve<INewsService> (),
//				Router = e.Resolve<IRouter> (),
//				ViewModelsFactory = e.Resolve<IViewModelsFactory> (),
//				ActionsFactory = e.Resolve<IActionsFactory> ()
//			});

			builder
				.Register (e => new NewsListViewModel (
					e.Resolve<IRouter> (),
					e.Resolve<IViewModelsFactory> (),
					e.Resolve<IActionsFactory> ()
				));

			container = builder.Build ();

			// TODO: Remove bad code from AppSetup
			ViewModelsFactoryImpl viewModelsFactory = (ViewModelsFactoryImpl)container.Resolve<IViewModelsFactory> ();
			viewModelsFactory.Container = container;

			ActionsFactoryImpl actionsFactory = (ActionsFactoryImpl)container.Resolve<IActionsFactory> ();
			actionsFactory.Container = container;

		}

		public IRouter Router {
			get { 
				return container.Resolve<IRouter> ();
			}
		}
	}
}

