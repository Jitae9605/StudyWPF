using Caliburn.Micro;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfMoogaBox.Helpers;
using WpfMoogaBox.Models;

namespace WpfMoogaBox.ViewModels
{
	public class CompleteResViewModel : Conductor<object>
	{
	
		public CompleteResViewModel(string Get_ID, Mv_Info GetSelectedMvIfod)
		{
			this.ActivateAsync();
			
			AddDatatoTxtBox = new BindableCollection<string>();
			ID = Get_ID;

			AddDatatoTxtBox.Add(GetSelectedMvIfod.MvName);
			AddDatatoTxtBox.Add(GetSelectedMvIfod.Hall);
			AddDatatoTxtBox.Add(GetSelectedMvIfod.StartTime + " ~ " + GetSelectedMvIfod.EndTime);
			AddDatatoTxtBox.Add(GetSelectedMvIfod.Seat);
			AddDatatoTxtBox.Add(ID);
			
		}

		public async void GotoPrintTicket(object sender, MouseButtonEventHandler e)
		{
			IWindowManager wManager = new WindowManager();
			var result = wManager.ShowWindowAsync(new CheckReservationViewModel(ID));

			App.Current.Windows[0].Close();
			App.Current.Windows[1].Close();
		}

		BindableCollection<string> addDatatoTxtBox;

		public BindableCollection<string> AddDatatoTxtBox
		{
			get => addDatatoTxtBox;
			set
			{
				addDatatoTxtBox = value;
				NotifyOfPropertyChange(() => AddDatatoTxtBox);
			}
		}

		public string ID { get; set; }
	}
}
