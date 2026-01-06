using System;

namespace System.Net.Sockets
{
	/// <summary>Specifies socket send and receive behaviors.</summary>
	// Token: 0x020005C0 RID: 1472
	[Flags]
	public enum SocketFlags
	{
		/// <summary>Use no flags for this call.</summary>
		// Token: 0x04001C27 RID: 7207
		None = 0,
		/// <summary>Process out-of-band data.</summary>
		// Token: 0x04001C28 RID: 7208
		OutOfBand = 1,
		/// <summary>Peek at the incoming message.</summary>
		// Token: 0x04001C29 RID: 7209
		Peek = 2,
		/// <summary>Send without using routing tables.</summary>
		// Token: 0x04001C2A RID: 7210
		DontRoute = 4,
		/// <summary>Provides a standard value for the number of WSABUF structures that are used to send and receive data. This value is not used or supported on .NET Framework 4.5.</summary>
		// Token: 0x04001C2B RID: 7211
		MaxIOVectorLength = 16,
		/// <summary>The message was too large to fit into the specified buffer and was truncated.</summary>
		// Token: 0x04001C2C RID: 7212
		Truncated = 256,
		/// <summary>Indicates that the control data did not fit into an internal 64-KB buffer and was truncated.</summary>
		// Token: 0x04001C2D RID: 7213
		ControlDataTruncated = 512,
		/// <summary>Indicates a broadcast packet.</summary>
		// Token: 0x04001C2E RID: 7214
		Broadcast = 1024,
		/// <summary>Indicates a multicast packet.</summary>
		// Token: 0x04001C2F RID: 7215
		Multicast = 2048,
		/// <summary>Partial send or receive for message.</summary>
		// Token: 0x04001C30 RID: 7216
		Partial = 32768
	}
}
