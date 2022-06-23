using System.Windows.Controls;
using WpfBikeShop;

namespace WPF_BikeShop
{
	/// <summary>
	/// ProductMangement.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class ProductMangement : Page
	{
		public ProductMangement()
		{
			InitializeComponent();
		}

		ProductsFactory factory = new ProductsFactory();

		private void txtSerch_TextChanged(object sender, TextChangedEventArgs e)
		{

			dgrProducts.ItemsSource = factory.FindProducts(txtSerch.Text);
		}
	}
}
