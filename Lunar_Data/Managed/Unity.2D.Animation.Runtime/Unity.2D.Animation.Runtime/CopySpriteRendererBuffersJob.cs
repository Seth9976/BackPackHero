using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace UnityEngine.U2D.Animation
{
	// Token: 0x0200002F RID: 47
	[BurstCompile]
	internal struct CopySpriteRendererBuffersJob : IJobParallelFor
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x000050E4 File Offset: 0x000032E4
		public void Execute(int i)
		{
			SpriteSkinData spriteSkinData = this.spriteSkinData[i];
			IntPtr intPtr = default(IntPtr);
			int num = 0;
			if (this.isSpriteSkinValidForDeformArray[i])
			{
				intPtr = this.ptrVertices + spriteSkinData.deformVerticesStartPos;
				num = spriteSkinData.spriteVertexCount * spriteSkinData.spriteVertexStreamSize;
			}
			this.buffers[i] = intPtr;
			this.bufferSizes[i] = num;
		}

		// Token: 0x04000095 RID: 149
		[ReadOnly]
		public NativeArray<bool> isSpriteSkinValidForDeformArray;

		// Token: 0x04000096 RID: 150
		[ReadOnly]
		public NativeArray<SpriteSkinData> spriteSkinData;

		// Token: 0x04000097 RID: 151
		[ReadOnly]
		[NativeDisableUnsafePtrRestriction]
		public IntPtr ptrVertices;

		// Token: 0x04000098 RID: 152
		[WriteOnly]
		public NativeArray<IntPtr> buffers;

		// Token: 0x04000099 RID: 153
		[WriteOnly]
		public NativeArray<int> bufferSizes;
	}
}
