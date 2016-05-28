using System;
using Redux;
using System.Threading.Tasks;


namespace StartingPCL
{
	public delegate Task AsyncActionsCreator<TState>(Dispatcher dispatch, Func<TState> getState);

	public static class StoreExtensions
	{
		/// <summary>
		/// Extension on IStore to dispatch multiple actions via a thunk. 
		/// Can be used like https://github.com/gaearon/redux-thunk without the need of middleware.
		/// </summary>
		public static Task DispatchAsync<TState>(this IStore<TState> store, AsyncActionsCreator<TState> actionsCreator)
		{
			return actionsCreator(store.Dispatch, store.GetState);
		}

		public static IAction Dispatch(this IAction action) {
			return App.Store.Dispatch(action);
		}

		public static Task DispatchAsync(this AsyncActionsCreator<State> actionCreator) {
			return actionCreator (App.Store.Dispatch, App.Store.GetState);
		}
	}
}
