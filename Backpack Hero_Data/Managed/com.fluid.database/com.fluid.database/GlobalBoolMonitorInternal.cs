using System;
using CleverCrow.Fluid.Utilities.UnityEvents;

namespace CleverCrow.Fluid.Databases
{
	// Token: 0x0200000B RID: 11
	public class GlobalBoolMonitorInternal
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000229C File Offset: 0x0000049C
		public IUnityEvent EventTrue { get; } = new UnityEventPlus();

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000022A4 File Offset: 0x000004A4
		public IUnityEvent EventFalse { get; } = new UnityEventPlus();

		// Token: 0x0600001E RID: 30 RVA: 0x000022AC File Offset: 0x000004AC
		public GlobalBoolMonitorInternal(IDatabaseInstance database, IKeyValueDefinition<bool>[] definitions)
		{
			this._database = database;
			this._definitions = definitions;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000022D8 File Offset: 0x000004D8
		public void UpdateEvent()
		{
			bool flag = true;
			foreach (IKeyValueDefinition<bool> keyValueDefinition in this._definitions)
			{
				if (!this._database.Bools.Get(keyValueDefinition.Key, keyValueDefinition.DefaultValue))
				{
					flag = false;
				}
			}
			if (flag)
			{
				this.EventTrue.Invoke();
				return;
			}
			this.EventFalse.Invoke();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000233C File Offset: 0x0000053C
		public void BindChangeMonitor()
		{
			foreach (IKeyValueDefinition<bool> keyValueDefinition in this._definitions)
			{
				this._database.Bools.AddKeyListener(keyValueDefinition.Key, new Action<bool>(this.BindMethod));
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002384 File Offset: 0x00000584
		private void BindMethod(bool value)
		{
			this.UpdateEvent();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000238C File Offset: 0x0000058C
		public void UnbindChangeMonitor()
		{
			foreach (IKeyValueDefinition<bool> keyValueDefinition in this._definitions)
			{
				this._database.Bools.RemoveKeyListener(keyValueDefinition.Key, new Action<bool>(this.BindMethod));
			}
		}

		// Token: 0x0400000C RID: 12
		private readonly IDatabaseInstance _database;

		// Token: 0x0400000D RID: 13
		private readonly IKeyValueDefinition<bool>[] _definitions;
	}
}
