using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using WpfMoogaBox.Helpers;
using WpfMoogaBox.Models;

namespace WpfMoogaBox.ViewModels
{
	public class BuyMenuViewModel : Conductor<object>
	{
		public BuyMenuViewModel(string Get_ID)
		{
			InitBuyMenuPage(Get_ID);

			GetDataFromDb_Snack();
		}

		public void CanclePayment_BuySnack(object sender, MouseButtonEventArgs e)
		{
			IWindowManager wManager = new WindowManager();
			var result = wManager.ShowWindowAsync(new MainScreenViewModel());
			TryCloseAsync();
		}

		public void LetsPay_BuySnack(object sender, MouseButtonEventArgs e)
		{
			if(datagridData.Count == 0)
			{
				Commons.ShowMessageAsync("선택된 메뉴 없음", "먼저 메뉴를 선택해주세요!");
				return;
			}

			CheckStockAndInsertTmpTbl();

			this.TryCloseAsync();

			var wManager = new WindowManager();
			var res = wManager.ShowWindowAsync(new PaymentWindowViewModel(ID));
		}

		public void Delete_Selected_SnackItem(object sender, object datagrid_in_BuyMenu, MouseButtonEventArgs e)
		{
			// 선택사항 없으면 클릭이벤트 무시

			if(!datagridData.Remove(datagrid_in_BuyMenu as Snack_Info))
			{
				Commons.ShowMessageAsync("선택된것 없음", "먼저 삭제하고자 하는 메뉴를 선택해주세요!");

			}

			int temp_sum = 0;
			foreach (var item in datagridData)
			{
				temp_sum += item.Sum;
			}
			Sum_All_Food_Price = "합계 : ";
			Sum_All_Food_Price += temp_sum.ToString();
			Sum_All_Food_Price += " 원";
		}

		public void Click_SnackButton(object sender, MouseButtonEventArgs e)
		{
			AddDataGrid(sender);
		}

		public void SnackMenuSelectButton_Popcorn_Click(object sender, MouseButtonEventArgs e)
		{
			SnackMenuSelect_Popcorn();
		}
		
		public void SnackMenuSelectButton_Drink_Click(object sender, MouseButtonEventArgs e)
		{
			SnackMenuSelect_Drink();
		}

		public void SnackMenuSelectButton_Set_Click(object sender, MouseButtonEventArgs e)
		{
			SnackMenuSelect_Set();
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
				NotifyOfPropertyChange(() => DatagridData);

			}
		}

		private Dictionary<string, string> name_Num_Link ;
		public Dictionary<string,string> Name_Num_Link
		{
			get => name_Num_Link;
			set
			{
				name_Num_Link = value;
				NotifyOfPropertyChange(() => Name_Num_Link);

			}
		}

		private BindableCollection<Snack_Info> datagridData;
		public BindableCollection<Snack_Info> DatagridData
		{
			get => datagridData;
			set
			{
				datagridData = value;
				NotifyOfPropertyChange(() => DatagridData);
				NotifyOfPropertyChange(() => Snack_Info);
				NotifyOfPropertyChange(() => Sum_All_Food_Price);

			}
		}

		public Snack_Info Snack_Info { get; set; }
		public string ID { get; set; }

		/// <summary>
		/// DB의 Snack 테이블의 데이터를 모두 가져와 저장
		/// </summary>
		public void GetDataFromDb_Snack()
		{
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
				Name_Num_Link.Add(SnackName, SnackNum);
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

		/// <summary>
		/// 콜렉션 및 ID값 초기화
		/// </summary>
		/// <param name="Get_ID"></param>
		public void InitBuyMenuPage(string Get_ID)
		{
			ID = Get_ID;
			MenuImageSource = new BindableCollection<string>();
			MenuName = new BindableCollection<string>();
			MenuNum = new BindableCollection<string>();
			MenuPrice = new BindableCollection<string>();
			IsSnackListNull = new BindableCollection<bool>();
			MenuSelect = new BindableCollection<bool>();
			MenuInfo_Name = new BindableCollection<string>();
			MenuInfo_Price = new BindableCollection<string>();
			MenuInfo_Num = new BindableCollection<string>();
			Sum_All_Food_Price = "합계 : 0 원";
			Name_Num_Link = new Dictionary<string, string>();
			datagridData = new BindableCollection<Snack_Info>();
		}

		/// <summary>
		/// 저장된 Snack목록중 Popcorn만 가져와 이미지 및 정보 게시, 버튼 비활성화
		/// </summary>
		public void SnackMenuSelect_Popcorn()
		{
			MenuSelect[0] = true;
			MenuSelect[1] = false;
			MenuSelect[2] = false;

			for (int i = 0; i < 8; i++)
			{
				MenuName[i] = MenuInfo_Name[i];
				MenuNum[i] = MenuInfo_Num[i];
				MenuPrice[i] = MenuInfo_Price[i];
				string imagepath = "\\Resources\\";
				imagepath += MenuName[i] + ".png";
				MenuImageSource[i] = imagepath;
			}
			for (int j = 8; j < 12; j++)
			{
				string imagepath = "\\Resources\\No_Picture.jpg";
				MenuImageSource[j] = imagepath;
			}
			for (int k = 0; k < 4; k++)
			{
				IsSnackListNull[k] = true;
			}
		}

		/// <summary>
		/// 저장된 Snack목록중 Drink만 가져와 이미지 및 정보 게시, 버튼 비활성화
		/// </summary>
		public void SnackMenuSelect_Drink()
		{
			MenuSelect[0] = false;
			MenuSelect[1] = true;
			MenuSelect[2] = false;

			for (int i = 0; i < 8; i++)
			{
				MenuName[i] = MenuInfo_Name[i + 8];
				MenuNum[i] = MenuInfo_Num[i + 8];
				MenuPrice[i] = MenuInfo_Price[i + 8];
				string imagepath = "\\Resources\\";
				imagepath += "콜라.png";
				MenuImageSource[i] = imagepath;
			}
			for (int j = 8; j < 12; j++)
			{
				string imagepath = "\\Resources\\No_Picture.jpg";
				MenuImageSource[j] = imagepath;
			}

			for (int k = 0; k < 4; k++)
			{
				IsSnackListNull[k] = true;
			}
		}

		/// <summary>
		/// 저장된 Snack목록중 Set만 가져와 이미지 및 정보 게시, 버튼 비활성화
		/// </summary>
		public void SnackMenuSelect_Set()
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

		/// <summary>
		/// 클릭된 버튼의 정보와 눌린 횟수 데이터그리드에 목록화 해 표시
		/// </summary>
		/// <param name="sender"></param>
		public void AddDataGrid(object sender)
		{
			int Itemcount = 1;
			Button button = sender as Button;
			Grid grid = button.Content as Grid;
			Label MenuNamelv = grid.Children[1] as Label;
			Label MenuPricelv = grid.Children[2] as Label;
			string ItemName = MenuNamelv.Content.ToString();
			string ItemNum = Name_Num_Link[MenuNamelv.Content.ToString()];
			int Itemprice = Convert.ToInt32(MenuPricelv.Content.ToString());

			foreach (var item in datagridData)
			{
				if (item.SnackNum == ItemNum)
				{
					Itemcount = item.Count + 1;
					datagridData.Remove(item);
					break;
				}
			}

			Snack_Info Snack_Info = new Snack_Info(ItemName, ItemNum, Itemprice, Itemcount);
			datagridData.Add(Snack_Info);

			int temp_sum = 0;
			foreach (var item in datagridData)
			{
				temp_sum += item.Sum;
			}
			Sum_All_Food_Price = "합계 : ";
			Sum_All_Food_Price += temp_sum.ToString();
			Sum_All_Food_Price += " 원";
		}

		/// <summary>
		/// 선택된 상품 수량과 재고수량확인후, 이상없으면면 선택내용 Tmp 테이블에 insert수행
		/// </summary>
		public void CheckStockAndInsertTmpTbl()
		{

			string ConnString = "Data Source=PC01;Initial Catalog=MoogaBox;Integrated Security=True";
			SqlConnection conn = new SqlConnection(ConnString);

			conn.Open();

			foreach (var item in datagridData)
			{
				string SqlQuery = @"SELECT SnackCount
								      FROM Maejum
									 WHERE SnackNum = @SnackNum";


				SqlCommand cmd = new SqlCommand(SqlQuery, conn);

				SqlParameter parmSnackNum1 = new SqlParameter("@SnackNum", item.SnackNum);
				cmd.Parameters.Add(parmSnackNum1);

				SqlDataReader reader = cmd.ExecuteReader();

				if (reader.Read())
				{
					int SnackCount = Convert.ToInt32(reader["SnackCount"].ToString());

					if (SnackCount < item.Count)
					{
						string message = $"{item.SnackName}의 재고가 부족합니다.\n 현재 재고 : {SnackCount}";
						Commons.ShowMessageAsync("재고부족", message);

						conn.Close();
						reader.Close();
						return;
					}
				}
				//conn.Close();
				reader.Close();

				//conn.Open();

				SqlQuery = @"INSERT INTO  TmpBuySnack
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

				cmd = new SqlCommand(SqlQuery, conn);

				SqlParameter parmID = new SqlParameter("@ID", ID);
				cmd.Parameters.Add(parmID);

				SqlParameter parSnackName = new SqlParameter("@SnackName", item.SnackName);
				cmd.Parameters.Add(parSnackName);

				SqlParameter parmSnackNum = new SqlParameter("@SnackNum", item.SnackNum);
				cmd.Parameters.Add(parmSnackNum);

				SqlParameter parmBuyPrice = new SqlParameter("@BuyPrice", Convert.ToInt32(item.Sum));
				cmd.Parameters.Add(parmBuyPrice);

				SqlParameter parmBuyCount = new SqlParameter("@BuyCount", Convert.ToInt32(item.Count));
				cmd.Parameters.Add(parmBuyCount);

				SqlParameter parmSnackPrice = new SqlParameter("@SnackPrice", Convert.ToInt32(item.SnackPrice));
				cmd.Parameters.Add(parmSnackPrice);

				cmd.ExecuteNonQuery();
			}


			conn.Close();
		}


	}
}
