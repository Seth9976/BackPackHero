using System;
using CleverCrow.Fluid.Databases;
using UnityEngine;

namespace CleverCrow.Fluid.Dialogues
{
	// Token: 0x02000009 RID: 9
	public interface IDatabaseInstanceExtended : IDatabaseInstance
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600002D RID: 45
		IKeyValueData<GameObject> GameObjects { get; }

		// Token: 0x0600002E RID: 46
		void ClearGameObjects();
	}
}
