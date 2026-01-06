using System;
using System.IO;
using System.Threading.Tasks;

namespace System.Net.Http
{
	// Token: 0x02000032 RID: 50
	public sealed class ReadOnlyMemoryContent : HttpContent
	{
		// Token: 0x0600018D RID: 397 RVA: 0x00006995 File Offset: 0x00004B95
		public ReadOnlyMemoryContent(ReadOnlyMemory<byte> content)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000027DD File Offset: 0x000009DD
		protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000027DD File Offset: 0x000009DD
		protected internal override bool TryComputeLength(out long length)
		{
			throw new PlatformNotSupportedException();
		}
	}
}
