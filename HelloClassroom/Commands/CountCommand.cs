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

		private void ParseJson()
		{
			// TODO: Parse the LUIS payload here
			//var request = JsonConvert.DeserializeObject<Dictionary<string, string>>(this.jsonData);

			_fromCount = 0;
			_toCount = 15;
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
				Data = new Dictionary<string, string>
				{
					["from"] = _fromCount.ToString(),
					["to"] = _toCount.ToString(),
				}
			};

			return command;
		}

		public CountCommand(string jsonData) : base(jsonData)
		{
		}
	}
}