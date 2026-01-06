using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020001A9 RID: 425
	internal interface IDragAndDropData
	{
		// Token: 0x06000DCB RID: 3531
		object GetGenericData(string key);

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000DCC RID: 3532
		object userData { get; }

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000DCD RID: 3533
		IEnumerable<Object> unityObjectReferences { get; }
	}
}
