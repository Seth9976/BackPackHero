using System;
using CleverCrow.Fluid.Databases;
using CleverCrow.Fluid.Dialogues.Conditions;
using CleverCrow.Fluid.Dialogues.Nodes;
using UnityEngine;

namespace CleverCrow.Fluid.Dialogues.Actions.Databases
{
	// Token: 0x0200004B RID: 75
	public abstract class IsBoolBase : ConditionDataBase
	{
		// Token: 0x0600013E RID: 318
		protected abstract IKeyValueData<bool> GetBoolInstance(IDialogueController dialogue);

		// Token: 0x0600013F RID: 319 RVA: 0x000045A4 File Offset: 0x000027A4
		public override void OnInit(IDialogueController dialogue)
		{
			this._condition = new ConditionBoolInternal(this.GetBoolInstance(dialogue));
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000045B8 File Offset: 0x000027B8
		public override bool OnGetIsValid(INode parent)
		{
			IsBoolBase.Comparison comparison = this._comparison;
			if (comparison == IsBoolBase.Comparison.Equal)
			{
				return this._condition.AreValuesEqual(this._variable, this._value);
			}
			if (comparison != IsBoolBase.Comparison.NotEqual)
			{
				throw new ArgumentOutOfRangeException();
			}
			return this._condition.AreValuesNotEqual(this._variable, this._value);
		}

		// Token: 0x04000081 RID: 129
		private ConditionBoolInternal _condition;

		// Token: 0x04000082 RID: 130
		[SerializeField]
		private KeyValueDefinitionBool _variable;

		// Token: 0x04000083 RID: 131
		[SerializeField]
		private IsBoolBase.Comparison _comparison;

		// Token: 0x04000084 RID: 132
		[SerializeField]
		private bool _value = true;

		// Token: 0x0200006A RID: 106
		private enum Comparison
		{
			// Token: 0x040000C3 RID: 195
			Equal,
			// Token: 0x040000C4 RID: 196
			NotEqual
		}
	}
}
