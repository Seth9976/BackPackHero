using System;
using CleverCrow.Fluid.Databases;

namespace CleverCrow.Fluid.Dialogues.Actions.Databases
{
	// Token: 0x02000052 RID: 82
	public class ConditionIntInternal
	{
		// Token: 0x06000153 RID: 339 RVA: 0x000047A4 File Offset: 0x000029A4
		public ConditionIntInternal(IKeyValueData<int> database)
		{
			this._database = database;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000047B4 File Offset: 0x000029B4
		public bool IsComparisonValid(IKeyValueDefinition<int> definition, int value, NumberComparison comparison)
		{
			int num = this._database.Get(definition.Key, definition.DefaultValue);
			switch (comparison)
			{
			case NumberComparison.Equal:
				return num == value;
			case NumberComparison.NotEqual:
				return num != value;
			case NumberComparison.GreaterThan:
				return value > num;
			case NumberComparison.GreaterThanOrEqual:
				return value >= num;
			case NumberComparison.LessThan:
				return value < num;
			case NumberComparison.LessThanOrEqual:
				return value <= num;
			default:
				throw new ArgumentOutOfRangeException("comparison", comparison, null);
			}
		}

		// Token: 0x0400008F RID: 143
		private readonly IKeyValueData<int> _database;
	}
}
