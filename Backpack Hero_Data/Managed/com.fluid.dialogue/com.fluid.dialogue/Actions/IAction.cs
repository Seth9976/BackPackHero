using System;

namespace CleverCrow.Fluid.Dialogues.Actions
{
	// Token: 0x0200003C RID: 60
	public interface IAction : IUniqueId
	{
		// Token: 0x06000115 RID: 277
		ActionStatus Tick();

		// Token: 0x06000116 RID: 278
		void End();
	}
}
