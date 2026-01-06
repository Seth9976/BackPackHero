using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.AI
{
	// Token: 0x02000010 RID: 16
	[NativeHeader("Modules/AI/NavMesh/NavMesh.bindings.h")]
	public sealed class NavMeshData : Object
	{
		// Token: 0x060000BE RID: 190 RVA: 0x0000263B File Offset: 0x0000083B
		public NavMeshData()
		{
			NavMeshData.Internal_Create(this, 0);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000264D File Offset: 0x0000084D
		public NavMeshData(int agentTypeID)
		{
			NavMeshData.Internal_Create(this, agentTypeID);
		}

		// Token: 0x060000C0 RID: 192
		[StaticAccessor("NavMeshDataBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(4096)]
		private static extern void Internal_Create([Writable] NavMeshData mono, int agentTypeID);

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00002660 File Offset: 0x00000860
		public Bounds sourceBounds
		{
			get
			{
				Bounds bounds;
				this.get_sourceBounds_Injected(out bounds);
				return bounds;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00002678 File Offset: 0x00000878
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x0000268E File Offset: 0x0000088E
		public Vector3 position
		{
			get
			{
				Vector3 vector;
				this.get_position_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_position_Injected(ref value);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00002698 File Offset: 0x00000898
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x000026AE File Offset: 0x000008AE
		public Quaternion rotation
		{
			get
			{
				Quaternion quaternion;
				this.get_rotation_Injected(out quaternion);
				return quaternion;
			}
			set
			{
				this.set_rotation_Injected(ref value);
			}
		}

		// Token: 0x060000C6 RID: 198
		[MethodImpl(4096)]
		private extern void get_sourceBounds_Injected(out Bounds ret);

		// Token: 0x060000C7 RID: 199
		[MethodImpl(4096)]
		private extern void get_position_Injected(out Vector3 ret);

		// Token: 0x060000C8 RID: 200
		[MethodImpl(4096)]
		private extern void set_position_Injected(ref Vector3 value);

		// Token: 0x060000C9 RID: 201
		[MethodImpl(4096)]
		private extern void get_rotation_Injected(out Quaternion ret);

		// Token: 0x060000CA RID: 202
		[MethodImpl(4096)]
		private extern void set_rotation_Injected(ref Quaternion value);
	}
}
