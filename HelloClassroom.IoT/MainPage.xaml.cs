using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HelloWorld
{
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
				this.Frame.Navigate(typeof(Timer), null);
				var from = data.from.Value;
				var to = data.to.Value;
				timerViewModel = new TimerViewModel(Convert.ToInt32(from), Convert.ToInt32(to), command);
			}
		}
	}
}
