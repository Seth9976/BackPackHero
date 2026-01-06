using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200002A RID: 42
	[NativeHeader("Modules/Physics2D/Joint2D.h")]
	[RequireComponent(typeof(Transform), typeof(Rigidbody2D))]
	public class Joint2D : Behaviour
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060003BE RID: 958
		public extern Rigidbody2D attachedRigidbody
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060003BF RID: 959
		// (set) Token: 0x060003C0 RID: 960
		public extern Rigidbody2D connectedBody
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060003C1 RID: 961
		// (set) Token: 0x060003C2 RID: 962
		public extern bool enableCollision
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060003C3 RID: 963
		// (set) Token: 0x060003C4 RID: 964
		public extern float breakForce
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060003C5 RID: 965
		// (set) Token: 0x060003C6 RID: 966
		public extern float breakTorque
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x000084D8 File Offset: 0x000066D8
		public Vector2 reactionForce
		{
			[NativeMethod("GetReactionForceFixedTime")]
			get
			{
				Vector2 vector;
				this.get_reactionForce_Injected(out vector);
				return vector;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060003C8 RID: 968
		public extern float reactionTorque
		{
			[NativeMethod("GetReactionTorqueFixedTime")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x000084F0 File Offset: 0x000066F0
		public Vector2 GetReactionForce(float timeStep)
		{
			Vector2 vector;
			this.GetReactionForce_Injected(timeStep, out vector);
			return vector;
		}

		// Token: 0x060003CA RID: 970
		[MethodImpl(4096)]
		public extern float GetReactionTorque(float timeStep);

		// Token: 0x060003CC RID: 972
		[MethodImpl(4096)]
		private extern void get_reactionForce_Injected(out Vector2 ret);

		// Token: 0x060003CD RID: 973
		[MethodImpl(4096)]
		private extern void GetReactionForce_Injected(float timeStep, out Vector2 ret);
	}
}
