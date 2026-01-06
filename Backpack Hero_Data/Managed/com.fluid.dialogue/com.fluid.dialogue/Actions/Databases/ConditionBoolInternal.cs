using System;
using CleverCrow.Fluid.Databases;

namespace CleverCrow.Fluid.Dialogues.Actions.Databases
{
	// Token: 0x0200004C RID: 76
	public class ConditionBoolInternal
	{
		// Token: 0x06000142 RID: 322 RVA: 0x00004619 File Offset: 0x00002819
		public ConditionBoolInternal(IKeyValueData<bool> database)
		{
			this._database = database;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00004628 File Offset: 0x00002828
		public bool AreValuesEqual(IKeyValueDefinition<bool> definition, bool value)
		{
			return this._database.Get(definition.Key, definition.DefaultValue) == value;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00004644 File Offset: 0x00002844
		public bool AreValuesNotEqual(IKeyValueDefinition<bool> definition, bool value)
		{
			return this._database.Get(definition.Key, definition.DefaultValue) != value;
		}

		// Token: 0x04000085 RID: 133
		private readonly IKeyValueData<bool> _database;
	}
}
