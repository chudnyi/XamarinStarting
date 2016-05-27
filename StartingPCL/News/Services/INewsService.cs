using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StartingPCL
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

	public interface INewsService
	{
		Task<List<Article>> TopStories();
		Task<List<Article>> TopStories(TopStoriesCategory category);
	}
}

