﻿using System;
using System.Collections.Generic;

namespace StartingPCL
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

		public EntityId Id {
			get { return (EntityId)Url; }
		}

		public Article ()
		{
		}

		public override string ToString ()
		{
			return string.Format ("[Article: Title={0}, Section={1}, Subsection={2}, Abstract={3}, Url={4}, ShortUrl={5}, ItemType={6}]", Title, Section, Subsection, Abstract, Url, ShortUrl, ItemType);
		}


	}
}

