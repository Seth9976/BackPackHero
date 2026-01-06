using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x02000021 RID: 33
	internal class RenderGraphDebugData
	{
		// Token: 0x060000EC RID: 236 RVA: 0x00007298 File Offset: 0x00005498
		public void Clear()
		{
			this.passList.Clear();
			if (this.resourceLists[0] == null)
			{
				for (int i = 0; i < 2; i++)
				{
					this.resourceLists[i] = new List<RenderGraphDebugData.ResourceDebugData>();
				}
			}
			for (int j = 0; j < 2; j++)
			{
				this.resourceLists[j].Clear();
			}
		}

		// Token: 0x040000D9 RID: 217
		public List<RenderGraphDebugData.PassDebugData> passList = new List<RenderGraphDebugData.PassDebugData>();

		// Token: 0x040000DA RID: 218
		public List<RenderGraphDebugData.ResourceDebugData>[] resourceLists = new List<RenderGraphDebugData.ResourceDebugData>[2];

		// Token: 0x0200012A RID: 298
		[DebuggerDisplay("PassDebug: {name}")]
		public struct PassDebugData
		{
			// Token: 0x040004CA RID: 1226
			public string name;

			// Token: 0x040004CB RID: 1227
			public List<int>[] resourceReadLists;

			// Token: 0x040004CC RID: 1228
			public List<int>[] resourceWriteLists;

			// Token: 0x040004CD RID: 1229
			public bool culled;

			// Token: 0x040004CE RID: 1230
			public bool generateDebugData;
		}

		// Token: 0x0200012B RID: 299
		[DebuggerDisplay("ResourceDebug: {name} [{creationPassIndex}:{releasePassIndex}]")]
		public struct ResourceDebugData
		{
			// Token: 0x040004CF RID: 1231
			public string name;

			// Token: 0x040004D0 RID: 1232
			public bool imported;

			// Token: 0x040004D1 RID: 1233
			public int creationPassIndex;

			// Token: 0x040004D2 RID: 1234
			public int releasePassIndex;

			// Token: 0x040004D3 RID: 1235
			public List<int> consumerList;

			// Token: 0x040004D4 RID: 1236
			public List<int> producerList;
		}
	}
}
