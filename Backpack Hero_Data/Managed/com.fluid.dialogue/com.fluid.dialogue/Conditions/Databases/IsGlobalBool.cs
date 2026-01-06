using System;
using CleverCrow.Fluid.Databases;
using CleverCrow.Fluid.Dialogues.Actions.Databases;
using CleverCrow.Fluid.Utilities;

namespace CleverCrow.Fluid.Dialogues.Conditions.Databases
{
	// Token: 0x02000034 RID: 52
	[CreateMenu("Database/Globals/Is Bool", 0)]
	public class IsGlobalBool : IsBoolBase
	{
		// Token: 0x060000F8 RID: 248 RVA: 0x0000423D File Offset: 0x0000243D
		protected override IKeyValueData<bool> GetBoolInstance(IDialogueController dialogue)
		{
			return Singleton<GlobalDatabaseManager>.Instance.Database.Bools;
		}
	}
}
