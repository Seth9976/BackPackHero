using System;
using System.Linq;
using CleverCrow.Fluid.Dialogues.Actions;
using CleverCrow.Fluid.Dialogues.Conditions;
using CleverCrow.Fluid.Dialogues.Graphs;

namespace CleverCrow.Fluid.Dialogues.Nodes
{
	// Token: 0x0200001A RID: 26
	[CreateMenu("Hub/Default", 0)]
	public class NodeHubData : NodeDataBase
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00002DF7 File Offset: 0x00000FF7
		protected override string DefaultName
		{
			get
			{
				return "Hub";
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002E00 File Offset: 0x00001000
		public override INode GetRuntime(IGraph graphRuntime, IDialogueController dialogue)
		{
			return new NodeHub(graphRuntime, base.UniqueId, this.children.ToList<INodeData>(), this.conditions.Select((ConditionDataBase c) => c.GetRuntime(graphRuntime, dialogue)).ToList<ICondition>(), this.enterActions.Select((ActionDataBase c) => c.GetRuntime(graphRuntime, dialogue)).ToList<IAction>(), this.exitActions.Select((ActionDataBase c) => c.GetRuntime(graphRuntime, dialogue)).ToList<IAction>());
		}
	}
}
