using System;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x0200023F RID: 575
	internal abstract class SNIHandle
	{
		// Token: 0x06001A42 RID: 6722
		public abstract void Dispose();

		// Token: 0x06001A43 RID: 6723
		public abstract void SetAsyncCallbacks(SNIAsyncCallback receiveCallback, SNIAsyncCallback sendCallback);

		// Token: 0x06001A44 RID: 6724
		public abstract void SetBufferSize(int bufferSize);

		// Token: 0x06001A45 RID: 6725
		public abstract uint Send(SNIPacket packet);

		// Token: 0x06001A46 RID: 6726
		public abstract uint SendAsync(SNIPacket packet, bool disposePacketAfterSendAsync, SNIAsyncCallback callback = null);

		// Token: 0x06001A47 RID: 6727
		public abstract uint Receive(out SNIPacket packet, int timeoutInMilliseconds);

		// Token: 0x06001A48 RID: 6728
		public abstract uint ReceiveAsync(ref SNIPacket packet);

		// Token: 0x06001A49 RID: 6729
		public abstract uint EnableSsl(uint options);

		// Token: 0x06001A4A RID: 6730
		public abstract void DisableSsl();

		// Token: 0x06001A4B RID: 6731
		public abstract uint CheckConnection();

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001A4C RID: 6732
		public abstract uint Status { get; }

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001A4D RID: 6733
		public abstract Guid ConnectionId { get; }
	}
}
