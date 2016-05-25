using System;
using System.Collections.Generic;

namespace StartingShared
{
	public class Multimedia
	{
		public string url { get; set; }

		public string format { get; set; }

		public int height { get; set; }

		public int width { get; set; }

		public string type { get; set; }

		public string subtype { get; set; }

		public string caption { get; set; }

		public string copyright { get; set; }
	}

	public class Article
	{
		public string Title { get; set; }

		public string Section { get; set; }

		public string Subsection { get; set; }

		public string Abstract { get; set; }

		public string url { get; set; }

		public string short_url { get; set; }

		public string item_type { get; set; }

		public DateTime updated_date { get; set; }

		public DateTime created_date { get; set; }

		public DateTime published_date { get; set; }

		public List<Multimedia> multimedia { get; set; }

		public Article ()
		{
		}
	}
}

