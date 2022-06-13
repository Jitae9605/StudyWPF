using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Windows;
using WpfMoogaBox.ViewModels;

namespace WpfMoogaBox
{
	public class Bootstrapper : BootstrapperBase
	{
		private SimpleContainer container;

		public Bootstrapper()
		{
			Initialize();
		}

		protected override void Configure()
		{
			container = new SimpleContainer();
			container.Singleton<IWindowManager, WindowManager>();
			container.PerRequest<MainScreenViewModel>();
		}

		protected override void OnStartup(object sender, StartupEventArgs e)
		{
			//base.OnStartup(sender, e);
			DisplayRootViewFor<MainScreenViewModel>();
		}

		protected override object GetInstance(Type service, string key)
		{
			return container.GetInstance(service, key);
		}

		protected override IEnumerable<object> GetAllInstances(Type service)
		{
			return container.GetAllInstances(service);
		}

		protected override void BuildUp(object instance)
		{
			container.BuildUp(instance);
		}
	}
}
