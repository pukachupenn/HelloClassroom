using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;

namespace HelloWorld
{
	public sealed partial class Timer : Page
	{
		private TimerViewModel timerViewModel;

		public Timer()
		{
			this.InitializeComponent();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);

			dynamic deserializeObject = JsonConvert.DeserializeObject(e.Parameter.ToString());
			var data = deserializeObject.data;

			var minutes = data.minutes.Value;
			timerViewModel = new TimerViewModel(Convert.ToInt32(minutes), 0, "Timer");
			DataContext = timerViewModel;
		}
	}
}
