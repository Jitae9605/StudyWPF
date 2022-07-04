using Caliburn.Micro;
using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;
using WpfMoogaBox.Helpers;

namespace WpfMoogaBox.ViewModels
{
	public class CheckReservationViewModel : Screen
	{
		public CheckReservationViewModel()
		{
			
		}

		public void PrintTicket(object sender, MouseButtonEventArgs e)
		{
			if(string.IsNullOrEmpty(InputResNum))
			{
				Commons.ShowMessageAsync("오류", "먼저 예매번호를 입력해주세요.");
				return;
			}

			string ConnString = "Data Source=PC01;Initial Catalog=MoogaBox;Integrated Security=True";
			SqlConnection conn = new SqlConnection(ConnString);

			conn.Open();

			string SqlQuery = "SELECT * FROM Reservation WHERE ID = @ID";

			SqlCommand cmd = new SqlCommand(SqlQuery, conn);

			SqlParameter parmMvName = new SqlParameter("@ID", InputResNum);
			cmd.Parameters.Add(parmMvName);

			SqlDataReader reader = cmd.ExecuteReader();

			while (reader.Read())
			{
				string ID = reader["ID"].ToString();
				string MvName = reader["MvName"].ToString();
				string Hall = reader["Hall"].ToString();
				string SeatNum = reader["SeatNum"].ToString();
				string StartTime = DateTime.Parse(reader["StartTime"].ToString()).ToString("HH:mm");
				string EndTime = DateTime.Parse(reader["EndingTime"].ToString()).ToString("HH:mm");
				MessageBox.Show(ID, "값");
				MessageBox.Show(MvName, "값");
				MessageBox.Show(Hall, "값");
				MessageBox.Show(SeatNum, "값");
				MessageBox.Show(StartTime, "값");
				MessageBox.Show(EndTime, "값");
			}

			reader.Close();
			conn.Close();
		}

		public void CancelToMainMenuFromCheckRes(object sender, MouseButtonEventArgs e)
		{
			IWindowManager windowManager = new WindowManager();
			windowManager.ShowWindowAsync(new MainScreenViewModel());
			TryCloseAsync();
		}

		private string inputResNum;

		public string InputResNum
		{
			get => inputResNum;
			set
			{
				inputResNum = value;
				NotifyOfPropertyChange(() => InputResNum);
			}
		}

	}
}
