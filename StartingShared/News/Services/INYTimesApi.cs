using System;
using System.Threading.Tasks;
using Refit;

namespace StartingShared
{
	public enum TopStoriesCategory
	{
		home,
		opinion,
		world,
		national,
		politics,
		upshot,
		nyregion,
		business,
		technology,
		science,
		health,
		sports,
		arts,
		books,
		movies,
		theater,
		sundayreview,
		fashion,
		tmagazine,
		food,
		travel,
		magazine,
		realestate,
		automobiles,
		obituaries,
		insider
	}


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

