using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000B7 RID: 183
	public interface IMacro : IGraphRoot, IGraphParent, ISerializationDependency, ISerializationCallbackReceiver, IAotStubbable
	{
		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000491 RID: 1169
		// (set) Token: 0x06000492 RID: 1170
		IGraph graph { get; set; }
	}
}
