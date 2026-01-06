using System;
using CleverCrow.Fluid.Dialogues.Nodes;

namespace CleverCrow.Fluid.Dialogues.Conditions
{
	// Token: 0x02000031 RID: 49
	public interface IConditionData
	{
		// Token: 0x060000F2 RID: 242
		void OnInit(IDialogueController dialogue);

		// Token: 0x060000F3 RID: 243
		bool OnGetIsValid(INode parent);
	}
}
