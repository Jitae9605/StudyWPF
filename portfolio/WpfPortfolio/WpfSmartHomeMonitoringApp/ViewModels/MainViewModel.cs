using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfSmartHomeMonitoringApp.ViewModels
{
	public class MainViewModel : Conductor<object> // Screen에는 ActivateItem[Async] 메서드 없음!
	{
		public MainViewModel()
		{
			DisplayName = "SmartHome Mpnitoring v2.0";	// 프로그램 타이틀, 이름
		}

		public void LoadDataBaseView()
		{
			ActivateItemAsync(new DataBaseViewModel());
		}

		public void LoadRealTimeView()
		{
			ActivateItemAsync(new RealTimeViewModel());
		}

		public void LoadHistoryView()
		{
			ActivateItemAsync(new HistoryViewModel());
		}

		public void ExitProgram()
		{
			Environment.Exit(0); // 프로그램 종료
		}
	}
}
