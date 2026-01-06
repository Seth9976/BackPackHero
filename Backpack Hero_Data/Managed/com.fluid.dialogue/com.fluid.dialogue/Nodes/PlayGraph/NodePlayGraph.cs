using System;
using System.Collections.Generic;
using CleverCrow.Fluid.Dialogues.Actions;
using CleverCrow.Fluid.Dialogues.Conditions;
using CleverCrow.Fluid.Dialogues.Graphs;

namespace CleverCrow.Fluid.Dialogues.Nodes.PlayGraph
{
	// Token: 0x02000027 RID: 39
	public class NodePlayGraph : NodeBase
	{
		// Token: 0x060000BE RID: 190 RVA: 0x00003561 File Offset: 0x00001761
		public NodePlayGraph(IGraph runtime, string uniqueId, IGraphData graph, List<INodeData> children, List<ICondition> conditions, List<IAction> enterActions, List<IAction> exitActions)
			: base(runtime, uniqueId, children, conditions, enterActions, exitActions)
		{
			this._graph = graph;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000357A File Offset: 0x0000177A
		protected override void OnPlay(IDialoguePlayback playback)
		{
			playback.ParentCtrl.PlayChild(this._graph);
		}

		// Token: 0x0400004B RID: 75
		public readonly IGraphData _graph;
	}
}
