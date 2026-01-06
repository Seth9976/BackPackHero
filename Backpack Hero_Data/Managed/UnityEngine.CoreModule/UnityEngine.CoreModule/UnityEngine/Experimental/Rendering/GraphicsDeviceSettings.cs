using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x02000474 RID: 1140
	public static class GraphicsDeviceSettings
	{
		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06002829 RID: 10281
		// (set) Token: 0x0600282A RID: 10282
		[StaticAccessor("GetGfxDevice()", StaticAccessorType.Dot)]
		public static extern WaitForPresentSyncPoint waitForPresentSyncPoint
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x0600282B RID: 10283
		// (set) Token: 0x0600282C RID: 10284
		[StaticAccessor("GetGfxDevice()", StaticAccessorType.Dot)]
		public static extern GraphicsJobsSyncPoint graphicsJobsSyncPoint
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}
	}
}
