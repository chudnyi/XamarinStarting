using System;
using System.Threading.Tasks;
using Refit;

namespace StartingShared
{
	public interface INYTimesApi
	{
		[Get("/svc/topstories/v2/home.json?api-key={apiKey}")]
		Task<TopStoriesResponse> TopStories(string apiKey);
	}
}

