using System;
using CleverCrow.Fluid.Databases;
using UnityEngine;

namespace CleverCrow.Fluid.Dialogues.Actions.Databases
{
	// Token: 0x02000046 RID: 70
	[CreateMenu("Database/Locals/Set Float", 0)]
	public class SetLocalFloat : SetLocalVariableBase<float>
	{
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600012E RID: 302 RVA: 0x000044EF File Offset: 0x000026EF
		protected override KeyValueDefinitionBase<float> Variable
		{
			get
			{
				return this._variable;
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000044F7 File Offset: 0x000026F7
		protected override IKeyValueData<float> GetDatabase(IDialogueController dialogue)
		{
			return dialogue.LocalDatabase.Floats;
		}

		// Token: 0x0400007B RID: 123
		[SerializeField]
		public KeyValueDefinitionFloat _variable;
	}
}
