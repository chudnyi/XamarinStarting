using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StartingShared
{
	public interface INewsService
	{
		Task<List<Article>> TopStories();

	}
}

