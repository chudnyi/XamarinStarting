using System;
using System.Threading.Tasks;
using Refit;
using StartingPCL;

namespace StartingShared
{
	public interface INYTimesApi
	{
//		[Get ("/svc/topstories/v2/home.json?api-key={apiKey}")]
//		Task<TopStoriesResponse> TopStories (string apiKey);

		[Get ("/svc/topstories/v2/{category}.json?api-key={apiKey}")]
		Task<TopStoriesResponse> TopStories (string apiKey, TopStoriesCategory category = TopStoriesCategory.home);

		[Get ("/svc/books/v3/lists/best-sellers/history.json?api-key={apiKey}&age-group={ageGroup}")]
		Task<TopStoriesResponse> BooksBestSellers (string apiKey, int ageGroup);
	}
}

