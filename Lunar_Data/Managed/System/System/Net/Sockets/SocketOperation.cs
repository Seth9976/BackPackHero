using System;

namespace System.Net.Sockets
{
	// Token: 0x020005D1 RID: 1489
	internal enum SocketOperation
	{
		// Token: 0x04001CCA RID: 7370
		Accept,
		// Token: 0x04001CCB RID: 7371
		Connect,
		// Token: 0x04001CCC RID: 7372
		Receive,
		// Token: 0x04001CCD RID: 7373
		ReceiveFrom,
		// Token: 0x04001CCE RID: 7374
		Send,
		// Token: 0x04001CCF RID: 7375
		SendTo,
		// Token: 0x04001CD0 RID: 7376
		RecvJustCallback,
		// Token: 0x04001CD1 RID: 7377
		SendJustCallback,
		// Token: 0x04001CD2 RID: 7378
		Disconnect,
		// Token: 0x04001CD3 RID: 7379
		AcceptReceive,
		// Token: 0x04001CD4 RID: 7380
		ReceiveGeneric,
		// Token: 0x04001CD5 RID: 7381
		SendGeneric
	}
}
