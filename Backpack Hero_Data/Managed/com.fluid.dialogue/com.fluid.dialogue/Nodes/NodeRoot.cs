using System;
using System.Collections.Generic;
using CleverCrow.Fluid.Dialogues.Actions;
using CleverCrow.Fluid.Dialogues.Conditions;
using CleverCrow.Fluid.Dialogues.Graphs;

namespace CleverCrow.Fluid.Dialogues.Nodes
{
	// Token: 0x02000025 RID: 37
	public class NodeRoot : NodeBase
	{
		// Token: 0x060000BA RID: 186 RVA: 0x000034AF File Offset: 0x000016AF
		public NodeRoot(IGraph runtime, string uniqueId, List<INodeData> children, List<ICondition> conditions, List<IAction> enterActions, List<IAction> exitActions)
			: base(runtime, uniqueId, children, conditions, enterActions, exitActions)
		{
		}
	}
}
