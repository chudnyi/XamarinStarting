using System;
using Redux;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Immutable;

namespace StartingPCL
{
	public class ReducerNews
	{
		public static StateNews Execute (StateNews state, IAction action)
		{
			if (action is NewsFetchAction) {
				return new StateNews (state) {
					IsBusy = true
				};
			} else if (action is NewsFetchSuccessAction) {
				var success = action as NewsFetchSuccessAction;
				var modelsMap = success.Articles.ToImmutableDictionary (model => model.Id);
				modelsMap = state.ArticlesById.SetItems (modelsMap);
				var ids = modelsMap.Keys.ToImmutableList ();

				return new StateNews (state) {
					IsBusy = false,
					ArticlesById = modelsMap,
					VisibleArticlesIds =  ids
				};
			
			} else if (action is NewsFetchFailureAction) {
//				var failure = action as NewsFetchFailureAction;
				return new StateNews (state) {
					IsBusy = false
				};
			}

			return state;
		}

	}
}

