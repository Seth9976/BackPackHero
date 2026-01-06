using System;

namespace System.Net.Sockets
{
	/// <summary>The type of asynchronous socket operation most recently performed with this context object.</summary>
	// Token: 0x020005BC RID: 1468
	public enum SocketAsyncOperation
	{
		/// <summary>None of the socket operations.</summary>
		// Token: 0x04001BE4 RID: 7140
		None,
		/// <summary>A socket Accept operation. </summary>
		// Token: 0x04001BE5 RID: 7141
		Accept,
		/// <summary>A socket Connect operation.</summary>
		// Token: 0x04001BE6 RID: 7142
		Connect,
		/// <summary>A socket Disconnect operation.</summary>
		// Token: 0x04001BE7 RID: 7143
		Disconnect,
		/// <summary>A socket Receive operation.</summary>
		// Token: 0x04001BE8 RID: 7144
		Receive,
		/// <summary>A socket ReceiveFrom operation.</summary>
		// Token: 0x04001BE9 RID: 7145
		ReceiveFrom,
		/// <summary>A socket ReceiveMessageFrom operation.</summary>
		// Token: 0x04001BEA RID: 7146
		ReceiveMessageFrom,
		/// <summary>A socket Send operation.</summary>
		// Token: 0x04001BEB RID: 7147
		Send,
		/// <summary>A socket SendPackets operation.</summary>
		// Token: 0x04001BEC RID: 7148
		SendPackets,
		/// <summary>A socket SendTo operation.</summary>
		// Token: 0x04001BED RID: 7149
		SendTo
	}
}
