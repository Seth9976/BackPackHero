using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000153 RID: 339
	[NativeHeader("Runtime/Camera/Projector.h")]
	public sealed class Projector : Behaviour
	{
		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000E2C RID: 3628
		// (set) Token: 0x06000E2D RID: 3629
		public extern float nearClipPlane
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000E2E RID: 3630
		// (set) Token: 0x06000E2F RID: 3631
		public extern float farClipPlane
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000E30 RID: 3632
		// (set) Token: 0x06000E31 RID: 3633
		public extern float fieldOfView
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000E32 RID: 3634
		// (set) Token: 0x06000E33 RID: 3635
		public extern float aspectRatio
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000E34 RID: 3636
		// (set) Token: 0x06000E35 RID: 3637
		public extern bool orthographic
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000E36 RID: 3638
		// (set) Token: 0x06000E37 RID: 3639
		public extern float orthographicSize
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000E38 RID: 3640
		// (set) Token: 0x06000E39 RID: 3641
		public extern int ignoreLayers
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000E3A RID: 3642
		// (set) Token: 0x06000E3B RID: 3643
		public extern Material material
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}
	}
}
