using System;
using CleverCrow.Fluid.Databases;
using CleverCrow.Fluid.Utilities;
using UnityEngine;

namespace CleverCrow.Fluid.Dialogues.Actions.Databases
{
	// Token: 0x02000044 RID: 68
	[CreateMenu("Database/Globals/Set String", 0)]
	public class SetGlobalString : SetLocalVariableBase<string>
	{
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000128 RID: 296 RVA: 0x000044B1 File Offset: 0x000026B1
		protected override KeyValueDefinitionBase<string> Variable
		{
			get
			{
				return this._variable;
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000044B9 File Offset: 0x000026B9
		protected override IKeyValueData<string> GetDatabase(IDialogueController dialogue)
		{
			return Singleton<GlobalDatabaseManager>.Instance.Database.Strings;
		}

		// Token: 0x04000079 RID: 121
		[SerializeField]
		public KeyValueDefinitionString _variable;
	}
}
