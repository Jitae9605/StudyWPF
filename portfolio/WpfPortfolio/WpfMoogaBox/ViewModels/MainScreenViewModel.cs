using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;

namespace WpfMoogaBox.ViewModels
{
	public class MainScreenViewModel : Conductor<object>
	{
		public MainScreenViewModel()
		{
			InitScreenMenu();
		}

		public void LoadCheckReservationView()
		{
			var wManager = new WindowManager();
			var result = wManager.ShowWindowAsync(new CheckReservationViewModel());
			CloseMainCustomerMenu();
		}

		public void LoadMakeReservationView()
		{
			string Send_ID = DateTime.Now.ToString("yyMMddHHmmss");
			var wManager = new WindowManager();			
			var result = wManager.ShowWindowAsync(new MakeReservationViewModel(Send_ID));
			CloseMainCustomerMenu();

		}

		public void LoadBuyMenuView()
		{
			string Send_ID = DateTime.Now.ToString("yyMMddHHmmss");
			var wManager = new WindowManager();
			var result = wManager.ShowWindowAsync(new BuyMenuViewModel(Send_ID));
			CloseMainCustomerMenu();

		}

		public void CloseMainCustomerMenu()
		{
			this.TryCloseAsync();
		}


		/// <summary>
		/// DB내 임시 테이블 초기화
		/// </summary>
		public void InitScreenMenu()
		{
			string ConnString = "Data Source=PC01;Initial Catalog=MoogaBox;Integrated Security=True";
			SqlConnection conn = new SqlConnection(ConnString);

			conn.Open();

			string SqlQuery = "DELETE TmpReservation";
			SqlCommand cmd = new SqlCommand(SqlQuery, conn);
			cmd.ExecuteNonQuery();

			SqlQuery = "DELETE TmpBuySnack";
			cmd = new SqlCommand(SqlQuery, conn);
			cmd.ExecuteNonQuery();

			conn.Close();
		}
	}
}
