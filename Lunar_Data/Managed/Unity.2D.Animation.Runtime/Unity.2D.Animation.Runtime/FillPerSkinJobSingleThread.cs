using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace UnityEngine.U2D.Animation
{
	// Token: 0x0200002E RID: 46
	[BurstCompile]
	internal struct FillPerSkinJobSingleThread : IJob
	{
		// Token: 0x060000F6 RID: 246 RVA: 0x00004F90 File Offset: 0x00003190
		public void Execute()
		{
			int num = 0;
			int length = this.spriteSkinDataArray.Length;
			for (int i = num; i < length; i++)
			{
				SpriteSkinData spriteSkinData = this.spriteSkinDataArray[i];
				spriteSkinData.deformVerticesStartPos = -1;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				if (this.isSpriteSkinValidForDeformArray[i])
				{
					spriteSkinData.deformVerticesStartPos = this.combinedSkinBatch.deformVerticesStartPos;
					num2 = spriteSkinData.spriteVertexCount * spriteSkinData.spriteVertexStreamSize;
					num3 = spriteSkinData.spriteVertexCount;
					num4 = spriteSkinData.bindPoses.Length;
				}
				this.combinedSkinBatch.verticesIndex.x = this.combinedSkinBatch.verticesIndex.y;
				this.combinedSkinBatch.verticesIndex.y = this.combinedSkinBatch.verticesIndex.x + num3;
				this.combinedSkinBatch.bindPosesIndex.x = this.combinedSkinBatch.bindPosesIndex.y;
				this.combinedSkinBatch.bindPosesIndex.y = this.combinedSkinBatch.bindPosesIndex.x + num4;
				this.spriteSkinDataArray[i] = spriteSkinData;
				this.perSkinJobDataArray[i] = this.combinedSkinBatch;
				this.combinedSkinBatch.deformVerticesStartPos = this.combinedSkinBatch.deformVerticesStartPos + num2;
			}
			this.combinedSkinBatchArray[0] = this.combinedSkinBatch;
		}

		// Token: 0x04000090 RID: 144
		public PerSkinJobData combinedSkinBatch;

		// Token: 0x04000091 RID: 145
		[ReadOnly]
		public NativeArray<bool> isSpriteSkinValidForDeformArray;

		// Token: 0x04000092 RID: 146
		public NativeArray<SpriteSkinData> spriteSkinDataArray;

		// Token: 0x04000093 RID: 147
		public NativeArray<PerSkinJobData> perSkinJobDataArray;

		// Token: 0x04000094 RID: 148
		public NativeArray<PerSkinJobData> combinedSkinBatchArray;
	}
}
