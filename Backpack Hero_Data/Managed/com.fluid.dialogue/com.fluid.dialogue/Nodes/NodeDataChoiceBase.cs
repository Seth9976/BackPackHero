using System;
using System.Collections.Generic;
using System.Linq;
using CleverCrow.Fluid.Dialogues.Actions;
using CleverCrow.Fluid.Dialogues.Choices;
using CleverCrow.Fluid.Dialogues.Conditions;
using UnityEngine;

namespace CleverCrow.Fluid.Dialogues.Nodes
{
	// Token: 0x02000022 RID: 34
	public abstract class NodeDataChoiceBase : NodeDataBase
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00003235 File Offset: 0x00001435
		public override List<ChoiceData> Choices
		{
			get
			{
				return this.choices;
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003240 File Offset: 0x00001440
		public override void ClearConnectionChildren()
		{
			base.ClearConnectionChildren();
			foreach (ChoiceData choiceData in this.choices)
			{
				choiceData.ClearConnectionChildren();
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003298 File Offset: 0x00001498
		public override void SortConnectionsByPosition()
		{
			base.SortConnectionsByPosition();
			foreach (ChoiceData choiceData in this.choices)
			{
				choiceData.SortConnectionsByPosition();
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000032F0 File Offset: 0x000014F0
		public override NodeDataBase GetDataCopy()
		{
			NodeDataChoiceBase nodeDataChoiceBase = base.GetDataCopy() as NodeDataChoiceBase;
			nodeDataChoiceBase.choices = this.choices.Select(new Func<ChoiceData, ChoiceData>(Object.Instantiate<ChoiceData>)).ToList<ChoiceData>();
			nodeDataChoiceBase.conditions = this.conditions.Select(new Func<ConditionDataBase, ConditionDataBase>(Object.Instantiate<ConditionDataBase>)).ToList<ConditionDataBase>();
			nodeDataChoiceBase.enterActions = this.enterActions.Select(new Func<ActionDataBase, ActionDataBase>(Object.Instantiate<ActionDataBase>)).ToList<ActionDataBase>();
			nodeDataChoiceBase.exitActions = this.exitActions.Select(new Func<ActionDataBase, ActionDataBase>(Object.Instantiate<ActionDataBase>)).ToList<ActionDataBase>();
			return nodeDataChoiceBase;
		}

		// Token: 0x04000047 RID: 71
		[HideInInspector]
		public List<ChoiceData> choices = new List<ChoiceData>();
	}
}
