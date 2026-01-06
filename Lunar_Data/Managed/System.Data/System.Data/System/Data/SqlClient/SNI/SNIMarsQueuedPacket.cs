using System;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x02000244 RID: 580
	internal class SNIMarsQueuedPacket
	{
		// Token: 0x06001A7C RID: 6780 RVA: 0x000847C0 File Offset: 0x000829C0
		public SNIMarsQueuedPacket(SNIPacket packet, SNIAsyncCallback callback)
		{
			this._packet = packet;
			this._callback = callback;
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001A7D RID: 6781 RVA: 0x000847D6 File Offset: 0x000829D6
		// (set) Token: 0x06001A7E RID: 6782 RVA: 0x000847DE File Offset: 0x000829DE
		public SNIPacket Packet
		{
			get
			{
				return this._packet;
			}
			set
			{
				this._packet = value;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001A7F RID: 6783 RVA: 0x000847E7 File Offset: 0x000829E7
		// (set) Token: 0x06001A80 RID: 6784 RVA: 0x000847EF File Offset: 0x000829EF
		public SNIAsyncCallback Callback
		{
			get
			{
				return this._callback;
			}
			set
			{
				this._callback = value;
			}
		}

		// Token: 0x04001311 RID: 4881
		private SNIPacket _packet;

		// Token: 0x04001312 RID: 4882
		private SNIAsyncCallback _callback;
	}
}
