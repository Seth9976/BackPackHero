using System;
using System.Collections.Generic;
using CleverCrow.Fluid.Dialogues.Actions;
using CleverCrow.Fluid.Dialogues.Conditions;
using CleverCrow.Fluid.Dialogues.Graphs;

namespace CleverCrow.Fluid.Dialogues.Nodes
{
	// Token: 0x02000019 RID: 25
	public class NodeHub : NodeBase
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00002DE6 File Offset: 0x00000FE6
		public NodeHub(IGraph graph, string uniqueId, List<INodeData> children, List<ICondition> conditions, List<IAction> enterActions, List<IAction> exitActions)
			: base(graph, uniqueId, children, conditions, enterActions, exitActions)
		{
		}
	}
}
