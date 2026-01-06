using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001EF RID: 495
	public interface IMouseEvent
	{
		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000F41 RID: 3905
		EventModifiers modifiers { get; }

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000F42 RID: 3906
		Vector2 mousePosition { get; }

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000F43 RID: 3907
		Vector2 localMousePosition { get; }

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000F44 RID: 3908
		Vector2 mouseDelta { get; }

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000F45 RID: 3909
		int clickCount { get; }

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000F46 RID: 3910
		int button { get; }

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000F47 RID: 3911
		int pressedButtons { get; }

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000F48 RID: 3912
		bool shiftKey { get; }

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000F49 RID: 3913
		bool ctrlKey { get; }

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000F4A RID: 3914
		bool commandKey { get; }

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000F4B RID: 3915
		bool altKey { get; }

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000F4C RID: 3916
		bool actionKey { get; }
	}
}
