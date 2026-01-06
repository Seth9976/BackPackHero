using System;
using CleverCrow.Fluid.Databases;
using UnityEngine;

namespace CleverCrow.Fluid.Dialogues.Actions.Databases
{
	// Token: 0x02000048 RID: 72
	[CreateMenu("Database/Locals/Set String", 0)]
	public class SetLocalString : SetLocalVariableBase<string>
	{
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00004529 File Offset: 0x00002729
		protected override KeyValueDefinitionBase<string> Variable
		{
			get
			{
				return this._variable;
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00004531 File Offset: 0x00002731
		protected override IKeyValueData<string> GetDatabase(IDialogueController dialogue)
		{
			return dialogue.LocalDatabase.Strings;
		}

		// Token: 0x0400007D RID: 125
		[SerializeField]
		public KeyValueDefinitionString _variable;
	}
}
