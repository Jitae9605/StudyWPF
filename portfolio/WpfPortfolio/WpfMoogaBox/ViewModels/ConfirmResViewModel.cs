using Caliburn.Micro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
			SelectedSeatNum = 0;
			SelectedSeatNum = Get_SeatCount;

			// string[] ThroughData = new string[] { seleted.ID, seleted.MvName, seleted.Hall, seleted.StartTime, seleted.EndTime };
			// string[] ThroughData2 = SelectedSeats;
			// int ThroughData3 = SeatCount;


			// 영화정보
			SelectionInfo.Add(Get_SelectedMVInfo[0]);	// ID
			SelectionInfo.Add(Get_SelectedMVInfo[1]);	// MvName
			SelectionInfo.Add(Get_SelectedMVInfo[2]);	// Hall
			SelectionInfo.Add(Get_SelectedMVInfo[3]);	// StartTime
			SelectionInfo.Add(Get_SelectedMVInfo[4]);	// EndTime

			// 좌석
			SelectedSeatfortxtbox = "";
			for(int i = 0; i < SelectedSeatNum; i++)
			{
				SelectedSeat.Add(Get_SelectedSeats[i]);
				SelectedSeatfortxtbox += SelectedSeat[i];

				if(!(i == SelectedSeatNum - 1))
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

			int PriceOfTicket = 8000;
			string ConnString = "Data Source=PC01;Initial Catalog=MoogaBox;Integrated Security=True";
			SqlConnection conn = new SqlConnection(ConnString);

			conn.Open();

			string SqlQuery = @"INSERT INTO TmpReservation
								          ( ID
								          , MvName
								          , Hall
								          , SeatNum
								          , StartTime
								          , Ccount
								          , Mmoney)
								   VALUES 	 
								          ( @ID
								          , @MvName
								          , @Hall
								          , @SeatNum
								          , @StartTime
								          , @Ccount
								          , @Mmoney)";


			SqlCommand cmd = new SqlCommand(SqlQuery, conn);

			SqlParameter parmID = new SqlParameter("@ID", SelectionInfo[0]);
			cmd.Parameters.Add(parmID);

			SqlParameter parmMvName = new SqlParameter("@MvName", SelectionInfo[1]);
			cmd.Parameters.Add(parmMvName);

			SqlParameter parmHall = new SqlParameter("@Hall", SelectionInfo[2]);
			cmd.Parameters.Add(parmHall);

			SqlParameter parmSeatNum = new SqlParameter("@SeatNum", SelectedSeatfortxtbox);
			cmd.Parameters.Add(parmSeatNum);

			SqlParameter parmStartTime = new SqlParameter("@StartTime", SelectionInfo[3]);
			cmd.Parameters.Add(parmStartTime);

			SqlParameter parmCcount = new SqlParameter("@Ccount", SelectedSeatNum);
			cmd.Parameters.Add(parmCcount);

			SqlParameter parmMmoney = new SqlParameter("@Mmoney", PriceOfTicket);
			cmd.Parameters.Add(parmMmoney);



			cmd.ExecuteNonQuery();

			conn.Close();

			this.TryCloseAsync();
			
			var wManager = new WindowManager();
			var res = wManager.ShowWindowAsync(new MessageBox_BuySnackViewModel(SelectionInfo[0]));
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

		private int selectedSeatNum;
		public int SelectedSeatNum
		{
			get => selectedSeatNum;
			set
			{
				selectedSeatNum = value;
				NotifyOfPropertyChange(() => SelectedSeatNum);

			}
		}

	}
}
