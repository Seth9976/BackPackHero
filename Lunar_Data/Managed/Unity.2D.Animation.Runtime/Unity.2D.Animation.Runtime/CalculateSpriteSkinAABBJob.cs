using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace UnityEngine.U2D.Animation
{
	// Token: 0x0200002D RID: 45
	[BurstCompile]
	internal struct CalculateSpriteSkinAABBJob : IJobParallelFor
	{
		// Token: 0x060000F5 RID: 245 RVA: 0x00004F2C File Offset: 0x0000312C
		public unsafe void Execute(int i)
		{
			if (!this.isSpriteSkinValidForDeformArray[i])
			{
				return;
			}
			SpriteSkinData spriteSkinData = this.spriteSkinData[i];
			byte* unsafePtr = (byte*)this.vertices.GetUnsafePtr<byte>();
			NativeSlice<float3> nativeSlice = NativeSliceUnsafeUtility.ConvertExistingDataToNativeSlice<float3>((void*)(unsafePtr + spriteSkinData.deformVerticesStartPos), spriteSkinData.spriteVertexStreamSize, spriteSkinData.spriteVertexCount);
			this.bounds[i] = SpriteSkinUtility.CalculateSpriteSkinBounds(nativeSlice);
		}

		// Token: 0x0400008C RID: 140
		public NativeSlice<byte> vertices;

		// Token: 0x0400008D RID: 141
		[ReadOnly]
		public NativeArray<bool> isSpriteSkinValidForDeformArray;

		// Token: 0x0400008E RID: 142
		[ReadOnly]
		public NativeArray<SpriteSkinData> spriteSkinData;

		// Token: 0x0400008F RID: 143
		[WriteOnly]
		public NativeArray<Bounds> bounds;
	}
}
