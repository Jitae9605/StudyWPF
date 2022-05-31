using System.Windows;
using System.Windows.Media;

namespace WPF_BikeShop
{
	/// <summary>
	/// MainWindow.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			InitClass();
		}

		private void InitClass()
		{
			Human driver = new Human
			{
				FirstName = "Nick",
				HasDriverLicense = true
			};

			Car car = new Car();
			car.Speed = 100;
			car.Color = Colors.White;
			car.Driver = driver;
		}
	}
}
