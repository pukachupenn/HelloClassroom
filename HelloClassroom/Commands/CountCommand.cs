using System.Collections.Generic;
using System.Threading.Tasks;
using HelloClassroom.Models;
using Newtonsoft.Json;

namespace HelloClassroom.Commands
{
	public class CountCommand : CommandBase
	{
		private int _fromCount;
		private int _toCount;

		public CountCommand(string jsonData) : base(jsonData)
		{
		}

		public override Task ProcessCommand()
		{
			ParseJson();
			return Task.FromResult(0);
		}

		public override DeviceCommand GenerateJsonPayload()
		{
			DeviceCommand command = new DeviceCommand()
			{
				Type = CommandType.Count,
				Data = new Dictionary<string, object>
				{
					["from"] = _fromCount,
					["to"] = _toCount,
				}
			};

			return command;
		}

		private void ParseJson()
		{
			// TODO: Parse the LUIS payload here
			//var request = JsonConvert.DeserializeObject<Dictionary<string, string>>(this.jsonData);

			_fromCount = 0;
			_toCount = 15;
		}
	}
}