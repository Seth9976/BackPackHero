using System;

namespace System.Net
{
	// Token: 0x02000449 RID: 1097
	internal class ScatterGatherBuffers
	{
		// Token: 0x060022AC RID: 8876 RVA: 0x0007EFA0 File Offset: 0x0007D1A0
		internal ScatterGatherBuffers()
		{
		}

		// Token: 0x060022AD RID: 8877 RVA: 0x0007EFB3 File Offset: 0x0007D1B3
		internal ScatterGatherBuffers(long totalSize)
		{
			if (totalSize > 0L)
			{
				this.currentChunk = this.AllocateMemoryChunk((totalSize > 2147483647L) ? int.MaxValue : ((int)totalSize));
			}
		}

		// Token: 0x060022AE RID: 8878 RVA: 0x0007EFEC File Offset: 0x0007D1EC
		internal BufferOffsetSize[] GetBuffers()
		{
			if (this.Empty)
			{
				return null;
			}
			BufferOffsetSize[] array = new BufferOffsetSize[this.chunkCount];
			int num = 0;
			for (ScatterGatherBuffers.MemoryChunk next = this.headChunk; next != null; next = next.Next)
			{
				array[num] = new BufferOffsetSize(next.Buffer, 0, next.FreeOffset, false);
				num++;
			}
			return array;
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x060022AF RID: 8879 RVA: 0x0007F03F File Offset: 0x0007D23F
		private bool Empty
		{
			get
			{
				return this.headChunk == null || this.chunkCount == 0;
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x060022B0 RID: 8880 RVA: 0x0007F054 File Offset: 0x0007D254
		internal int Length
		{
			get
			{
				return this.totalLength;
			}
		}

		// Token: 0x060022B1 RID: 8881 RVA: 0x0007F05C File Offset: 0x0007D25C
		internal void Write(byte[] buffer, int offset, int count)
		{
			while (count > 0)
			{
				int num = (this.Empty ? 0 : (this.currentChunk.Buffer.Length - this.currentChunk.FreeOffset));
				if (num == 0)
				{
					ScatterGatherBuffers.MemoryChunk memoryChunk = this.AllocateMemoryChunk(count);
					if (this.currentChunk != null)
					{
						this.currentChunk.Next = memoryChunk;
					}
					this.currentChunk = memoryChunk;
				}
				int num2 = ((count < num) ? count : num);
				Buffer.BlockCopy(buffer, offset, this.currentChunk.Buffer, this.currentChunk.FreeOffset, num2);
				offset += num2;
				count -= num2;
				this.totalLength += num2;
				this.currentChunk.FreeOffset += num2;
			}
		}

		// Token: 0x060022B2 RID: 8882 RVA: 0x0007F114 File Offset: 0x0007D314
		private ScatterGatherBuffers.MemoryChunk AllocateMemoryChunk(int newSize)
		{
			if (newSize > this.nextChunkLength)
			{
				this.nextChunkLength = newSize;
			}
			ScatterGatherBuffers.MemoryChunk memoryChunk = new ScatterGatherBuffers.MemoryChunk(this.nextChunkLength);
			if (this.Empty)
			{
				this.headChunk = memoryChunk;
			}
			this.nextChunkLength *= 2;
			this.chunkCount++;
			return memoryChunk;
		}

		// Token: 0x0400142A RID: 5162
		private ScatterGatherBuffers.MemoryChunk headChunk;

		// Token: 0x0400142B RID: 5163
		private ScatterGatherBuffers.MemoryChunk currentChunk;

		// Token: 0x0400142C RID: 5164
		private int nextChunkLength = 1024;

		// Token: 0x0400142D RID: 5165
		private int totalLength;

		// Token: 0x0400142E RID: 5166
		private int chunkCount;

		// Token: 0x0200044A RID: 1098
		private class MemoryChunk
		{
			// Token: 0x060022B3 RID: 8883 RVA: 0x0007F169 File Offset: 0x0007D369
			internal MemoryChunk(int bufferSize)
			{
				this.Buffer = new byte[bufferSize];
			}

			// Token: 0x0400142F RID: 5167
			internal byte[] Buffer;

			// Token: 0x04001430 RID: 5168
			internal int FreeOffset;

			// Token: 0x04001431 RID: 5169
			internal ScatterGatherBuffers.MemoryChunk Next;
		}
	}
}
