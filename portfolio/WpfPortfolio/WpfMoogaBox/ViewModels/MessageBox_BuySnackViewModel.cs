using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfMoogaBox.ViewModels
{
	public class MessageBox_BuySnackViewModel : Conductor<object>
	{
		public MessageBox_BuySnackViewModel(string Get_ID)
		{
			ID = Get_ID;
		}

		public void CanceltoPaying(object sender, MouseButtonEventArgs e)
		{
			this.TryCloseAsync();
			
			IWindowManager wManager = new WindowManager();
			var result = wManager.ShowWindowAsync(new PaymentWindowViewModel(ID));
			
		}

		public void NexttoBuySnack(object sender, MouseButtonEventArgs e)
		{
			this.TryCloseAsync();
			
			IWindowManager wManager = new WindowManager();
			var result = wManager.ShowWindowAsync(new BuyMenuViewModel(ID));
			
		}

		private string iD;

		public string ID 
		{ get => iD; set => iD = value; }
	}
}
