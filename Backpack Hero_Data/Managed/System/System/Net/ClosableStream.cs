using System;
using System.IO;
using System.Threading;

namespace System.Net
{
	// Token: 0x02000386 RID: 902
	internal class ClosableStream : DelegatedStream
	{
		// Token: 0x06001D97 RID: 7575 RVA: 0x0006C2A7 File Offset: 0x0006A4A7
		internal ClosableStream(Stream stream, EventHandler onClose)
			: base(stream)
		{
			this._onClose = onClose;
		}

		// Token: 0x06001D98 RID: 7576 RVA: 0x0006C2B7 File Offset: 0x0006A4B7
		public override void Close()
		{
			if (Interlocked.Increment(ref this._closed) == 1)
			{
				EventHandler onClose = this._onClose;
				if (onClose == null)
				{
					return;
				}
				onClose(this, new EventArgs());
			}
		}

		// Token: 0x04000F63 RID: 3939
		private readonly EventHandler _onClose;

		// Token: 0x04000F64 RID: 3940
		private int _closed;
	}
}
