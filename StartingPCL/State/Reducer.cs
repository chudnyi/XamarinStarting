using System;
using Redux;

namespace StartingPCL
{
	public class Reducer
	{
		public static State Execute(State state, IAction action) {
			return new State () {
				StateNews = ReducerNews.Execute (state.StateNews, action)
			};
		}
	}
}

