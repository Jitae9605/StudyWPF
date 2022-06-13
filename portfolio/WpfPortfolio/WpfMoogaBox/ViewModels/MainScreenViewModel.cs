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

			ActivateItemAsync(new CheckReservationViewModel());
		}

		public void LoadMakeReservationView()
		{
			//var settings = new Dictionary<string, object>
			//{
			//	{ "SizeToContent", SizeToContent.Manual },
			//	{ "Height" , 100  },
			//	{ "Width"  , 1500 },
			//};
			//ActivateItemAsync(new MakeReservationViewModel());
			var wManager = new WindowManager();			
			var result = wManager.ShowDialogAsync(new MakeReservationViewModel());
		}

		public void LoadBuyMenuView()
		{
			ActivateItemAsync(new BuyMenuViewModel());
		}
	}
}
