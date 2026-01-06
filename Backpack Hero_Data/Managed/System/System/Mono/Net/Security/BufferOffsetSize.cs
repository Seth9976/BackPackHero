using System;

namespace Mono.Net.Security
{
	// Token: 0x02000087 RID: 135
	internal class BufferOffsetSize
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000216 RID: 534 RVA: 0x000061C7 File Offset: 0x000043C7
		public int EndOffset
		{
			get
			{
				return this.Offset + this.Size;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000217 RID: 535 RVA: 0x000061D6 File Offset: 0x000043D6
		public int Remaining
		{
			get
			{
				return this.Buffer.Length - this.Offset - this.Size;
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x000061F0 File Offset: 0x000043F0
		public BufferOffsetSize(byte[] buffer, int offset, int size)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || offset + size > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			this.Buffer = buffer;
			this.Offset = offset;
			this.Size = size;
			this.Complete = false;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00006253 File Offset: 0x00004453
		public override string ToString()
		{
			return string.Format("[BufferOffsetSize: {0} {1}]", this.Offset, this.Size);
		}

		// Token: 0x040001FA RID: 506
		public byte[] Buffer;

		// Token: 0x040001FB RID: 507
		public int Offset;

		// Token: 0x040001FC RID: 508
		public int Size;

		// Token: 0x040001FD RID: 509
		public int TotalBytes;

		// Token: 0x040001FE RID: 510
		public bool Complete;
	}
}
