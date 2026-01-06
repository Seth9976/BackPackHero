using System;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x0200006A RID: 106
	public interface IGraphDebugData
	{
		// Token: 0x0600037F RID: 895
		IGraphElementDebugData GetOrCreateElementData(IGraphElementWithDebugData element);

		// Token: 0x06000380 RID: 896
		IGraphDebugData GetOrCreateChildGraphData(IGraphParentElement element);

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000381 RID: 897
		IEnumerable<IGraphElementDebugData> elementsData { get; }
	}
}
