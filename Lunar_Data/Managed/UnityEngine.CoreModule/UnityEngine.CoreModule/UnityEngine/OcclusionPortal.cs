using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200014F RID: 335
	[NativeHeader("Runtime/Camera/OcclusionPortal.h")]
	public sealed class OcclusionPortal : Component
	{
		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000E13 RID: 3603
		// (set) Token: 0x06000E14 RID: 3604
		[NativeProperty("IsOpen")]
		public extern bool open
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}
	}
}
