using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000150 RID: 336
	[NativeHeader("Runtime/Camera/OcclusionArea.h")]
	public sealed class OcclusionArea : Component
	{
		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000E16 RID: 3606 RVA: 0x00012A4C File Offset: 0x00010C4C
		// (set) Token: 0x06000E17 RID: 3607 RVA: 0x00012A62 File Offset: 0x00010C62
		public Vector3 center
		{
			get
			{
				Vector3 vector;
				this.get_center_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_center_Injected(ref value);
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000E18 RID: 3608 RVA: 0x00012A6C File Offset: 0x00010C6C
		// (set) Token: 0x06000E19 RID: 3609 RVA: 0x00012A82 File Offset: 0x00010C82
		public Vector3 size
		{
			get
			{
				Vector3 vector;
				this.get_size_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_size_Injected(ref value);
			}
		}

		// Token: 0x06000E1B RID: 3611
		[MethodImpl(4096)]
		private extern void get_center_Injected(out Vector3 ret);

		// Token: 0x06000E1C RID: 3612
		[MethodImpl(4096)]
		private extern void set_center_Injected(ref Vector3 value);

		// Token: 0x06000E1D RID: 3613
		[MethodImpl(4096)]
		private extern void get_size_Injected(out Vector3 ret);

		// Token: 0x06000E1E RID: 3614
		[MethodImpl(4096)]
		private extern void set_size_Injected(ref Vector3 value);
	}
}
