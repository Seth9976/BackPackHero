using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Common
{
	/// <summary>Reads a forward-only stream of rows from a data source.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200033E RID: 830
	public abstract class DbDataReader : MarshalByRefObject, IDataReader, IDisposable, IDataRecord, IEnumerable, IAsyncDisposable
	{
		/// <summary>Gets a value indicating the depth of nesting for the current row.</summary>
		/// <returns>The depth of nesting for the current row.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06002829 RID: 10281
		public abstract int Depth { get; }

		/// <summary>Gets the number of columns in the current row.</summary>
		/// <returns>The number of columns in the current row.</returns>
		/// <exception cref="T:System.NotSupportedException">There is no current connection to an instance of SQL Server. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x0600282A RID: 10282
		public abstract int FieldCount { get; }

		/// <summary>Gets a value that indicates whether this <see cref="T:System.Data.Common.DbDataReader" /> contains one or more rows.</summary>
		/// <returns>true if the <see cref="T:System.Data.Common.DbDataReader" /> contains one or more rows; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x0600282B RID: 10283
		public abstract bool HasRows { get; }

		/// <summary>Gets a value indicating whether the <see cref="T:System.Data.Common.DbDataReader" /> is closed.</summary>
		/// <returns>true if the <see cref="T:System.Data.Common.DbDataReader" /> is closed; otherwise false.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Data.SqlClient.SqlDataReader" /> is closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x0600282C RID: 10284
		public abstract bool IsClosed { get; }

		/// <summary>Gets the number of rows changed, inserted, or deleted by execution of the SQL statement. </summary>
		/// <returns>The number of rows changed, inserted, or deleted. -1 for SELECT statements; 0 if no rows were affected or the statement failed.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x0600282D RID: 10285
		public abstract int RecordsAffected { get; }

		/// <summary>Gets the number of fields in the <see cref="T:System.Data.Common.DbDataReader" /> that are not hidden.</summary>
		/// <returns>The number of fields that are not hidden.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x0600282E RID: 10286 RVA: 0x000B1DF0 File Offset: 0x000AFFF0
		public virtual int VisibleFieldCount
		{
			get
			{
				return this.FieldCount;
			}
		}

		/// <summary>Gets the value of the specified column as an instance of <see cref="T:System.Object" />.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006EB RID: 1771
		public abstract object this[int ordinal] { get; }

		/// <summary>Gets the value of the specified column as an instance of <see cref="T:System.Object" />.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="name">The name of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">No column with the specified name was found. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006EC RID: 1772
		public abstract object this[string name] { get; }

		/// <summary>Closes the <see cref="T:System.Data.Common.DbDataReader" /> object.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002831 RID: 10289 RVA: 0x000094D4 File Offset: 0x000076D4
		public virtual void Close()
		{
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Data.Common.DbDataReader" /> class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002832 RID: 10290 RVA: 0x000B1DF8 File Offset: 0x000AFFF8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the managed resources used by the <see cref="T:System.Data.Common.DbDataReader" /> and optionally releases the unmanaged resources.</summary>
		/// <param name="disposing">true to release managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x06002833 RID: 10291 RVA: 0x000B1E01 File Offset: 0x000B0001
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
		}

		/// <summary>Gets name of the data type of the specified column.</summary>
		/// <returns>A string representing the name of the data type.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002834 RID: 10292
		public abstract string GetDataTypeName(int ordinal);

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the rows in the data reader.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the rows in the data reader.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002835 RID: 10293
		[EditorBrowsable(EditorBrowsableState.Never)]
		public abstract IEnumerator GetEnumerator();

		/// <summary>Gets the data type of the specified column.</summary>
		/// <returns>The data type of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002836 RID: 10294
		public abstract Type GetFieldType(int ordinal);

		/// <summary>Gets the name of the column, given the zero-based column ordinal.</summary>
		/// <returns>The name of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002837 RID: 10295
		public abstract string GetName(int ordinal);

		/// <summary>Gets the column ordinal given the name of the column.</summary>
		/// <returns>The zero-based column ordinal.</returns>
		/// <param name="name">The name of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The name specified is not a valid column name.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002838 RID: 10296
		public abstract int GetOrdinal(string name);

		/// <summary>Returns a <see cref="T:System.Data.DataTable" /> that describes the column metadata of the <see cref="T:System.Data.Common.DbDataReader" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that describes the column metadata.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Data.SqlClient.SqlDataReader" /> is closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002839 RID: 10297 RVA: 0x0007C361 File Offset: 0x0007A561
		public virtual DataTable GetSchemaTable()
		{
			throw new NotSupportedException();
		}

		/// <summary>Gets the value of the specified column as a Boolean.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600283A RID: 10298
		public abstract bool GetBoolean(int ordinal);

		/// <summary>Gets the value of the specified column as a byte.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600283B RID: 10299
		public abstract byte GetByte(int ordinal);

		/// <summary>Reads a stream of bytes from the specified column, starting at location indicated by <paramref name="dataOffset" />, into the buffer, starting at the location indicated by <paramref name="bufferOffset" />.</summary>
		/// <returns>The actual number of bytes read.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <param name="dataOffset">The index within the row from which to begin the read operation.</param>
		/// <param name="buffer">The buffer into which to copy the data.</param>
		/// <param name="bufferOffset">The index with the buffer to which the data will be copied.</param>
		/// <param name="length">The maximum number of characters to read.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600283C RID: 10300
		public abstract long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length);

		/// <summary>Gets the value of the specified column as a single character.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600283D RID: 10301
		public abstract char GetChar(int ordinal);

		/// <summary>Reads a stream of characters from the specified column, starting at location indicated by <paramref name="dataOffset" />, into the buffer, starting at the location indicated by <paramref name="bufferOffset" />.</summary>
		/// <returns>The actual number of characters read.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <param name="dataOffset">The index within the row from which to begin the read operation.</param>
		/// <param name="buffer">The buffer into which to copy the data.</param>
		/// <param name="bufferOffset">The index with the buffer to which the data will be copied.</param>
		/// <param name="length">The maximum number of characters to read.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600283E RID: 10302
		public abstract long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length);

		/// <summary>Returns a <see cref="T:System.Data.Common.DbDataReader" /> object for the requested column ordinal.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DbDataReader" /> object.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600283F RID: 10303 RVA: 0x000B1E0C File Offset: 0x000B000C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public DbDataReader GetData(int ordinal)
		{
			return this.GetDbDataReader(ordinal);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Data.IDataRecord.GetData(System.Int32)" />.</summary>
		/// <returns>An instance of <see cref="T:System.Data.IDataReader" /> to be used when the field points to more remote structured data.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		// Token: 0x06002840 RID: 10304 RVA: 0x000B1E0C File Offset: 0x000B000C
		IDataReader IDataRecord.GetData(int ordinal)
		{
			return this.GetDbDataReader(ordinal);
		}

		/// <summary>Returns a <see cref="T:System.Data.Common.DbDataReader" /> object for the requested column ordinal that can be overridden with a provider-specific implementation.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DbDataReader" /> object.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		// Token: 0x06002841 RID: 10305 RVA: 0x00060F32 File Offset: 0x0005F132
		protected virtual DbDataReader GetDbDataReader(int ordinal)
		{
			throw ADP.NotSupported();
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002842 RID: 10306
		public abstract DateTime GetDateTime(int ordinal);

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Decimal" /> object.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002843 RID: 10307
		public abstract decimal GetDecimal(int ordinal);

		/// <summary>Gets the value of the specified column as a double-precision floating point number.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002844 RID: 10308
		public abstract double GetDouble(int ordinal);

		/// <summary>Gets the value of the specified column as a single-precision floating point number.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002845 RID: 10309
		public abstract float GetFloat(int ordinal);

		/// <summary>Gets the value of the specified column as a globally-unique identifier (GUID).</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002846 RID: 10310
		public abstract Guid GetGuid(int ordinal);

		/// <summary>Gets the value of the specified column as a 16-bit signed integer.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002847 RID: 10311
		public abstract short GetInt16(int ordinal);

		/// <summary>Gets the value of the specified column as a 32-bit signed integer.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002848 RID: 10312
		public abstract int GetInt32(int ordinal);

		/// <summary>Gets the value of the specified column as a 64-bit signed integer.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002849 RID: 10313
		public abstract long GetInt64(int ordinal);

		/// <summary>Returns the provider-specific field type of the specified column.</summary>
		/// <returns>The <see cref="T:System.Type" /> object that describes the data type of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600284A RID: 10314 RVA: 0x000B1E15 File Offset: 0x000B0015
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual Type GetProviderSpecificFieldType(int ordinal)
		{
			return this.GetFieldType(ordinal);
		}

		/// <summary>Gets the value of the specified column as an instance of <see cref="T:System.Object" />.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600284B RID: 10315 RVA: 0x0005F232 File Offset: 0x0005D432
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual object GetProviderSpecificValue(int ordinal)
		{
			return this.GetValue(ordinal);
		}

		/// <summary>Gets all provider-specific attribute columns in the collection for the current row.</summary>
		/// <returns>The number of instances of <see cref="T:System.Object" /> in the array.</returns>
		/// <param name="values">An array of <see cref="T:System.Object" /> into which to copy the attribute columns.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600284C RID: 10316 RVA: 0x000B1E1E File Offset: 0x000B001E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual int GetProviderSpecificValues(object[] values)
		{
			return this.GetValues(values);
		}

		/// <summary>Gets the value of the specified column as an instance of <see cref="T:System.String" />.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600284D RID: 10317
		public abstract string GetString(int ordinal);

		/// <summary>Retrieves data as a <see cref="T:System.IO.Stream" />.</summary>
		/// <returns>The returned object.</returns>
		/// <param name="ordinal">Retrieves data as a <see cref="T:System.IO.Stream" />.</param>
		/// <exception cref="T:System.InvalidOperationException">The connection drops or is closed during the data retrieval.The <see cref="T:System.Data.Common.DbDataReader" /> is closed during the data retrieval.There is no data ready to be read (for example, the first <see cref="M:System.Data.Common.DbDataReader.Read" /> hasn't been called, or returned false).Tried to read a previously-read column in sequential mode.There was an asynchronous operation in progress. This applies to all Get* methods when running in sequential mode, as they could be called while reading a stream.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">Trying to read a column that does not exist.</exception>
		/// <exception cref="T:System.InvalidCastException">The returned type was not one of the types below:binaryimagevarbinaryudt</exception>
		// Token: 0x0600284E RID: 10318 RVA: 0x000B1E28 File Offset: 0x000B0028
		public virtual Stream GetStream(int ordinal)
		{
			Stream stream;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				long num = 0L;
				byte[] array = new byte[4096];
				long bytes;
				do
				{
					bytes = this.GetBytes(ordinal, num, array, 0, array.Length);
					memoryStream.Write(array, 0, (int)bytes);
					num += bytes;
				}
				while (bytes > 0L);
				stream = new MemoryStream(memoryStream.ToArray(), false);
			}
			return stream;
		}

		/// <summary>Retrieves data as a <see cref="T:System.IO.TextReader" />.</summary>
		/// <returns>The returned object.</returns>
		/// <param name="ordinal">Retrieves data as a <see cref="T:System.IO.TextReader" />.</param>
		/// <exception cref="T:System.InvalidOperationException">The connection drops or is closed during the data retrieval.The <see cref="T:System.Data.Common.DbDataReader" /> is closed during the data retrieval.There is no data ready to be read (for example, the first <see cref="M:System.Data.Common.DbDataReader.Read" /> hasn't been called, or returned false).Tried to read a previously-read column in sequential mode.There was an asynchronous operation in progress. This applies to all Get* methods when running in sequential mode, as they could be called while reading a stream.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">Trying to read a column that does not exist.</exception>
		/// <exception cref="T:System.InvalidCastException">The returned type was not one of the types below:charncharntextnvarchartextvarchar</exception>
		// Token: 0x0600284F RID: 10319 RVA: 0x000B1E9C File Offset: 0x000B009C
		public virtual TextReader GetTextReader(int ordinal)
		{
			if (this.IsDBNull(ordinal))
			{
				return new StringReader(string.Empty);
			}
			return new StringReader(this.GetString(ordinal));
		}

		/// <summary>Gets the value of the specified column as an instance of <see cref="T:System.Object" />.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002850 RID: 10320
		public abstract object GetValue(int ordinal);

		/// <summary>Synchronously gets the value of the specified column as a type.</summary>
		/// <returns>The column to be retrieved.</returns>
		/// <param name="ordinal">The column to be retrieved.</param>
		/// <typeparam name="T">Synchronously gets the value of the specified column as a type.</typeparam>
		/// <exception cref="T:System.InvalidOperationException">The connection drops or is closed during the data retrieval.The <see cref="T:System.Data.SqlClient.SqlDataReader" /> is closed during the data retrieval.There is no data ready to be read (for example, the first <see cref="M:System.Data.SqlClient.SqlDataReader.Read" /> hasn't been called, or returned false).Tried to read a previously-read column in sequential mode.There was an asynchronous operation in progress. This applies to all Get* methods when running in sequential mode, as they could be called while reading a stream.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">Trying to read a column that does not exist.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="T" /> doesn’t match the type returned by SQL Server or cannot be cast.</exception>
		// Token: 0x06002851 RID: 10321 RVA: 0x000B1EBE File Offset: 0x000B00BE
		public virtual T GetFieldValue<T>(int ordinal)
		{
			return (T)((object)this.GetValue(ordinal));
		}

		/// <summary>Asynchronously gets the value of the specified column as a type.</summary>
		/// <returns>The type of the value to be returned.</returns>
		/// <param name="ordinal">The type of the value to be returned.</param>
		/// <typeparam name="T">The type of the value to be returned. See the remarks section for more information.</typeparam>
		/// <exception cref="T:System.InvalidOperationException">The connection drops or is closed during the data retrieval.The <see cref="T:System.Data.Common.DbDataReader" /> is closed during the data retrieval.There is no data ready to be read (for example, the first <see cref="M:System.Data.Common.DbDataReader.Read" /> hasn't been called, or returned false).Tried to read a previously-read column in sequential mode.There was an asynchronous operation in progress. This applies to all Get* methods when running in sequential mode, as they could be called while reading a stream.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">Trying to read a column that does not exist.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="T" /> doesn’t match the type returned by the data source  or cannot be cast.</exception>
		// Token: 0x06002852 RID: 10322 RVA: 0x000B1ECC File Offset: 0x000B00CC
		public Task<T> GetFieldValueAsync<T>(int ordinal)
		{
			return this.GetFieldValueAsync<T>(ordinal, CancellationToken.None);
		}

		/// <summary>Asynchronously gets the value of the specified column as a type.</summary>
		/// <returns>The type of the value to be returned.</returns>
		/// <param name="ordinal">The type of the value to be returned.</param>
		/// <param name="cancellationToken">The cancellation instruction, which propagates a notification that operations should be canceled. This does not guarantee the cancellation. A setting of CancellationToken.None makes this method equivalent to <see cref="M:System.Data.Common.DbDataReader.GetFieldValueAsync``1(System.Int32)" />. The returned task must be marked as cancelled.</param>
		/// <typeparam name="T">The type of the value to be returned. See the remarks section for more information.</typeparam>
		/// <exception cref="T:System.InvalidOperationException">The connection drops or is closed during the data retrieval.The <see cref="T:System.Data.Common.DbDataReader" /> is closed during the data retrieval.There is no data ready to be read (for example, the first <see cref="M:System.Data.Common.DbDataReader.Read" /> hasn't been called, or returned false).Tried to read a previously-read column in sequential mode.There was an asynchronous operation in progress. This applies to all Get* methods when running in sequential mode, as they could be called while reading a stream.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">Trying to read a column that does not exist.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="T" /> doesn’t match the type returned by the data source or cannot be cast.</exception>
		// Token: 0x06002853 RID: 10323 RVA: 0x000B1EDC File Offset: 0x000B00DC
		public virtual Task<T> GetFieldValueAsync<T>(int ordinal, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return ADP.CreatedTaskWithCancellation<T>();
			}
			Task<T> task;
			try
			{
				task = Task.FromResult<T>(this.GetFieldValue<T>(ordinal));
			}
			catch (Exception ex)
			{
				task = Task.FromException<T>(ex);
			}
			return task;
		}

		/// <summary>Populates an array of objects with the column values of the current row.</summary>
		/// <returns>The number of instances of <see cref="T:System.Object" /> in the array.</returns>
		/// <param name="values">An array of <see cref="T:System.Object" /> into which to copy the attribute columns.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002854 RID: 10324
		public abstract int GetValues(object[] values);

		/// <summary>Gets a value that indicates whether the column contains nonexistent or missing values.</summary>
		/// <returns>true if the specified column is equivalent to <see cref="T:System.DBNull" />; otherwise false.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002855 RID: 10325
		public abstract bool IsDBNull(int ordinal);

		/// <summary>An asynchronous version of <see cref="M:System.Data.Common.DbDataReader.IsDBNull(System.Int32)" />, which gets a value that indicates whether the column contains non-existent or missing values.</summary>
		/// <returns>true if the specified column value is equivalent to DBNull otherwise false.</returns>
		/// <param name="ordinal">The zero-based column to be retrieved.</param>
		/// <exception cref="T:System.InvalidOperationException">The connection drops or is closed during the data retrieval.The <see cref="T:System.Data.Common.DbDataReader" /> is closed during the data retrieval.There is no data ready to be read (for example, the first <see cref="M:System.Data.Common.DbDataReader.Read" /> hasn't been called, or returned false).Trying to read a previously read column in sequential mode.There was an asynchronous operation in progress. This applies to all Get* methods when running in sequential mode, as they could be called while reading a stream.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">Trying to read a column that does not exist.</exception>
		// Token: 0x06002856 RID: 10326 RVA: 0x000B1F24 File Offset: 0x000B0124
		public Task<bool> IsDBNullAsync(int ordinal)
		{
			return this.IsDBNullAsync(ordinal, CancellationToken.None);
		}

		/// <summary>An asynchronous version of <see cref="M:System.Data.Common.DbDataReader.IsDBNull(System.Int32)" />, which gets a value that indicates whether the column contains non-existent or missing values. Optionally, sends a notification that operations should be cancelled.</summary>
		/// <returns>true if the specified column value is equivalent to DBNull otherwise false.</returns>
		/// <param name="ordinal">The zero-based column to be retrieved.</param>
		/// <param name="cancellationToken">The cancellation instruction, which propagates a notification that operations should be canceled. This does not guarantee the cancellation. A setting of CancellationToken.None makes this method equivalent to <see cref="M:System.Data.Common.DbDataReader.IsDBNullAsync(System.Int32)" />. The returned task must be marked as cancelled.</param>
		/// <exception cref="T:System.InvalidOperationException">The connection drops or is closed during the data retrieval.The <see cref="T:System.Data.Common.DbDataReader" /> is closed during the data retrieval.There is no data ready to be read (for example, the first <see cref="M:System.Data.Common.DbDataReader.Read" /> hasn't been called, or returned false).Trying to read a previously read column in sequential mode.There was an asynchronous operation in progress. This applies to all Get* methods when running in sequential mode, as they could be called while reading a stream.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">Trying to read a column that does not exist.</exception>
		// Token: 0x06002857 RID: 10327 RVA: 0x000B1F34 File Offset: 0x000B0134
		public virtual Task<bool> IsDBNullAsync(int ordinal, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return ADP.CreatedTaskWithCancellation<bool>();
			}
			Task<bool> task;
			try
			{
				task = (this.IsDBNull(ordinal) ? ADP.TrueTask : ADP.FalseTask);
			}
			catch (Exception ex)
			{
				task = Task.FromException<bool>(ex);
			}
			return task;
		}

		/// <summary>Advances the reader to the next result when reading the results of a batch of statements.</summary>
		/// <returns>true if there are more result sets; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002858 RID: 10328
		public abstract bool NextResult();

		/// <summary>Advances the reader to the next record in a result set.</summary>
		/// <returns>true if there are more rows; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002859 RID: 10329
		public abstract bool Read();

		/// <summary>An asynchronous version of <see cref="M:System.Data.Common.DbDataReader.Read" />, which advances the reader to the next record in a result set. This method invokes <see cref="M:System.Data.Common.DbDataReader.ReadAsync(System.Threading.CancellationToken)" /> with CancellationToken.None.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <exception cref="T:System.Data.Common.DbException">An error occurred while executing the command text.</exception>
		// Token: 0x0600285A RID: 10330 RVA: 0x000B1F84 File Offset: 0x000B0184
		public Task<bool> ReadAsync()
		{
			return this.ReadAsync(CancellationToken.None);
		}

		/// <summary>This is the asynchronous version of <see cref="M:System.Data.Common.DbDataReader.Read" />.  Providers should override with an appropriate implementation. The cancellationToken may optionally be ignored.The default implementation invokes the synchronous <see cref="M:System.Data.Common.DbDataReader.Read" /> method and returns a completed task, blocking the calling thread. The default implementation will return a cancelled task if passed an already cancelled cancellationToken.  Exceptions thrown by Read will be communicated via the returned Task Exception property.Do not invoke other methods and properties of the DbDataReader object until the returned Task is complete.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <param name="cancellationToken">The cancellation instruction.</param>
		/// <exception cref="T:System.Data.Common.DbException">An error occurred while executing the command text.</exception>
		// Token: 0x0600285B RID: 10331 RVA: 0x000B1F94 File Offset: 0x000B0194
		public virtual Task<bool> ReadAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return ADP.CreatedTaskWithCancellation<bool>();
			}
			Task<bool> task;
			try
			{
				task = (this.Read() ? ADP.TrueTask : ADP.FalseTask);
			}
			catch (Exception ex)
			{
				task = Task.FromException<bool>(ex);
			}
			return task;
		}

		/// <summary>An asynchronous version of <see cref="M:System.Data.Common.DbDataReader.NextResult" />, which advances the reader to the next result when reading the results of a batch of statements.Invokes <see cref="M:System.Data.Common.DbDataReader.NextResultAsync(System.Threading.CancellationToken)" /> with CancellationToken.None.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <exception cref="T:System.Data.Common.DbException">An error occurred while executing the command text.</exception>
		// Token: 0x0600285C RID: 10332 RVA: 0x000B1FE4 File Offset: 0x000B01E4
		public Task<bool> NextResultAsync()
		{
			return this.NextResultAsync(CancellationToken.None);
		}

		/// <summary>This is the asynchronous version of <see cref="M:System.Data.Common.DbDataReader.NextResult" />. Providers should override with an appropriate implementation. The <paramref name="cancellationToken" /> may optionally be ignored.The default implementation invokes the synchronous <see cref="M:System.Data.Common.DbDataReader.NextResult" /> method and returns a completed task, blocking the calling thread. The default implementation will return a cancelled task if passed an already cancelled <paramref name="cancellationToken" />. Exceptions thrown by <see cref="M:System.Data.Common.DbDataReader.NextResult" /> will be communicated via the returned Task Exception property.Other methods and properties of the DbDataReader object should not be invoked while the returned Task is not yet completed.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <param name="cancellationToken">The cancellation instruction.</param>
		/// <exception cref="T:System.Data.Common.DbException">An error occurred while executing the command text.</exception>
		// Token: 0x0600285D RID: 10333 RVA: 0x000B1FF4 File Offset: 0x000B01F4
		public virtual Task<bool> NextResultAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return ADP.CreatedTaskWithCancellation<bool>();
			}
			Task<bool> task;
			try
			{
				task = (this.NextResult() ? ADP.TrueTask : ADP.FalseTask);
			}
			catch (Exception ex)
			{
				task = Task.FromException<bool>(ex);
			}
			return task;
		}

		// Token: 0x0600285E RID: 10334 RVA: 0x000B2044 File Offset: 0x000B0244
		public virtual Task CloseAsync()
		{
			Task task;
			try
			{
				this.Close();
				task = Task.CompletedTask;
			}
			catch (Exception ex)
			{
				task = Task.FromException(ex);
			}
			return task;
		}

		// Token: 0x0600285F RID: 10335 RVA: 0x000B2078 File Offset: 0x000B0278
		public virtual ValueTask DisposeAsync()
		{
			this.Dispose();
			return default(ValueTask);
		}
	}
}
