using Caliburn.Micro;
using WpfSmartHomeMonitoringApp.Helpers;

namespace WpfSmartHomeMonitoringApp.ViewModels
{
	public class CustomInfoViewModel : Conductor<object>
	{
		private string applicationInfo;

		public CustomInfoViewModel(string title)
		{
			this.DisplayName = title;
		}

		public string ApplicationInfo
		{
			get => applicationInfo; 
			set
			{
				applicationInfo = value;
				NotifyOfPropertyChange(() => ApplicationInfo);
			}
		}

		public void AcceptClose()
		{
			// 창닫기
			TryCloseAsync(true);
		}

	}
}
