using System;
using CleverCrow.Fluid.Databases;

namespace CleverCrow.Fluid.Dialogues.Actions.Databases
{
	// Token: 0x02000053 RID: 83
	[CreateMenu("Database/Locals/Is Int", 0)]
	public class IsLocalInt : IsIntBase
	{
		// Token: 0x06000155 RID: 341 RVA: 0x00004831 File Offset: 0x00002A31
		protected override IKeyValueData<int> GetIntInstance(IDialogueController dialogue)
		{
			return dialogue.LocalDatabase.Ints;
		}
	}
}
