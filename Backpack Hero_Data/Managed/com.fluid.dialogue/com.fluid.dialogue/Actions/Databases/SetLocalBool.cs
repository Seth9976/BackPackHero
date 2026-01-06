using System;
using CleverCrow.Fluid.Databases;
using UnityEngine;

namespace CleverCrow.Fluid.Dialogues.Actions.Databases
{
	// Token: 0x02000045 RID: 69
	[CreateMenu("Database/Locals/Set Bool", 0)]
	public class SetLocalBool : SetLocalVariableBase<bool>
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600012B RID: 299 RVA: 0x000044D2 File Offset: 0x000026D2
		protected override KeyValueDefinitionBase<bool> Variable
		{
			get
			{
				return this._variable;
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000044DA File Offset: 0x000026DA
		protected override IKeyValueData<bool> GetDatabase(IDialogueController dialogue)
		{
			return dialogue.LocalDatabase.Bools;
		}

		// Token: 0x0400007A RID: 122
		[SerializeField]
		public KeyValueDefinitionBool _variable;
	}
}
