using System;
using Pathfinding.Graphs.Grid;
using Unity.Burst;
using Unity.Burst.CompilerServices;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding.Jobs
{
	// Token: 0x02000173 RID: 371
	[BurstCompile(FloatMode = FloatMode.Fast)]
	public struct JobCopyHits : IJob, GridIterationUtilities.ISliceAction
	{
		// Token: 0x06000A80 RID: 2688 RVA: 0x0003BBCF File Offset: 0x00039DCF
		public void Execute()
		{
			this.slice.AssertMatchesOuter<Vector3>(this.points);
			this.slice.AssertMatchesOuter<float4>(this.normals);
			GridIterationUtilities.ForEachCellIn3DSlice<JobCopyHits>(this.slice, ref this);
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0003BC00 File Offset: 0x00039E00
		public void Execute(uint outerIdx, uint innerIdx)
		{
			Aliasing.ExpectNotAliased<NativeArray<Vector3>, NativeArray<float4>>(in this.points, in this.normals);
			Vector3 normal = this.hits[(int)innerIdx].normal;
			float4 @float = new float4(normal.x, normal.y, normal.z, 0f);
			this.normals[(int)outerIdx] = math.normalizesafe(@float, default(float4));
			if (math.lengthsq(@float) > 1.1754944E-38f)
			{
				this.points[(int)outerIdx] = this.hits[(int)innerIdx].point;
			}
		}

		// Token: 0x04000721 RID: 1825
		[ReadOnly]
		public NativeArray<RaycastHit> hits;

		// Token: 0x04000722 RID: 1826
		[WriteOnly]
		public NativeArray<Vector3> points;

		// Token: 0x04000723 RID: 1827
		[WriteOnly]
		public NativeArray<float4> normals;

		// Token: 0x04000724 RID: 1828
		public Slice3D slice;
	}
}
