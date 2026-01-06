using System;
using CleverCrow.Fluid.Databases;
using CleverCrow.Fluid.Dialogues.Conditions;
using CleverCrow.Fluid.Dialogues.Nodes;
using UnityEngine;

namespace CleverCrow.Fluid.Dialogues.Actions.Databases
{
	// Token: 0x02000051 RID: 81
	public abstract class IsIntBase : ConditionDataBase
	{
		// Token: 0x0600014F RID: 335
		protected abstract IKeyValueData<int> GetIntInstance(IDialogueController dialogue);

		// Token: 0x06000150 RID: 336 RVA: 0x00004769 File Offset: 0x00002969
		public override void OnInit(IDialogueController dialogue)
		{
			this._condition = new ConditionIntInternal(this.GetIntInstance(dialogue));
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000477D File Offset: 0x0000297D
		public override bool OnGetIsValid(INode parent)
		{
			return this._condition.IsComparisonValid(this._variable, this._value, this._comparison);
		}

		// Token: 0x0400008B RID: 139
		private ConditionIntInternal _condition;

		// Token: 0x0400008C RID: 140
		[SerializeField]
		private KeyValueDefinitionInt _variable;

		// Token: 0x0400008D RID: 141
		[SerializeField]
		private NumberComparison _comparison;

		// Token: 0x0400008E RID: 142
		[SerializeField]
		private int _value;
	}
}
