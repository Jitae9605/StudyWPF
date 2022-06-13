using Caliburn.Micro;

namespace WpfMoogaBox.ViewModels
{
	public class MainViewModel : Conductor<object> // Screen에는 ActivateItem[Async] 메서드 없음!
	{
		private int mainWidth;
		private int mainHeight;

		public int MainWidth
		{
			get => mainWidth; set
			{
				mainWidth = value;
				NotifyOfPropertyChange(() => MainWidth);
			}
		}
		public int MainHeight
		{
			get => mainHeight; set
			{
				mainHeight = value;
				NotifyOfPropertyChange(() => MainHeight);
			}
		}


		public MainViewModel()
		{
			ActivateItemAsync(new MainScreenViewModel());
			DisplayName = "MoogaBox v2.0";  // 프로그램 타이틀, 이름
			MainWidth = 800;
			MainHeight = 600;
		}

		
	}
}


