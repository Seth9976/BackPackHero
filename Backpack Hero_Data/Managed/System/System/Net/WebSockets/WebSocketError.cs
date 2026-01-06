using System;

namespace System.Net.WebSockets
{
	/// <summary>Contains the list of possible WebSocket errors.</summary>
	// Token: 0x020005FD RID: 1533
	public enum WebSocketError
	{
		/// <summary>Indicates that there was no native error information for the exception.</summary>
		// Token: 0x04001E02 RID: 7682
		Success,
		/// <summary>Indicates that a WebSocket frame with an unknown opcode was received.</summary>
		// Token: 0x04001E03 RID: 7683
		InvalidMessageType,
		/// <summary>Indicates a general error.</summary>
		// Token: 0x04001E04 RID: 7684
		Faulted,
		/// <summary>Indicates that an unknown native error occurred.</summary>
		// Token: 0x04001E05 RID: 7685
		NativeError,
		/// <summary>Indicates that the incoming request was not a valid websocket request.</summary>
		// Token: 0x04001E06 RID: 7686
		NotAWebSocket,
		/// <summary>Indicates that the client requested an unsupported version of the WebSocket protocol.</summary>
		// Token: 0x04001E07 RID: 7687
		UnsupportedVersion,
		/// <summary>Indicates that the client requested an unsupported WebSocket subprotocol.</summary>
		// Token: 0x04001E08 RID: 7688
		UnsupportedProtocol,
		/// <summary>Indicates an error occurred when parsing the HTTP headers during the opening handshake.</summary>
		// Token: 0x04001E09 RID: 7689
		HeaderError,
		/// <summary>Indicates that the connection was terminated unexpectedly.</summary>
		// Token: 0x04001E0A RID: 7690
		ConnectionClosedPrematurely,
		/// <summary>Indicates the WebSocket is an invalid state for the given operation (such as being closed or aborted).</summary>
		// Token: 0x04001E0B RID: 7691
		InvalidState
	}
}
