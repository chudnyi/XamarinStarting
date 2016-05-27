using System;
using System.Threading.Tasks;
using Refit;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using StartingPCL;

namespace StartingShared
{
	public class NYTimesNewsService : INewsService
	{
		const string TopStoriesApiKey = "8d18d66383464f1da0872ac13988b2a3";
		const string ApiBaseAddress = "https://api.nytimes.com";

		private INYTimesApi restService;

		INYTimesApi RestService {
			get {
				if (restService == null) {
					restService = Refit.RestService.For<StartingShared.INYTimesApi> (ApiBaseAddress);
//					new HttpMessageHandler()
//					new HttpClient()
				}
				return restService;
			}
		}

		public NYTimesNewsService ()
		{
		}

		async Task<TopStoriesResponse> RequestTopStories (TopStoriesCategory category = TopStoriesCategory.home)
		{
			string NYTimesApiKey = TopStoriesApiKey;
			var res = await this.RestService.TopStories (NYTimesApiKey);
			return res;
		}

		public async Task<List<Article>> TopStories (TopStoriesCategory category)
		{
			var response = await this.RequestTopStories (category);
			List<Article> articles = response.Results;
			return articles;
		}

		public Task<List<Article>> TopStories ()
		{
			return this.TopStories (TopStoriesCategory.home);
		}
	}
}

