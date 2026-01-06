using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001D9 RID: 473
	public interface IEventHandler
	{
		// Token: 0x06000ED0 RID: 3792
		void SendEvent(EventBase e);

		// Token: 0x06000ED1 RID: 3793
		void HandleEvent(EventBase evt);

		// Token: 0x06000ED2 RID: 3794
		bool HasTrickleDownHandlers();

		// Token: 0x06000ED3 RID: 3795
		bool HasBubbleUpHandlers();
	}
}
