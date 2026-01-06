using System;
using CleverCrow.Fluid.Dialogues.Nodes;

namespace CleverCrow.Fluid.Dialogues.Graphs
{
	// Token: 0x0200002B RID: 43
	public interface IGraph
	{
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000D0 RID: 208
		INode Root { get; }

		// Token: 0x060000D1 RID: 209
		INode GetCopy(INodeData nodeData);
	}
}
