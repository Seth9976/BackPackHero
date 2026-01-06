using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200002D RID: 45
	[NativeHeader("Modules/Physics/SphereCollider.h")]
	[RequiredByNativeCode]
	public class SphereCollider : Collider
	{
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000311 RID: 785 RVA: 0x00005410 File Offset: 0x00003610
		// (set) Token: 0x06000312 RID: 786 RVA: 0x00005426 File Offset: 0x00003626
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

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000313 RID: 787
		// (set) Token: 0x06000314 RID: 788
		public extern float radius
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000316 RID: 790
		[MethodImpl(4096)]
		private extern void get_center_Injected(out Vector3 ret);

		// Token: 0x06000317 RID: 791
		[MethodImpl(4096)]
		private extern void set_center_Injected(ref Vector3 value);
	}
}
