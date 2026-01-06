using System;
using Pathfinding.Serialization;

namespace Pathfinding
{
	// Token: 0x020000CC RID: 204
	public interface IGraphInternals
	{
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000670 RID: 1648
		// (set) Token: 0x06000671 RID: 1649
		string SerializedEditorSettings { get; set; }

		// Token: 0x06000672 RID: 1650
		void OnDestroy();

		// Token: 0x06000673 RID: 1651
		void DisposeUnmanagedData();

		// Token: 0x06000674 RID: 1652
		void DestroyAllNodes();

		// Token: 0x06000675 RID: 1653
		IGraphUpdatePromise ScanInternal(bool async);

		// Token: 0x06000676 RID: 1654
		void SerializeExtraInfo(GraphSerializationContext ctx);

		// Token: 0x06000677 RID: 1655
		void DeserializeExtraInfo(GraphSerializationContext ctx);

		// Token: 0x06000678 RID: 1656
		void PostDeserialization(GraphSerializationContext ctx);
	}
}
