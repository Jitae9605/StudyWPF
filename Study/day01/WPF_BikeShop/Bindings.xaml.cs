using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPF_BikeShop
{
	/// <summary>
	/// Bindings.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class Bindings : Page
	{
		public Bindings()
		{
			InitializeComponent();

			Car c = new Car
			{
				Speed = 100,
				Color = Colors.Crimson,
				Driver = new Human
				{
					FirstName = "Nick",
					HasDriverLicense = true
				}
			};


			//txtSpeed.DataContext = c;					// - 일반적 WPF 데이터바인딩 방식(이 데이터가 xaml로 보내진다.)
			//txtColor.DataContext = c;
			//txtFirstName.DataContext = c;
			// txtSpeed.Text = c.Speed.ToString();	// 고전적인 윈폼방식

			//stxPanel.DataContext = c;                   // - 부모에게 데이터를 보내면 자식들도 모두 사용가능

			// 그런데 이 파일 자체를 데이터로 넣을수 있다
			this.DataContext = c;   // this = Bindings.xaml -> 여기로 c의 데이터를 모두 보낸다
									// 이러면 이름도 필요없다 - 변수명 빼고)

			var carlist = new List<Car>();

			for (int i = 0; i < 10; i++)
			{
				carlist.Add(new Car
				{
					Speed = i * 10,
					Color = Colors.Purple
				});
			}

			lbxCars.DataContext = carlist;
		}
	}
}
