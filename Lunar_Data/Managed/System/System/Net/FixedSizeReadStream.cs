using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x02000494 RID: 1172
	internal class FixedSizeReadStream : WebReadStream
	{
		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06002500 RID: 9472 RVA: 0x00088DBF File Offset: 0x00086FBF
		public long ContentLength { get; }

		// Token: 0x06002501 RID: 9473 RVA: 0x00088DC7 File Offset: 0x00086FC7
		public FixedSizeReadStream(WebOperation operation, Stream innerStream, long contentLength)
			: base(operation, innerStream)
		{
			this.ContentLength = contentLength;
		}

		// Token: 0x06002502 RID: 9474 RVA: 0x00088DD8 File Offset: 0x00086FD8
		protected override async Task<int> ProcessReadAsync(byte[] buffer, int offset, int size, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			long num = this.ContentLength - this.position;
			int num2;
			if (num == 0L)
			{
				num2 = 0;
			}
			else
			{
				int num3 = (int)Math.Min(num, (long)size);
				int num4 = await base.InnerStream.ReadAsync(buffer, offset, num3, cancellationToken).ConfigureAwait(false);
				if (num4 <= 0)
				{
					num2 = num4;
				}
				else
				{
					this.position += (long)num4;
					num2 = num4;
				}
			}
			return num2;
		}

		// Token: 0x04001570 RID: 5488
		private long position;
	}
}
