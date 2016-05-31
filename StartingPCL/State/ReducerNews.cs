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
				var articlesById = success.Articles.ToImmutableDictionary (model => model.Id);
				// merge with current articles
				articlesById = state.ArticlesById.SetItems (articlesById);

				ImmutableList<EntityId> visibleIds;
				int numberOfNewArticles = 0;
				if (state.VisibleArticlesIds.Count == 0) {
					visibleIds = articlesById.Keys.ToImmutableList ();
					numberOfNewArticles = 0;
				} else {
					visibleIds = state.VisibleArticlesIds;
					numberOfNewArticles = articlesById.Count - state.VisibleArticlesIds.Count;
				}

				return new StateNews (state) {
					IsBusy = false,
					ArticlesById = articlesById,
					VisibleArticlesIds = visibleIds,
					NumberOfNewArticles = numberOfNewArticles
				};
			
			} else if (action is NewsFetchFailureAction) {
//				var failure = action as NewsFetchFailureAction;
				return new StateNews (state) {
					IsBusy = false
				};
			} else if (action is ShowNewArticlesAction) {
				if (state.VisibleArticlesIds.Count != state.ArticlesById.Count) {
					var newIds = state.ArticlesById.Keys
						.Where (id => !state.VisibleArticlesIds.Contains (id));

					return new StateNews (state) { 
						VisibleArticlesIds =  state.VisibleArticlesIds.InsertRange (0, newIds),
						NumberOfNewArticles = 0
					};
				}
			}

			return state;
		}

	}
}

