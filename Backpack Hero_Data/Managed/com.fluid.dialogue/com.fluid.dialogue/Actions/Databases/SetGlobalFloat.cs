using System;
using CleverCrow.Fluid.Databases;
using CleverCrow.Fluid.Utilities;
using UnityEngine;

namespace CleverCrow.Fluid.Dialogues.Actions.Databases
{
	// Token: 0x02000042 RID: 66
	[CreateMenu("Database/Globals/Set Float", 0)]
	public class SetGlobalFloat : SetLocalVariableBase<float>
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000122 RID: 290 RVA: 0x0000446F File Offset: 0x0000266F
		protected override KeyValueDefinitionBase<float> Variable
		{
			get
			{
				return this._variable;
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00004477 File Offset: 0x00002677
		protected override IKeyValueData<float> GetDatabase(IDialogueController dialogue)
		{
			return Singleton<GlobalDatabaseManager>.Instance.Database.Floats;
		}

		// Token: 0x04000077 RID: 119
		[SerializeField]
		public KeyValueDefinitionFloat _variable;
	}
}
