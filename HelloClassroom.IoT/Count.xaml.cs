namespace HelloClassroom.IoT
{
    using System;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;
    using Newtonsoft.Json;

    public sealed partial class Count : Page
	{
		private TimerViewModel timerViewModel;

		public Count()
		{
			this.InitializeComponent();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);

			dynamic deserializeObject = JsonConvert.DeserializeObject(e.Parameter.ToString());
			var data = deserializeObject.Data;

			int from = data.from;
			int to = data.to;
			timerViewModel = new TimerViewModel(from, to, "Count");
			DataContext = timerViewModel;
		}
	}
}
