using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMoogaBox.ViewModels
{
	public class PaymentWindowViewModel : Conductor<object>
	{
		public PaymentWindowViewModel(string Get_ID)
		{
			ID = Get_ID;
		}


		public string ID { get; set; }
	}

	
}
