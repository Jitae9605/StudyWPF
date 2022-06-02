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
using System.Windows.Media.Imaging;
using WpfNaverMovieFinder.models;

namespace WpfNaverMovieFinder
{
	/// <summary>
	/// MainWindow.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class MainWindow : MetroWindow
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 검색버튼 클릭 이벤트 핸들러
		/// 네이버 OpenAPI 검색
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSearch_Click(object sender, RoutedEventArgs e)
		{
			// 상태바의 검색결과 내용 초기화
			stsResult.Content = string.Empty;

			// 검색창에 입력된값이 비었거나 null인걸을 판별
			if (string.IsNullOrEmpty(txtSearchName.Text))
			{
				// 상태바에 안내문 표시 및 안내 메세지박스 팝업
				stsResult.Content = "검색할 영화명을 입력하고 검색버튼을 눌러주세요.";
				//MessageBox.Show("검색할 영화명을 입력하고 검색버튼을 눌러주세요.");
				Commons.ShowMessageAsync("검색", "검색할 영화명을 입력하고 검색버튼을 눌러주세요.");

				return;
			}



			// 검색시작
			//Commons.ShowMessageAsync("결과", $"{txtSearchName.Text}");
			try
			{
				SearchNaverOpenApi(txtSearchName.Text);
				Commons.ShowMessageAsync("검색", "영화검색 완료!!");
			}
			catch
			{

			}
		}

		/// <summary>
		/// 네이버 실제 검색 메서드
		/// </summary>
		/// <param name="serchName"></param> 
		private void SearchNaverOpenApi(string serchName)
		{
			string clientID = "dJX_GSQ1yGgk6k91iJj0";
			string clientSecret = "HrLgNkRzqP";
			string openApiUri = $"https://openapi.naver.com/v1/search/movie?start=1&display=30&query={serchName}";
			string result = string.Empty;   // 빈값으로 초기화

			// 리소스 반환목적
			WebRequest request = null;
			WebResponse response = null;
			Stream stream = null;
			StreamReader reader = null;

			// NaverOpenAPI 접속해서 실제 요청
			try
			{
				// 리퀘스트의 헤더꼴등은 웹페이지 등에 따라 모두 다르다!(요구하는 것도 모양도 이름도)
				request = WebRequest.Create(openApiUri);
				request.Headers.Add("X-Naver-Client-Id", clientID); // 중요!
				request.Headers.Add("X-Naver-Client-Secret", clientSecret); // 중요!!

				response = request.GetResponse();	// 요청한것에 대한 응답을 받는다.

				stream = response.GetResponseStream();	// 요청한것에 대한 응답을 스트림으로 부터 받아온다.
				reader = new StreamReader(stream);

				result = reader.ReadToEnd();
			}
			catch(Exception ex)
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

			var parsedJson = JObject.Parse(result);			// string to json

			int total = Convert.ToInt32(parsedJson["total"]);
			int display = Convert.ToInt32(parsedJson["display"]);

			stsResult.Content = $"{total} 중 {display} 호출 성공!";

			// 데이터그리드에 검색결과 할당
			var items = parsedJson["items"];	// 결과중 "items"만을 뽑아온다.(Title, image, url 등이 들어있음)
			var json_array = (JArray)items;		// 이를 JArray형식으로 변환해 json_array에 저장 

			List<MovieItem> movieItems = new List<MovieItem>();

			foreach(var item in json_array)
			{
				MovieItem movie = new MovieItem( 
					Regex.Replace( item["title"].ToString(),@"<(.|\n)*?>",string.Empty),	// title 에서 html태그(<b>,<\b>...) 삭제
					item["link"].ToString(),		
					item["image"].ToString(),
					Regex.Replace(item["subtitle"].ToString(), @"<(.|\n)*?>", string.Empty),
					item["pubDate"].ToString(),
					item["director"].ToString().Replace("|", ", ").Substring(0, item["director"].ToString().Replace("|", ", ").Length - 1), // "|" 를 ", "로 교체
					item["actor"].ToString().Replace("|", ", ").Substring(0, item["director"].ToString().Replace("|", ", ").Length - 1),
					item["userRating"].ToString());
				movieItems.Add(movie);
			}


			this.DataContext = movieItems;
		}

		private void btnAddWatchList_Click(object sender, RoutedEventArgs e)
		{

		}

		private void btnViewWatchList_Click(object sender, RoutedEventArgs e)
		{

		}

		private void btnDelWatchList_Click(object sender, RoutedEventArgs e)
		{

		}

		private void btnWatchTrailer_Click(object sender, RoutedEventArgs e)
		{

		}


		/// <summary>
		/// 선택한 영화의 포스터 보이기
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void grdResult_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
		{
			if(grdResult.SelectedItem is MovieItem)
			{
				var movie = grdResult.SelectedItem as MovieItem;

				if (string.IsNullOrEmpty(movie.Image))
				{
					imgPoster.Source = new BitmapImage(new Uri("/resource/No_Picture.jpg",UriKind.RelativeOrAbsolute));
				}
				else
				{
					imgPoster.Source = new BitmapImage(new Uri(movie.Image, UriKind.RelativeOrAbsolute));
				}
			}
		}

		private void txtSearchName_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
		{
			if (e.Key == System.Windows.Input.Key.Enter) btnSearch_Click(sender, e);
		}

		/// <summary>
		/// 네이버영화 웹브라우저 열기
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnNaverTrailer_Click(object sender, RoutedEventArgs e)
		{
			if(grdResult.SelectedItems.Count == 0)
			{
				Commons.ShowMessageAsync("네이버 영화", "영화를 선택하세요.");
				return;
			}

			if(grdResult.SelectedItems.Count > 1)
			{
				Commons.ShowMessageAsync("네이버 영화", "영화를 하나만 선택하세요.");
				return;
			}

			string linkurl = (grdResult.SelectedItem as MovieItem).Link;
			Process.Start(linkurl);


		}
	}
}
