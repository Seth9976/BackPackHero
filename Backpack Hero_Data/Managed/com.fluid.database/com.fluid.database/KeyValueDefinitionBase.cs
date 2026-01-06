using System;
using UnityEngine;

namespace CleverCrow.Fluid.Databases
{
	// Token: 0x02000005 RID: 5
	public abstract class KeyValueDefinitionBase<V> : ScriptableObject, IKeyValueDefinition<V>
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002188 File Offset: 0x00000388
		public string Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002190 File Offset: 0x00000390
		public V DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
		}

		// Token: 0x04000005 RID: 5
		protected const string CREATE_PATH = "Fluid/Database";

		// Token: 0x04000006 RID: 6
		public string key;

		// Token: 0x04000007 RID: 7
		public V defaultValue;
	}
}
