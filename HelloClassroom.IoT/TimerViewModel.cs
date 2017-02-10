using System;
using System.ComponentModel;
using Windows.UI.Xaml;

namespace HelloWorld
{
	public class TimerViewModel : INotifyPropertyChanged
	{
		private readonly TimeSpan Duration;
		System.Diagnostics.Stopwatch sw;
		private DispatcherTimer timer;
		public event PropertyChangedEventHandler PropertyChanged;
		private static int low;
		private int high;
		private string command; 

		public TimerViewModel(int start, int end, string func)
		{
			sw = System.Diagnostics.Stopwatch.StartNew();
			timer = new DispatcherTimer
			{
				Interval = TimeSpan.FromSeconds(1)
			};

			timer.Tick += timer_Tick;
			timer.Start();

			command = func;
			low = start;
			high = end;

			if (func.Equals("Timer"))
			{
				Duration = TimeSpan.FromMinutes(start);
			}
		}

		public string Timer
		{
			get
			{
				var timeElapsed = Duration - sw.Elapsed;
				return timeElapsed.ToString(@"hh\:mm\:ss");
			}
		}

		public int Count
		{
			get
			{
				var current = sw.Elapsed.Seconds + low;
				return current <= high ? current : high;
			}
		}

		private void timer_Tick(object sender, object o)
		{
			RaisePropertyChanged(command);
		}

		private void RaisePropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
