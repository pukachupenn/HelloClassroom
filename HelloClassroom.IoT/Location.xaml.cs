using System;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;

namespace HelloClassroom.IoT
{
	public sealed partial class Location : Page
	{
		public Location()
		{
			this.InitializeComponent();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);

			dynamic deserializeObject = JsonConvert.DeserializeObject(e.Parameter.ToString());
			var data = deserializeObject.Data;

			string area = data.Area;
			textBox.Text = area;

			string description = data.Description;
			textBox1.Text = description;

			string name = data.Name;
			textBox2.Text = name;

			string population = data.Population;
			textBox3.Text = population;

			string type = data.Type;
			textBox4.Text = type;

			string thumbnail = data.Thumbnail;
			image.Source = new BitmapImage(new Uri(thumbnail));
		}

		public static BitmapImage Base64StringToBitmap(string base64String)
		{
			if (base64String == null)
				return null;

			var buffer = System.Convert.FromBase64String(base64String);
			using (InMemoryRandomAccessStream ms = new InMemoryRandomAccessStream())
			{
				using (DataWriter writer = new DataWriter(ms.GetOutputStreamAt(0)))
				{
					writer.WriteBytes(buffer);
					writer.StoreAsync();
				}

				var image = new BitmapImage();
				image.SetSource(ms);
				return image;
			}
		}
	}
}
