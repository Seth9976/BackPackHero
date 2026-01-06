using System;
using System.Collections.Generic;
using System.Linq;
using CleverCrow.Fluid.Dialogues.Choices;
using CleverCrow.Fluid.Dialogues.Conditions;
using CleverCrow.Fluid.Dialogues.Graphs;

namespace CleverCrow.Fluid.Dialogues.Nodes
{
	// Token: 0x02000014 RID: 20
	[CreateMenu("Hub/Choice", 0)]
	public class NodeChoiceHubData : NodeDataChoiceBase
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000028B4 File Offset: 0x00000AB4
		protected override string DefaultName
		{
			get
			{
				return "Choice Hub";
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000028BB File Offset: 0x00000ABB
		public override bool HideInspectorActions
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000028C0 File Offset: 0x00000AC0
		public override INode GetRuntime(IGraph graphRuntime, IDialogueController dialogue)
		{
			List<IChoice> list = this.choices.Select((ChoiceData c) => c.GetRuntime(graphRuntime, dialogue)).ToList<IChoice>();
			return new NodeChoiceHub(base.UniqueId, list, this.conditions.Select((ConditionDataBase c) => c.GetRuntime(graphRuntime, dialogue)).ToList<ICondition>());
		}
	}
}
