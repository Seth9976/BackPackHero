using System;
using CleverCrow.Fluid.Databases;
using CleverCrow.Fluid.Dialogues.Actions.Databases;
using CleverCrow.Fluid.Utilities;

namespace CleverCrow.Fluid.Dialogues.Conditions.Databases
{
	// Token: 0x02000036 RID: 54
	[CreateMenu("Database/Globals/Is Int", 0)]
	public class IsGlobalInt : IsIntBase
	{
		// Token: 0x060000FC RID: 252 RVA: 0x0000426F File Offset: 0x0000246F
		protected override IKeyValueData<int> GetIntInstance(IDialogueController dialogue)
		{
			return Singleton<GlobalDatabaseManager>.Instance.Database.Ints;
		}
	}
}
