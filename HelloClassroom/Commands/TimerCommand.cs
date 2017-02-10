using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fuzzy.Cortana;
using HelloClassroom.Models;

namespace HelloClassroom.Commands
{
	public class TimerCommand : CommandBase
	{
		private int _timerMinutes;

		public TimerCommand(IEnumerable<lEntity> entities) : base(entities)
		{
		}

		public override Task ProcessCommand()
		{
			ParseEntities();
			return Task.FromResult(0);
		}

		public override DeviceCommand GenerateJsonPayload()
		{
			DeviceCommand command = new DeviceCommand()
			{
				Type = CommandType.Timer,
				Data = new Dictionary<string, object>
				{
					["timer"] = _timerMinutes,
				}
			};

			return command;
		}

		private void ParseEntities()
		{
			_timerMinutes = 2;

			foreach (lEntity ent in _entities)
			{
				var entityType = ent.type;
				if (entityType.Equals("TimerDuration"))
				{
					int.TryParse(ent.entity, out _timerMinutes);
					 break;
				}
			}
		}
	}
}