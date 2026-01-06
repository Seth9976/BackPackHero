using System;

namespace System.Data
{
	/// <summary>Provides data for the <see cref="E:System.Data.DataTable.ColumnChanging" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200004C RID: 76
	public class DataColumnChangeEventArgs : EventArgs
	{
		// Token: 0x0600036E RID: 878 RVA: 0x00010B15 File Offset: 0x0000ED15
		internal DataColumnChangeEventArgs(DataRow row)
		{
			this.Row = row;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataColumnChangeEventArgs" /> class.</summary>
		/// <param name="row">The <see cref="T:System.Data.DataRow" /> of the column with the changing value. </param>
		/// <param name="column">The <see cref="T:System.Data.DataColumn" /> with the changing value. </param>
		/// <param name="value">The new value. </param>
		// Token: 0x0600036F RID: 879 RVA: 0x00010B24 File Offset: 0x0000ED24
		public DataColumnChangeEventArgs(DataRow row, DataColumn column, object value)
		{
			this.Row = row;
			this._column = column;
			this.ProposedValue = value;
		}

		/// <summary>Gets the <see cref="T:System.Data.DataColumn" /> with a changing value.</summary>
		/// <returns>The <see cref="T:System.Data.DataColumn" /> with a changing value.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000370 RID: 880 RVA: 0x00010B41 File Offset: 0x0000ED41
		public DataColumn Column
		{
			get
			{
				return this._column;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataRow" /> of the column with a changing value.</summary>
		/// <returns>The <see cref="T:System.Data.DataRow" /> of the column with a changing value.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000371 RID: 881 RVA: 0x00010B49 File Offset: 0x0000ED49
		public DataRow Row { get; }

		/// <summary>Gets or sets the proposed new value for the column.</summary>
		/// <returns>The proposed value, of type <see cref="T:System.Object" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000372 RID: 882 RVA: 0x00010B51 File Offset: 0x0000ED51
		// (set) Token: 0x06000373 RID: 883 RVA: 0x00010B59 File Offset: 0x0000ED59
		public object ProposedValue { get; set; }

		// Token: 0x06000374 RID: 884 RVA: 0x00010B62 File Offset: 0x0000ED62
		internal void InitializeColumnChangeEvent(DataColumn column, object value)
		{
			this._column = column;
			this.ProposedValue = value;
		}

		// Token: 0x040004D2 RID: 1234
		private DataColumn _column;
	}
}
