using System;
using System.Linq;
using CleverCrow.Fluid.Dialogues.Actions;
using CleverCrow.Fluid.Dialogues.Conditions;
using CleverCrow.Fluid.Dialogues.Graphs;

namespace CleverCrow.Fluid.Dialogues.Nodes
{
	// Token: 0x02000026 RID: 38
	public class NodeRootData : NodeDataBase
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000BB RID: 187 RVA: 0x000034C0 File Offset: 0x000016C0
		protected override string DefaultName
		{
			get
			{
				return "Root";
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000034C8 File Offset: 0x000016C8
		public override INode GetRuntime(IGraph graphRuntime, IDialogueController dialogue)
		{
			return new NodeRoot(graphRuntime, base.UniqueId, this.children.ToList<INodeData>(), this.conditions.Select((ConditionDataBase c) => c.GetRuntime(graphRuntime, dialogue)).ToList<ICondition>(), this.enterActions.Select((ActionDataBase c) => c.GetRuntime(graphRuntime, dialogue)).ToList<IAction>(), this.exitActions.Select((ActionDataBase c) => c.GetRuntime(graphRuntime, dialogue)).ToList<IAction>());
		}
	}
}
