using System;

namespace System.Data
{
	/// <summary>Provides data for the <see cref="M:System.Data.DataTable.NewRow" /> method.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200007B RID: 123
	public sealed class DataTableNewRowEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of <see cref="T:System.Data.DataTableNewRowEventArgs" />.</summary>
		/// <param name="dataRow">The <see cref="T:System.Data.DataRow" /> being added.</param>
		// Token: 0x0600087C RID: 2172 RVA: 0x00026833 File Offset: 0x00024A33
		public DataTableNewRowEventArgs(DataRow dataRow)
		{
			this.Row = dataRow;
		}

		/// <summary>Gets the row that is being added.</summary>
		/// <returns>The <see cref="T:System.Data.DataRow" /> that is being added. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600087D RID: 2173 RVA: 0x00026842 File Offset: 0x00024A42
		public DataRow Row { get; }
	}
}
