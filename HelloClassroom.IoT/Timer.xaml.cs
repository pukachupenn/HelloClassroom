namespace HelloClassroom.IoT
{
    using System;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;
    using Newtonsoft.Json;

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
			var data = deserializeObject.Data;

			var minutes = data.timer.Value;
			timerViewModel = new TimerViewModel(Convert.ToInt32(minutes), 0, "Timer");
			DataContext = timerViewModel;
		}
	}
}
