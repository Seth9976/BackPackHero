using System;
using System.Linq;
using CleverCrow.Fluid.Dialogues.Actions;
using CleverCrow.Fluid.Dialogues.Conditions;
using CleverCrow.Fluid.Dialogues.Graphs;

namespace CleverCrow.Fluid.Dialogues.Nodes
{
	// Token: 0x0200001D RID: 29
	public class NodeLinkData : NodeDataBase
	{
		// Token: 0x0600008A RID: 138 RVA: 0x00002ED0 File Offset: 0x000010D0
		public override INode GetRuntime(IGraph graphRuntime, IDialogueController dialogue)
		{
			return new NodeLink(graphRuntime, base.UniqueId, (this.children.Count > 0) ? this.children[0] : null, this.conditions.Select((ConditionDataBase c) => c.GetRuntime(graphRuntime, dialogue)).ToList<ICondition>(), this.enterActions.Select((ActionDataBase c) => c.GetRuntime(graphRuntime, dialogue)).ToList<IAction>(), this.exitActions.Select((ActionDataBase c) => c.GetRuntime(graphRuntime, dialogue)).ToList<IAction>());
		}
	}
}
