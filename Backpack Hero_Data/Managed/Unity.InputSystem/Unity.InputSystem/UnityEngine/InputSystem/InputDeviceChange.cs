using System;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000038 RID: 56
	public enum InputDeviceChange
	{
		// Token: 0x04000169 RID: 361
		Added,
		// Token: 0x0400016A RID: 362
		Removed,
		// Token: 0x0400016B RID: 363
		Disconnected,
		// Token: 0x0400016C RID: 364
		Reconnected,
		// Token: 0x0400016D RID: 365
		Enabled,
		// Token: 0x0400016E RID: 366
		Disabled,
		// Token: 0x0400016F RID: 367
		UsageChanged,
		// Token: 0x04000170 RID: 368
		ConfigurationChanged,
		// Token: 0x04000171 RID: 369
		SoftReset,
		// Token: 0x04000172 RID: 370
		HardReset,
		// Token: 0x04000173 RID: 371
		[Obsolete("Destroyed enum has been deprecated.")]
		Destroyed
	}
}
