using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Threading.Tasks;
using System.Windows;

namespace WPFNaverNewsSearch
{
	public class Commons
	{
		/// <summary>
		/// 메트로 UI용 메시지박스 실행 메소드
		/// </summary>
		/// <param name="title"></param>	메시지박스 타이틀
		/// <param name="message"></param>	메시지박스 내용
		/// <param name="style"></param>
		/// <returns></returns>
		public static async Task<MessageDialogResult> ShowMessageAsync(
			string title, string message, MessageDialogStyle style = MessageDialogStyle.Affirmative)
		{
			return await ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync(title, message, style, null);
		}
	}
}