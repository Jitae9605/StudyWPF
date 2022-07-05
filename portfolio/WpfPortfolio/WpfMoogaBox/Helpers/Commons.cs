using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Threading.Tasks;
using System.Windows;

namespace WpfMoogaBox.Helpers
{
	public class Commons
	{
		/// <summary>
		/// 메트로 UI용 메시지박스 실행 메소드
		/// </summary>
		/// <param name="title"></param>
		/// <param name="message"></param>
		/// <param name="style"></param>
		/// <returns></returns>
		public static async Task<MessageDialogResult> ShowMessageAsync(
			string title, string message, int i = 0, MessageDialogStyle style = MessageDialogStyle.Affirmative)
		{
			return await (Application.Current.Windows[i] as MetroWindow).ShowMessageAsync(title, message, style, null);
		}
	}
}