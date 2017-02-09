using System.Collections.Generic;
using HelloClassroom.Commands;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HelloClassroom.Models
{
	public class DeviceCommand
	{
		[JsonConverter(typeof(StringEnumConverter))]
		public CommandType Type { get; set; }

		public Dictionary<string, object> Data { get; set; }
	}
}