using System;
using CleverCrow.Fluid.Databases;

namespace CleverCrow.Fluid.Dialogues.Actions.Databases
{
	// Token: 0x0200004A RID: 74
	public class SetKeyValueInternal<T>
	{
		// Token: 0x0600013C RID: 316 RVA: 0x00004586 File Offset: 0x00002786
		public SetKeyValueInternal(IKeyValueData<T> database)
		{
			this._database = database;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00004595 File Offset: 0x00002795
		public void WriteValue(string key, T value)
		{
			this._database.Set(key, value);
		}

		// Token: 0x04000080 RID: 128
		private readonly IKeyValueData<T> _database;
	}
}
