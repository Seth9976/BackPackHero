using System;
using System.Collections.Generic;
using System.Linq;
using CleverCrow.Fluid.Dialogues.Actions;
using CleverCrow.Fluid.Dialogues.Choices;
using CleverCrow.Fluid.Dialogues.Conditions;
using CleverCrow.Fluid.Dialogues.Graphs;

namespace CleverCrow.Fluid.Dialogues.Nodes
{
	// Token: 0x02000018 RID: 24
	[CreateMenu("Hub/Dice Roll", 0)]
	public class NodeDiceData : NodeDataChoiceBase
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00002D38 File Offset: 0x00000F38
		protected override string DefaultName
		{
			get
			{
				return "Dice Roll Hub";
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002D40 File Offset: 0x00000F40
		public override INode GetRuntime(IGraph graphRuntime, IDialogueController dialogue)
		{
			List<IChoice> list = this.choices.Select((ChoiceData c) => c.GetRuntime(graphRuntime, dialogue)).ToList<IChoice>();
			return new NodeDice(base.UniqueId, list, this.conditions.Select((ConditionDataBase c) => c.GetRuntime(graphRuntime, dialogue)).ToList<ICondition>(), this.enterActions.Select((ActionDataBase a) => a.GetRuntime(graphRuntime, dialogue)).ToList<IAction>(), this.exitActions.Select((ActionDataBase a) => a.GetRuntime(graphRuntime, dialogue)).ToList<IAction>());
		}
	}
}
