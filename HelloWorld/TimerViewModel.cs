using System;
using System.ComponentModel;
using Windows.UI.Xaml;

namespace HelloWorld
{
	public class TimerViewModel : INotifyPropertyChanged
	{
		static readonly TimeSpan duration = TimeSpan.FromMinutes(2);
		System.Diagnostics.Stopwatch sw;
		private DispatcherTimer timer;
		public event PropertyChangedEventHandler PropertyChanged;
		private static int low;
		private int high;
		private string command; 

		public TimerViewModel(int start, int end, string func)
		{
			sw = System.Diagnostics.Stopwatch.StartNew();
			timer = new DispatcherTimer();
			timer.Interval = TimeSpan.FromSeconds(1);
			timer.Tick += timer_Tick;
			timer.Start();

			low = start;
			high = end;
			command = func;
		}

		public string TimeFromStart
		{
			get
			{
				var timer = duration - sw.Elapsed;
				return timer.ToString(@"hh\:mm\:ss");
			}
		}

		public int Counting
		{
			get
			{
				var current = sw.Elapsed.Seconds + low;
				if (current <= high)
				{
					return current;
				}
				return high;
			}
		}

		private void timer_Tick(object sender, object o)
		{
			RaisePropertyChanged(command);
		}

		private void RaisePropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
