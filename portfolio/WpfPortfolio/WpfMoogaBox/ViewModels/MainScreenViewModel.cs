using Caliburn.Micro;
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
			var wManager = new WindowManager();			
			var result = wManager.ShowWindowAsync(new MakeReservationViewModel());
			CloseMainCustomerMenu();

		}

		public void LoadBuyMenuView()
		{
			var wManager = new WindowManager();
			var result = wManager.ShowWindowAsync(new BuyMenuViewModel());
			CloseMainCustomerMenu();

		}

		public void CloseMainCustomerMenu()
		{
			this.TryCloseAsync();
		}
	}
}
