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

		public MainPage()
		{
			this.InitializeComponent();

			string json = "{\"type\":\"Count\",\"data\":{\"from\":0,\"to\":15}}";

			dynamic deserializeObject = JsonConvert.DeserializeObject(json);
			string command = deserializeObject.type;
			var data = deserializeObject.data;

			if (command == "Count")
			{
				this.Frame.Navigate(typeof(HelloClassroom.IoT.Timer), null);
				var from = data.from.Value;
				var to = data.to.Value;
				timerViewModel = new TimerViewModel(Convert.ToInt32(from), Convert.ToInt32(to), command);
			}
		}
	}
}
