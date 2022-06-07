using MahApps.Metro.Controls;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
		bool IsFavorite = false;    // 네이버 API로 검색한것인지 or 즐겨찾기(DB)에서 가져온건지 확인하는 값
									// true -> DB  ,  false -> NaverOpenAPI
		public MainWindow()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 검색버튼 클릭 이벤트 핸들러
		/// 네이버 OpenAPI 검색
		/// </summary>
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
				IsFavorite = false;
			}
			catch(Exception ex)
			{
				Commons.ShowMessageAsync("예외", $"예외발생 : {ex} ");

			}
		}

		/// <summary>
		/// 네이버 실제 검색 메서드
		/// </summary>
		/// <param name="serchName"></param> 검색내용
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
					item["director"].ToString().Replace("|", ", ").Trim(',',' '), // "|" 를 ", "로 교체 + 맨 끝의 ", "제거 
					item["actor"].ToString().Replace("|", ", ").Trim(',', ' '),
					item["userRating"].ToString());
				movieItems.Add(movie);
			}


			this.DataContext = movieItems;
		}

		/// <summary>
		/// 즐겨찾기 추가
		/// </summary>
		private void btnAddWatchList_Click(object sender, RoutedEventArgs e)
		{
			if(grdResult.SelectedItems.Count == 0)
			{
				Commons.ShowMessageAsync("오류", "즐겨찾기에 추가 할 영화를 선택해주세요(복수선택 가능)");
				return;
			}

			if(IsFavorite == true)
			{
				Commons.ShowMessageAsync("오류", "이미 즐겨찾기한 영화입니다!");
				return;
			}

			List<TblFavoriteMovies> list = new List<TblFavoriteMovies>();
			foreach (MovieItem item in grdResult.SelectedItems)
			{
				TblFavoriteMovies temp = new TblFavoriteMovies()
				{
					// Idx = identity 이므로 생략
					Title = item.Title,
					Link = item.Link,
					Image = item.Image,
					SubTitle = item.SubTitle,
					PubDate = item.PubDate,
					Director = item.Director,
					Actor = item.Actor,
					UserRating = item.UserRating,
					RegDate = DateTime.Now
				};

				list.Add(temp);
			}

			// EF 테이블 데이터 입력(insert 부분)
			try
			{
				using (var ctx = new OpenApiLabEntities())      // App.config에 있음
				{
					ctx.Set<TblFavoriteMovies>().AddRange(list);	// insert문 대체
					ctx.SaveChanges();	// = commit 대체
				}
				Commons.ShowMessageAsync("저장", "즐겨찾기 추가 성공!!");
			}

			catch(Exception ex)
			{
				Commons.ShowMessageAsync("예외", $"예외발생 : {ex}");
			}
		}

		/// <summary>
		/// 즐겨찾기 조회
		/// </summary>
		private void btnViewWatchList_Click(object sender, RoutedEventArgs e)
		{
			this.DataContext = null;
			txtSearchName.Text = string.Empty;

			List<TblFavoriteMovies> list = new List<TblFavoriteMovies>();

			try
			{
				using (var ctx = new OpenApiLabEntities())
				{
					list = ctx.TblFavoriteMovies.ToList();
				}

				this.DataContext = list;
				stsResult.Content = $"즐겨찾기 {list.Count}개 조회";
				Commons.ShowMessageAsync("즐겨찾기", "즐겨찾기 조회 완료");
				IsFavorite = true;	//db에서 왔음

			}
			catch(Exception ex)
			{
				Commons.ShowMessageAsync("예외", $"예외발생 : {ex}");
				IsFavorite = false;
			}
		}

		/// <summary>
		/// 즐겨찾기 목록삭제
		/// </summary>
		private void btnDelWatchList_Click(object sender, RoutedEventArgs e)
		{
			if(IsFavorite == false)
			{
				Commons.ShowMessageAsync("오류", "즐겨찾기 내용이 아니면 삭제할 수 없습니다.");
				return;
			}

			if(grdResult.SelectedItems.Count == 0)
			{
				Commons.ShowMessageAsync("오류", "삭제할 영화를 선택하세요.");
				return;
			}

			foreach (TblFavoriteMovies item in grdResult.SelectedItems)
			{
				using (var ctx = new OpenApiLabEntities())
				{
					// DB에서의 삭제처리
					var delItem = ctx.TblFavoriteMovies.Find(item.idx);
					ctx.Entry(delItem).State = System.Data.Entity.EntityState.Deleted;  // 객체상태를 삭제로 변경
					ctx.SaveChanges();	// = 커밋
				}
			}

			btnViewWatchList_Click(sender, e);	// 즐겨찾기보기 버튼클릭 이벤트 실행
		}

		/// <summary>
		/// 유튜브 예고편 보기
		/// </summary>
		private void btnWatchTrailer_Click(object sender, RoutedEventArgs e)
		{
			if(grdResult.SelectedItems.Count == 0)
			{
				Commons.ShowMessageAsync("유튜브영화", "영화를 선택하세요");
				return;
			}
			
			if(grdResult.SelectedItems.Count > 1)
			{
				Commons.ShowMessageAsync("유튜브영화", "영화를 하나만 선택하세요.");
				return;
			}

			string movieName = "";	// string empty

			if(IsFavorite == true)  // 즐겨찾기 DB검색한 값
			{
				movieName = (grdResult.SelectedItem as TblFavoriteMovies).Title;
			}
			else
			{
				movieName = (grdResult.SelectedItem as MovieItem).Title;
			}

			var trailerWindow = new TrailerWindow(movieName);	// 영화제목을 파라미터로 받는 생성자
			trailerWindow.Owner = this;		// 메인 윈도우
			trailerWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
			trailerWindow.ShowDialog();		// 모달창


		}


		/// <summary>
		/// 선택한 영화의 포스터 보이기
		/// </summary>
		private void grdResult_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
		{
			if(grdResult.SelectedItem is MovieItem)	// 네이버 API에서 온값인 경우
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

			if(grdResult.SelectedItem is TblFavoriteMovies)                // 즐겨찾기 DB에서 온값인 경우
			{
				var movie = grdResult.SelectedItem as TblFavoriteMovies;

				if (string.IsNullOrEmpty(movie.Image))
				{
					imgPoster.Source = new BitmapImage(new Uri("/resource/No_Picture.jpg", UriKind.RelativeOrAbsolute));
				}
				else
				{
					imgPoster.Source = new BitmapImage(new Uri(movie.Image, UriKind.RelativeOrAbsolute));
				}
			}
		}

		/// <summary>
		/// enter 누르면 검색되게하기
		/// </summary>
		private void txtSearchName_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
		{
			if (e.Key == System.Windows.Input.Key.Enter) btnSearch_Click(sender, e);
		}

		/// <summary>
		/// 네이버영화 웹브라우저 열기
		/// </summary>
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

			string linkurl = String.Empty;
			if(IsFavorite == true)
			{
				linkurl = (grdResult.SelectedItem as TblFavoriteMovies).Link;
			} else
			{
				linkurl = (grdResult.SelectedItem as MovieItem).Link;

			}
			Process.Start(linkurl);


		}
	}
}
