using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000151 RID: 337
	[NativeHeader("Runtime/Camera/Flare.h")]
	public sealed class Flare : Object
	{
		// Token: 0x06000E1F RID: 3615 RVA: 0x00012A8C File Offset: 0x00010C8C
		public Flare()
		{
			Flare.Internal_Create(this);
		}

		// Token: 0x06000E20 RID: 3616
		[MethodImpl(4096)]
		private static extern void Internal_Create([Writable] Flare self);
	}
}
