using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfMoogaBox.Helpers;
using WpfMoogaBox.Models;

namespace WpfMoogaBox.ViewModels
{
	public class MakeReservationViewModel : Screen
	{

		public Selection seleted = new Selection();
		public MakeReservationViewModel(string Get_ID)
		{
			SelectedMv_img = "\\Resources\\No_Picture.jpg";
			string ConnString = "Data Source=PC01;Initial Catalog=MoogaBox;Integrated Security=True";
			SqlConnection conn = new SqlConnection(ConnString);
			ID = Get_ID;

			conn.Open();

			string SqlQuery = "SELECT * FROM Movie";

			SqlCommand cmd = new SqlCommand(SqlQuery, conn);

			SqlDataReader reader = cmd.ExecuteReader();

			MV_Times = new BindableCollection<Mv_Info> { };

			int i = 0;

			CheckTimes = new BindableCollection<bool>();

			while (reader.Read())
			{
				
				string MvName = reader["MvName"].ToString();
				string Hall = reader["Hall"].ToString();
				DateTime StartTime = DateTime.Parse(reader["StartTime"].ToString());
				DateTime RunningTime = DateTime.Parse(reader["RunningTime"].ToString());
				string MvNum = reader["MvNum"].ToString();


				bool CanIEnable = Timeisover(StartTime);

				CheckTimes.Add(CanIEnable);
				Debug.WriteLine(CheckTimes[i]);

				MV_Times.Add(new Mv_Info(MvName, Hall, StartTime, RunningTime, MvNum));
				i++;
			}

			reader.Close();
			conn.Close();
		}

		public bool Timeisover(DateTime startTime)
		{
			DateTime NowTime = DateTime.Now;
			if (NowTime.Hour < startTime.Hour || 
				(NowTime.Hour == startTime.Hour && NowTime.Minute < startTime.Minute))
				return true;
			else 
				return false;
			
		}

		public void Clicked(object sender, object mvName,  MouseButtonEventArgs e)
		{
			SelectedMv = new Selection(new Mv_Info("", "", DateTime.Now, DateTime.Now, ""));
			Button button = sender as Button;

			string StartTime = button.Content.ToString();
			string MvName = mvName.ToString();

			string ConnString = "Data Source=PC01;Initial Catalog=MoogaBox;Integrated Security=True";
			SqlConnection conn = new SqlConnection(ConnString);

			conn.Open();

			string SqlQuery = "SELECT * FROM Movie WHERE MvName = @MvName AND StartTime = @StartTime";



			SqlCommand cmd = new SqlCommand(SqlQuery, conn);

			SqlParameter parmMvName = new SqlParameter("@MvName", MvName);
			cmd.Parameters.Add(parmMvName);

			SqlParameter parmStartTime = new SqlParameter("@StartTime", StartTime);
			cmd.Parameters.Add(parmStartTime);

			SqlDataReader reader = cmd.ExecuteReader();

			while (reader.Read())
			{
				string selectedMvName = reader["MvName"].ToString();
				string selectedHall = reader["Hall"].ToString();
				DateTime selectedStartTime = DateTime.Parse(reader["StartTime"].ToString());
				DateTime selectedRunningTime = DateTime.Parse(reader["RunningTime"].ToString());
				string selectedMvNum = reader["MvNum"].ToString();

				seleted = SelectedMv = new Selection(new Mv_Info(selectedMvName, selectedHall, selectedStartTime, selectedRunningTime, selectedMvNum));
			}

			switch (SelectedMv.MvName)
			{
				case "닥터 스트레인지 : 대혼돈의 멀티버스":
					SelectedMv_img = "\\Resources\\닥터new.png";
					break;
				case "범죄도시2":
					SelectedMv_img = "/Resources/범죄도시2.png";
					break;
				case "쥬라기 월드: 도미니언":
					SelectedMv_img = "/Resources/쥬라기월드new.png";
					break;
				default:
					break;
			}
			reader.Close();
			conn.Close();
		}

		public void Cancel(object sender, MouseButtonEventArgs e)
		{
			Button button = sender as Button;
			button.IsCancel = true;
			IWindowManager wManager = new WindowManager();
			var result = wManager.ShowWindowAsync(new MainScreenViewModel());
			this.TryCloseAsync();
		}

		public void Next(object sender, MouseButtonEventArgs e)
		{
			if(string.IsNullOrEmpty(seleted.MvName))
			{
				
				return;
			}

			string[] Send_SelectedMVInfo = new string[] { ID, seleted.MvName, seleted.Hall, seleted.StartTime, seleted.EndTime };
			this.TryCloseAsync();
			LoadSelectSeatViewModel(Send_SelectedMVInfo);
		}

		private void LoadSelectSeatViewModel(string[] Send_SelectedMVInfo)
		{
			IWindowManager wManager = new WindowManager();
			var result = wManager.ShowWindowAsync(new SelectSeatViewModel(Send_SelectedMVInfo));
		}

		private BindableCollection<Mv_Info> mV_Times;

		public BindableCollection<Mv_Info> MV_Times
		{
			get => mV_Times; 
			set
			{
				mV_Times = value;
				NotifyOfPropertyChange(() => MV_Times);
			}
		}

		private Selection selectedMv;

		public Selection SelectedMv
		{
			get => selectedMv;
			set
			{ 
				selectedMv = value;
				NotifyOfPropertyChange(() => SelectedMv);
			}
		}

		private string selectedMv_img;

		public string SelectedMv_img
		{
			get => selectedMv_img;
			set
			{
				selectedMv_img = value;
				NotifyOfPropertyChange(() => SelectedMv_img);
			}
		}



		private BindableCollection<bool> checkTimes;
		public BindableCollection<bool> CheckTimes
		{
			get => checkTimes; 
			set
			{
				checkTimes = value;
				NotifyOfPropertyChange(() => CheckTimes);

			}
		}

		private string id;
		public string ID
		{
			get => id; 
			set
			{
				id = value;
				NotifyOfPropertyChange(() => ID);

			}
		}

		


	}
}
