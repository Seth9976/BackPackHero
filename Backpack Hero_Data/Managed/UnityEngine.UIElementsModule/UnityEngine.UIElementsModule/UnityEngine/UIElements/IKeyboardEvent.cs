using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001E7 RID: 487
	public interface IKeyboardEvent
	{
		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000F10 RID: 3856
		EventModifiers modifiers { get; }

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000F11 RID: 3857
		char character { get; }

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000F12 RID: 3858
		KeyCode keyCode { get; }

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000F13 RID: 3859
		bool shiftKey { get; }

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000F14 RID: 3860
		bool ctrlKey { get; }

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000F15 RID: 3861
		bool commandKey { get; }

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000F16 RID: 3862
		bool altKey { get; }

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000F17 RID: 3863
		bool actionKey { get; }
	}
}
