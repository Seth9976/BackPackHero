using System;

namespace System.Net
{
	// Token: 0x02000431 RID: 1073
	internal class BufferOffsetSize
	{
		// Token: 0x0600223B RID: 8763 RVA: 0x0007DBE4 File Offset: 0x0007BDE4
		internal BufferOffsetSize(byte[] buffer, int offset, int size, bool copyBuffer)
		{
			if (copyBuffer)
			{
				byte[] array = new byte[size];
				global::System.Buffer.BlockCopy(buffer, offset, array, 0, size);
				offset = 0;
				buffer = array;
			}
			this.Buffer = buffer;
			this.Offset = offset;
			this.Size = size;
		}

		// Token: 0x0600223C RID: 8764 RVA: 0x0007DC27 File Offset: 0x0007BE27
		internal BufferOffsetSize(byte[] buffer, bool copyBuffer)
			: this(buffer, 0, buffer.Length, copyBuffer)
		{
		}

		// Token: 0x040013AA RID: 5034
		internal byte[] Buffer;

		// Token: 0x040013AB RID: 5035
		internal int Offset;

		// Token: 0x040013AC RID: 5036
		internal int Size;
	}
}
