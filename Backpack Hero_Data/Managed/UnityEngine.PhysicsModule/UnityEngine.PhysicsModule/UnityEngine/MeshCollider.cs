using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200002A RID: 42
	[NativeHeader("Runtime/Graphics/Mesh/Mesh.h")]
	[NativeHeader("Modules/Physics/MeshCollider.h")]
	[RequiredByNativeCode]
	public class MeshCollider : Collider
	{
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002EA RID: 746
		// (set) Token: 0x060002EB RID: 747
		public extern Mesh sharedMesh
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002EC RID: 748
		// (set) Token: 0x060002ED RID: 749
		public extern bool convex
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002EE RID: 750
		// (set) Token: 0x060002EF RID: 751
		public extern MeshColliderCookingOptions cookingOptions
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x00005308 File Offset: 0x00003508
		// (set) Token: 0x060002F1 RID: 753 RVA: 0x00002193 File Offset: 0x00000393
		[Obsolete("Configuring smooth sphere collisions is no longer needed.", true)]
		[EditorBrowsable(1)]
		public bool smoothSphereCollisions
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0000531C File Offset: 0x0000351C
		// (set) Token: 0x060002F3 RID: 755 RVA: 0x00002193 File Offset: 0x00000393
		[Obsolete("MeshCollider.skinWidth is no longer used.")]
		public float skinWidth
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x00005334 File Offset: 0x00003534
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x00002193 File Offset: 0x00000393
		[Obsolete("MeshCollider.inflateMesh is no longer supported. The new cooking algorithm doesn't need inflation to be used.")]
		public bool inflateMesh
		{
			get
			{
				return false;
			}
			set
			{
			}
		}
	}
}
