using System;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000132 RID: 306
	[UsedByNativeCode]
	[NativeHeader("Runtime/Graphics/LightmapData.h")]
	[StructLayout(0)]
	public sealed class LightmapData
	{
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000992 RID: 2450 RVA: 0x0000E728 File Offset: 0x0000C928
		// (set) Token: 0x06000993 RID: 2451 RVA: 0x0000E740 File Offset: 0x0000C940
		[Obsolete("Use lightmapColor property (UnityUpgradable) -> lightmapColor", false)]
		public Texture2D lightmapLight
		{
			get
			{
				return this.m_Light;
			}
			set
			{
				this.m_Light = value;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x0000E74C File Offset: 0x0000C94C
		// (set) Token: 0x06000995 RID: 2453 RVA: 0x0000E740 File Offset: 0x0000C940
		public Texture2D lightmapColor
		{
			get
			{
				return this.m_Light;
			}
			set
			{
				this.m_Light = value;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000996 RID: 2454 RVA: 0x0000E764 File Offset: 0x0000C964
		// (set) Token: 0x06000997 RID: 2455 RVA: 0x0000E77C File Offset: 0x0000C97C
		public Texture2D lightmapDir
		{
			get
			{
				return this.m_Dir;
			}
			set
			{
				this.m_Dir = value;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x0000E788 File Offset: 0x0000C988
		// (set) Token: 0x06000999 RID: 2457 RVA: 0x0000E7A0 File Offset: 0x0000C9A0
		public Texture2D shadowMask
		{
			get
			{
				return this.m_ShadowMask;
			}
			set
			{
				this.m_ShadowMask = value;
			}
		}

		// Token: 0x040003D5 RID: 981
		internal Texture2D m_Light;

		// Token: 0x040003D6 RID: 982
		internal Texture2D m_Dir;

		// Token: 0x040003D7 RID: 983
		internal Texture2D m_ShadowMask;
	}
}
