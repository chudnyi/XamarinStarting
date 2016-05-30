using System;
using System.Collections.Generic;
using Redux;
using System.Threading.Tasks;
using Autofac;

namespace StartingPCL
{
	internal class ActionsFactoryImpl : IActionsFactory
	{
		private readonly INewsService newsService;

		public ActionsFactoryImpl (INewsService newsService)
		{
			if (newsService == null)
				throw Error.ArgumentNull (nameof (newsService));

			this.newsService = newsService;
		}


		public IAction CreateNewsFetchAction (TopStoriesCategory category)
		{
			return new NewsFetchAction {
				Category = category
			};
		}

		public AsyncActionsCreator<State> NewsFetchActionAsync_1 (TopStoriesCategory category)
		{
			return async (dispatch, getState) => {

				System.Diagnostics.Debug.WriteLine ("NewsFetchAction...");
				dispatch (new NewsFetchAction{ Category = category });

				await Task.Delay (1000);

				System.Diagnostics.Debug.WriteLine ("NewsFetchSuccessAction...");
				dispatch (new NewsFetchSuccessAction { Articles = new List<Article> { 
						new Article () { Url = "1" }, 
						new Article () { Url = "2" }, 
						new Article () { Url = "3" }
					}
				});
			};

		}

		public AsyncActionsCreator<State> NewsFetchActionAsync (TopStoriesCategory category)
		{
			return async (dispatch, getState) => {
				Log.Info ("NewsFetchAction...");
				dispatch (new NewsFetchAction{ Category = category });

				try {
					var models = await this.newsService.TopStories (category);
					Log.Info ($"NewsFetchSuccessAction... {models.Count}");

					dispatch (new NewsFetchSuccessAction { Articles = models });
				} catch (Exception ex) {
					dispatch (new NewsFetchFailureAction { Error = ex });
				}
			};

		}
	}
}

