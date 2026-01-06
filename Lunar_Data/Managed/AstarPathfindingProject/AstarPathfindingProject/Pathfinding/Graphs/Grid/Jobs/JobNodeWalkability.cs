using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding.Graphs.Grid.Jobs
{
	// Token: 0x02000222 RID: 546
	[BurstCompile(FloatMode = FloatMode.Fast)]
	public struct JobNodeWalkability : IJob
	{
		// Token: 0x06000CF0 RID: 3312 RVA: 0x00051DA0 File Offset: 0x0004FFA0
		public void Execute()
		{
			float num = math.cos(math.radians(this.maxSlope));
			float4 @float = new float4(this.up.x, this.up.y, this.up.z, 0f);
			float3 xyz = @float.xyz;
			for (int i = 0; i < this.nodeNormals.Length; i++)
			{
				bool flag = math.any(this.nodeNormals[i]);
				bool flag2 = flag;
				if (!flag && !this.unwalkableWhenNoGround && i < this.layerStride)
				{
					flag2 = true;
					this.nodeNormals[i] = @float;
				}
				if (flag2 && this.useRaycastNormal && flag && math.dot(this.nodeNormals[i], @float) < num)
				{
					flag2 = false;
				}
				if (flag2 && i + this.layerStride < this.nodeNormals.Length && math.any(this.nodeNormals[i + this.layerStride]))
				{
					flag2 = math.dot(xyz, this.nodePositions[i + this.layerStride] - this.nodePositions[i]) >= this.characterHeight;
				}
				this.nodeWalkable[i] = flag2;
			}
		}

		// Token: 0x040009FC RID: 2556
		public bool useRaycastNormal;

		// Token: 0x040009FD RID: 2557
		public float maxSlope;

		// Token: 0x040009FE RID: 2558
		public Vector3 up;

		// Token: 0x040009FF RID: 2559
		public bool unwalkableWhenNoGround;

		// Token: 0x04000A00 RID: 2560
		public float characterHeight;

		// Token: 0x04000A01 RID: 2561
		public int layerStride;

		// Token: 0x04000A02 RID: 2562
		[ReadOnly]
		public NativeArray<float3> nodePositions;

		// Token: 0x04000A03 RID: 2563
		public NativeArray<float4> nodeNormals;

		// Token: 0x04000A04 RID: 2564
		[WriteOnly]
		public NativeArray<bool> nodeWalkable;
	}
}
