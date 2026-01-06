using System;

namespace CleverCrow.Fluid.Dialogues.Actions
{
	// Token: 0x02000039 RID: 57
	public interface IActionData
	{
		// Token: 0x06000107 RID: 263
		void OnInit(IDialogueController dialogue);

		// Token: 0x06000108 RID: 264
		void OnStart();

		// Token: 0x06000109 RID: 265
		ActionStatus OnUpdate();

		// Token: 0x0600010A RID: 266
		void OnExit();

		// Token: 0x0600010B RID: 267
		void OnReset();
	}
}
