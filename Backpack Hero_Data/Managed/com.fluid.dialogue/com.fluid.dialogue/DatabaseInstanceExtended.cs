using System;
using CleverCrow.Fluid.Databases;
using UnityEngine;

namespace CleverCrow.Fluid.Dialogues
{
	// Token: 0x02000005 RID: 5
	public class DatabaseInstanceExtended : DatabaseInstance, IDatabaseInstanceExtended, IDatabaseInstance
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000020B1 File Offset: 0x000002B1
		public IKeyValueData<GameObject> GameObjects { get; } = new KeyValueDataGameObject();

		// Token: 0x06000010 RID: 16 RVA: 0x000020B9 File Offset: 0x000002B9
		public void ClearGameObjects()
		{
			this.GameObjects.Clear();
		}
	}
}
