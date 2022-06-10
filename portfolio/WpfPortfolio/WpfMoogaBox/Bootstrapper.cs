using Caliburn.Micro;
using System.Windows;
using WpfMoogaBox.ViewModels;

namespace WpfMoogaBox
{
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
