using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fuzzy.Cortana;
using HelloClassroom.Models;
using Newtonsoft.Json;

namespace HelloClassroom.Commands
{
	public class CountCommand : CommandBase
	{
		private int _fromCount;
		private int _toCount;

		public CountCommand(IEnumerable<lEntity> entities) : base(entities)
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
			_fromCount = 1;
			_toCount = 10;

			foreach (lEntity ent in _entities)
			{
				var entityType = ent.type;
				if (entityType.Equals("To"))
				{
					int.TryParse(ent.entity, out _toCount);
				}
				else if (entityType.Equals("From"))
				{
					int.TryParse(ent.entity, out _fromCount);
				}
			}
		}
	}
}