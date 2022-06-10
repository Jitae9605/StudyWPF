using Caliburn.Micro;

namespace WpfMoogaBox.ViewModels
{
	public class MainViewModel : Conductor<object> // Screen에는 ActivateItem[Async] 메서드 없음!
	{

		public MainViewModel()
		{
			ActivateItemAsync(new MainScreenViewModel());
			DisplayName = "MoogaBox v2.0";  // 프로그램 타이틀, 이름
		}


	}
}
