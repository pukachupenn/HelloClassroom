using System.Threading.Tasks;
using HelloClassroom.Models;

namespace HelloClassroom.Commands
{
	public enum CommandType
	{
		Count,
		Location,
	}

	public abstract class CommandBase
	{
		private string jsonData;

		public CommandBase(string jsonData)
		{
			this.jsonData = jsonData;
		}

		public abstract Task ProcessCommand();

		public abstract DeviceCommand GenerateJsonPayload();

		public async Task<DeviceCommand> Run()
		{
			await ProcessCommand();
			return GenerateJsonPayload();
		}
	}
}