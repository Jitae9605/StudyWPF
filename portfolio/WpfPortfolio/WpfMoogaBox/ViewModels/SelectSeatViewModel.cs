using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
			//CheckAlreadyRes();

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

		}

		private void CheckAlreadyRes(int ZeroOrOne)
		{
			// ZeroOrOne == 0 = 자리있음
			// ZeroOrOne == 1 = 자리없음


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
					for (int j = i; i < SeatCount - 2; j++)
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

			

		

			//string ConnString = "Data Source=PC01;Initial Catalog=MoogaBox;Integrated Security=True";
			//SqlConnection conn = new SqlConnection(ConnString);

			//conn.Open();

			//string SqlQuery = "SELECT * FROM Movie WHERE MvName = @MvName AND StartTime = @StartTime";



			//SqlCommand cmd = new SqlCommand(SqlQuery, conn);

			//SqlParameter parmMvName = new SqlParameter("@MvName", MvName);
			//cmd.Parameters.Add(parmMvName);

			//SqlParameter parmStartTime = new SqlParameter("@StartTime", StartTime);
			//cmd.Parameters.Add(parmStartTime);

			//SqlDataReader reader = cmd.ExecuteReader();

			//while (reader.Read())
			//{
			//	string selectedMvName = reader["MvName"].ToString();
			//	string selectedHall = reader["Hall"].ToString();
			//	DateTime selectedStartTime = DateTime.Parse(reader["StartTime"].ToString());
			//	DateTime selectedRunningTime = DateTime.Parse(reader["RunningTime"].ToString());
			//	string selectedMvNum = reader["MvNum"].ToString();

			//	seleted = SelectedMv = new Selection(new Mv_Info(selectedMvName, selectedHall, selectedStartTime, selectedRunningTime, selectedMvNum));
			//}

			//switch (SelectedMv.MvName)
			//{
			//	case "닥터 스트레인지 : 대혼돈의 멀티버스":
			//		SelectedMv_img = "\\Resources\\닥터new.png";
			//		break;
			//	case "범죄도시2":
			//		SelectedMv_img = "/Resources/범죄도시2.png";
			//		break;
			//	case "쥬라기 월드: 도미니언":
			//		SelectedMv_img = "/Resources/쥬라기월드new.png";
			//		break;
			//	default:
			//		break;
			//}
			//reader.Close();
			//conn.Close();
		}

		public void Cancel2(object sender, MouseButtonEventArgs e)
		{
			Button button = sender as Button;
			button.IsCancel = true;
		}

		public void Next2(object sender, MouseButtonEventArgs e)
		{
			//if (string.IsNullOrEmpty(seleted.MvName))
			//{
			//	Commons.ShowMessageAsync("알림", "먼저 영화와 시간을 선택해주세요");
			//	return;
			//}

			//string ID = DateTime.Now.ToString("yyMMddHHmmss");
			//string[] ThroughData = new string[] { ID, seleted.MvName, seleted.Hall, seleted.StartTime };


			//string a = "안녕";
			
			for(int i = 0; i < SeatCount; i++)
			{
				SelectedSeats[i] = CheckedSeat[i];
			}


			string[] ThroughData = new string[] { seleted.ID, seleted.MvName, seleted.Hall, seleted.StartTime, seleted.EndTime };
			string[] ThroughData2 = SelectedSeats;

			var wManager = new WindowManager();

			Cancel2(sender, e);
			var result = wManager.ShowDialogAsync(new ConfirmResViewModel(ThroughData, ThroughData2));
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

		public void btnA01()
		{

		}
		public void btnA02()
		{

		}
		public void btnA03()
		{

		}
		public void btnA04()
		{

		}
		public void btnA05()
		{

		}

		public void btnB01(object sender)
		{
			Button button = 
		}

		public void btnB02()
		{

		}

		public void btnB03()
		{

		}

		public void btnB04()
		{

		}

		public void btnB05()
		{

		}

		public void btnC01()
		{

		}
		public void btnC02()
		{

		}
		public void btnC03()
		{

		}
		public void btnC04()
		{

		}
		public void btnC05()
		{

		}
		public void btnD01()
		{

		}
		public void btnD02()
		{

		}
		public void btnD03()
		{

		}
		public void btnD04()
		{

		}
		public void btnD05()
		{

		}


	}
}
