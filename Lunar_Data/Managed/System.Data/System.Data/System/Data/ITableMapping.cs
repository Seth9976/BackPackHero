using System;

namespace System.Data
{
	/// <summary>Associates a source table with a table in a <see cref="T:System.Data.DataSet" />, and is implemented by the <see cref="T:System.Data.Common.DataTableMapping" /> class, which is used in common by .NET Framework data providers.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000B7 RID: 183
	public interface ITableMapping
	{
		/// <summary>Gets the derived <see cref="T:System.Data.Common.DataColumnMappingCollection" /> for the <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>A collection of data column mappings.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000B7A RID: 2938
		IColumnMappingCollection ColumnMappings { get; }

		/// <summary>Gets or sets the case-insensitive name of the table within the <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>The case-insensitive name of the table within the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000B7B RID: 2939
		// (set) Token: 0x06000B7C RID: 2940
		string DataSetTable { get; set; }

		/// <summary>Gets or sets the case-sensitive name of the source table.</summary>
		/// <returns>The case-sensitive name of the source table.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000B7D RID: 2941
		// (set) Token: 0x06000B7E RID: 2942
		string SourceTable { get; set; }
	}
}
