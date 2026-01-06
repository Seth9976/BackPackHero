using System;
using System.Collections;

namespace System.Data.SqlClient
{
	/// <summary>Collection of <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> objects that inherits from <see cref="T:System.Collections.CollectionBase" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000159 RID: 345
	public sealed class SqlBulkCopyColumnMappingCollection : CollectionBase
	{
		// Token: 0x06001107 RID: 4359 RVA: 0x000548A6 File Offset: 0x00052AA6
		internal SqlBulkCopyColumnMappingCollection()
		{
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06001108 RID: 4360 RVA: 0x000548AE File Offset: 0x00052AAE
		// (set) Token: 0x06001109 RID: 4361 RVA: 0x000548B6 File Offset: 0x00052AB6
		internal bool ReadOnly { get; set; }

		/// <summary>Gets the <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object at the specified index.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> to find.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002F6 RID: 758
		public SqlBulkCopyColumnMapping this[int index]
		{
			get
			{
				return (SqlBulkCopyColumnMapping)base.List[index];
			}
		}

		/// <summary>Adds the specified mapping to the <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMappingCollection" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object.</returns>
		/// <param name="bulkCopyColumnMapping">The <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object that describes the mapping to be added to the collection.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600110B RID: 4363 RVA: 0x000548D4 File Offset: 0x00052AD4
		public SqlBulkCopyColumnMapping Add(SqlBulkCopyColumnMapping bulkCopyColumnMapping)
		{
			this.AssertWriteAccess();
			if ((string.IsNullOrEmpty(bulkCopyColumnMapping.SourceColumn) && bulkCopyColumnMapping.SourceOrdinal == -1) || (string.IsNullOrEmpty(bulkCopyColumnMapping.DestinationColumn) && bulkCopyColumnMapping.DestinationOrdinal == -1))
			{
				throw SQL.BulkLoadNonMatchingColumnMapping();
			}
			base.InnerList.Add(bulkCopyColumnMapping);
			return bulkCopyColumnMapping;
		}

		/// <summary>Creates a new <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> and adds it to the collection, using column names to specify both source and destination columns.</summary>
		/// <returns>A column mapping.</returns>
		/// <param name="sourceColumn">The name of the source column within the data source.</param>
		/// <param name="destinationColumn">The name of the destination column within the destination table.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600110C RID: 4364 RVA: 0x00054927 File Offset: 0x00052B27
		public SqlBulkCopyColumnMapping Add(string sourceColumn, string destinationColumn)
		{
			this.AssertWriteAccess();
			return this.Add(new SqlBulkCopyColumnMapping(sourceColumn, destinationColumn));
		}

		/// <summary>Creates a new <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> and adds it to the collection, using an ordinal for the source column and a string for the destination column.</summary>
		/// <returns>A column mapping.</returns>
		/// <param name="sourceColumnIndex">The ordinal position of the source column within the data source.</param>
		/// <param name="destinationColumn">The name of the destination column within the destination table.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600110D RID: 4365 RVA: 0x0005493C File Offset: 0x00052B3C
		public SqlBulkCopyColumnMapping Add(int sourceColumnIndex, string destinationColumn)
		{
			this.AssertWriteAccess();
			return this.Add(new SqlBulkCopyColumnMapping(sourceColumnIndex, destinationColumn));
		}

		/// <summary>Creates a new <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> and adds it to the collection, using a column name to describe the source column and an ordinal to specify the destination column.</summary>
		/// <returns>A column mapping.</returns>
		/// <param name="sourceColumn">The name of the source column within the data source.</param>
		/// <param name="destinationColumnIndex">The ordinal position of the destination column within the destination table.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600110E RID: 4366 RVA: 0x00054951 File Offset: 0x00052B51
		public SqlBulkCopyColumnMapping Add(string sourceColumn, int destinationColumnIndex)
		{
			this.AssertWriteAccess();
			return this.Add(new SqlBulkCopyColumnMapping(sourceColumn, destinationColumnIndex));
		}

		/// <summary>Creates a new <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> and adds it to the collection, using ordinals to specify both source and destination columns.</summary>
		/// <returns>A column mapping.</returns>
		/// <param name="sourceColumnIndex">The ordinal position of the source column within the data source.</param>
		/// <param name="destinationColumnIndex">The ordinal position of the destination column within the destination table.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600110F RID: 4367 RVA: 0x00054966 File Offset: 0x00052B66
		public SqlBulkCopyColumnMapping Add(int sourceColumnIndex, int destinationColumnIndex)
		{
			this.AssertWriteAccess();
			return this.Add(new SqlBulkCopyColumnMapping(sourceColumnIndex, destinationColumnIndex));
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x0005497B File Offset: 0x00052B7B
		private void AssertWriteAccess()
		{
			if (this.ReadOnly)
			{
				throw SQL.BulkLoadMappingInaccessible();
			}
		}

		/// <summary>Clears the contents of the collection.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001111 RID: 4369 RVA: 0x0005498B File Offset: 0x00052B8B
		public new void Clear()
		{
			this.AssertWriteAccess();
			base.Clear();
		}

		/// <summary>Gets a value indicating whether a specified <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object exists in the collection.</summary>
		/// <returns>true if the specified mapping exists in the collection; otherwise false.</returns>
		/// <param name="value">A valid <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001112 RID: 4370 RVA: 0x00054999 File Offset: 0x00052B99
		public bool Contains(SqlBulkCopyColumnMapping value)
		{
			return base.InnerList.Contains(value);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMappingCollection" /> to an array of <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> items, starting at a particular index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> array that is the destination of the elements copied from <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMappingCollection" />. The array must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001113 RID: 4371 RVA: 0x000549A7 File Offset: 0x00052BA7
		public void CopyTo(SqlBulkCopyColumnMapping[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x000549B8 File Offset: 0x00052BB8
		internal void CreateDefaultMapping(int columnCount)
		{
			for (int i = 0; i < columnCount; i++)
			{
				base.InnerList.Add(new SqlBulkCopyColumnMapping(i, i));
			}
		}

		/// <summary>Gets the index of the specified <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object.</summary>
		/// <returns>The zero-based index of the column mapping, or -1 if the column mapping is not found in the collection.</returns>
		/// <param name="value">The <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object for which to search.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001115 RID: 4373 RVA: 0x000549E4 File Offset: 0x00052BE4
		public int IndexOf(SqlBulkCopyColumnMapping value)
		{
			return base.InnerList.IndexOf(value);
		}

		/// <summary>Insert a new <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> at the index specified.</summary>
		/// <param name="index">Integer value of the location within the <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMappingCollection" />  at which to insert the new <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" />.</param>
		/// <param name="value">
		///   <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object to be inserted in the collection.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001116 RID: 4374 RVA: 0x000549F2 File Offset: 0x00052BF2
		public void Insert(int index, SqlBulkCopyColumnMapping value)
		{
			this.AssertWriteAccess();
			base.InnerList.Insert(index, value);
		}

		/// <summary>Removes the specified <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> element from the <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMappingCollection" />.</summary>
		/// <param name="value">
		///   <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object to be removed from the collection.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001117 RID: 4375 RVA: 0x00054A07 File Offset: 0x00052C07
		public void Remove(SqlBulkCopyColumnMapping value)
		{
			this.AssertWriteAccess();
			base.InnerList.Remove(value);
		}

		/// <summary>Removes the mapping at the specified index from the collection.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object to be removed from the collection.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001118 RID: 4376 RVA: 0x00054A1B File Offset: 0x00052C1B
		public new void RemoveAt(int index)
		{
			this.AssertWriteAccess();
			base.RemoveAt(index);
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x00054A2C File Offset: 0x00052C2C
		internal void ValidateCollection()
		{
			foreach (object obj in base.InnerList)
			{
				SqlBulkCopyColumnMapping sqlBulkCopyColumnMapping = (SqlBulkCopyColumnMapping)obj;
				SqlBulkCopyColumnMappingCollection.MappingSchema mappingSchema = ((sqlBulkCopyColumnMapping.SourceOrdinal != -1) ? ((sqlBulkCopyColumnMapping.DestinationOrdinal != -1) ? SqlBulkCopyColumnMappingCollection.MappingSchema.OrdinalsOrdinals : SqlBulkCopyColumnMappingCollection.MappingSchema.OrdinalsNames) : ((sqlBulkCopyColumnMapping.DestinationOrdinal != -1) ? SqlBulkCopyColumnMappingCollection.MappingSchema.NemesOrdinals : SqlBulkCopyColumnMappingCollection.MappingSchema.NamesNames));
				if (this._mappingSchema == SqlBulkCopyColumnMappingCollection.MappingSchema.Undefined)
				{
					this._mappingSchema = mappingSchema;
				}
				else if (this._mappingSchema != mappingSchema)
				{
					throw SQL.BulkLoadMappingsNamesOrOrdinalsOnly();
				}
			}
		}

		// Token: 0x04000B4C RID: 2892
		private SqlBulkCopyColumnMappingCollection.MappingSchema _mappingSchema;

		// Token: 0x0200015A RID: 346
		private enum MappingSchema
		{
			// Token: 0x04000B4F RID: 2895
			Undefined,
			// Token: 0x04000B50 RID: 2896
			NamesNames,
			// Token: 0x04000B51 RID: 2897
			NemesOrdinals,
			// Token: 0x04000B52 RID: 2898
			OrdinalsNames,
			// Token: 0x04000B53 RID: 2899
			OrdinalsOrdinals
		}
	}
}
