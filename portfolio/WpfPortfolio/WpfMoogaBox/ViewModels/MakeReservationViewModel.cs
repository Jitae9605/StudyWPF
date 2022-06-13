using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using WpfMoogaBox.Models;

namespace WpfMoogaBox.ViewModels
{
	public class MakeReservationViewModel : Screen
	{
		public MakeReservationViewModel()
		{
			string ConnString = "Data Source=PC01;Initial Catalog=MoogaBox;Integrated Security=True";
			SqlConnection conn = new SqlConnection(ConnString);

			conn.Open();

			string SqlQuery = "SELECT * FROM Movie";

			SqlCommand cmd = new SqlCommand(SqlQuery, conn);

			SqlDataReader reader = cmd.ExecuteReader();

			MV_Times = new BindableCollection<Mv_Info> { };
			
			while (reader.Read())
			{
				
				string MvName = reader["MvName"].ToString();
				string Hall = reader["Hall"].ToString();
				DateTime StartTime = DateTime.Parse(reader["StartTime"].ToString());
				string MvNum = reader["MvNum"].ToString();




				MV_Times.Add(new Mv_Info(MvName, Hall, StartTime, MvNum));
				

			}

			List<string> Mv_Time = new List<string>();

			foreach (Mv_Info mv_info in MV_Times)
			{
				Mv_Time.Add(mv_info.StartTime);
			}

			reader.Close();
			conn.Close();
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
	}
}
