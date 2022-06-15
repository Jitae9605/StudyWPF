using Caliburn.Micro;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
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
		public ConfirmResViewModel(string[] GetData1, string[] GetData2, int GetData3)
		{

			SelectionInfo = new BindableCollection<string>();
			SelectedSeat = new BindableCollection<string>();

			// string[] ThroughData = new string[] { seleted.ID, seleted.MvName, seleted.Hall, seleted.StartTime, seleted.EndTime };
			// string[] ThroughData2 = SelectedSeats;
			// int ThroughData3 = SeatCount;


			// 영화정보
			SelectionInfo.Add(GetData1[0]);
			SelectionInfo.Add(GetData1[1]);
			SelectionInfo.Add(GetData1[2]);
			SelectionInfo.Add(GetData1[3]);
			SelectionInfo.Add(GetData1[4]);

			// 좌석
			SelectedSeatfortxtbox = "";
			for(int i = 0; i < GetData3; i++)
			{
				SelectedSeat.Add(GetData2[i]);
				SelectedSeatfortxtbox += SelectedSeat[i];

				if(!(i == GetData3 - 1))
				{
					SelectedSeatfortxtbox += " ,";
				}
			}
		}

		public void Cancel3(object sender, MouseButtonEventArgs e)
		{
			Button button = sender as Button;
			button.IsCancel = true;
		}

		public async void Next3(object sender, MouseButtonEventArgs e)
		{
			var result = await Commons.ShowMessageAsync("추가 구매","매점식품을 추가 구매하시겠습니까?", MessageDialogStyle.AffirmativeAndNegative) ;
			if(result == MessageDialogResult.Affirmative)
			{
				var wManager = new WindowManager();

				Cancel3(sender, e);
				var res = wManager.ShowDialogAsync(new BuyMenuViewModel());
			}
			else
			{
				var wManager = new WindowManager();

				Cancel3(sender, e);
				var res = wManager.ShowDialogAsync(new BuyMenuViewModel());
			}
			//for (int i = 0; i < SeatCount; i++)
			//{
			//	SelectedSeats[i] = CheckedSeat[i];
			//}


			//string[] ThroughData = new string[] { seleted.ID, seleted.MvName, seleted.Hall, seleted.StartTime, seleted.EndTime };
			//string[] ThroughData2 = SelectedSeats;
			//int ThroughData3 = SeatCount;

			//var wManager = new WindowManager();

			//Cancel2(sender, e);
			//var result = wManager.ShowDialogAsync(new ConfirmResViewModel(ThroughData, ThroughData2, ThroughData3));
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
