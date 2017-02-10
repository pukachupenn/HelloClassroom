using System.Collections.Generic;
using System.Threading.Tasks;
using Fuzzy.Cortana;
using HelloClassroom.Models;

namespace HelloClassroom.Commands
{
	public class NoneCommand : CommandBase
	{
		public NoneCommand() : base(null)
		{
		}

		public override Task ProcessCommand()
		{
			return Task.FromResult(0);
		}

		public override DeviceCommand GenerateJsonPayload()
		{
			DeviceCommand command = new DeviceCommand()
			{
				Type = CommandType.None
			};

			return command;
		}
	}
}