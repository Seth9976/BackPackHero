using System;

namespace System.Net.WebSockets
{
	/// <summary> Defines the different states a WebSockets instance can be in.</summary>
	// Token: 0x02000601 RID: 1537
	public enum WebSocketState
	{
		/// <summary>Reserved for future use.</summary>
		// Token: 0x04001E17 RID: 7703
		None,
		/// <summary>The connection is negotiating the handshake with the remote endpoint.</summary>
		// Token: 0x04001E18 RID: 7704
		Connecting,
		/// <summary>The initial state after the HTTP handshake has been completed.</summary>
		// Token: 0x04001E19 RID: 7705
		Open,
		/// <summary>A close message was sent to the remote endpoint.</summary>
		// Token: 0x04001E1A RID: 7706
		CloseSent,
		/// <summary>A close message was received from the remote endpoint.</summary>
		// Token: 0x04001E1B RID: 7707
		CloseReceived,
		/// <summary>Indicates the WebSocket close handshake completed gracefully.</summary>
		// Token: 0x04001E1C RID: 7708
		Closed,
		/// <summary>Reserved for future use.</summary>
		// Token: 0x04001E1D RID: 7709
		Aborted
	}
}
