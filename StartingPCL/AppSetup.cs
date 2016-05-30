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
				.RegisterType<StackNavigationRouter> ()
				.As<IRouter> ()
				.SingleInstance ();



			container = builder.Build ();
		}

		public IRouter Router {
			get { 
				return container.Resolve<IRouter> ();
			}
		}
	}
}

