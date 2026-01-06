using System;
using CleverCrow.Fluid.Databases;
using CleverCrow.Fluid.Dialogues.Conditions;
using CleverCrow.Fluid.Dialogues.Nodes;
using UnityEngine;

namespace CleverCrow.Fluid.Dialogues.Actions.Databases
{
	// Token: 0x0200004E RID: 78
	[CreateMenu("Database/Locals/Is Float", 0)]
	public abstract class IsFloatBase : ConditionDataBase
	{
		// Token: 0x06000147 RID: 327
		protected abstract IKeyValueData<float> GetFloatInstance(IDialogueController dialogue);

		// Token: 0x06000148 RID: 328 RVA: 0x00004678 File Offset: 0x00002878
		public override void OnInit(IDialogueController dialogue)
		{
			this._condition = new ConditionFloatInternal(this.GetFloatInstance(dialogue));
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000468C File Offset: 0x0000288C
		public override bool OnGetIsValid(INode parent)
		{
			return this._condition.IsComparisonValid(this._variable, this._value, this._comparison);
		}

		// Token: 0x04000086 RID: 134
		private ConditionFloatInternal _condition;

		// Token: 0x04000087 RID: 135
		[SerializeField]
		private KeyValueDefinitionFloat _variable;

		// Token: 0x04000088 RID: 136
		[SerializeField]
		private NumberComparison _comparison;

		// Token: 0x04000089 RID: 137
		[SerializeField]
		private float _value;
	}
}
