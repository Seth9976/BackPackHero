using System;

namespace UnityEngine
{
	// Token: 0x02000178 RID: 376
	public enum ScreenOrientation
	{
		// Token: 0x040004CB RID: 1227
		[Obsolete("Enum member Unknown has been deprecated.", false)]
		Unknown,
		// Token: 0x040004CC RID: 1228
		[Obsolete("Use LandscapeLeft instead (UnityUpgradable) -> LandscapeLeft", true)]
		Landscape = 3,
		// Token: 0x040004CD RID: 1229
		Portrait = 1,
		// Token: 0x040004CE RID: 1230
		PortraitUpsideDown,
		// Token: 0x040004CF RID: 1231
		LandscapeLeft,
		// Token: 0x040004D0 RID: 1232
		LandscapeRight,
		// Token: 0x040004D1 RID: 1233
		AutoRotation
	}
}
