using System;
using CleverCrow.Fluid.Databases;

namespace CleverCrow.Fluid.Dialogues.Actions.Databases
{
	// Token: 0x0200004D RID: 77
	[CreateMenu("Database/Locals/Is Bool", 0)]
	public class IsLocalBool : IsBoolBase
	{
		// Token: 0x06000145 RID: 325 RVA: 0x00004663 File Offset: 0x00002863
		protected override IKeyValueData<bool> GetBoolInstance(IDialogueController dialogue)
		{
			return dialogue.LocalDatabase.Bools;
		}
	}
}
