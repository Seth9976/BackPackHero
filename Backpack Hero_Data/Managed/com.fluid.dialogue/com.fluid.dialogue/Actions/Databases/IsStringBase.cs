using System;
using CleverCrow.Fluid.Databases;
using CleverCrow.Fluid.Dialogues.Conditions;
using CleverCrow.Fluid.Dialogues.Nodes;
using UnityEngine;

namespace CleverCrow.Fluid.Dialogues.Actions.Databases
{
	// Token: 0x02000055 RID: 85
	public abstract class IsStringBase : ConditionDataBase
	{
		// Token: 0x06000159 RID: 345
		protected abstract IKeyValueData<string> GetStringInstance(IDialogueController dialogue);

		// Token: 0x0600015A RID: 346 RVA: 0x0000485B File Offset: 0x00002A5B
		public override void OnInit(IDialogueController dialogue)
		{
			this._condition = new ConditionStringInternal(this.GetStringInstance(dialogue));
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00004870 File Offset: 0x00002A70
		public override bool OnGetIsValid(INode parent)
		{
			IsStringBase.Comparison comparison = this._comparison;
			if (comparison == IsStringBase.Comparison.Equal)
			{
				return this._condition.AreValuesEqual(this._variable, this._value);
			}
			if (comparison != IsStringBase.Comparison.NotEqual)
			{
				throw new ArgumentOutOfRangeException();
			}
			return this._condition.AreValuesNotEqual(this._variable, this._value);
		}

		// Token: 0x04000090 RID: 144
		private ConditionStringInternal _condition;

		// Token: 0x04000091 RID: 145
		[SerializeField]
		private KeyValueDefinitionString _variable;

		// Token: 0x04000092 RID: 146
		[SerializeField]
		private IsStringBase.Comparison _comparison;

		// Token: 0x04000093 RID: 147
		[SerializeField]
		private string _value;

		// Token: 0x0200006B RID: 107
		private enum Comparison
		{
			// Token: 0x040000C6 RID: 198
			Equal,
			// Token: 0x040000C7 RID: 199
			NotEqual
		}
	}
}
