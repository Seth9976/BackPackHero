using System;
using System.Linq;
using CleverCrow.Fluid.Dialogues.Actions;
using CleverCrow.Fluid.Dialogues.Conditions;
using CleverCrow.Fluid.Dialogues.Graphs;
using CleverCrow.Fluid.Dialogues.Nodes.PlayGraph;

namespace CleverCrow.Fluid.Dialogues.Nodes
{
	// Token: 0x02000024 RID: 36
	[CreateMenu("Play Graph", 0)]
	public class NodePlayGraphData : NodeDataBase
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00003409 File Offset: 0x00001609
		protected override string DefaultName
		{
			get
			{
				return "Play Graph";
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003410 File Offset: 0x00001610
		public override INode GetRuntime(IGraph graphRuntime, IDialogueController dialogue)
		{
			return new NodePlayGraph(graphRuntime, base.UniqueId, this.dialogueGraph, this.children.ToList<INodeData>(), this.conditions.Select((ConditionDataBase c) => c.GetRuntime(graphRuntime, dialogue)).ToList<ICondition>(), this.enterActions.Select((ActionDataBase c) => c.GetRuntime(graphRuntime, dialogue)).ToList<IAction>(), this.exitActions.Select((ActionDataBase c) => c.GetRuntime(graphRuntime, dialogue)).ToList<IAction>());
		}

		// Token: 0x0400004A RID: 74
		public DialogueGraph dialogueGraph;
	}
}
