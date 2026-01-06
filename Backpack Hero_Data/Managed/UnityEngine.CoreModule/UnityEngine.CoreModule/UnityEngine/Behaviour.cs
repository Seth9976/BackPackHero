using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001F9 RID: 505
	[NativeHeader("Runtime/Mono/MonoBehaviour.h")]
	[UsedByNativeCode]
	public class Behaviour : Component
	{
		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06001664 RID: 5732
		// (set) Token: 0x06001665 RID: 5733
		[RequiredByNativeCode]
		[NativeProperty]
		public extern bool enabled
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06001666 RID: 5734
		[NativeProperty]
		public extern bool isActiveAndEnabled
		{
			[NativeMethod("IsAddedToManager")]
			[MethodImpl(4096)]
			get;
		}
	}
}
