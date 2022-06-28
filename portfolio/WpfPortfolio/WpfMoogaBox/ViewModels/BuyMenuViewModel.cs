using Caliburn.Micro;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace WpfMoogaBox.ViewModels
{
	public class BuyMenuViewModel : Conductor<object>
	{
		public BuyMenuViewModel()
		{
			MenuImageSource = new BindableCollection<string>();
			MenuName = new BindableCollection<string>();
			MenuNum = new BindableCollection<string>();
			MenuPrice = new BindableCollection<string>();
			IsSnackListNull = new BindableCollection<bool>();
			MenuSelect = new BindableCollection<bool>();
			MenuInfo_Name = new BindableCollection<string>();
			MenuInfo_Price = new BindableCollection<string>();
			MenuInfo_Num = new BindableCollection<string>();
			Sum_All_Food_Price = "";



			// 서버에서 snack에서 다들고 오기
			string ConnString = "Data Source=PC01;Initial Catalog=MoogaBox;Integrated Security=True";
			SqlConnection conn = new SqlConnection(ConnString);

			conn.Open();

			string SqlQuery = @"SELECT  SnackName
									  , SnackNum
									  , SnackPrice
								   FROM Snack";

			SqlCommand cmd = new SqlCommand(SqlQuery, conn);

			SqlDataReader reader = cmd.ExecuteReader();

			while (reader.Read())
			{
				string SnackName = reader["SnackName"].ToString();
				string SnackNum = reader["SnackNum"].ToString();
				string SnackPrice = reader["SnackPrice"].ToString();

				MenuInfo_Name.Add(SnackName);
				MenuInfo_Num.Add(SnackNum);
				MenuInfo_Price.Add(SnackPrice);

				
			}

			// 처음 상태 = 팝콘 눌려져있는 상태
			MenuSelect.Add(true);
			MenuSelect.Add(false);
			MenuSelect.Add(false);

			for (int i = 0; i < 8; i++)
			{
				MenuName.Add(MenuInfo_Name[i]);
				MenuNum.Add(MenuInfo_Num[i]);
				MenuPrice.Add(MenuInfo_Price[i]);
				string imagepath = "\\Resources\\";
				imagepath += MenuName[i] + ".png";
				MenuImageSource.Add(imagepath);
			}

			for (int j = 8; j < 12; j++)
			{
				string imagepath = "\\Resources\\No_Picture.jpg";
				MenuImageSource.Add(imagepath);
			}

			for (int k = 0; k < 4; k++)
			{
				IsSnackListNull.Add(true);
			}

			reader.Close();
			conn.Close();
		}

		public void CanclePayment_BuySnack(object sender, MouseButtonEventArgs e)
		{ 

		}

		public void LetsPay_BuySnack(object sender, MouseButtonEventArgs e)
		{

		}

		public void Click_SnackButton(object sender, MouseButtonEventArgs e)
		{
			Button button = sender as Button;
			Grid grid = button.Content as Grid;
			Label MenuNamelv = grid.Children[1] as Label;
			Label MenuPricelv = grid.Children[2] as Label;
			MessageBox.Show(MenuNamelv.Content.ToString());
			MessageBox.Show(MenuPricelv.Content.ToString());
		}

		public void SnackMenuSelectButton_Popcorn_Click(object sender, MouseButtonEventArgs e)
		{
			MenuSelect[0] = true;
			MenuSelect[1] = false;
			MenuSelect[2] = false;

			for(int i = 0; i < 8; i++)
			{
				MenuName[i] = MenuInfo_Name[i];
				MenuNum[i] = MenuInfo_Num[i];
				MenuPrice[i] = MenuInfo_Price[i];
				string imagepath = "\\Resources\\";
				imagepath += MenuName[i] + ".png";
				MenuImageSource[i] = imagepath;
			}
			for(int j = 8; j < 12; j++)
			{
				string imagepath = "\\Resources\\No_Picture.jpg";
				MenuImageSource[j] = imagepath;
			}
			for (int k = 0; k < 4; k++)
			{
				IsSnackListNull[k] = true;
			}
		}
		public void SnackMenuSelectButton_Drink_Click(object sender, MouseButtonEventArgs e)
		{
			MenuSelect[0] = false;
			MenuSelect[1] = true;
			MenuSelect[2] = false;

			for (int i = 0; i < 8; i++)
			{
				MenuName[i] = MenuInfo_Name[i+8];
				MenuNum[i] = MenuInfo_Num[i+8];
				MenuPrice[i] = MenuInfo_Price[i+8];
				string imagepath = "\\Resources\\";
				imagepath += "콜라.png";
				MenuImageSource[i] = imagepath;
			}
			for (int j = 8; j < 12; j++)
			{
				string imagepath = "\\Resources\\No_Picture.jpg";
				MenuImageSource[j] = imagepath;
			}

			for(int k = 0; k < 4; k++)
			{
				IsSnackListNull[k] = true;
			}
		}
		public void SnackMenuSelectButton_Set_Click(object sender, MouseButtonEventArgs e)
		{
			MenuSelect[0] = false;
			MenuSelect[1] = false;
			MenuSelect[2] = true;

			for (int i = 0; i < 4; i++)
			{
				MenuName[i] = MenuInfo_Name[i + 16];
				MenuNum[i] = MenuInfo_Num[i + 16];
				MenuPrice[i] = MenuInfo_Price[i + 16];
				string imagepath = "\\Resources\\";
				imagepath += MenuName[i] + ".png";
				MenuImageSource[i] = imagepath;
			}
			for (int j = 4; j < 8; j++)
			{
				MenuName[j] = "";
				MenuNum[j] = "";
				MenuPrice[j] = "";
				string imagepath = "\\Resources\\No_Picture.jpg";
				MenuImageSource[j] = imagepath;
				IsSnackListNull[j - 4] = false;
			}
		}

		private BindableCollection<string> menuImageSource;
		public BindableCollection<string> MenuImageSource
		{
			get => menuImageSource;
			set
			{
				menuImageSource = value;
				NotifyOfPropertyChange(() => MenuImageSource);

			}
		}

		private BindableCollection<string> menuName;
		public BindableCollection<string> MenuName
		{
			get => menuName;
			set
			{
				menuName = value;
				NotifyOfPropertyChange(() => MenuName);
			}
		}

		private BindableCollection<string> menuNum;
		public BindableCollection<string> MenuNum
		{
			get => menuNum;
			set
			{
				menuNum = value;
				NotifyOfPropertyChange(() => MenuNum);

			}
		}

		private BindableCollection<string> menuPrice;
		public BindableCollection<string> MenuPrice
		{
			get => menuPrice;
			set
			{
				menuPrice = value;
				NotifyOfPropertyChange(() => MenuPrice);

			}
		}

		private BindableCollection<bool> isSnackListNull;
		public BindableCollection<bool> IsSnackListNull
		{
			get => isSnackListNull;
			set
			{
				isSnackListNull = value;
				NotifyOfPropertyChange(() => IsSnackListNull);

			}
		}

		private BindableCollection<bool> menuSelect;
		public BindableCollection<bool> MenuSelect
		{
			get => menuSelect;
			set
			{
				menuSelect = value;
				NotifyOfPropertyChange(() => MenuSelect);

			}
		}

		private BindableCollection<string> menuInfo_Name;
		public BindableCollection<string> MenuInfo_Name
		{
			get => menuInfo_Name;
			set
			{
				menuInfo_Name = value;
				NotifyOfPropertyChange(() => MenuInfo_Name);

			}
		}

		private BindableCollection<string> menuInfo_Num;
		public BindableCollection<string> MenuInfo_Num
		{
			get => menuInfo_Num;
			set
			{
				menuInfo_Num = value;
				NotifyOfPropertyChange(() => MenuInfo_Num);

			}
		}

		private BindableCollection<string> menuInfo_Price;
		public BindableCollection<string> MenuInfo_Price
		{
			get => menuInfo_Price;
			set
			{
				menuInfo_Price = value;
				NotifyOfPropertyChange(() => MenuInfo_Price);

			}
		}
		
		private string sum_All_Food_Price;
		public string Sum_All_Food_Price
		{
			get => sum_All_Food_Price;
			set
			{
				sum_All_Food_Price = value;
				NotifyOfPropertyChange(() => Sum_All_Food_Price);

			}
		}


	}
}
