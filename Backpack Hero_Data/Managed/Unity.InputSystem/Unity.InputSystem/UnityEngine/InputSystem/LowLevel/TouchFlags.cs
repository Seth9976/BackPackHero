using System;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000D4 RID: 212
	[Flags]
	internal enum TouchFlags : byte
	{
		// Token: 0x04000531 RID: 1329
		IndirectTouch = 1,
		// Token: 0x04000532 RID: 1330
		PrimaryTouch = 8,
		// Token: 0x04000533 RID: 1331
		TapPress = 16,
		// Token: 0x04000534 RID: 1332
		TapRelease = 32,
		// Token: 0x04000535 RID: 1333
		OrphanedPrimaryTouch = 64,
		// Token: 0x04000536 RID: 1334
		BeganInSameFrame = 128
	}
}
