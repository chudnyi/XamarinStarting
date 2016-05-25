using System;
using System.Threading.Tasks;
using Refit;
using System.Collections.Generic;
using System.Diagnostics;

namespace StartingShared
{
	public class NYTimesNewsService : INewsService
	{
		const string ApiKey = "8d18d66383464f1da0872ac13988b2a3";
		const string ApiBaseAddress = "https://api.nytimes.com";

		private INYTimesApi restService;

		INYTimesApi RestService {
			get {
				if (restService == null)
					restService = Refit.RestService.For<StartingShared.INYTimesApi> (ApiBaseAddress);
				return restService;
			}
		}

		public NYTimesNewsService ()
		{
		}

		async Task<TopStoriesResponse> RequestTopStories ()
		{
			var res = await this.RestService.TopStories (ApiKey);
			return res;
		}

		public async Task<List<Article>> TopStories ()
		{
			var response = await this.RequestTopStories ();
			List<Article> articles = response.Results;

//			return new List<Article> { new Article{ Title = "Article 1" }, new Article{ Title = "Article 2" } };
			return articles;
		}

	}
}

