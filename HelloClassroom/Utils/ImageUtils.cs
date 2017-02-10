using System;
using System.IO;
using System.Net;
using System.Text;

namespace HelloClassroom.Utils
{
	public static class ImageUtils
	{
		public static string ConvertImageUrlToBase64(string url)
		{
			StringBuilder stringBuilder = new StringBuilder();

			byte[] _byte = GetImage(url);

			stringBuilder.Append(Convert.ToBase64String(_byte, 0, _byte.Length));

			return stringBuilder.ToString();
		}

		private static byte[] GetImage(string url)
		{
			byte[] buffer = null;

			try
			{
				HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

				HttpWebResponse response = (HttpWebResponse)req.GetResponse();
				Stream stream = response.GetResponseStream();

				if (stream != null)
				{
					using (BinaryReader br = new BinaryReader(stream))
					{
						int len = (int)(response.ContentLength);
						buffer = br.ReadBytes(len);
						br.Close();
					}

					stream.Close();
				}

				response.Close();
			}
			catch (Exception)
			{
				buffer = null;
			}

			return buffer;
		}
	}
}