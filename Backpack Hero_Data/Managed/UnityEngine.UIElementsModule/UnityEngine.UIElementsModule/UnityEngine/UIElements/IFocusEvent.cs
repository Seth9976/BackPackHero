using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001DB RID: 475
	public interface IFocusEvent
	{
		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000EE5 RID: 3813
		Focusable relatedTarget { get; }

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000EE6 RID: 3814
		FocusChangeDirection direction { get; }
	}
}
