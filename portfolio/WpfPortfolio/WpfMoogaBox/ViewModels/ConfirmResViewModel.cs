using Caliburn.Micro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfMoogaBox.Helpers;

namespace WpfMoogaBox.ViewModels
{
	public class ConfirmResViewModel : Conductor<object>
	{
		public ConfirmResViewModel(string[] Get_SelectedMVInfo, string[] Get_SelectedSeats, int Get_SeatCount)
		{

			SelectionInfo = new BindableCollection<string>();
			SelectedSeat = new BindableCollection<string>();

			// string[] ThroughData = new string[] { seleted.ID, seleted.MvName, seleted.Hall, seleted.StartTime, seleted.EndTime };
			// string[] ThroughData2 = SelectedSeats;
			// int ThroughData3 = SeatCount;


			// 영화정보
			SelectionInfo.Add(Get_SelectedMVInfo[0]);
			SelectionInfo.Add(Get_SelectedMVInfo[1]);
			SelectionInfo.Add(Get_SelectedMVInfo[2]);
			SelectionInfo.Add(Get_SelectedMVInfo[3]);
			SelectionInfo.Add(Get_SelectedMVInfo[4]);

			// 좌석
			SelectedSeatfortxtbox = "";
			for(int i = 0; i < Get_SeatCount; i++)
			{
				SelectedSeat.Add(Get_SelectedSeats[i]);
				SelectedSeatfortxtbox += SelectedSeat[i];

				if(!(i == Get_SeatCount - 1))
				{
					SelectedSeatfortxtbox += " ,";
				}
			}
		}

		public void Cancel3(object sender, MouseButtonEventArgs e)
		{
			this.TryCloseAsync();
			var wManager = new WindowManager();
			string[] Send_SelectedMVInfo = new string[] { SelectionInfo[0], SelectionInfo[1], SelectionInfo[2], SelectionInfo[3], SelectionInfo[4] };

			var res = wManager.ShowWindowAsync(new SelectSeatViewModel(Send_SelectedMVInfo));
		}

		public void Next3(object sender, MouseButtonEventArgs e)
		{
			this.TryCloseAsync();
			// TmpRes..에 저장
			var wManager = new WindowManager();
			var res = wManager.ShowWindowAsync(new MessageBox_BuySnackViewModel());
		}


		private BindableCollection<string> selectionInfo;
		public BindableCollection<string> SelectionInfo
		{
			get => selectionInfo;
			set
			{
				selectionInfo = value;
				NotifyOfPropertyChange(() => SelectionInfo);

			}
		}

		private BindableCollection<string> selectedSeat;
		public BindableCollection<string> SelectedSeat
		{
			get => selectedSeat;
			set
			{
				selectedSeat = value;
				NotifyOfPropertyChange(() => SelectedSeat);

			}
		}


		private string selectedSeatfortxtbox;
		public string SelectedSeatfortxtbox
		{
			get => selectedSeatfortxtbox; 
			set
			{
				selectedSeatfortxtbox = value;
				NotifyOfPropertyChange(() => SelectedSeatfortxtbox);

			}
		}

	}
}
