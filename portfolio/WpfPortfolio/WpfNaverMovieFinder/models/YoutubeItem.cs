﻿using System.Windows.Media.Imaging;

namespace WpfNaverMovieFinder.models
{
	internal class YoutubeItem

	{
		public string Title { get; set; }
		public string Author { get; set; }
		public string URL { get; set; }
		public BitmapImage Thumbnail { get; set; }

		public YoutubeItem(string title, string author, string url)
		{
			Title = title;
			Author = author;
			URL = url;

		}
	}
}
