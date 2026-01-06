using System;
using System.IO;

namespace Unity.VisualScripting.Antlr3.Runtime
{
	// Token: 0x02000030 RID: 48
	public class ANTLRReaderStream : ANTLRStringStream
	{
		// Token: 0x06000206 RID: 518 RVA: 0x00006419 File Offset: 0x00005419
		protected ANTLRReaderStream()
		{
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00006421 File Offset: 0x00005421
		public ANTLRReaderStream(TextReader reader)
			: this(reader, ANTLRReaderStream.INITIAL_BUFFER_SIZE, ANTLRReaderStream.READ_BUFFER_SIZE)
		{
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00006434 File Offset: 0x00005434
		public ANTLRReaderStream(TextReader reader, int size)
			: this(reader, size, ANTLRReaderStream.READ_BUFFER_SIZE)
		{
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00006443 File Offset: 0x00005443
		public ANTLRReaderStream(TextReader reader, int size, int readChunkSize)
		{
			this.Load(reader, size, readChunkSize);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00006454 File Offset: 0x00005454
		public virtual void Load(TextReader reader, int size, int readChunkSize)
		{
			if (reader == null)
			{
				return;
			}
			if (size <= 0)
			{
				size = ANTLRReaderStream.INITIAL_BUFFER_SIZE;
			}
			if (readChunkSize <= 0)
			{
				readChunkSize = ANTLRReaderStream.READ_BUFFER_SIZE;
			}
			try
			{
				this.data = new char[size];
				int num = 0;
				int num2;
				do
				{
					if (num + readChunkSize > this.data.Length)
					{
						char[] array = new char[this.data.Length * 2];
						Array.Copy(this.data, 0, array, 0, this.data.Length);
						this.data = array;
					}
					num2 = reader.Read(this.data, num, readChunkSize);
					num += num2;
				}
				while (num2 != 0);
				this.n = num;
			}
			finally
			{
				reader.Close();
			}
		}

		// Token: 0x04000080 RID: 128
		public static readonly int READ_BUFFER_SIZE = 1024;

		// Token: 0x04000081 RID: 129
		public static readonly int INITIAL_BUFFER_SIZE = 1024;
	}
}
