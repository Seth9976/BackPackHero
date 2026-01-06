using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000075 RID: 117
	public interface IGraphParent
	{
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600039F RID: 927
		IGraph childGraph { get; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060003A0 RID: 928
		bool isSerializationRoot { get; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060003A1 RID: 929
		Object serializedObject { get; }

		// Token: 0x060003A2 RID: 930
		IGraph DefaultGraph();
	}
}
