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

		public SelectSeatViewModel(string[] Get_SelectedMVInfo)
		{
			// string[] Get_SelectedMVInfo = new string[] { ID, seleted.MvName, seleted.Hall, seleted.StartTime, seleted.EndTime, selected.MVNum  };
			InitSeatSelectPage(Get_SelectedMVInfo);
		}

		public void ClickedSeat(object sender, MouseButtonEventArgs e)
		{
			MakeSratArray(sender);
		}


		public void Cancel2(object sender, MouseButtonEventArgs e)
		{
			var wManager = new WindowManager();
			var result = wManager.ShowWindowAsync(new MakeReservationViewModel(seleted.ID));
			this.TryCloseAsync();

		}

		public void Next2(object sender, MouseButtonEventArgs e)
		{
			for(int i = 0; i < SeatCount; i++)
			{
				SelectedSeats[i] = CheckedSeat[i];
			}

			string[] Send_SelectedMVInfo = new string[] { seleted.ID, seleted.MvName, seleted.Hall, seleted.StartTime, seleted.EndTime, seleted.MvNum };
			string[] Send_SelectedSeats = SelectedSeats;
			int Send_SeatCount = SeatCount;
			this.TryCloseAsync();

			var wManager = new WindowManager();
			var result = wManager.ShowWindowAsync(new ConfirmResViewModel(Send_SelectedMVInfo, Send_SelectedSeats, Send_SeatCount));
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

		/// <summary>
		/// 넘겨받은 정보 저장 및 DB내의 이미 예매된 좌석 선택버튼 비활성화
		/// </summary>
		/// <param name="Get_SelectedMVInfo"></param>
		public void InitSeatSelectPage(string[] Get_SelectedMVInfo)
		{
			AleadyResed = new BindableCollection<bool>();
			bool[] AlreadyRes = new bool[20];


			Selected = new BindableCollection<string>();

			

			Selected.Add(Get_SelectedMVInfo[0].ToString());
			Selected.Add(Get_SelectedMVInfo[1].ToString());
			Selected.Add(Get_SelectedMVInfo[2].ToString());
			Selected.Add(Get_SelectedMVInfo[3].ToString());
			Selected.Add(Get_SelectedMVInfo[4].ToString());
			Selected.Add(Get_SelectedMVInfo[5].ToString());

			seleted.ID = Get_SelectedMVInfo[0].ToString();
			seleted.MvName = Get_SelectedMVInfo[1].ToString();
			seleted.Hall = Get_SelectedMVInfo[2].ToString();
			seleted.StartTime = Get_SelectedMVInfo[3].ToString();
			seleted.EndTime = Get_SelectedMVInfo[4].ToString();
			seleted.MvNum = Get_SelectedMVInfo[5].ToString();

			CheckedSeat = new BindableCollection<string>();

			AlreadyRes = CheckAlreadyRes(seleted.Hall, seleted.MvName, seleted.StartTime);
			for (int i = 0; i < 20; i++)
			{
				AleadyResed.Add(AlreadyRes[i]);
			}
		}

		/// <summary>
		/// 좌석이 이미 예매되어있는지 판별
		/// </summary>
		/// <param name="ThNum"></param>
		/// <param name="MvName"></param>
		/// <param name="StartTime"></param>
		/// <returns></returns>
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

			for (int j = 0; j < 20; j++)
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

		/// <summary>
		/// 선택 좌석 리스트제작 및 선택 좌석수 체크
		/// </summary>
		/// <param name="sender"></param>
		public void MakeSratArray(object sender)
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

			if (SeatCount > 3)
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
	}
}
