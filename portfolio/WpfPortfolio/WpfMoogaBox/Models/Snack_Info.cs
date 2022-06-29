using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMoogaBox.Models
{
	public class Snack_Info
	{
		public string SnackName { get; set; }
		public string SnackNum { get; set; }
		public int SnackPrice { get; set; }
		public int Count { get; set; }
		public int Sum { get; set; }

		public Snack_Info(string snackName, string snackNum, int snackPrice, int count = 1)
		{
			SnackName = snackName;
			SnackNum = snackNum;
			SnackPrice = snackPrice;
			Count = count;
			Sum = snackPrice * count;
		}
	}
}
