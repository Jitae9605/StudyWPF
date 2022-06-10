using MahApps.Metro.Controls;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPFNaverNewsSearch
{
	/// <summary>
	/// MainWindow.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class MainWindow : MetroWindow
	{
		string curr_cisplayNum;
		string curr_view = "0";
		int result_total = 0;

		public MainWindow()
		{
			InitializeComponent();
		}


		/// <summary>
		/// 검색 작업(ENTER 입력)
		/// </summary>
		private void txtSearch_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				curr_view = "0";
				result_total = 0;
				//Commons.ShowMessageAsync("실행", "뉴스검색 실행!");
				SerchNaverNews();
			}
		}

		/// <summary>
		/// 네이버로 부터 검색결과 내용을 받아오는 메서드
		/// </summary>
		public void SerchNaverNews()
		{
			string clientID = "dJX_GSQ1yGgk6k91iJj0";
			string clientSecret = "HrLgNkRzqP";
			string keyword = txtSearch.Text;
			string Viewdisplay = curr_cisplayNum;
			string pageNum = curr_view;
			string ViewNum = (Convert.ToInt32(pageNum) * Convert.ToInt32(Viewdisplay) + 1).ToString();
			string base_url = $"https://openapi.naver.com/v1/search/news.json?start={ViewNum}&display={Viewdisplay}&query={keyword}";
			string result;


			WebRequest request = null;
			WebResponse response = null;
			Stream stream = null;
			StreamReader reader = null;

			// NaverOpenAPI 접속해서 실제 요청
			try
			{
				// 리퀘스트의 헤더꼴등은 웹페이지 등에 따라 모두 다르다!(요구하는 것도 모양도 이름도)
				request = WebRequest.Create(base_url);
				request.Headers.Add("X-Naver-Client-Id", clientID); // 중요!
				request.Headers.Add("X-Naver-Client-Secret", clientSecret); // 중요!!

				response = request.GetResponse();   // 요청한것에 대한 응답을 받는다.
				stream = response.GetResponseStream();  // 요청한것에 대한 응답을 스트림으로 부터 받아온다.
				reader = new StreamReader(stream);

				result = reader.ReadToEnd();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				// 리소스 반환
				//request.Close();  // 얘는 자동으로 삭제되는듯?
				reader.Close();
				stream.Close();
				response.Close();
			}

			//MessageBox.Show(result); // 네이버 뉴스 검색결과의 꼴(json) 확인(한번 확인하고 스샷찍기)

			var parsedJson = JObject.Parse(result); // string to json

			int total = Convert.ToInt32(parsedJson["total"]); // 전체 검색 결과수
			result_total = total;
			stsResult.Content = $"검색결과 : {result_total}개 / 현재 페이지 : {(Convert.ToInt32(curr_view) * Convert.ToInt32(curr_cisplayNum)) + 1} ~ {(Convert.ToInt32(curr_view) + 1) * Convert.ToInt32(curr_cisplayNum)}";
			int display = Convert.ToInt32(parsedJson["display"]); // 10

			List<NewsItem> newsItems = new List<NewsItem>();

			var items = parsedJson["items"];
			var json_array = (JArray)items;

			foreach (var item in json_array)
			{
				var temp = DateTime.Parse(item["pubDate"].ToString());

				NewsItem news = new NewsItem()
				{
					Title = Regex.Replace(item["title"].ToString(), @"<(.|\n)*?>", string.Empty),
					//Title = item["title"].ToString(),
					OriginalLink = item["originallink"].ToString(),
					Link = item["link"].ToString(),
					Description = Regex.Replace(item["description"].ToString(), @"<(.|\n)*?>", string.Empty),
					PubDate = temp.ToString("yyyy-MM-dd HH:mm"), // 게시일 형식변경
				};

				newsItems.Add(news);
			}
			this.DataContext = newsItems;

		}

		private void dgrResult_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
		{
			if (dgrResult.SelectedItem == null) return; // 두번째 검색부터 생기는 오류를 제거

			string link = (dgrResult.SelectedItem as NewsItem).Link;
			Process.Start(link);
		}

		private void cobViewNum_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ComboBox currentComboBox = sender as ComboBox;
			if (currentComboBox != null)
			{
				ComboBoxItem currentItem = currentComboBox.SelectedItem as ComboBoxItem;
				if (currentItem != null)
				{
					curr_cisplayNum = currentItem.DataContext.ToString();
				}

			}

		}

		private void btnPreb_Click(object sender, RoutedEventArgs e)
		{
			if (Convert.ToInt32(curr_view) > 0)
			{
				curr_view = (Convert.ToInt32(curr_view) - 1).ToString();
				SerchNaverNews();
			}
			else
				return;
		}

		private void btnNext_Click(object sender, RoutedEventArgs e)
		{
			if (Convert.ToInt32(curr_view) * Convert.ToInt32(curr_cisplayNum) < result_total)
			{
				curr_view = (Convert.ToInt32(curr_view) + 1).ToString();
				SerchNaverNews();
			}
			else
				return;
		}
	}

	internal class NewsItem
	{
		public string Title { get; set; }
		public string OriginalLink { get; set; }
		public string Link { get; set; }
		public string Description { get; set; }
		public string PubDate { get; set; }
	}
}

// 추가할만한 기능
//	- 뉴스 스크랩기능
//	- 다음 결과보기 <<   >> 만들기	- 완료
//	- 뷰 갯수 지정기능 추가(10개보기, 50개 보기 ... )	- 완료
//	- 타이틀등에 <b>.. 다빼기 - 완료
//	- 아이콘
//	- 