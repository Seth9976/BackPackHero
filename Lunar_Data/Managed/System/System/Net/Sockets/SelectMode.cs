using System;

namespace System.Net.Sockets
{
	/// <summary>Defines the polling modes for the <see cref="M:System.Net.Sockets.Socket.Poll(System.Int32,System.Net.Sockets.SelectMode)" /> method.</summary>
	// Token: 0x020005BB RID: 1467
	public enum SelectMode
	{
		/// <summary>Read status mode.</summary>
		// Token: 0x04001BE0 RID: 7136
		SelectRead,
		/// <summary>Write status mode.</summary>
		// Token: 0x04001BE1 RID: 7137
		SelectWrite,
		/// <summary>Error status mode.</summary>
		// Token: 0x04001BE2 RID: 7138
		SelectError
	}
}
