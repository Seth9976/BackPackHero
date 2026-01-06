using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001E2 RID: 482
	internal interface IEventDispatchingStrategy
	{
		// Token: 0x06000EFD RID: 3837
		bool CanDispatchEvent(EventBase evt);

		// Token: 0x06000EFE RID: 3838
		void DispatchEvent(EventBase evt, IPanel panel);
	}
}
