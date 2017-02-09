using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using HelloClassroom.Commands;
using Newtonsoft.Json.Linq;

namespace HelloClassroom.Models
{
	public class DeviceCommand
	{
		public CommandType Type { get; set; }
	}
}