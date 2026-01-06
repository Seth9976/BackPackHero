using System;
using CleverCrow.Fluid.Databases;

namespace CleverCrow.Fluid.Dialogues.Actions.Databases
{
	// Token: 0x02000050 RID: 80
	[CreateMenu("Database/Locals/Is Float", 0)]
	public class ConditionLocalFloat : IsFloatBase
	{
		// Token: 0x0600014D RID: 333 RVA: 0x00004754 File Offset: 0x00002954
		protected override IKeyValueData<float> GetFloatInstance(IDialogueController dialogue)
		{
			return dialogue.LocalDatabase.Floats;
		}
	}
}
