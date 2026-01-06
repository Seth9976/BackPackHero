using System;
using CleverCrow.Fluid.Databases;
using CleverCrow.Fluid.Utilities;
using UnityEngine;

namespace CleverCrow.Fluid.Dialogues.Actions.Databases
{
	// Token: 0x02000043 RID: 67
	[CreateMenu("Database/Globals/Set Int", 0)]
	public class SetGlobalInt : SetLocalVariableBase<int>
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00004490 File Offset: 0x00002690
		protected override KeyValueDefinitionBase<int> Variable
		{
			get
			{
				return this._variable;
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00004498 File Offset: 0x00002698
		protected override IKeyValueData<int> GetDatabase(IDialogueController dialogue)
		{
			return Singleton<GlobalDatabaseManager>.Instance.Database.Ints;
		}

		// Token: 0x04000078 RID: 120
		[SerializeField]
		public KeyValueDefinitionInt _variable;
	}
}
