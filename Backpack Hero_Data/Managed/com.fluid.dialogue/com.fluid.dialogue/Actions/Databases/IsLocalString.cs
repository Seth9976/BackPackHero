using System;
using CleverCrow.Fluid.Databases;

namespace CleverCrow.Fluid.Dialogues.Actions.Databases
{
	// Token: 0x02000054 RID: 84
	[CreateMenu("Database/Locals/Is String", 0)]
	public class IsLocalString : IsStringBase
	{
		// Token: 0x06000157 RID: 343 RVA: 0x00004846 File Offset: 0x00002A46
		protected override IKeyValueData<string> GetStringInstance(IDialogueController dialogue)
		{
			return dialogue.LocalDatabase.Strings;
		}
	}
}
