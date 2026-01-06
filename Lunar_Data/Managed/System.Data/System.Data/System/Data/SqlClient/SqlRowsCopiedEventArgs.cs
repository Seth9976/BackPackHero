using System;

namespace System.Data.SqlClient
{
	/// <summary>Represents the set of arguments passed to the <see cref="T:System.Data.SqlClient.SqlRowsCopiedEventHandler" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000138 RID: 312
	public class SqlRowsCopiedEventArgs : EventArgs
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Data.SqlClient.SqlRowsCopiedEventArgs" /> object.</summary>
		/// <param name="rowsCopied">An <see cref="T:System.Int64" /> that indicates the number of rows copied during the current bulk copy operation. </param>
		// Token: 0x06001023 RID: 4131 RVA: 0x0004F79D File Offset: 0x0004D99D
		public SqlRowsCopiedEventArgs(long rowsCopied)
		{
			this._rowsCopied = rowsCopied;
		}

		/// <summary>Gets or sets a value that indicates whether the bulk copy operation should be aborted.</summary>
		/// <returns>true if the bulk copy operation should be aborted; otherwise false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06001024 RID: 4132 RVA: 0x0004F7AC File Offset: 0x0004D9AC
		// (set) Token: 0x06001025 RID: 4133 RVA: 0x0004F7B4 File Offset: 0x0004D9B4
		public bool Abort
		{
			get
			{
				return this._abort;
			}
			set
			{
				this._abort = value;
			}
		}

		/// <summary>Gets a value that returns the number of rows copied during the current bulk copy operation.</summary>
		/// <returns>int that returns the number of rows copied.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06001026 RID: 4134 RVA: 0x0004F7BD File Offset: 0x0004D9BD
		public long RowsCopied
		{
			get
			{
				return this._rowsCopied;
			}
		}

		// Token: 0x04000A96 RID: 2710
		private bool _abort;

		// Token: 0x04000A97 RID: 2711
		private long _rowsCopied;
	}
}
