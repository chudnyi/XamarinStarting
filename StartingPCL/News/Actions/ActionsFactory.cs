﻿using System;
using System.Collections.Generic;
using Redux;
using System.Threading.Tasks;

namespace StartingPCL
{
	internal class ActionsFactory : IActionsFactory
	{

		public IAction CreateNewsFetchAction (TopStoriesCategory category)
		{
			return new NewsFetchAction {
				Category = category
			};
		}

		public AsyncActionsCreator<State> NewsFetchActionAsync (TopStoriesCategory category)
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

	}}
