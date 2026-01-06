using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000158 RID: 344
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Runtime/Graphics/Mesh/MeshFilter.h")]
	public sealed class MeshFilter : Component
	{
		// Token: 0x06000EA0 RID: 3744 RVA: 0x00004557 File Offset: 0x00002757
		[RequiredByNativeCode]
		private void DontStripMeshFilter()
		{
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000EA1 RID: 3745
		// (set) Token: 0x06000EA2 RID: 3746
		public extern Mesh sharedMesh
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000EA3 RID: 3747
		// (set) Token: 0x06000EA4 RID: 3748
		public extern Mesh mesh
		{
			[NativeName("GetInstantiatedMeshFromScript")]
			[MethodImpl(4096)]
			get;
			[NativeName("SetInstantiatedMesh")]
			[MethodImpl(4096)]
			set;
		}
	}
}
