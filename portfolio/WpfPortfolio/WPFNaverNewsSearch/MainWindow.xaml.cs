using MahApps.Metro.Controls;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFNaverNewsSearch
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
		/// 검색 작업(ENTER 입력)
		/// </summary>
		private void txtSearch_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.Key == Key.Enter)
			{
				//Commons.ShowMessageAsync("실행", "뉴스검색 실행!");
				SerchNaverNews();
			}
		}

		/// <summary>
		/// 네이버로 부터 검색결과 내용을 받아오는 메서드
		/// </summary>
		private void SerchNaverNews()
		{
			string clientID = "dJX_GSQ1yGgk6k91iJj0";
			string clientSecret = "HrLgNkRzqP";
			string keyword = txtSearch.Text;
			string base_url = $"https://openapi.naver.com/v1/search/news.json?start=1&display=100&query={keyword}";
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

			MessageBox.Show(result);

			//var parsedJson = JObject.Parse(result);         // string to json
		}
	}
}
