using System;
using System.Data.Common;

namespace System.Data.SqlClient
{
	/// <summary>Defines the mapping between a column in a <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> instance's data source and a column in the instance's destination table. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000158 RID: 344
	public sealed class SqlBulkCopyColumnMapping
	{
		/// <summary>Name of the column being mapped in the destination database table.</summary>
		/// <returns>The string value of the <see cref="P:System.Data.SqlClient.SqlBulkCopyColumnMapping.DestinationColumn" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x060010FA RID: 4346 RVA: 0x0005475A File Offset: 0x0005295A
		// (set) Token: 0x060010FB RID: 4347 RVA: 0x00054770 File Offset: 0x00052970
		public string DestinationColumn
		{
			get
			{
				if (this._destinationColumnName != null)
				{
					return this._destinationColumnName;
				}
				return string.Empty;
			}
			set
			{
				this._destinationColumnOrdinal = (this._internalDestinationColumnOrdinal = -1);
				this._destinationColumnName = value;
			}
		}

		/// <summary>Ordinal value of the destination column within the destination table.</summary>
		/// <returns>The integer value of the <see cref="P:System.Data.SqlClient.SqlBulkCopyColumnMapping.DestinationOrdinal" /> property, or -1 if the property has not been set.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x060010FC RID: 4348 RVA: 0x00054794 File Offset: 0x00052994
		// (set) Token: 0x060010FD RID: 4349 RVA: 0x0005479C File Offset: 0x0005299C
		public int DestinationOrdinal
		{
			get
			{
				return this._destinationColumnOrdinal;
			}
			set
			{
				if (value >= 0)
				{
					this._destinationColumnName = null;
					this._internalDestinationColumnOrdinal = value;
					this._destinationColumnOrdinal = value;
					return;
				}
				throw ADP.IndexOutOfRange(value);
			}
		}

		/// <summary>Name of the column being mapped in the data source.</summary>
		/// <returns>The string value of the <see cref="P:System.Data.SqlClient.SqlBulkCopyColumnMapping.SourceColumn" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x060010FE RID: 4350 RVA: 0x000547CB File Offset: 0x000529CB
		// (set) Token: 0x060010FF RID: 4351 RVA: 0x000547E4 File Offset: 0x000529E4
		public string SourceColumn
		{
			get
			{
				if (this._sourceColumnName != null)
				{
					return this._sourceColumnName;
				}
				return string.Empty;
			}
			set
			{
				this._sourceColumnOrdinal = (this._internalSourceColumnOrdinal = -1);
				this._sourceColumnName = value;
			}
		}

		/// <summary>The ordinal position of the source column within the data source.</summary>
		/// <returns>The integer value of the <see cref="P:System.Data.SqlClient.SqlBulkCopyColumnMapping.SourceOrdinal" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06001100 RID: 4352 RVA: 0x00054808 File Offset: 0x00052A08
		// (set) Token: 0x06001101 RID: 4353 RVA: 0x00054810 File Offset: 0x00052A10
		public int SourceOrdinal
		{
			get
			{
				return this._sourceColumnOrdinal;
			}
			set
			{
				if (value >= 0)
				{
					this._sourceColumnName = null;
					this._internalSourceColumnOrdinal = value;
					this._sourceColumnOrdinal = value;
					return;
				}
				throw ADP.IndexOutOfRange(value);
			}
		}

		/// <summary>Default constructor that initializes a new <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object.</summary>
		// Token: 0x06001102 RID: 4354 RVA: 0x0005483F File Offset: 0x00052A3F
		public SqlBulkCopyColumnMapping()
		{
			this._internalSourceColumnOrdinal = -1;
		}

		/// <summary>Creates a new column mapping, using column names to refer to source and destination columns.</summary>
		/// <param name="sourceColumn">The name of the source column within the data source.</param>
		/// <param name="destinationColumn">The name of the destination column within the destination table.</param>
		// Token: 0x06001103 RID: 4355 RVA: 0x0005484E File Offset: 0x00052A4E
		public SqlBulkCopyColumnMapping(string sourceColumn, string destinationColumn)
		{
			this.SourceColumn = sourceColumn;
			this.DestinationColumn = destinationColumn;
		}

		/// <summary>Creates a new column mapping, using a column ordinal to refer to the source column and a column name for the target column.</summary>
		/// <param name="sourceColumnOrdinal">The ordinal position of the source column within the data source.</param>
		/// <param name="destinationColumn">The name of the destination column within the destination table.</param>
		// Token: 0x06001104 RID: 4356 RVA: 0x00054864 File Offset: 0x00052A64
		public SqlBulkCopyColumnMapping(int sourceColumnOrdinal, string destinationColumn)
		{
			this.SourceOrdinal = sourceColumnOrdinal;
			this.DestinationColumn = destinationColumn;
		}

		/// <summary>Creates a new column mapping, using a column name to refer to the source column and a column ordinal for the target column.</summary>
		/// <param name="sourceColumn">The name of the source column within the data source.</param>
		/// <param name="destinationOrdinal">The ordinal position of the destination column within the destination table.</param>
		// Token: 0x06001105 RID: 4357 RVA: 0x0005487A File Offset: 0x00052A7A
		public SqlBulkCopyColumnMapping(string sourceColumn, int destinationOrdinal)
		{
			this.SourceColumn = sourceColumn;
			this.DestinationOrdinal = destinationOrdinal;
		}

		/// <summary>Creates a new column mapping, using column ordinals to refer to source and destination columns.</summary>
		/// <param name="sourceColumnOrdinal">The ordinal position of the source column within the data source.</param>
		/// <param name="destinationOrdinal">The ordinal position of the destination column within the destination table.</param>
		// Token: 0x06001106 RID: 4358 RVA: 0x00054890 File Offset: 0x00052A90
		public SqlBulkCopyColumnMapping(int sourceColumnOrdinal, int destinationOrdinal)
		{
			this.SourceOrdinal = sourceColumnOrdinal;
			this.DestinationOrdinal = destinationOrdinal;
		}

		// Token: 0x04000B46 RID: 2886
		internal string _destinationColumnName;

		// Token: 0x04000B47 RID: 2887
		internal int _destinationColumnOrdinal;

		// Token: 0x04000B48 RID: 2888
		internal string _sourceColumnName;

		// Token: 0x04000B49 RID: 2889
		internal int _sourceColumnOrdinal;

		// Token: 0x04000B4A RID: 2890
		internal int _internalDestinationColumnOrdinal;

		// Token: 0x04000B4B RID: 2891
		internal int _internalSourceColumnOrdinal;
	}
}
