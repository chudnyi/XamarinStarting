using System;
using System.Collections.Generic;
using StartingPCL;

namespace StartingPCL
{
	internal class TopStoriesResponse
	{
		public string Status { get; set; }
		public string Section { get; set; }
		// results
		public List<Article> Results { get; set; }

		public TopStoriesResponse ()
		{
		}
	}
}

