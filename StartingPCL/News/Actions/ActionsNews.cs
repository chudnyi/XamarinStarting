using System;
using System.Collections.Generic;
using Redux;
using System.Threading.Tasks;

namespace StartingPCL
{
	public class NewsFetchAction : IAction
	{
		public TopStoriesCategory Category;
	}

	public class NewsFetchSuccessAction: IAction
	{
		public List<Article> Articles;
	}

	public class NewsFetchFailureAction: IAction
	{
		public Exception Error;
	}
}

