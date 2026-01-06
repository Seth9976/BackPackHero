using System;
using CleverCrow.Fluid.Databases;

namespace CleverCrow.Fluid.Dialogues.Actions.Databases
{
	// Token: 0x02000056 RID: 86
	public class ConditionStringInternal
	{
		// Token: 0x0600015D RID: 349 RVA: 0x000048CA File Offset: 0x00002ACA
		public ConditionStringInternal(IKeyValueData<string> database)
		{
			this._database = database;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000048D9 File Offset: 0x00002AD9
		public bool AreValuesEqual(IKeyValueDefinition<string> definition, string value)
		{
			return this._database.Get(definition.Key, definition.DefaultValue) == value;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x000048F8 File Offset: 0x00002AF8
		public bool AreValuesNotEqual(IKeyValueDefinition<string> definition, string value)
		{
			return this._database.Get(definition.Key, definition.DefaultValue) != value;
		}

		// Token: 0x04000094 RID: 148
		private readonly IKeyValueData<string> _database;
	}
}
