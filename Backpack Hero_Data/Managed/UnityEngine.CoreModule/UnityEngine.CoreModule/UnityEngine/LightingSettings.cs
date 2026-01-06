using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200011B RID: 283
	[NativeHeader("Runtime/Graphics/LightingSettings.h")]
	[PreventReadOnlyInstanceModification]
	public sealed class LightingSettings : Object
	{
		// Token: 0x06000791 RID: 1937 RVA: 0x00004557 File Offset: 0x00002757
		[RequiredByNativeCode]
		internal void LightingSettingsDontStripMe()
		{
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0000B766 File Offset: 0x00009966
		public LightingSettings()
		{
			LightingSettings.Internal_Create(this);
		}

		// Token: 0x06000793 RID: 1939
		[MethodImpl(4096)]
		private static extern void Internal_Create([Writable] LightingSettings self);

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000794 RID: 1940
		// (set) Token: 0x06000795 RID: 1941
		[NativeName("EnableBakedLightmaps")]
		public extern bool bakedGI
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000796 RID: 1942
		// (set) Token: 0x06000797 RID: 1943
		[NativeName("EnableRealtimeLightmaps")]
		public extern bool realtimeGI
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000798 RID: 1944
		// (set) Token: 0x06000799 RID: 1945
		[NativeName("RealtimeEnvironmentLighting")]
		public extern bool realtimeEnvironmentLighting
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}
	}
}
