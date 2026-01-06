using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200002E RID: 46
	public interface IFocusRing
	{
		// Token: 0x06000125 RID: 293
		FocusChangeDirection GetFocusChangeDirection(Focusable currentFocusable, EventBase e);

		// Token: 0x06000126 RID: 294
		Focusable GetNextFocusable(Focusable currentFocusable, FocusChangeDirection direction);
	}
}
