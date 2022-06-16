using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using WpfMoogaBox.Helpers;
using WpfMoogaBox.Models;

namespace WpfMoogaBox.ViewModels
{
	public class SelectSeatViewModel : Screen
	{
		Selection seleted = new Selection();

		string[] SelectedSeats = new string[4];
		int SeatCount = 0;
		public SelectSeatViewModel(string[] GetData)
		{
			AleadyResed = new BindableCollection<bool>();
			bool[] AlreadyRes = new bool[20];

			

			Selected = new BindableCollection<string>();

			// string[] ThroughData = new string[] { ID, seleted.MvName, seleted.Hall, seleted.StartTime, seleted.EndTime  };

			Selected.Add(GetData[0].ToString());
			Selected.Add(GetData[1].ToString());
			Selected.Add(GetData[2].ToString());
			Selected.Add(GetData[3].ToString());
			Selected.Add(GetData[4].ToString());

			seleted.ID = GetData[0].ToString();
			seleted.MvName = GetData[1].ToString();
			seleted.Hall = GetData[2].ToString();
			seleted.StartTime = GetData[3].ToString();
			seleted.EndTime = GetData[4].ToString();

			CheckedSeat = new BindableCollection<string>();

			AlreadyRes = CheckAlreadyRes(seleted.Hall , seleted.MvName, seleted.StartTime);
			for(int i = 0; i < 20; i++)
			{
				AleadyResed.Add(AlreadyRes[i]);
			}

		}

		private bool[] CheckAlreadyRes(string ThNum, string MvName, string StartTime)
		{
			// ZeroOrOne == 0 = 자리있음
			// ZeroOrOne == 1 = 자리없음

			int[] ListOfResedSeat = new int[20];
			bool[] BoolListOfResedSeat = new bool[20];

			string ConnString = "Data Source=PC01;Initial Catalog=MoogaBox;Integrated Security=True";
			SqlConnection conn = new SqlConnection(ConnString);

			conn.Open();

			string SqlQuery = "SELECT Eempty FROM CrJo WHERE MvName = @MvName AND ThNum = @ThNum AND StartTime = @StartTime";



			SqlCommand cmd = new SqlCommand(SqlQuery, conn);

			SqlParameter parmMvName = new SqlParameter("@MvName", MvName);
			cmd.Parameters.Add(parmMvName);

			SqlParameter parmThNum = new SqlParameter("@ThNum", ThNum);
			cmd.Parameters.Add(parmThNum);

			SqlParameter parmStartTime = new SqlParameter("@StartTime", StartTime);
			cmd.Parameters.Add(parmStartTime);

			SqlDataReader reader = cmd.ExecuteReader();

			int i = 0;
			while (reader.Read())
			{
				ListOfResedSeat[i] = Convert.ToInt32(reader["Eempty"]);
				i++;
			}

			for(int j = 0; j < 20; j++)
			{
				if (ListOfResedSeat[j] == 0)
				{
					BoolListOfResedSeat[j] = false;
				}

				else
				{
					BoolListOfResedSeat[j] = true;
				}
			}

			conn.Close();
			reader.Close();

			return BoolListOfResedSeat;

		}

		public void ChangeColorButton(object sender, MouseButtonEventArgs e)
		{
			Button button = sender as Button;
		}

		public void ClickedSeat(object sender, MouseButtonEventArgs e)
		{
			CountSeatNum = new int();

			ToggleButton button = sender as ToggleButton;

			for (int i = 0; i < SeatCount; i++)
			{
				if (CheckedSeat[i] == button.Content.ToString())
				{
					CheckedSeat.Remove(CheckedSeat[i]);
					SeatCount--;
					for (int j = i; j < SeatCount - 1; j++)
						CheckedSeat[j] = CheckedSeat[j + 1];
					CountSeatNum = SeatCount;
					return;
				}
			}

			if(SeatCount > 3)
			{
				button.IsChecked = false;
				CountSeatNum = 4;
				MessageBox.Show("좌석선택은 4개 까지 입니다.");
				return;
			}
			
			CheckedSeat.Add(button.Content.ToString());
			SeatCount++;
			CountSeatNum = SeatCount;
			Thread.Sleep(1);
		}

		public void Cancel2(object sender, MouseButtonEventArgs e)
		{
			Button button = sender as Button;
			button.IsCancel = true;
		}

		public void Next2(object sender, MouseButtonEventArgs e)
		{
			for(int i = 0; i < SeatCount; i++)
			{
				SelectedSeats[i] = CheckedSeat[i];
			}


			string[] ThroughData = new string[] { seleted.ID, seleted.MvName, seleted.Hall, seleted.StartTime, seleted.EndTime };
			string[] ThroughData2 = SelectedSeats;
			int ThroughData3 = SeatCount;

			var wManager = new WindowManager();

			Cancel2(sender, e);
			var result = wManager.ShowDialogAsync(new ConfirmResViewModel(ThroughData, ThroughData2, ThroughData3));
		}

		private BindableCollection<string> checkedSeat;
		public BindableCollection<string> CheckedSeat
		{
			get => checkedSeat; 
			set
			{
				checkedSeat = value;
				NotifyOfPropertyChange(() => CheckedSeat);
			}
		}

		private BindableCollection<string> selected;
		public BindableCollection<string> Selected
		{
			get => selected;
			set
			{
				selected = value;
				NotifyOfPropertyChange(() => Selected);

			}
		}

		private int countSeatNum;
		public int CountSeatNum
		{
			get => countSeatNum;
			set
			{
				countSeatNum = value;
				NotifyOfPropertyChange(() => CountSeatNum);

			}
		}

		private BindableCollection<bool> aleadyResed;
		public BindableCollection<bool> AleadyResed
		{
			get => aleadyResed;
			set
			{
				aleadyResed = value;
				NotifyOfPropertyChange(() => AleadyResed);

			}
		}


	}
}
