using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000168 RID: 360
	public interface IGraphWithVariables : IGraph, IDisposable, IPrewarmable, IAotStubbable, ISerializationDepender, ISerializationCallbackReceiver
	{
		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600099E RID: 2462
		VariableDeclarations variables { get; }

		// Token: 0x0600099F RID: 2463
		IEnumerable<string> GetDynamicVariableNames(VariableKind kind, GraphReference reference);
	}
}
