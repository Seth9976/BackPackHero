using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x020004B6 RID: 1206
	internal class MonoChunkStream : WebReadStream
	{
		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x060026E7 RID: 9959 RVA: 0x00090691 File Offset: 0x0008E891
		protected WebHeaderCollection Headers { get; }

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x060026E8 RID: 9960 RVA: 0x00090699 File Offset: 0x0008E899
		protected MonoChunkParser Decoder { get; }

		// Token: 0x060026E9 RID: 9961 RVA: 0x000906A1 File Offset: 0x0008E8A1
		public MonoChunkStream(WebOperation operation, Stream innerStream, WebHeaderCollection headers)
			: base(operation, innerStream)
		{
			this.Headers = headers;
			this.Decoder = new MonoChunkParser(headers);
		}

		// Token: 0x060026EA RID: 9962 RVA: 0x000906C0 File Offset: 0x0008E8C0
		protected override async Task<int> ProcessReadAsync(byte[] buffer, int offset, int size, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			int num;
			if (this.Decoder.DataAvailable)
			{
				num = this.Decoder.Read(buffer, offset, size);
			}
			else
			{
				int num2 = 0;
				byte[] moreBytes = null;
				while (num2 == 0 && this.Decoder.WantMore)
				{
					int num3 = this.Decoder.ChunkLeft;
					if (num3 <= 0)
					{
						num3 = 1024;
					}
					else if (num3 > 16384)
					{
						num3 = 16384;
					}
					if (moreBytes == null || moreBytes.Length < num3)
					{
						moreBytes = new byte[num3];
					}
					num2 = await base.InnerStream.ReadAsync(moreBytes, 0, num3, cancellationToken).ConfigureAwait(false);
					if (num2 <= 0)
					{
						return num2;
					}
					this.Decoder.Write(moreBytes, 0, num2);
					num2 = this.Decoder.Read(buffer, offset, size);
				}
				num = num2;
			}
			return num;
		}

		// Token: 0x060026EB RID: 9963 RVA: 0x00090724 File Offset: 0x0008E924
		internal override async Task FinishReading(CancellationToken cancellationToken)
		{
			await base.FinishReading(cancellationToken).ConfigureAwait(false);
			cancellationToken.ThrowIfCancellationRequested();
			if (this.Decoder.DataAvailable)
			{
				MonoChunkStream.ThrowExpectingChunkTrailer();
			}
			while (this.Decoder.WantMore)
			{
				byte[] buffer = new byte[256];
				int num = await base.InnerStream.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false);
				if (num <= 0)
				{
					MonoChunkStream.ThrowExpectingChunkTrailer();
				}
				this.Decoder.Write(buffer, 0, num);
				if (this.Decoder.Read(buffer, 0, 1) != 0)
				{
					MonoChunkStream.ThrowExpectingChunkTrailer();
				}
				buffer = null;
			}
		}

		// Token: 0x060026EC RID: 9964 RVA: 0x0009076F File Offset: 0x0008E96F
		private static void ThrowExpectingChunkTrailer()
		{
			throw new WebException("Expecting chunk trailer.", null, WebExceptionStatus.ServerProtocolViolation, null);
		}
	}
}
