using System;
using System.Collections.Generic;
using CleverCrow.Fluid.Dialogues.Nodes;

namespace CleverCrow.Fluid.Dialogues.Graphs
{
	// Token: 0x02000028 RID: 40
	public interface IGraphData
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000C0 RID: 192
		INodeData Root { get; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000C1 RID: 193
		IReadOnlyList<INodeData> Nodes { get; }
	}
}
