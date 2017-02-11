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
			
			string json = "{\"type\":\"Count\",\"data\":{\"from\":0,\"to\":15}}";
	//		Frame.Navigate(typeof(Count), json);
		}

	    protected override void OnNavigatedTo(NavigationEventArgs e)
        {
			//base.OnNavigatedTo(e);
   //         string json = "{\"type\":\"Count\",\"data\":{\"from\":0,\"to\":15}}";

   //         dynamic deserializeObject = JsonConvert.DeserializeObject(json);
   //         string command = deserializeObject.type;
   //         var data = deserializeObject.data;

   //         if (command == "Count")
   //         {
   //             Frame.Navigate(typeof(Count), json);
	            
	  //          //var from = data.from.Value;
	  //          //var to = data.to.Value;
	  //          //timerViewModel = new TimerViewModel(Convert.ToInt32(from), Convert.ToInt32(to), command);
   //         }
            //Frame.Navigate(typeof(Location), "{}");
            //commandTimer = ThreadPoolTimer.CreatePeriodicTimer(timer => CheckIncomingCommand(), TimeSpan.FromSeconds(1));
        }

        private void CheckIncomingCommand()
        {
            string json = AzureIoTHub.ReceiveCloudToDeviceMessageAsync().Result;

            dynamic deserializeObject = JsonConvert.DeserializeObject(json);
            string command = deserializeObject.Type;

            Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                if (command.Equals("Timer"))
                {
                    Frame.Navigate(typeof(Timer), json);
                }
                else if (command.Equals("Count"))
                {
                    Frame.Navigate(typeof(Count), json);
                }
                else if (command.Equals("Location"))
                {
                    Frame.Navigate(typeof(Location), json);
                }
            }).GetResults();
        }

		private void button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
		{
			string json = "{\"type\":\"Count\",\"data\":{\"from\":0,\"to\":15}}";
			this.Frame.Navigate(typeof (Count), json);
		}
	}
}
