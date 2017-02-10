using System.Collections.Generic;
using System.Threading.Tasks;
using Fuzzy.Cortana;
using HelloClassroom.Models;

namespace HelloClassroom.Commands
{
	public enum CommandType
	{
		None,
		Count,
		Location,
		Timer,
		Calculation,
		Dictionary,
	}

	public abstract class CommandBase
	{
		protected IEnumerable<lEntity> _entities;

		protected CommandBase(IEnumerable<lEntity> entities)
		{
			_entities = entities;
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