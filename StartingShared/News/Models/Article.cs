using System;
using System.Collections.Generic;

namespace StartingShared
{
	public class Multimedia
	{
		public string Url { get; set; }

		public string Format { get; set; }

		public int Height { get; set; }

		public int Width { get; set; }

//		public string Type { get; set; }

		public string Subtype { get; set; }

		public string Caption { get; set; }

		public string Copyright { get; set; }
	}

	public class Article
	{
		public string Title { get; set; }

		public string Section { get; set; }

		public string Subsection { get; set; }

		public string Abstract { get; set; }

		public string Url { get; set; }

		public string ShortUrl { get; set; }

		public string ItemType { get; set; }

//		public DateTime UpdatedDate { get; set; }
//
//		public DateTime CreatedDate { get; set; }
//
//		public DateTime PublishedDate { get; set; }

		public List<Multimedia> Multimedia { get; set; }

		public Article ()
		{
		}
	}
}

