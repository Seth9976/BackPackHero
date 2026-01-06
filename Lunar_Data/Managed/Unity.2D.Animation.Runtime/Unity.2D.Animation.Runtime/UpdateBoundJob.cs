using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace UnityEngine.U2D.Animation
{
	// Token: 0x0200003E RID: 62
	[BurstCompile]
	internal struct UpdateBoundJob : IJobParallelFor
	{
		// Token: 0x0600014D RID: 333 RVA: 0x00007354 File Offset: 0x00005554
		public void Execute(int i)
		{
			Bounds bounds = this.spriteSkinBound[i];
			int transformIndex = this.rootTransformIndex[this.rootTransformId[i]].transformIndex;
			int transformIndex2 = this.boneTransformIndex[this.rootBoneTransformId[i]].transformIndex;
			if (transformIndex < 0 || transformIndex2 < 0)
			{
				return;
			}
			float4x4 float4x = this.rootTransform[transformIndex];
			float4x4 float4x2 = this.boneTransform[transformIndex2];
			float4x4 float4x3 = math.mul(float4x, float4x2);
			float4 @float = new float4(bounds.center, 1f);
			float4 float2 = new float4(bounds.extents, 0f);
			float4 float3 = math.mul(float4x3, @float + new float4(-float2.x, -float2.y, float2.z, float2.w));
			float4 float4 = math.mul(float4x3, @float + new float4(-float2.x, float2.y, float2.z, float2.w));
			float4 float5 = math.mul(float4x3, @float + float2);
			float4 float6 = math.mul(float4x3, @float + new float4(float2.x, -float2.y, float2.z, float2.w));
			float4 float7 = math.min(float3, math.min(float4, math.min(float5, float6)));
			float2 = (math.max(float3, math.max(float4, math.max(float5, float6))) - float7) * 0.5f;
			@float = float7 + float2;
			this.bounds[i] = new Bounds
			{
				center = new Vector3(@float.x, @float.y, @float.z),
				extents = new Vector3(float2.x, float2.y, float2.z)
			};
		}

		// Token: 0x040000D3 RID: 211
		[ReadOnly]
		public NativeArray<int> rootTransformId;

		// Token: 0x040000D4 RID: 212
		[ReadOnly]
		public NativeArray<int> rootBoneTransformId;

		// Token: 0x040000D5 RID: 213
		[ReadOnly]
		public NativeArray<float4x4> rootTransform;

		// Token: 0x040000D6 RID: 214
		[ReadOnly]
		public NativeArray<float4x4> boneTransform;

		// Token: 0x040000D7 RID: 215
		[ReadOnly]
		public NativeHashMap<int, TransformAccessJob.TransformData> rootTransformIndex;

		// Token: 0x040000D8 RID: 216
		[ReadOnly]
		public NativeHashMap<int, TransformAccessJob.TransformData> boneTransformIndex;

		// Token: 0x040000D9 RID: 217
		[ReadOnly]
		public NativeArray<Bounds> spriteSkinBound;

		// Token: 0x040000DA RID: 218
		public NativeArray<Bounds> bounds;
	}
}
