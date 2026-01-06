using System;
using CleverCrow.Fluid.Databases;
using UnityEngine;

namespace CleverCrow.Fluid.Dialogues
{
	// Token: 0x0200000A RID: 10
	public interface IGameObjectOverride
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600002F RID: 47
		IKeyValueDefinition<GameObject> Definition { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000030 RID: 48
		GameObject Value { get; }
	}
}
