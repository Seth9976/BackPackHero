using System;

namespace System.Net
{
	/// <summary>Represents the method that notifies callers when a continue response is received by the client.</summary>
	/// <param name="StatusCode">The numeric value of the HTTP status from the server. </param>
	/// <param name="httpHeaders">The headers returned with the 100-continue response from the server. </param>
	// Token: 0x02000403 RID: 1027
	// (Invoke) Token: 0x060020E0 RID: 8416
	public delegate void HttpContinueDelegate(int StatusCode, WebHeaderCollection httpHeaders);
}
