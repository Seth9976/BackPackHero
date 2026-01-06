using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace UnityEngine.U2D.Animation
{
	// Token: 0x0200002B RID: 43
	[BurstCompile]
	internal struct BoneDeformBatchedJob : IJobParallelFor
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x00004BDC File Offset: 0x00002DDC
		public void Execute(int i)
		{
			int x = this.boneLookupData[i].x;
			int y = this.boneLookupData[i].y;
			SpriteSkinData spriteSkinData = this.spriteSkinData[x];
			int num = spriteSkinData.boneTransformId[y];
			int transformIndex = this.boneTransformIndex[num].transformIndex;
			if (transformIndex < 0)
			{
				return;
			}
			float4x4 float4x = this.boneTransform[transformIndex];
			Matrix4x4 matrix4x = spriteSkinData.bindPoses[y];
			int transformIndex2 = this.rootTransformIndex[spriteSkinData.transformId].transformIndex;
			this.finalBoneTransforms[i] = math.mul(this.rootTransform[transformIndex2], math.mul(float4x, matrix4x));
		}

		// Token: 0x04000080 RID: 128
		[ReadOnly]
		public NativeArray<float4x4> boneTransform;

		// Token: 0x04000081 RID: 129
		[ReadOnly]
		public NativeArray<float4x4> rootTransform;

		// Token: 0x04000082 RID: 130
		[ReadOnly]
		public NativeArray<int2> boneLookupData;

		// Token: 0x04000083 RID: 131
		[ReadOnly]
		public NativeArray<SpriteSkinData> spriteSkinData;

		// Token: 0x04000084 RID: 132
		[ReadOnly]
		public NativeHashMap<int, TransformAccessJob.TransformData> rootTransformIndex;

		// Token: 0x04000085 RID: 133
		[ReadOnly]
		public NativeHashMap<int, TransformAccessJob.TransformData> boneTransformIndex;

		// Token: 0x04000086 RID: 134
		[WriteOnly]
		public NativeArray<float4x4> finalBoneTransforms;
	}
}
