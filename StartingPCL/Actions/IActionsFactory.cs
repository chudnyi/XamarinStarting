using System;
using Redux;

namespace StartingPCL
{
	public interface IActionsFactory
	{
		IAction CreateNewsFetchAction (TopStoriesCategory category);

		AsyncActionsCreator<State> NewsFetchActionAsync (TopStoriesCategory category);

		AsyncActionsCreator<State> ShowNewArticlesAsync ();
	}
}

