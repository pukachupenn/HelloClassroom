using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fuzzy.Cortana;
using HelloClassroom.Models;

namespace HelloClassroom.Commands
{
	public class CalculationCommand : CommandBase
	{
		private int _number1;
		private int _number2;
		private Operation _operation;

		public CalculationCommand(IEnumerable<lEntity> entities) : base(entities)
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
				Type = CommandType.Calculation,
				Data = new Dictionary<string, object>
				{
					["number1"] = _number1,
					["number2"] = _number2,
					["operationString"] = GetSymbol(_operation),
					["_answer"] = GetAnswer(_number1, _number2, _operation)
				}
			};

			return command;
		}

		private void ParseEntities()
		{
			foreach (lEntity ent in _entities)
			{
				var entityType = ent.type;
				if (entityType.Equals("Number1"))
				{
					int.TryParse(ent.entity, out _number1);
				}
				else if (entityType.Equals("Number2"))
				{
					int.TryParse(ent.entity, out _number2);
				}
				else if (entityType.Equals("Operation"))
				{
					if (ent.entity.Equals("plus", StringComparison.InvariantCultureIgnoreCase)
						|| ent.entity.Equals("add", StringComparison.InvariantCultureIgnoreCase))
					{
						_operation = Operation.Add;
					}
					else if (ent.entity.Equals("minus", StringComparison.InvariantCultureIgnoreCase)
						|| ent.entity.Equals("decrease", StringComparison.InvariantCultureIgnoreCase)
						|| ent.entity.Equals("subtract", StringComparison.InvariantCultureIgnoreCase))
					{
						_operation = Operation.Subtract;
					}
					else if (ent.entity.Equals("multiply", StringComparison.InvariantCultureIgnoreCase)
						|| ent.entity.Equals("times", StringComparison.InvariantCultureIgnoreCase)
						|| ent.entity.Equals("in to", StringComparison.InvariantCultureIgnoreCase)
						|| ent.entity.Equals("into", StringComparison.InvariantCultureIgnoreCase))
					{
						_operation = Operation.Multiply;
					}
					else if (ent.entity.Equals("divide", StringComparison.InvariantCultureIgnoreCase)
						|| ent.entity.Equals("split", StringComparison.InvariantCultureIgnoreCase))
					{
						_operation = Operation.Divide;
					}
				}
			}
		}


		private static int GetAnswer(int number1, int number2, Operation operation)
		{
			switch (operation)
			{
				 case Operation.Add:
					return number1 + number2;
				case Operation.Multiply:
					return number1 * number2;
				case Operation.Subtract:
					return number1 - number2;
				case Operation.Divide:
					return number1 / number2;
			}

			return 0;
		}

		private static string GetSymbol(Operation operation)
		{
			switch (operation)
			{
				case Operation.Add:
					return "+";
				case Operation.Multiply:
					return "x";
				case Operation.Subtract:
					return "-";
				case Operation.Divide:
					return "/";
			}

			return null;
		}

		private enum Operation
		{
			Add,
			Subtract,
			Multiply,
			Divide
		}
	}
}