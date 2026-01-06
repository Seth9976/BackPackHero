using System;
using System.Collections.Generic;
using Pathfinding.Serialization;

namespace Pathfinding
{
	// Token: 0x02000055 RID: 85
	public interface IGraphInternals
	{
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600041E RID: 1054
		// (set) Token: 0x0600041F RID: 1055
		string SerializedEditorSettings { get; set; }

		// Token: 0x06000420 RID: 1056
		void OnDestroy();

		// Token: 0x06000421 RID: 1057
		void DestroyAllNodes();

		// Token: 0x06000422 RID: 1058
		IEnumerable<Progress> ScanInternal();

		// Token: 0x06000423 RID: 1059
		void SerializeExtraInfo(GraphSerializationContext ctx);

		// Token: 0x06000424 RID: 1060
		void DeserializeExtraInfo(GraphSerializationContext ctx);

		// Token: 0x06000425 RID: 1061
		void PostDeserialization(GraphSerializationContext ctx);

		// Token: 0x06000426 RID: 1062
		void DeserializeSettingsCompatibility(GraphSerializationContext ctx);
	}
}
