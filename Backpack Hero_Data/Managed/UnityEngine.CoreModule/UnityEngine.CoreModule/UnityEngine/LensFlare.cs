using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000152 RID: 338
	[NativeHeader("Runtime/Camera/Flare.h")]
	public sealed class LensFlare : Behaviour
	{
		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000E21 RID: 3617
		// (set) Token: 0x06000E22 RID: 3618
		public extern float brightness
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000E23 RID: 3619
		// (set) Token: 0x06000E24 RID: 3620
		public extern float fadeSpeed
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000E25 RID: 3621 RVA: 0x00012AA0 File Offset: 0x00010CA0
		// (set) Token: 0x06000E26 RID: 3622 RVA: 0x00012AB6 File Offset: 0x00010CB6
		public Color color
		{
			get
			{
				Color color;
				this.get_color_Injected(out color);
				return color;
			}
			set
			{
				this.set_color_Injected(ref value);
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000E27 RID: 3623
		// (set) Token: 0x06000E28 RID: 3624
		public extern Flare flare
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000E2A RID: 3626
		[MethodImpl(4096)]
		private extern void get_color_Injected(out Color ret);

		// Token: 0x06000E2B RID: 3627
		[MethodImpl(4096)]
		private extern void set_color_Injected(ref Color value);
	}
}
