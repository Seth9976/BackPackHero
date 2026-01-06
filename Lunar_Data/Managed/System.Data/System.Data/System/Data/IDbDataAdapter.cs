using System;

namespace System.Data
{
	/// <summary>Represents a set of command-related properties that are used to fill the <see cref="T:System.Data.DataSet" /> and update a data source, and is implemented by .NET Framework data providers that access relational databases.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000B4 RID: 180
	public interface IDbDataAdapter : IDataAdapter
	{
		/// <summary>Gets or sets an SQL statement used to select records in the data source.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> that is used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to select records from data source for placement in the data set.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000B68 RID: 2920
		// (set) Token: 0x06000B69 RID: 2921
		IDbCommand SelectCommand { get; set; }

		/// <summary>Gets or sets an SQL statement used to insert new records into the data source.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to insert records in the data source for new rows in the data set.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000B6A RID: 2922
		// (set) Token: 0x06000B6B RID: 2923
		IDbCommand InsertCommand { get; set; }

		/// <summary>Gets or sets an SQL statement used to update records in the data source.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to update records in the data source for modified rows in the data set.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000B6C RID: 2924
		// (set) Token: 0x06000B6D RID: 2925
		IDbCommand UpdateCommand { get; set; }

		/// <summary>Gets or sets an SQL statement for deleting records from the data set.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to delete records in the data source for deleted rows in the data set.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000B6E RID: 2926
		// (set) Token: 0x06000B6F RID: 2927
		IDbCommand DeleteCommand { get; set; }
	}
}
