using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x020003E7 RID: 999
	[UsedByNativeCode]
	[NativeHeader("Runtime/Camera/BatchRendererGroup.h")]
	public struct BatchCullingContext
	{
		// Token: 0x060021CB RID: 8651 RVA: 0x00037BF8 File Offset: 0x00035DF8
		[Obsolete("For internal BatchRendererGroup use only")]
		public BatchCullingContext(NativeArray<Plane> inCullingPlanes, NativeArray<BatchVisibility> inOutBatchVisibility, NativeArray<int> outVisibleIndices, LODParameters inLodParameters)
		{
			this.cullingPlanes = inCullingPlanes;
			this.batchVisibility = inOutBatchVisibility;
			this.visibleIndices = outVisibleIndices;
			this.visibleIndicesY = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(null, 0, Allocator.Invalid);
			this.lodParameters = inLodParameters;
			this.cullingMatrix = Matrix4x4.identity;
			this.nearPlane = 0f;
		}

		// Token: 0x060021CC RID: 8652 RVA: 0x00037C48 File Offset: 0x00035E48
		[Obsolete("For internal BatchRendererGroup use only")]
		public BatchCullingContext(NativeArray<Plane> inCullingPlanes, NativeArray<BatchVisibility> inOutBatchVisibility, NativeArray<int> outVisibleIndices, LODParameters inLodParameters, Matrix4x4 inCullingMatrix, float inNearPlane)
		{
			this.cullingPlanes = inCullingPlanes;
			this.batchVisibility = inOutBatchVisibility;
			this.visibleIndices = outVisibleIndices;
			this.visibleIndicesY = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(null, 0, Allocator.Invalid);
			this.lodParameters = inLodParameters;
			this.cullingMatrix = inCullingMatrix;
			this.nearPlane = inNearPlane;
		}

		// Token: 0x060021CD RID: 8653 RVA: 0x00037C87 File Offset: 0x00035E87
		internal BatchCullingContext(NativeArray<Plane> inCullingPlanes, NativeArray<BatchVisibility> inOutBatchVisibility, NativeArray<int> outVisibleIndices, NativeArray<int> outVisibleIndicesY, LODParameters inLodParameters, Matrix4x4 inCullingMatrix, float inNearPlane)
		{
			this.cullingPlanes = inCullingPlanes;
			this.batchVisibility = inOutBatchVisibility;
			this.visibleIndices = outVisibleIndices;
			this.visibleIndicesY = outVisibleIndicesY;
			this.lodParameters = inLodParameters;
			this.cullingMatrix = inCullingMatrix;
			this.nearPlane = inNearPlane;
		}

		// Token: 0x04000C4D RID: 3149
		public readonly NativeArray<Plane> cullingPlanes;

		// Token: 0x04000C4E RID: 3150
		public NativeArray<BatchVisibility> batchVisibility;

		// Token: 0x04000C4F RID: 3151
		public NativeArray<int> visibleIndices;

		// Token: 0x04000C50 RID: 3152
		public NativeArray<int> visibleIndicesY;

		// Token: 0x04000C51 RID: 3153
		public readonly LODParameters lodParameters;

		// Token: 0x04000C52 RID: 3154
		public readonly Matrix4x4 cullingMatrix;

		// Token: 0x04000C53 RID: 3155
		public readonly float nearPlane;
	}
}
