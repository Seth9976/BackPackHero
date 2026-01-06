using System;

namespace UnityEngine.InputSystem.Users
{
	// Token: 0x02000080 RID: 128
	[Flags]
	public enum InputUserPairingOptions
	{
		// Token: 0x04000393 RID: 915
		None = 0,
		// Token: 0x04000394 RID: 916
		ForcePlatformUserAccountSelection = 1,
		// Token: 0x04000395 RID: 917
		ForceNoPlatformUserAccountSelection = 2,
		// Token: 0x04000396 RID: 918
		UnpairCurrentDevicesFromUser = 8
	}
}
