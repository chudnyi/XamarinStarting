using System;
using NSpec;
using StartingPCL;
using System.Collections.Immutable;
using FluentAssertions;
using System.Diagnostics;

namespace Starting.Tests
{

	public class describe_PrimeFactors : nspec
	{
//		Article article;
//		StateNews stateNews;

		void before_each()
		{
//			article = new Article();
//			stateNews = new StateNews () {
//				ArticlesById = ImmutableDictionary.Create<EntityId, Article>(),
//				VisibleArticlesIds =  ImmutableList.Create<EntityId>(),
//				IsBusy = true,
//				NumberOfNewArticles = 220
//			};
		}

		void it_state_news_copy_ctor() {
			var stateNews = new StateNews () {
				ArticlesById = ImmutableDictionary.Create<EntityId, Article>(),
				VisibleArticlesIds =  ImmutableList.Create<EntityId>(),
				IsBusy = true,
				NumberOfNewArticles = 220
			};

			var stateTest = new StateNews (stateNews);

			stateNews.Should ().Equals (stateTest);
			stateNews.NumberOfNewArticles.Should ().Be (stateTest.NumberOfNewArticles);
			stateTest.NumberOfNewArticles.Should ().Be (220);
		}
	}

	public static class Assertions
	{
		public static void equals(this object actual, object expected)
		{
			if(actual != expected) throw new InvalidOperationException("values were not equal");
		}
	}
}

