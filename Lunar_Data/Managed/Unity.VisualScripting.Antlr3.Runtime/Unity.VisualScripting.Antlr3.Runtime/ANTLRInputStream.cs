using System;
using System.IO;
using System.Text;

namespace Unity.VisualScripting.Antlr3.Runtime
{
	// Token: 0x02000031 RID: 49
	public class ANTLRInputStream : ANTLRReaderStream
	{
		// Token: 0x0600020C RID: 524 RVA: 0x00006512 File Offset: 0x00005512
		protected ANTLRInputStream()
		{
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000651A File Offset: 0x0000551A
		public ANTLRInputStream(Stream istream)
			: this(istream, null)
		{
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00006524 File Offset: 0x00005524
		public ANTLRInputStream(Stream istream, Encoding encoding)
			: this(istream, ANTLRReaderStream.INITIAL_BUFFER_SIZE, encoding)
		{
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00006533 File Offset: 0x00005533
		public ANTLRInputStream(Stream istream, int size)
			: this(istream, size, null)
		{
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000653E File Offset: 0x0000553E
		public ANTLRInputStream(Stream istream, int size, Encoding encoding)
			: this(istream, size, ANTLRReaderStream.READ_BUFFER_SIZE, encoding)
		{
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00006550 File Offset: 0x00005550
		public ANTLRInputStream(Stream istream, int size, int readBufferSize, Encoding encoding)
		{
			StreamReader streamReader;
			if (encoding != null)
			{
				streamReader = new StreamReader(istream, encoding);
			}
			else
			{
				streamReader = new StreamReader(istream);
			}
			this.Load(streamReader, size, readBufferSize);
		}
	}
}
