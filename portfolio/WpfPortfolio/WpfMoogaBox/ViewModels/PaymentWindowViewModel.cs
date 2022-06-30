using Caliburn.Micro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfMoogaBox.Helpers;
using WpfMoogaBox.Models;

namespace WpfMoogaBox.ViewModels
{
	public class PaymentWindowViewModel : Conductor<object>
	{
		public PaymentWindowViewModel(string Get_ID)
		{
			InitPaymentPage(Get_ID);

			int Sum_MV = AddDatagrid_Mv();
			int Sum_SNACK = AddDatagrid_Snack();

			Sum_All = (Sum_MV + Sum_SNACK).ToString();
			Sum_All += " 원";			
		}


		public async void LetsPay(object sender, MouseButtonEventHandler e)
		{
			string ConnString = "Data Source=PC01;Initial Catalog=MoogaBox;Integrated Security=True";
			SqlConnection conn = new SqlConnection(ConnString);

			conn.Open();
			string SqlQuery = "";

			// 매점 Tmp -> BuySnack
			foreach (var item in datagridData_Snack)
			{
				SqlQuery = @"INSERT INTO  BuySnack
								       ( ID
								       , SnackName
								       , SnackNum
								       , BuyPrice
								       , BuyCount
								       , SnackPrice)
								 VALUES	 
								       ( @ID
								       , @SnackName
								       , @SnackNum
								       , @BuyPrice
								       , @BuyCount
								       , @SnackPrice)";

				SqlCommand cmd = new SqlCommand(SqlQuery, conn);

				SqlParameter parmID = new SqlParameter("@ID", ID);
				cmd.Parameters.Add(parmID);

				SqlParameter parSnackName = new SqlParameter("@SnackName", item.SnackName);
				cmd.Parameters.Add(parSnackName);

				SqlParameter parmSnackNum = new SqlParameter("@SnackNum", item.SnackNum);
				cmd.Parameters.Add(parmSnackNum);

				SqlParameter parmBuyPrice = new SqlParameter("@BuyPrice", item.Sum);
				cmd.Parameters.Add(parmBuyPrice);

				SqlParameter parmBuyCount = new SqlParameter("@BuyCount", item.Count);
				cmd.Parameters.Add(parmBuyCount);

				SqlParameter parmSnackPrice = new SqlParameter("@SnackPrice", item.SnackPrice);
				cmd.Parameters.Add(parmSnackPrice);

				cmd.ExecuteNonQuery();

				SqlQuery = @"UPDATE Maejum
							    SET SnackCount = SnackCount - @SnackCount2
							  WHERE SnackNum = @SnackNum2";

				cmd = new SqlCommand(SqlQuery, conn);

				SqlParameter parmBuyCount2 = new SqlParameter("@SnackCount2", item.Count);
				cmd.Parameters.Add(parmBuyCount2);

				SqlParameter parmSnackNum2 = new SqlParameter("@SnackNum2", item.SnackNum);
				cmd.Parameters.Add(parmSnackNum2);

				cmd.ExecuteNonQuery();
			}

			// 영화 Tmp -> Res..
			SqlQuery = "";

			foreach (var item in DatagridData_Mv)
			{
				SqlQuery = @"INSERT INTO  Reservation
							   	        ( ID
							   	        , MvName
							   	        , MvNum
							   	        , Hall
							   	        , SeatNum
							   	        , StartTime
							   	        , Ccount
							   	        , Mmoney)
							   	  VALUES	 
							   	        ( @ID
							   	        , @MvName
							   	        , @MvNum
							   	        , @Hall
							   	        , @SeatNum
							   	        , @StartTime
							   	        , @Ccount
							   	        , @Mmoney)";

				SqlCommand cmd = new SqlCommand(SqlQuery, conn);

				SqlParameter parmID = new SqlParameter("@ID", ID);
				cmd.Parameters.Add(parmID);

				SqlParameter parMvName = new SqlParameter("@MvName", item.MvName);
				cmd.Parameters.Add(parMvName);

				SqlParameter parMvNum = new SqlParameter("@MvNum", item.MvNum);
				cmd.Parameters.Add(parMvNum);

				SqlParameter parmHall = new SqlParameter("@Hall", item.Hall);
				cmd.Parameters.Add(parmHall);

				SqlParameter parmSeatNum = new SqlParameter("@SeatNum", item.Seat);
				cmd.Parameters.Add(parmSeatNum);

				SqlParameter parmStartTime = new SqlParameter("@StartTime", DateTime.Parse(item.StartTime));
				cmd.Parameters.Add(parmStartTime);

				SqlParameter parmCcount = new SqlParameter("@Ccount", item.Count);
				cmd.Parameters.Add(parmCcount);

				SqlParameter parmMmoney = new SqlParameter("@Mmoney", item.Count * item.MvPrice);
				cmd.Parameters.Add(parmMmoney);

				cmd.ExecuteNonQuery();

				string[] Seat = item.Seat.Split(',');


				for(int i = 0; i < Seat.Length; i++)
				{
					SqlQuery = @"UPDATE   CrJo
								  SET Eempty = 0
								WHERE MvName = @MvName2
								  AND MvNum = @MvNum2
								  AND SeatNum = @SeatNum";



					cmd = new SqlCommand(SqlQuery, conn);

					SqlParameter parMvName2 = new SqlParameter("@MvName2", item.MvName);
					cmd.Parameters.Add(parMvName2);

					SqlParameter parmMvNum2 = new SqlParameter("@MvNum2", item.MvNum);
					cmd.Parameters.Add(parmMvNum2);

					SqlParameter parmSeat = new SqlParameter("@SeatNum", Seat[i]);
					cmd.Parameters.Add(parmSeat);

					cmd.ExecuteNonQuery();
				}
			}

			MessageBox.Show("결제가 완료되었습니다.", "결제완료");

			CanclePayment(sender, e);
		}


		public void CanclePayment(object sender, MouseButtonEventHandler e)
		{
			IWindowManager wManager = new WindowManager();
			var result = wManager.ShowWindowAsync(new MainScreenViewModel());
			TryCloseAsync();
		}

		private BindableCollection<Snack_Info> datagridData_Snack;
		public BindableCollection<Snack_Info> DatagridData_Snack
		{
			get => datagridData_Snack;
			set
			{
				datagridData_Snack = value;
				NotifyOfPropertyChange(() => DatagridData_Snack);
			}
		}

		private BindableCollection<Mv_Info> datagridData_Mv;
		public BindableCollection<Mv_Info> DatagridData_Mv
		{
			get => datagridData_Mv;
			set
			{
				datagridData_Mv = value;
				NotifyOfPropertyChange(() => DatagridData_Mv);
			}
		}

		private string sum_Mv;
		public string Sum_Mv
		{
			get => sum_Mv;
			set
			{
				sum_Mv = value;
				NotifyOfPropertyChange(() => Sum_Mv);
			}
		}

		private string sum_Snack;
		public string Sum_Snack
		{
			get => sum_Snack;
			set
			{
				sum_Snack = value;
				NotifyOfPropertyChange(() => Sum_Snack);
			}
		}

		private string sum_All;
		public string Sum_All
		{
			get => sum_All;
			set
			{
				sum_All = value;
				NotifyOfPropertyChange(() => Sum_All);
			}
		}

		public string ID { get; set; }

		/// <summary>
		/// 초기화
		/// </summary>
		/// <param name="Get_ID"></param>
		public void InitPaymentPage(string Get_ID)
		{
			ID = Get_ID;
			DatagridData_Mv = new BindableCollection<Mv_Info>();
			DatagridData_Snack = new BindableCollection<Snack_Info>();

			Sum_Mv = " 0 원";
			Sum_Snack = " 0 원";
			Sum_All = " 0 원";
		}

		/// <summary>
		/// DB에 저장된 TmpRes..데이터를 불러와 데이터그리드에 저장하고 합계액을 반환
		/// </summary>
		/// <returns></returns>
		public int AddDatagrid_Mv()
		{
			int Sum_MV = 0;

			string ConnString = "Data Source=PC01;Initial Catalog=MoogaBox;Integrated Security=True";
			SqlConnection conn = new SqlConnection(ConnString);

			conn.Open();

			string SqlQuery = @"SELECT MvName
									 , MvNum
									 , Hall
									 , SeatNum
									 , StartTime
									 , Ccount
									 , Mmoney
								  FROM TmpReservation";

			SqlCommand cmd = new SqlCommand(SqlQuery, conn);

			SqlDataReader reader = cmd.ExecuteReader();

			while (reader.Read())
			{

				string MvName = reader["MvName"].ToString();
				string MvNum = reader["MvNum"].ToString();
				string Hall = reader["Hall"].ToString();
				string Seat = reader["SeatNum"].ToString();
				DateTime StartTime = DateTime.Parse(reader["StartTime"].ToString());
				int Count = Convert.ToInt32(reader["Ccount"].ToString());

				Mv_Info mv_Info = new Mv_Info(MvName, Hall, StartTime, Seat, Count, MvNum);
				Sum_MV = mv_Info.Count * mv_Info.MvPrice;

				Sum_Mv = Sum_MV.ToString();
				Sum_Mv += " 원";

				DatagridData_Mv.Add(mv_Info);
			}

			reader.Close();
			conn.Close();

			return Sum_MV;
		}

		/// <summary>
		/// DB에 저장된 TmpBuySnack데이터를 불러와 데이터그리드에 저장하고 합계액을 반환
		/// </summary>
		/// <returns></returns>
		public int AddDatagrid_Snack()
		{
			int Sum_SNACK = 0;

			string ConnString = "Data Source=PC01;Initial Catalog=MoogaBox;Integrated Security=True";
			SqlConnection conn = new SqlConnection(ConnString);

			string SqlQuery = @"SELECT  ID
						       , SnackName
						       , SnackNum
						       , BuyPrice
						       , BuyCount
						       , SnackPrice
						   FROM  TmpBuySnack";

			SqlCommand cmd = new SqlCommand(SqlQuery, conn);

			SqlDataReader reader = cmd.ExecuteReader();

			while (reader.Read())
			{
				string SnackName = reader["SnackName"].ToString();
				string SnackNum = reader["SnackNum"].ToString();
				int BuyPrice = Convert.ToInt32(reader["BuyPrice"].ToString());
				int BuyCount = Convert.ToInt32(reader["BuyCount"].ToString());
				int SnackPrice = Convert.ToInt32(reader["SnackPrice"].ToString());

				Snack_Info snack_Info = new Snack_Info(SnackName, SnackNum, SnackPrice, BuyCount);
				Sum_SNACK += BuyPrice;
				Sum_Snack = Sum_SNACK.ToString();
				Sum_Snack += " 원";
				DatagridData_Snack.Add(snack_Info);
			}

			reader.Close();
			conn.Close();

			return Sum_SNACK;
		}

	}


}
