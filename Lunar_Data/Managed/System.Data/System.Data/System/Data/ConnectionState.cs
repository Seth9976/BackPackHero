using System;

namespace System.Data
{
	/// <summary>Describes the current state of the connection to a data source.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200003F RID: 63
	[Flags]
	public enum ConnectionState
	{
		/// <summary>The connection is closed.</summary>
		// Token: 0x04000491 RID: 1169
		Closed = 0,
		/// <summary>The connection is open.</summary>
		// Token: 0x04000492 RID: 1170
		Open = 1,
		/// <summary>The connection object is connecting to the data source.</summary>
		// Token: 0x04000493 RID: 1171
		Connecting = 2,
		/// <summary>The connection object is executing a command. (This value is reserved for future versions of the product.) </summary>
		// Token: 0x04000494 RID: 1172
		Executing = 4,
		/// <summary>The connection object is retrieving data. (This value is reserved for future versions of the product.) </summary>
		// Token: 0x04000495 RID: 1173
		Fetching = 8,
		/// <summary>The connection to the data source is broken. This can occur only after the connection has been opened. A connection in this state may be closed and then re-opened. (This value is reserved for future versions of the product.) </summary>
		// Token: 0x04000496 RID: 1174
		Broken = 16
	}
}
