using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000068 RID: 104
	public interface IGraph : IDisposable, IPrewarmable, IAotStubbable, ISerializationDepender, ISerializationCallbackReceiver
	{
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600036E RID: 878
		// (set) Token: 0x0600036F RID: 879
		Vector2 pan { get; set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000370 RID: 880
		// (set) Token: 0x06000371 RID: 881
		float zoom { get; set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000372 RID: 882
		MergedGraphElementCollection elements { get; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000373 RID: 883
		string title { get; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000374 RID: 884
		string summary { get; }

		// Token: 0x06000375 RID: 885
		IGraphData CreateData();

		// Token: 0x06000376 RID: 886
		IGraphDebugData CreateDebugData();

		// Token: 0x06000377 RID: 887
		void Instantiate(GraphReference instance);

		// Token: 0x06000378 RID: 888
		void Uninstantiate(GraphReference instance);
	}
}
