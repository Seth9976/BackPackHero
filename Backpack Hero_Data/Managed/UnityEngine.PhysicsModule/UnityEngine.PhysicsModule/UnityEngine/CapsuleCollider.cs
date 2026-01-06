using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200002B RID: 43
	[NativeHeader("Modules/Physics/CapsuleCollider.h")]
	[RequiredByNativeCode]
	public class CapsuleCollider : Collider
	{
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00005348 File Offset: 0x00003548
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x0000535E File Offset: 0x0000355E
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

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002F9 RID: 761
		// (set) Token: 0x060002FA RID: 762
		public extern float radius
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002FB RID: 763
		// (set) Token: 0x060002FC RID: 764
		public extern float height
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060002FD RID: 765
		// (set) Token: 0x060002FE RID: 766
		public extern int direction
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00005368 File Offset: 0x00003568
		internal Vector2 GetGlobalExtents()
		{
			Vector2 vector;
			this.GetGlobalExtents_Injected(out vector);
			return vector;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00005380 File Offset: 0x00003580
		internal Matrix4x4 CalculateTransform()
		{
			Matrix4x4 matrix4x;
			this.CalculateTransform_Injected(out matrix4x);
			return matrix4x;
		}

		// Token: 0x06000302 RID: 770
		[MethodImpl(4096)]
		private extern void get_center_Injected(out Vector3 ret);

		// Token: 0x06000303 RID: 771
		[MethodImpl(4096)]
		private extern void set_center_Injected(ref Vector3 value);

		// Token: 0x06000304 RID: 772
		[MethodImpl(4096)]
		private extern void GetGlobalExtents_Injected(out Vector2 ret);

		// Token: 0x06000305 RID: 773
		[MethodImpl(4096)]
		private extern void CalculateTransform_Injected(out Matrix4x4 ret);
	}
}
