using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Windows;

namespace WpfMoogaBox.ViewModels
{
	public class MainScreenViewModel : Conductor<object>
	{
		public MainScreenViewModel()
		{

		}

		public void LoadCheckReservationView()
		{
			var wManager = new WindowManager();
			var result = wManager.ShowWindowAsync(new CheckReservationViewModel());
			CloseMainCustomerMenu();
		}

		public void LoadMakeReservationView()
		{
			string Send_ID = DateTime.Now.ToString("yyMMddHHmmss");
			var wManager = new WindowManager();			
			var result = wManager.ShowWindowAsync(new MakeReservationViewModel(Send_ID));
			CloseMainCustomerMenu();

		}

		public void LoadBuyMenuView()
		{
			string Send_ID = DateTime.Now.ToString("yyMMddHHmmss");
			var wManager = new WindowManager();
			var result = wManager.ShowWindowAsync(new BuyMenuViewModel(Send_ID));
			CloseMainCustomerMenu();

		}

		public void CloseMainCustomerMenu()
		{
			this.TryCloseAsync();
		}
	}
}
