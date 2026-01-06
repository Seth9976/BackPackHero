using System;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000017 RID: 23
	public interface IInputInteraction
	{
		// Token: 0x0600012B RID: 299
		void Process(ref InputInteractionContext context);

		// Token: 0x0600012C RID: 300
		void Reset();
	}
}
