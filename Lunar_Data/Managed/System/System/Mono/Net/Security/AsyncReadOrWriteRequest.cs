using System;

namespace Mono.Net.Security
{
	// Token: 0x02000090 RID: 144
	internal abstract class AsyncReadOrWriteRequest : AsyncProtocolRequest
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00006A4B File Offset: 0x00004C4B
		protected BufferOffsetSize UserBuffer { get; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00006A53 File Offset: 0x00004C53
		// (set) Token: 0x0600023B RID: 571 RVA: 0x00006A5B File Offset: 0x00004C5B
		protected int CurrentSize { get; set; }

		// Token: 0x0600023C RID: 572 RVA: 0x00006A64 File Offset: 0x00004C64
		public AsyncReadOrWriteRequest(MobileAuthenticatedStream parent, bool sync, byte[] buffer, int offset, int size)
			: base(parent, sync)
		{
			this.UserBuffer = new BufferOffsetSize(buffer, offset, size);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00006A7E File Offset: 0x00004C7E
		public override string ToString()
		{
			return string.Format("[{0}: {1}]", base.Name, this.UserBuffer);
		}
	}
}
