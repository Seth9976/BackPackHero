using System;
using CleverCrow.Fluid.Dialogues.Graphs;
using CleverCrow.Fluid.Dialogues.Nodes;
using UnityEngine;

namespace CleverCrow.Fluid.Dialogues.Conditions
{
	// Token: 0x02000030 RID: 48
	public abstract class ConditionDataBase : NodeNestedDataBase<ICondition>, IConditionData
	{
		// Token: 0x060000EE RID: 238 RVA: 0x000041CC File Offset: 0x000023CC
		public virtual void OnInit(IDialogueController dialogue)
		{
		}

		// Token: 0x060000EF RID: 239
		public abstract bool OnGetIsValid(INode parent);

		// Token: 0x060000F0 RID: 240 RVA: 0x000041CE File Offset: 0x000023CE
		public override ICondition GetRuntime(IGraph graphRuntime, IDialogueController dialogue)
		{
			return new ConditionRuntime(dialogue, this._uniqueId, Object.Instantiate<ConditionDataBase>(this));
		}
	}
}
