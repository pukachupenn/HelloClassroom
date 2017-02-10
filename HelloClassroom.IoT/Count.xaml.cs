﻿namespace HelloClassroom.IoT
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
			var data = deserializeObject.data;

			var from = data.from.Value;
			var to = data.to.Value;
			timerViewModel = new TimerViewModel(Convert.ToInt32(from), Convert.ToInt32(to), "Count");
			DataContext = timerViewModel;
		}
	}
}
