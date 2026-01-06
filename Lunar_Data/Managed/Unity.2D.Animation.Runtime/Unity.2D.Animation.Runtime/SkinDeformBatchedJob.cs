using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace UnityEngine.U2D.Animation
{
	// Token: 0x0200002C RID: 44
	[BurstCompile]
	internal struct SkinDeformBatchedJob : IJobParallelFor
	{
		// Token: 0x060000F4 RID: 244 RVA: 0x00004CA4 File Offset: 0x00002EA4
		public unsafe void Execute(int i)
		{
			int x = this.vertexLookupData[i].x;
			int y = this.vertexLookupData[i].y;
			PerSkinJobData perSkinJobData = this.perSkinJobData[x];
			SpriteSkinData spriteSkinData = this.spriteSkinData[x];
			float3 @float = spriteSkinData.vertices[y];
			float4 float2 = spriteSkinData.tangents[y];
			BoneWeight boneWeight = spriteSkinData.boneWeights[y];
			int num = boneWeight.boneIndex0 + perSkinJobData.bindPosesIndex.x;
			int num2 = boneWeight.boneIndex1 + perSkinJobData.bindPosesIndex.x;
			int num3 = boneWeight.boneIndex2 + perSkinJobData.bindPosesIndex.x;
			int num4 = boneWeight.boneIndex3 + perSkinJobData.bindPosesIndex.x;
			byte* unsafePtr = (byte*)this.vertices.GetUnsafePtr<byte>();
			byte* ptr = unsafePtr + spriteSkinData.deformVerticesStartPos;
			NativeSlice<float3> nativeSlice = NativeSliceUnsafeUtility.ConvertExistingDataToNativeSlice<float3>((void*)ptr, spriteSkinData.spriteVertexStreamSize, spriteSkinData.spriteVertexCount);
			if (spriteSkinData.hasTangents)
			{
				NativeSlice<float4> nativeSlice2 = NativeSliceUnsafeUtility.ConvertExistingDataToNativeSlice<float4>((void*)(ptr + spriteSkinData.tangentVertexOffset), spriteSkinData.spriteVertexStreamSize, spriteSkinData.spriteVertexCount);
				float4 float3 = new float4(float2.xyz, 0f);
				float3 = math.mul(this.finalBoneTransforms[num], float3) * boneWeight.weight0 + math.mul(this.finalBoneTransforms[num2], float3) * boneWeight.weight1 + math.mul(this.finalBoneTransforms[num3], float3) * boneWeight.weight2 + math.mul(this.finalBoneTransforms[num4], float3) * boneWeight.weight3;
				nativeSlice2[y] = new float4(math.normalize(float3.xyz), float2.w);
			}
			nativeSlice[y] = math.transform(this.finalBoneTransforms[num], @float) * boneWeight.weight0 + math.transform(this.finalBoneTransforms[num2], @float) * boneWeight.weight1 + math.transform(this.finalBoneTransforms[num3], @float) * boneWeight.weight2 + math.transform(this.finalBoneTransforms[num4], @float) * boneWeight.weight3;
		}

		// Token: 0x04000087 RID: 135
		public NativeSlice<byte> vertices;

		// Token: 0x04000088 RID: 136
		[ReadOnly]
		public NativeArray<float4x4> finalBoneTransforms;

		// Token: 0x04000089 RID: 137
		[ReadOnly]
		public NativeArray<PerSkinJobData> perSkinJobData;

		// Token: 0x0400008A RID: 138
		[ReadOnly]
		public NativeArray<SpriteSkinData> spriteSkinData;

		// Token: 0x0400008B RID: 139
		[ReadOnly]
		public NativeArray<int2> vertexLookupData;
	}
}
