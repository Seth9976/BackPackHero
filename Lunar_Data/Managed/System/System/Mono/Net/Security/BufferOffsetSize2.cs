using System;

namespace Mono.Net.Security
{
	// Token: 0x02000088 RID: 136
	internal class BufferOffsetSize2 : BufferOffsetSize
	{
		// Token: 0x0600021A RID: 538 RVA: 0x00006275 File Offset: 0x00004475
		public BufferOffsetSize2(int size)
			: base(new byte[size], 0, 0)
		{
			this.InitialSize = size;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000628C File Offset: 0x0000448C
		public void Reset()
		{
			this.Offset = (this.Size = 0);
			this.TotalBytes = 0;
			this.Buffer = new byte[this.InitialSize];
			this.Complete = false;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x000062C8 File Offset: 0x000044C8
		public void MakeRoom(int size)
		{
			if (base.Remaining >= size)
			{
				return;
			}
			int num = size - base.Remaining;
			if (this.Offset == 0 && this.Size == 0)
			{
				this.Buffer = new byte[size];
				return;
			}
			byte[] array = new byte[this.Buffer.Length + num];
			this.Buffer.CopyTo(array, 0);
			this.Buffer = array;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00006329 File Offset: 0x00004529
		public void AppendData(byte[] buffer, int offset, int size)
		{
			this.MakeRoom(size);
			global::System.Buffer.BlockCopy(buffer, offset, this.Buffer, base.EndOffset, size);
			this.Size += size;
		}

		// Token: 0x040001FF RID: 511
		public readonly int InitialSize;
	}
}
