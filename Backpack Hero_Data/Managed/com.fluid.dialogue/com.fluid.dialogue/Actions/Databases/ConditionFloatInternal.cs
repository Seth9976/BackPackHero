using System;
using CleverCrow.Fluid.Databases;

namespace CleverCrow.Fluid.Dialogues.Actions.Databases
{
	// Token: 0x0200004F RID: 79
	public class ConditionFloatInternal
	{
		// Token: 0x0600014B RID: 331 RVA: 0x000046B3 File Offset: 0x000028B3
		public ConditionFloatInternal(IKeyValueData<float> database)
		{
			this._database = database;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x000046C4 File Offset: 0x000028C4
		public bool IsComparisonValid(IKeyValueDefinition<float> definition, float value, NumberComparison comparison)
		{
			float num = this._database.Get(definition.Key, definition.DefaultValue);
			switch (comparison)
			{
			case NumberComparison.Equal:
				return Math.Abs(num - value) < 0.01f;
			case NumberComparison.NotEqual:
				return Math.Abs(num - value) > 0.01f;
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

		// Token: 0x0400008A RID: 138
		private readonly IKeyValueData<float> _database;
	}
}
