using System.Threading.Tasks;
using HelloClassroom.Models;

namespace HelloClassroom.Commands
{
	public class CountCommand : CommandBase
	{
		public override Task ProcessCommand()
		{
			throw new System.NotImplementedException();
		}

		public override DeviceCommand GenerateJsonPayload()
		{
			throw new System.NotImplementedException();
		}

		public CountCommand(string jsonData) : base(jsonData)
		{
		}
	}
}