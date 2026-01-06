using System;
using CleverCrow.Fluid.Databases;
using CleverCrow.Fluid.Dialogues.Actions.Databases;
using CleverCrow.Fluid.Utilities;

namespace CleverCrow.Fluid.Dialogues.Conditions.Databases
{
	// Token: 0x02000037 RID: 55
	[CreateMenu("Database/Globals/Is String", 0)]
	public class IsGlobalString : IsStringBase
	{
		// Token: 0x060000FE RID: 254 RVA: 0x00004288 File Offset: 0x00002488
		protected override IKeyValueData<string> GetStringInstance(IDialogueController dialogue)
		{
			return Singleton<GlobalDatabaseManager>.Instance.Database.Strings;
		}
	}
}
