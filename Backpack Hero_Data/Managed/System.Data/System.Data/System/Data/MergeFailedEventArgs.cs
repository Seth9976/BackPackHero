using System;

namespace System.Data
{
	/// <summary>Occurs when a target and source DataRow have the same primary key value, and the <see cref="P:System.Data.DataSet.EnforceConstraints" /> property is set to true.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000BD RID: 189
	public class MergeFailedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of a <see cref="T:System.Data.MergeFailedEventArgs" /> class with the <see cref="T:System.Data.DataTable" /> and a description of the merge conflict.</summary>
		/// <param name="table">The <see cref="T:System.Data.DataTable" /> object. </param>
		/// <param name="conflict">A description of the merge conflict. </param>
		// Token: 0x06000B86 RID: 2950 RVA: 0x00032FEF File Offset: 0x000311EF
		public MergeFailedEventArgs(DataTable table, string conflict)
		{
			this.Table = table;
			this.Conflict = conflict;
		}

		/// <summary>Returns the <see cref="T:System.Data.DataTable" /> object.</summary>
		/// <returns>The <see cref="T:System.Data.DataTable" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000B87 RID: 2951 RVA: 0x00033005 File Offset: 0x00031205
		public DataTable Table { get; }

		/// <summary>Returns a description of the merge conflict.</summary>
		/// <returns>A description of the merge conflict.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000B88 RID: 2952 RVA: 0x0003300D File Offset: 0x0003120D
		public string Conflict { get; }
	}
}
