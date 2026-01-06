using System;

namespace System.Net.WebSockets
{
	/// <summary>Indicates the message type.</summary>
	// Token: 0x020005FF RID: 1535
	public enum WebSocketMessageType
	{
		/// <summary>The message is clear text.</summary>
		// Token: 0x04001E0E RID: 7694
		Text,
		/// <summary>The message is in binary format.</summary>
		// Token: 0x04001E0F RID: 7695
		Binary,
		/// <summary>A receive has completed because a close message was received.</summary>
		// Token: 0x04001E10 RID: 7696
		Close
	}
}
