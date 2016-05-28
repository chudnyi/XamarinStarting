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

		async Task<TopStoriesResponse> RequestTopStories (TopStoriesCategory category)
		{
			TopStoriesResponse res = null;
			try {
				res = await this.RestService.TopStories (TopStoriesApiKey);	
			} catch (Exception ex) {
				// TODO: Do not handle all exceptions
				// TODO: Implement error handling 
				Debug.WriteLine ($"Loading error: {ex}");
			}

			return res;
		}

		public async Task<List<Article>> TopStories (TopStoriesCategory category)
		{
			var response = await this.RequestTopStories (category);
			List<Article> articles = response != null ? response.Results : new List<Article> ();
			return articles;
		}

		public Task<List<Article>> TopStories ()
		{
			return this.TopStories (TopStoriesCategory.home);
		}
	}
}

