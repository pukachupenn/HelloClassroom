using Windows.System.Threading;
using Windows.UI.Core;
using Windows.UI.Xaml.Navigation;

namespace HelloClassroom.IoT
{
    using System;
    using Windows.UI.Xaml.Controls;
    using Newtonsoft.Json;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
	{
		private TimerViewModel timerViewModel;

	    private ThreadPoolTimer commandTimer;

		public MainPage()
		{
			this.InitializeComponent();
		}
    }
}
