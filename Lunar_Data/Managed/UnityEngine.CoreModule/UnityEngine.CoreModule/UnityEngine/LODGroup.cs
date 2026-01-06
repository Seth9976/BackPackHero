using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000199 RID: 409
	[NativeHeader("Runtime/Graphics/LOD/LODGroup.h")]
	[NativeHeader("Runtime/Graphics/LOD/LODUtility.h")]
	[NativeHeader("Runtime/Graphics/LOD/LODGroupManager.h")]
	[StaticAccessor("GetLODGroupManager()", StaticAccessorType.Dot)]
	public class LODGroup : Component
	{
		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x00012E18 File Offset: 0x00011018
		// (set) Token: 0x06000EF6 RID: 3830 RVA: 0x00012E2E File Offset: 0x0001102E
		public Vector3 localReferencePoint
		{
			get
			{
				Vector3 vector;
				this.get_localReferencePoint_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_localReferencePoint_Injected(ref value);
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000EF7 RID: 3831
		// (set) Token: 0x06000EF8 RID: 3832
		public extern float size
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000EF9 RID: 3833
		public extern int lodCount
		{
			[NativeMethod("GetLODCount")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000EFA RID: 3834
		// (set) Token: 0x06000EFB RID: 3835
		public extern LODFadeMode fadeMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000EFC RID: 3836
		// (set) Token: 0x06000EFD RID: 3837
		public extern bool animateCrossFading
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000EFE RID: 3838
		// (set) Token: 0x06000EFF RID: 3839
		public extern bool enabled
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000F00 RID: 3840
		[FreeFunction("UpdateLODGroupBoundingBox", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void RecalculateBounds();

		// Token: 0x06000F01 RID: 3841
		[FreeFunction("GetLODs_Binding", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern LOD[] GetLODs();

		// Token: 0x06000F02 RID: 3842 RVA: 0x00012E38 File Offset: 0x00011038
		[Obsolete("Use SetLODs instead.")]
		public void SetLODS(LOD[] lods)
		{
			this.SetLODs(lods);
		}

		// Token: 0x06000F03 RID: 3843
		[FreeFunction("SetLODs_Binding", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetLODs(LOD[] lods);

		// Token: 0x06000F04 RID: 3844
		[FreeFunction("ForceLODLevel", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void ForceLOD(int index);

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000F05 RID: 3845
		// (set) Token: 0x06000F06 RID: 3846
		[StaticAccessor("GetLODGroupManager()")]
		public static extern float crossFadeAnimationDuration
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000F07 RID: 3847 RVA: 0x00012E44 File Offset: 0x00011044
		internal Vector3 worldReferencePoint
		{
			get
			{
				Vector3 vector;
				this.get_worldReferencePoint_Injected(out vector);
				return vector;
			}
		}

		// Token: 0x06000F09 RID: 3849
		[MethodImpl(4096)]
		private extern void get_localReferencePoint_Injected(out Vector3 ret);

		// Token: 0x06000F0A RID: 3850
		[MethodImpl(4096)]
		private extern void set_localReferencePoint_Injected(ref Vector3 value);

		// Token: 0x06000F0B RID: 3851
		[MethodImpl(4096)]
		private extern void get_worldReferencePoint_Injected(out Vector3 ret);
	}
}
