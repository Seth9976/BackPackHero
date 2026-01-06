using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000009 RID: 9
	[ExcludeFromPreset]
	[NativeHeader("Modules/Terrain/Public/Tree.h")]
	public sealed class Tree : Component
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600007A RID: 122
		// (set) Token: 0x0600007B RID: 123
		[NativeProperty("TreeData")]
		public extern ScriptableObject data
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600007C RID: 124
		public extern bool hasSpeedTreeWind
		{
			[NativeMethod("HasSpeedTreeWind")]
			[MethodImpl(4096)]
			get;
		}
	}
}
