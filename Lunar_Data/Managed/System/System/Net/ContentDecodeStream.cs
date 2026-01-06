using System;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x02000481 RID: 1153
	internal class ContentDecodeStream : WebReadStream
	{
		// Token: 0x06002460 RID: 9312 RVA: 0x00086530 File Offset: 0x00084730
		public static ContentDecodeStream Create(WebOperation operation, Stream innerStream, ContentDecodeStream.Mode mode)
		{
			Stream stream;
			if (mode == ContentDecodeStream.Mode.GZip)
			{
				stream = new GZipStream(innerStream, CompressionMode.Decompress);
			}
			else
			{
				stream = new DeflateStream(innerStream, CompressionMode.Decompress);
			}
			return new ContentDecodeStream(operation, stream, innerStream);
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06002461 RID: 9313 RVA: 0x0008655A File Offset: 0x0008475A
		private Stream OriginalInnerStream { get; }

		// Token: 0x06002462 RID: 9314 RVA: 0x00086562 File Offset: 0x00084762
		private ContentDecodeStream(WebOperation operation, Stream decodeStream, Stream originalInnerStream)
			: base(operation, decodeStream)
		{
			this.OriginalInnerStream = originalInnerStream;
		}

		// Token: 0x06002463 RID: 9315 RVA: 0x00086573 File Offset: 0x00084773
		protected override Task<int> ProcessReadAsync(byte[] buffer, int offset, int size, CancellationToken cancellationToken)
		{
			return base.InnerStream.ReadAsync(buffer, offset, size, cancellationToken);
		}

		// Token: 0x06002464 RID: 9316 RVA: 0x00086588 File Offset: 0x00084788
		internal override Task FinishReading(CancellationToken cancellationToken)
		{
			WebReadStream webReadStream = this.OriginalInnerStream as WebReadStream;
			if (webReadStream != null)
			{
				return webReadStream.FinishReading(cancellationToken);
			}
			return Task.CompletedTask;
		}

		// Token: 0x02000482 RID: 1154
		internal enum Mode
		{
			// Token: 0x04001540 RID: 5440
			GZip,
			// Token: 0x04001541 RID: 5441
			Deflate
		}
	}
}
