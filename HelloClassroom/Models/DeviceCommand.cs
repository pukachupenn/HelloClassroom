using System.Collections.Generic;
using HelloClassroom.Commands;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HelloClassroom.Models
{
	/// <summary>
	/// JSON serializable class which holds a payload to be sent 
	/// to the IoT app.
	/// </summary>
	public class DeviceCommand
	{
		[JsonConverter(typeof(StringEnumConverter))]
		public CommandType Type { get; set; }

		public Dictionary<string, object> Data { get; set; }
	}
}