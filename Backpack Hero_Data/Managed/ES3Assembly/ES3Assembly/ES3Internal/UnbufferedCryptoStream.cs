using System;
using System.IO;

namespace ES3Internal
{
	// Token: 0x020000C6 RID: 198
	public class UnbufferedCryptoStream : MemoryStream
	{
		// Token: 0x060003E2 RID: 994 RVA: 0x0001EBE4 File Offset: 0x0001CDE4
		public UnbufferedCryptoStream(Stream stream, bool isReadStream, string password, int bufferSize, EncryptionAlgorithm alg)
		{
			this.stream = stream;
			this.isReadStream = isReadStream;
			this.password = password;
			this.bufferSize = bufferSize;
			this.alg = alg;
			if (isReadStream)
			{
				alg.Decrypt(stream, this, password, bufferSize);
			}
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0001EC20 File Offset: 0x0001CE20
		protected override void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			this.disposed = true;
			if (!this.isReadStream)
			{
				this.alg.Encrypt(this, this.stream, this.password, this.bufferSize);
			}
			this.stream.Dispose();
			base.Dispose(disposing);
		}

		// Token: 0x040000FF RID: 255
		private readonly Stream stream;

		// Token: 0x04000100 RID: 256
		private readonly bool isReadStream;

		// Token: 0x04000101 RID: 257
		private string password;

		// Token: 0x04000102 RID: 258
		private int bufferSize;

		// Token: 0x04000103 RID: 259
		private EncryptionAlgorithm alg;

		// Token: 0x04000104 RID: 260
		private bool disposed;
	}
}
