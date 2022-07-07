using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BarcodeLib;
using System.Drawing;
using Color = System.Drawing.Color;
using System.Drawing.Imaging;

namespace WpfMoogaBox.ViewModels
{
	public class PrintTicketViewModel : Conductor<object>
	{
		public PrintTicketViewModel(string Get_ID)
		{
			Ticket_MvInfo = new BindableCollection<string>();
			InputResNum = Get_ID;

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

				Ticket_MvInfo.Add("예매번호 : " + ID);
				Ticket_MvInfo.Add("제목 : " + MvName);
				Ticket_MvInfo.Add("상영관 : " + Hall + " 관");
				Ticket_MvInfo.Add("좌석 : " + SeatNum);
				Ticket_MvInfo.Add("상영시간 : " + StartTime + " ~ " + EndTime);

				Barcode barcodeAPI = new Barcode();

				// Define basic settings of the image
				int imageWidth = 350;
				int imageHeight = 150;
				Color foreColor = Color.Black;
				Color backColor = Color.White;
				string data = ID;

				// Generate the barcode with your settings
				Image barcodeImage = barcodeAPI.Encode(TYPE.UPCA, data, foreColor, backColor, imageWidth, imageHeight);

				// Store image in some path with the desired format
				barcodeImage.Save("../../Resources/barcode.png", ImageFormat.Png);


				switch (MvName)
				{
					case "닥터 스트레인지 : 대혼돈의 멀티버스":
						MvName = "/Resources/닥터new.png";
						break;
					case "범죄도시2":
						MvName = "/Resources/범죄도시2.png";
						break;
					case "쥬라기 월드: 도미니언":
						MvName = "/Resources/쥬라기월드new.png";
						break;
					default:
						break;
				}

				Mv_Poster_Img_Source = MvName;
				Mv_Barcode_Image = "/Resources/barcode.png";
			}

			reader.Close();
			conn.Close();
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

		private BindableCollection<string> ticket_MvInfo;

		public BindableCollection<string> Ticket_MvInfo
		{
			get => ticket_MvInfo;
			set
			{
				ticket_MvInfo = value;
				NotifyOfPropertyChange(() => Ticket_MvInfo);
			}
		}

		private string mv_Poster_Img_Source;

		public string Mv_Poster_Img_Source
		{
			get => mv_Poster_Img_Source;
			set
			{
				mv_Poster_Img_Source = value;
				NotifyOfPropertyChange(() => Mv_Poster_Img_Source);
			}
		}

		private string mv_Barcode_Image;

		public string Mv_Barcode_Image
		{
			get => mv_Barcode_Image;
			set
			{
				mv_Barcode_Image = value;
				NotifyOfPropertyChange(() => Mv_Barcode_Image);
			}
		}
		

	}
}
