using System;
using CleverCrow.Fluid.Databases;
using CleverCrow.Fluid.Dialogues.Actions.Databases;
using CleverCrow.Fluid.Utilities;

namespace CleverCrow.Fluid.Dialogues.Conditions.Databases
{
	// Token: 0x02000035 RID: 53
	[CreateMenu("Database/Globals/Is Float", 0)]
	public class IsGlobalFloat : IsFloatBase
	{
		// Token: 0x060000FA RID: 250 RVA: 0x00004256 File Offset: 0x00002456
		protected override IKeyValueData<float> GetFloatInstance(IDialogueController dialogue)
		{
			return Singleton<GlobalDatabaseManager>.Instance.Database.Floats;
		}
	}
}
