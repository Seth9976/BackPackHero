using System;
using CleverCrow.Fluid.Databases;
using UnityEngine;

namespace CleverCrow.Fluid.Dialogues.Actions.Databases
{
	// Token: 0x02000047 RID: 71
	[CreateMenu("Database/Locals/Set Int", 0)]
	public class SetLocalInt : SetLocalVariableBase<int>
	{
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000131 RID: 305 RVA: 0x0000450C File Offset: 0x0000270C
		protected override KeyValueDefinitionBase<int> Variable
		{
			get
			{
				return this._variable;
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00004514 File Offset: 0x00002714
		protected override IKeyValueData<int> GetDatabase(IDialogueController dialogue)
		{
			return dialogue.LocalDatabase.Ints;
		}

		// Token: 0x0400007C RID: 124
		[SerializeField]
		public KeyValueDefinitionInt _variable;
	}
}
