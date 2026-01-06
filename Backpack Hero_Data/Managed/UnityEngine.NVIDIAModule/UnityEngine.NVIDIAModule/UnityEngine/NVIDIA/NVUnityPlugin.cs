using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.NVIDIA
{
	// Token: 0x02000004 RID: 4
	[NativeHeader("Modules/NVIDIA/NVPlugins.h")]
	public static class NVUnityPlugin
	{
		// Token: 0x06000003 RID: 3
		[MethodImpl(4096)]
		public static extern bool Load();

		// Token: 0x06000004 RID: 4
		[MethodImpl(4096)]
		public static extern bool IsLoaded();
	}
}
