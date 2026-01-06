using System;
using CleverCrow.Fluid.Databases;
using CleverCrow.Fluid.Utilities;
using UnityEngine;

namespace CleverCrow.Fluid.Dialogues.Actions.Databases
{
	// Token: 0x02000041 RID: 65
	[CreateMenu("Database/Globals/Set Bool", 0)]
	public class SetGlobalBool : SetLocalVariableBase<bool>
	{
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600011F RID: 287 RVA: 0x0000444E File Offset: 0x0000264E
		protected override KeyValueDefinitionBase<bool> Variable
		{
			get
			{
				return this._variable;
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004456 File Offset: 0x00002656
		protected override IKeyValueData<bool> GetDatabase(IDialogueController dialogue)
		{
			return Singleton<GlobalDatabaseManager>.Instance.Database.Bools;
		}

		// Token: 0x04000076 RID: 118
		[SerializeField]
		public KeyValueDefinitionBool _variable;
	}
}
