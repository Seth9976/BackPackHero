using System;
using System.Collections.Generic;
using CleverCrow.Fluid.Dialogues.Actions;
using CleverCrow.Fluid.Dialogues.Conditions;
using CleverCrow.Fluid.Dialogues.Graphs;

namespace CleverCrow.Fluid.Dialogues.Nodes
{
	// Token: 0x0200001C RID: 28
	public class NodeLink : NodeBase
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00002E99 File Offset: 0x00001099
		public override bool IsValid
		{
			get
			{
				INode node = base.Children[0];
				return node != null && node.IsValid;
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002EB2 File Offset: 0x000010B2
		public NodeLink(IGraph graph, string UniqueId, INodeData child, List<ICondition> conditions, List<IAction> enterActions, List<IAction> exitActions)
			: base(graph, UniqueId, new List<INodeData> { child }, conditions, enterActions, exitActions)
		{
		}
	}
}
