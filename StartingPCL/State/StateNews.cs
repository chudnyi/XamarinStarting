﻿using System;
using System.Collections.Immutable;


namespace StartingPCL
{
	/// <summary>
	/// State of news module.
	/// </summary>
	public class StateNews
	{
		public ImmutableDictionary<EntityId, Article> ArticlesById = ImmutableDictionary.Create<EntityId, Article>();
		public ImmutableList<EntityId> VisibleArticlesIds =  ImmutableList.Create<EntityId>();
		public bool IsBusy;

		public StateNews() {
		}

		public StateNews(StateNews other) {
			ArticlesById = other.ArticlesById;
			VisibleArticlesIds = other.VisibleArticlesIds;
			IsBusy = other.IsBusy;
		}
	}
}
