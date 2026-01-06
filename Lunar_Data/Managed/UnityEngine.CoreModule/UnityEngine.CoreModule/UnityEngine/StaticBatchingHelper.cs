using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200019D RID: 413
	[NativeHeader("Runtime/Graphics/Mesh/MeshCombiner.h")]
	[NativeHeader("Runtime/Graphics/Mesh/MeshScriptBindings.h")]
	internal struct StaticBatchingHelper
	{
		// Token: 0x0600108B RID: 4235
		[FreeFunction("MeshScripting::CombineMeshVerticesForStaticBatching")]
		[MethodImpl(4096)]
		internal static extern Mesh InternalCombineVertices(MeshSubsetCombineUtility.MeshInstance[] meshes, string meshName);

		// Token: 0x0600108C RID: 4236
		[FreeFunction("MeshScripting::CombineMeshIndicesForStaticBatching")]
		[MethodImpl(4096)]
		internal static extern void InternalCombineIndices(MeshSubsetCombineUtility.SubMeshInstance[] submeshes, Mesh combinedMesh);

		// Token: 0x0600108D RID: 4237
		[FreeFunction("IsMeshBatchable")]
		[MethodImpl(4096)]
		internal static extern bool IsMeshBatchable(Mesh mesh);
	}
}
