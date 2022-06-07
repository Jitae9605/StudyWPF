using Caliburn.Micro;
using System.Windows;
using WPF_CaliburnApp.ViewModels;

namespace WPF_CaliburnApp
{
	/// <summary>
	/// 시작 윈도우 지정하는 클래스
	/// </summary>
	public class Bootstrapper : BootstrapperBase
	{
		public Bootstrapper()
		{
			Initialize();
		}

		protected override void OnStartup(object sender, StartupEventArgs e)
		{
			//base.OnStartup(sender, e);
			DisplayRootViewFor<MainViewModel>();
		}
	}
}
