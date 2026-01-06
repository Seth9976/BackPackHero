using System;

namespace System.Net.Sockets
{
	// Token: 0x020005D3 RID: 1491
	public struct SocketReceiveMessageFromResult
	{
		// Token: 0x04001CD8 RID: 7384
		public int ReceivedBytes;

		// Token: 0x04001CD9 RID: 7385
		public SocketFlags SocketFlags;

		// Token: 0x04001CDA RID: 7386
		public EndPoint RemoteEndPoint;

		// Token: 0x04001CDB RID: 7387
		public IPPacketInformation PacketInformation;
	}
}
