using System;
using System.Data;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Represents a single row of data and its metadata. This class cannot be inherited.</summary>
	// Token: 0x020003A6 RID: 934
	public class SqlDataRecord : IDataRecord
	{
		/// <summary>Gets the number of columns in the data row. This property is read-only.</summary>
		/// <returns>The number of columns in the data row as an integer.</returns>
		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06002CF6 RID: 11510 RVA: 0x000C26C0 File Offset: 0x000C08C0
		public virtual int FieldCount
		{
			get
			{
				this.EnsureSubclassOverride();
				return this._columnMetaData.Length;
			}
		}

		/// <summary>Returns the name of the column specified by the ordinal argument.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the column name.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />). </exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002CF7 RID: 11511 RVA: 0x000C26D0 File Offset: 0x000C08D0
		public virtual string GetName(int ordinal)
		{
			this.EnsureSubclassOverride();
			return this.GetSqlMetaData(ordinal).Name;
		}

		/// <summary>Returns the name of the data type for the column specified by the ordinal argument.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the data type of the column.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />). </exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002CF8 RID: 11512 RVA: 0x000C26E4 File Offset: 0x000C08E4
		public virtual string GetDataTypeName(int ordinal)
		{
			this.EnsureSubclassOverride();
			SqlMetaData sqlMetaData = this.GetSqlMetaData(ordinal);
			if (SqlDbType.Udt == sqlMetaData.SqlDbType)
			{
				return sqlMetaData.UdtTypeName;
			}
			return MetaType.GetMetaTypeFromSqlDbType(sqlMetaData.SqlDbType, false).TypeName;
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object representing the common language runtime (CLR) type that maps to the SQL Server type of the column specified by the <paramref name="ordinal" /> argument.</summary>
		/// <returns>The column type as a <see cref="T:System.Type" /> object.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />). </exception>
		/// <exception cref="T:System.TypeLoadException">The column is of a user-defined type that is not available to the calling application domain.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The column is of a user-defined type that is not available to the calling application domain.</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002CF9 RID: 11513 RVA: 0x000C2721 File Offset: 0x000C0921
		public virtual Type GetFieldType(int ordinal)
		{
			this.EnsureSubclassOverride();
			return MetaType.GetMetaTypeFromSqlDbType(this.GetSqlMetaData(ordinal).SqlDbType, false).ClassType;
		}

		/// <summary>Returns the common language runtime (CLR) type value for the column specified by the ordinal argument.</summary>
		/// <returns>The CLR type value of the column specified by the ordinal.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002CFA RID: 11514 RVA: 0x000C2740 File Offset: 0x000C0940
		public virtual object GetValue(int ordinal)
		{
			this.EnsureSubclassOverride();
			SmiMetaData smiMetaData = this.GetSmiMetaData(ordinal);
			return ValueUtilsSmi.GetValue200(this._eventSink, this._recordBuffer, ordinal, smiMetaData);
		}

		/// <summary>Returns the values for all the columns in the record, expressed as common language runtime (CLR) types, in an array.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that indicates the number of columns copied.</returns>
		/// <param name="values">The array into which to copy the values column values.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="values" /> is null.</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002CFB RID: 11515 RVA: 0x000C2770 File Offset: 0x000C0970
		public virtual int GetValues(object[] values)
		{
			this.EnsureSubclassOverride();
			if (values == null)
			{
				throw ADP.ArgumentNull("values");
			}
			int num = ((values.Length < this.FieldCount) ? values.Length : this.FieldCount);
			for (int i = 0; i < num; i++)
			{
				values[i] = this.GetValue(i);
			}
			return num;
		}

		/// <summary>Returns the column ordinal specified by the column name.</summary>
		/// <returns>The zero-based ordinal of the column as an integer.</returns>
		/// <param name="name">The name of the column to look up.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">The column name could not be found.</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002CFC RID: 11516 RVA: 0x000C27C0 File Offset: 0x000C09C0
		public virtual int GetOrdinal(string name)
		{
			this.EnsureSubclassOverride();
			if (this._fieldNameLookup == null)
			{
				string[] array = new string[this.FieldCount];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.GetSqlMetaData(i).Name;
				}
				this._fieldNameLookup = new FieldNameLookup(array, -1);
			}
			return this._fieldNameLookup.GetOrdinal(name);
		}

		/// <summary>Gets the common language runtime (CLR) type value for the column specified by the column <paramref name="ordinal" /> argument.</summary>
		/// <returns>The CLR type value of the column specified by the <paramref name="ordinal" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x17000771 RID: 1905
		public virtual object this[int ordinal]
		{
			get
			{
				this.EnsureSubclassOverride();
				return this.GetValue(ordinal);
			}
		}

		/// <summary>Gets the common language runtime (CLR) type value for the column specified by the column <paramref name="name" /> argument.</summary>
		/// <returns>The CLR type value of the column specified by the <paramref name="name" />.</returns>
		/// <param name="name">The name of the column.</param>
		// Token: 0x17000772 RID: 1906
		public virtual object this[string name]
		{
			get
			{
				this.EnsureSubclassOverride();
				return this.GetValue(this.GetOrdinal(name));
			}
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Boolean" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Boolean" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The column specified by <paramref name="ordinal" /> is null.</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002CFF RID: 11519 RVA: 0x000C2841 File Offset: 0x000C0A41
		public virtual bool GetBoolean(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetBoolean(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Byte" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Byte" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The column specified by <paramref name="ordinal" /> is null.</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002D00 RID: 11520 RVA: 0x000C2862 File Offset: 0x000C0A62
		public virtual byte GetByte(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetByte(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as an array of <see cref="T:System.Byte" /> objects.</summary>
		/// <returns>The number of bytes copied.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="fieldOffset">The offset into the field value to start retrieving bytes.</param>
		/// <param name="buffer">The target buffer to which to copy bytes.</param>
		/// <param name="bufferOffset">The offset into the buffer to which to start copying bytes.</param>
		/// <param name="length">The number of bytes to copy to the buffer.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The column specified by <paramref name="ordinal" /> is null.</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002D01 RID: 11521 RVA: 0x000C2884 File Offset: 0x000C0A84
		public virtual long GetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetBytes(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), fieldOffset, buffer, bufferOffset, length, true);
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Char" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Char" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The column specified by <paramref name="ordinal" /> is null.</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002D02 RID: 11522 RVA: 0x000C28B7 File Offset: 0x000C0AB7
		public virtual char GetChar(int ordinal)
		{
			this.EnsureSubclassOverride();
			throw ADP.NotSupported();
		}

		/// <summary>Gets the value for the column specified by the ordinal as an array of <see cref="T:System.Char" /> objects.</summary>
		/// <returns>The number of characters copied.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="fieldOffset">The offset into the field value to start retrieving characters.</param>
		/// <param name="buffer">The target buffer to copy chars to.</param>
		/// <param name="bufferOffset">The offset into the buffer to start copying chars to.</param>
		/// <param name="length">The number of chars to copy to the buffer.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The column specified by <paramref name="ordinal" /> is null.</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002D03 RID: 11523 RVA: 0x000C28C4 File Offset: 0x000C0AC4
		public virtual long GetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetChars(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), fieldOffset, buffer, bufferOffset, length);
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Guid" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Guid" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The column specified by <paramref name="ordinal" /> is null.</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002D04 RID: 11524 RVA: 0x000C28EB File Offset: 0x000C0AEB
		public virtual Guid GetGuid(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetGuid(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Int16" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Int16" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The column specified by <paramref name="ordinal" /> is null.</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002D05 RID: 11525 RVA: 0x000C290C File Offset: 0x000C0B0C
		public virtual short GetInt16(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetInt16(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Int32" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Int32" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The column specified by <paramref name="ordinal" /> is null.</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002D06 RID: 11526 RVA: 0x000C292D File Offset: 0x000C0B2D
		public virtual int GetInt32(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetInt32(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Int64" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Int64" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The column specified by <paramref name="ordinal" /> is null.</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002D07 RID: 11527 RVA: 0x000C294E File Offset: 0x000C0B4E
		public virtual long GetInt64(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetInt64(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a float.</summary>
		/// <returns>The column value as a float.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The column specified by <paramref name="ordinal" /> is null.</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002D08 RID: 11528 RVA: 0x000C296F File Offset: 0x000C0B6F
		public virtual float GetFloat(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSingle(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Double" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Double" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The column specified by <paramref name="ordinal" /> is null.</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002D09 RID: 11529 RVA: 0x000C2990 File Offset: 0x000C0B90
		public virtual double GetDouble(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetDouble(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.String" />.</summary>
		/// <returns>The column value as a <see cref="T:System.String" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002D0A RID: 11530 RVA: 0x000C29B4 File Offset: 0x000C0BB4
		public virtual string GetString(int ordinal)
		{
			this.EnsureSubclassOverride();
			SmiMetaData smiMetaData = this.GetSmiMetaData(ordinal);
			if (this._usesStringStorageForXml && SqlDbType.Xml == smiMetaData.SqlDbType)
			{
				return ValueUtilsSmi.GetString(this._eventSink, this._recordBuffer, ordinal, SqlDataRecord.s_maxNVarCharForXml);
			}
			return ValueUtilsSmi.GetString(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Decimal" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Decimal" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The column specified by <paramref name="ordinal" /> is null.</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002D0B RID: 11531 RVA: 0x000C2A12 File Offset: 0x000C0C12
		public virtual decimal GetDecimal(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetDecimal(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.DateTime" />.</summary>
		/// <returns>The column value as a <see cref="T:System.DateTime" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The column specified by <paramref name="ordinal" /> is null.</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002D0C RID: 11532 RVA: 0x000C2A33 File Offset: 0x000C0C33
		public virtual DateTime GetDateTime(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetDateTime(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Returns the specified column’s data as a <see cref="T:System.DateTimeOffset" />.</summary>
		/// <returns>The value of the specified column as a <see cref="T:System.DateTimeOffset" />.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		// Token: 0x06002D0D RID: 11533 RVA: 0x000C2A54 File Offset: 0x000C0C54
		public virtual DateTimeOffset GetDateTimeOffset(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetDateTimeOffset(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Returns the specified column’s data as a <see cref="T:System.TimeSpan" />.</summary>
		/// <returns>The value of the specified column as a <see cref="T:System.TimeSpan" />.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		// Token: 0x06002D0E RID: 11534 RVA: 0x000C2A75 File Offset: 0x000C0C75
		public virtual TimeSpan GetTimeSpan(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetTimeSpan(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Returns true if the column specified by the column ordinal parameter is null.</summary>
		/// <returns>true if the column is null; false otherwise.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002D0F RID: 11535 RVA: 0x000C2A96 File Offset: 0x000C0C96
		public virtual bool IsDBNull(int ordinal)
		{
			this.EnsureSubclassOverride();
			this.ThrowIfInvalidOrdinal(ordinal);
			return ValueUtilsSmi.IsDBNull(this._eventSink, this._recordBuffer, ordinal);
		}

		/// <summary>Returns a <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> object, describing the metadata of the column specified by the column ordinal.</summary>
		/// <returns>The column metadata as a <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> object.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002D10 RID: 11536 RVA: 0x000C2AB7 File Offset: 0x000C0CB7
		public virtual SqlMetaData GetSqlMetaData(int ordinal)
		{
			this.EnsureSubclassOverride();
			return this._columnMetaData[ordinal];
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents the type (as a SQL Server type, defined in <see cref="N:System.Data.SqlTypes" />) that maps to the SQL Server type of the column.</summary>
		/// <returns>The column type as a <see cref="T:System.Type" /> object.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />). </exception>
		/// <exception cref="T:System.TypeLoadException">The column is of a user-defined type that is not available to the calling application domain.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The column is of a user-defined type that is not available to the calling application domain.</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002D11 RID: 11537 RVA: 0x000C2AC7 File Offset: 0x000C0CC7
		public virtual Type GetSqlFieldType(int ordinal)
		{
			this.EnsureSubclassOverride();
			return MetaType.GetMetaTypeFromSqlDbType(this.GetSqlMetaData(ordinal).SqlDbType, false).SqlType;
		}

		/// <summary>Returns the data value stored in the column, expressed as a SQL Server type, specified by the column ordinal.</summary>
		/// <returns>The value of the column, expressed as a SQL Server type, as a <see cref="T:System.Object" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002D12 RID: 11538 RVA: 0x000C2AE8 File Offset: 0x000C0CE8
		public virtual object GetSqlValue(int ordinal)
		{
			this.EnsureSubclassOverride();
			SmiMetaData smiMetaData = this.GetSmiMetaData(ordinal);
			return ValueUtilsSmi.GetSqlValue200(this._eventSink, this._recordBuffer, ordinal, smiMetaData);
		}

		/// <summary>Returns the values for all the columns in the record, expressed as SQL Server types, in an array.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that indicates the number of columns copied.</returns>
		/// <param name="values">The array into which to copy the values column values. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="values" /> is null.</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002D13 RID: 11539 RVA: 0x000C2B18 File Offset: 0x000C0D18
		public virtual int GetSqlValues(object[] values)
		{
			this.EnsureSubclassOverride();
			if (values == null)
			{
				throw ADP.ArgumentNull("values");
			}
			int num = ((values.Length < this.FieldCount) ? values.Length : this.FieldCount);
			for (int i = 0; i < num; i++)
			{
				values[i] = this.GetSqlValue(i);
			}
			return num;
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Data.SqlTypes.SqlBinary" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Data.SqlTypes.SqlBinary" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002D14 RID: 11540 RVA: 0x000C2B67 File Offset: 0x000C0D67
		public virtual SqlBinary GetSqlBinary(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlBinary(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Data.SqlTypes.SqlBytes" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Data.SqlTypes.SqlBytes" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002D15 RID: 11541 RVA: 0x000C2B88 File Offset: 0x000C0D88
		public virtual SqlBytes GetSqlBytes(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlBytes(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Data.SqlTypes.SqlXml" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Data.SqlTypes.SqlXml" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002D16 RID: 11542 RVA: 0x000C2BA9 File Offset: 0x000C0DA9
		public virtual SqlXml GetSqlXml(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlXml(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002D17 RID: 11543 RVA: 0x000C2BCA File Offset: 0x000C0DCA
		public virtual SqlBoolean GetSqlBoolean(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlBoolean(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Data.SqlTypes.SqlByte" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002D18 RID: 11544 RVA: 0x000C2BEB File Offset: 0x000C0DEB
		public virtual SqlByte GetSqlByte(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlByte(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Data.SqlTypes.SqlChars" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Data.SqlTypes.SqlChars" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002D19 RID: 11545 RVA: 0x000C2C0C File Offset: 0x000C0E0C
		public virtual SqlChars GetSqlChars(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlChars(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Data.SqlTypes.SqlInt16" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002D1A RID: 11546 RVA: 0x000C2C2D File Offset: 0x000C0E2D
		public virtual SqlInt16 GetSqlInt16(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlInt16(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Data.SqlTypes.SqlInt32" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002D1B RID: 11547 RVA: 0x000C2C4E File Offset: 0x000C0E4E
		public virtual SqlInt32 GetSqlInt32(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlInt32(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Data.SqlTypes.SqlInt64" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002D1C RID: 11548 RVA: 0x000C2C6F File Offset: 0x000C0E6F
		public virtual SqlInt64 GetSqlInt64(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlInt64(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Data.SqlTypes.SqlSingle" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Data.SqlTypes.SqlSingle" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002D1D RID: 11549 RVA: 0x000C2C90 File Offset: 0x000C0E90
		public virtual SqlSingle GetSqlSingle(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlSingle(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Data.SqlTypes.SqlDouble" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Data.SqlTypes.SqlDouble" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002D1E RID: 11550 RVA: 0x000C2CB1 File Offset: 0x000C0EB1
		public virtual SqlDouble GetSqlDouble(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlDouble(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Data.SqlTypes.SqlMoney" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Data.SqlTypes.SqlMoney" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002D1F RID: 11551 RVA: 0x000C2CD2 File Offset: 0x000C0ED2
		public virtual SqlMoney GetSqlMoney(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlMoney(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Data.SqlTypes.SqlDateTime" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Data.SqlTypes.SqlDateTime" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002D20 RID: 11552 RVA: 0x000C2CF3 File Offset: 0x000C0EF3
		public virtual SqlDateTime GetSqlDateTime(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlDateTime(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002D21 RID: 11553 RVA: 0x000C2D14 File Offset: 0x000C0F14
		public virtual SqlDecimal GetSqlDecimal(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlDecimal(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Data.SqlTypes.SqlString" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002D22 RID: 11554 RVA: 0x000C2D35 File Offset: 0x000C0F35
		public virtual SqlString GetSqlString(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlString(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Data.SqlTypes.SqlGuid" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Data.SqlTypes.SqlGuid" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002D23 RID: 11555 RVA: 0x000C2D56 File Offset: 0x000C0F56
		public virtual SqlGuid GetSqlGuid(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlGuid(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Sets new values for all of the columns in the <see cref="T:Microsoft.SqlServer.Server.SqlDataRecord" />. These values are expressed as common language runtime (CLR) types.</summary>
		/// <returns>The number of column values set as an integer.</returns>
		/// <param name="values">The array of new values, expressed as CLR types boxed as <see cref="T:System.Object" /> references, for the <see cref="T:Microsoft.SqlServer.Server.SqlDataRecord" /> instance.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="values" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The size of values does not match the number of columns in the <see cref="T:Microsoft.SqlServer.Server.SqlDataRecord" /> instance.</exception>
		// Token: 0x06002D24 RID: 11556 RVA: 0x000C2D78 File Offset: 0x000C0F78
		public virtual int SetValues(params object[] values)
		{
			this.EnsureSubclassOverride();
			if (values == null)
			{
				throw ADP.ArgumentNull("values");
			}
			int num = ((values.Length > this.FieldCount) ? this.FieldCount : values.Length);
			ExtendedClrTypeCode[] array = new ExtendedClrTypeCode[num];
			for (int i = 0; i < num; i++)
			{
				SqlMetaData sqlMetaData = this.GetSqlMetaData(i);
				array[i] = MetaDataUtilsSmi.DetermineExtendedTypeCodeForUseWithSqlDbType(sqlMetaData.SqlDbType, false, values[i], sqlMetaData.Type);
				if (ExtendedClrTypeCode.Invalid == array[i])
				{
					throw ADP.InvalidCast();
				}
			}
			for (int j = 0; j < num; j++)
			{
				ValueUtilsSmi.SetCompatibleValueV200(this._eventSink, this._recordBuffer, j, this.GetSmiMetaData(j), values[j], array[j], 0, 0, null);
			}
			return num;
		}

		/// <summary>Sets a new value, expressed as a common language runtime (CLR) type, for the column specified by the column ordinal.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value for the specified column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002D25 RID: 11557 RVA: 0x000C2E28 File Offset: 0x000C1028
		public virtual void SetValue(int ordinal, object value)
		{
			this.EnsureSubclassOverride();
			SqlMetaData sqlMetaData = this.GetSqlMetaData(ordinal);
			ExtendedClrTypeCode extendedClrTypeCode = MetaDataUtilsSmi.DetermineExtendedTypeCodeForUseWithSqlDbType(sqlMetaData.SqlDbType, false, value, sqlMetaData.Type);
			if (ExtendedClrTypeCode.Invalid == extendedClrTypeCode)
			{
				throw ADP.InvalidCast();
			}
			ValueUtilsSmi.SetCompatibleValueV200(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value, extendedClrTypeCode, 0, 0, null);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Boolean" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		// Token: 0x06002D26 RID: 11558 RVA: 0x000C2E7F File Offset: 0x000C107F
		public virtual void SetBoolean(int ordinal, bool value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetBoolean(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Byte" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002D27 RID: 11559 RVA: 0x000C2EA1 File Offset: 0x000C10A1
		public virtual void SetByte(int ordinal, byte value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetByte(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified array of <see cref="T:System.Byte" /> values.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="fieldOffset">The offset into the field value to start copying bytes.</param>
		/// <param name="buffer">The target buffer from which to copy bytes.</param>
		/// <param name="bufferOffset">The offset into the buffer from which to start copying bytes.</param>
		/// <param name="length">The number of bytes to copy from the buffer.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002D28 RID: 11560 RVA: 0x000C2EC3 File Offset: 0x000C10C3
		public virtual void SetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetBytes(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), fieldOffset, buffer, bufferOffset, length);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Char" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002D29 RID: 11561 RVA: 0x000C28B7 File Offset: 0x000C0AB7
		public virtual void SetChar(int ordinal, char value)
		{
			this.EnsureSubclassOverride();
			throw ADP.NotSupported();
		}

		/// <summary>Sets the data stored in the column to the specified array of <see cref="T:System.Char" /> values.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="fieldOffset">The offset into the field value to start copying characters.</param>
		/// <param name="buffer">The target buffer from which to copy chars.</param>
		/// <param name="bufferOffset">The offset into the buffer from which to start copying chars.</param>
		/// <param name="length">The number of chars to copy from the buffer.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002D2A RID: 11562 RVA: 0x000C2EEB File Offset: 0x000C10EB
		public virtual void SetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetChars(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), fieldOffset, buffer, bufferOffset, length);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Int16" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002D2B RID: 11563 RVA: 0x000C2F13 File Offset: 0x000C1113
		public virtual void SetInt16(int ordinal, short value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetInt16(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Int32" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002D2C RID: 11564 RVA: 0x000C2F35 File Offset: 0x000C1135
		public virtual void SetInt32(int ordinal, int value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetInt32(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Int64" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002D2D RID: 11565 RVA: 0x000C2F57 File Offset: 0x000C1157
		public virtual void SetInt64(int ordinal, long value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetInt64(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified float value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002D2E RID: 11566 RVA: 0x000C2F79 File Offset: 0x000C1179
		public virtual void SetFloat(int ordinal, float value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSingle(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Double" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002D2F RID: 11567 RVA: 0x000C2F9B File Offset: 0x000C119B
		public virtual void SetDouble(int ordinal, double value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetDouble(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.String" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002D30 RID: 11568 RVA: 0x000C2FBD File Offset: 0x000C11BD
		public virtual void SetString(int ordinal, string value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetString(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Decimal" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002D31 RID: 11569 RVA: 0x000C2FDF File Offset: 0x000C11DF
		public virtual void SetDecimal(int ordinal, decimal value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetDecimal(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.DateTime" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002D32 RID: 11570 RVA: 0x000C3001 File Offset: 0x000C1201
		public virtual void SetDateTime(int ordinal, DateTime value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetDateTime(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the value of the column specified to the <see cref="T:System.TimeSpan" />.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> passed in is a negative number.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.TimeSpan" /> value passed in is greater than 24 hours in length.</exception>
		// Token: 0x06002D33 RID: 11571 RVA: 0x000C3023 File Offset: 0x000C1223
		public virtual void SetTimeSpan(int ordinal, TimeSpan value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetTimeSpan(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the value of the column specified to the <see cref="T:System.DateTimeOffset" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		// Token: 0x06002D34 RID: 11572 RVA: 0x000C3045 File Offset: 0x000C1245
		public virtual void SetDateTimeOffset(int ordinal, DateTimeOffset value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetDateTimeOffset(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the value in the specified column to <see cref="T:System.DBNull" />.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		// Token: 0x06002D35 RID: 11573 RVA: 0x000C3067 File Offset: 0x000C1267
		public virtual void SetDBNull(int ordinal)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetDBNull(this._eventSink, this._recordBuffer, ordinal, true);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Guid" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002D36 RID: 11574 RVA: 0x000C3082 File Offset: 0x000C1282
		public virtual void SetGuid(int ordinal, Guid value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetGuid(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Data.SqlTypes.SqlBoolean" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002D37 RID: 11575 RVA: 0x000C30A4 File Offset: 0x000C12A4
		public virtual void SetSqlBoolean(int ordinal, SqlBoolean value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlBoolean(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Data.SqlTypes.SqlByte" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002D38 RID: 11576 RVA: 0x000C30C6 File Offset: 0x000C12C6
		public virtual void SetSqlByte(int ordinal, SqlByte value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlByte(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Data.SqlTypes.SqlInt16" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002D39 RID: 11577 RVA: 0x000C30E8 File Offset: 0x000C12E8
		public virtual void SetSqlInt16(int ordinal, SqlInt16 value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlInt16(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Data.SqlTypes.SqlInt32" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002D3A RID: 11578 RVA: 0x000C310A File Offset: 0x000C130A
		public virtual void SetSqlInt32(int ordinal, SqlInt32 value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlInt32(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Data.SqlTypes.SqlInt64" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002D3B RID: 11579 RVA: 0x000C312C File Offset: 0x000C132C
		public virtual void SetSqlInt64(int ordinal, SqlInt64 value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlInt64(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Data.SqlTypes.SqlSingle" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002D3C RID: 11580 RVA: 0x000C314E File Offset: 0x000C134E
		public virtual void SetSqlSingle(int ordinal, SqlSingle value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlSingle(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Data.SqlTypes.SqlDouble" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002D3D RID: 11581 RVA: 0x000C3170 File Offset: 0x000C1370
		public virtual void SetSqlDouble(int ordinal, SqlDouble value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlDouble(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Data.SqlTypes.SqlMoney" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002D3E RID: 11582 RVA: 0x000C3192 File Offset: 0x000C1392
		public virtual void SetSqlMoney(int ordinal, SqlMoney value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlMoney(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Data.SqlTypes.SqlDateTime" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002D3F RID: 11583 RVA: 0x000C31B4 File Offset: 0x000C13B4
		public virtual void SetSqlDateTime(int ordinal, SqlDateTime value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlDateTime(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Data.SqlTypes.SqlXml" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002D40 RID: 11584 RVA: 0x000C31D6 File Offset: 0x000C13D6
		public virtual void SetSqlXml(int ordinal, SqlXml value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlXml(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Data.SqlTypes.SqlDecimal" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002D41 RID: 11585 RVA: 0x000C31F8 File Offset: 0x000C13F8
		public virtual void SetSqlDecimal(int ordinal, SqlDecimal value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlDecimal(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Data.SqlTypes.SqlString" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002D42 RID: 11586 RVA: 0x000C321A File Offset: 0x000C141A
		public virtual void SetSqlString(int ordinal, SqlString value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlString(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Data.SqlTypes.SqlBinary" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002D43 RID: 11587 RVA: 0x000C323C File Offset: 0x000C143C
		public virtual void SetSqlBinary(int ordinal, SqlBinary value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlBinary(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Data.SqlTypes.SqlGuid" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002D44 RID: 11588 RVA: 0x000C325E File Offset: 0x000C145E
		public virtual void SetSqlGuid(int ordinal, SqlGuid value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlGuid(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Data.SqlTypes.SqlChars" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002D45 RID: 11589 RVA: 0x000C3280 File Offset: 0x000C1480
		public virtual void SetSqlChars(int ordinal, SqlChars value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlChars(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Data.SqlTypes.SqlBytes" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002D46 RID: 11590 RVA: 0x000C32A2 File Offset: 0x000C14A2
		public virtual void SetSqlBytes(int ordinal, SqlBytes value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlBytes(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Inititializes a new <see cref="T:Microsoft.SqlServer.Server.SqlDataRecord" /> instance with the schema based on the array of <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> objects passed as an argument.</summary>
		/// <param name="metaData">An array of <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> objects that describe each column in the <see cref="T:Microsoft.SqlServer.Server.SqlDataRecord" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="metaData" /> is null. </exception>
		// Token: 0x06002D47 RID: 11591 RVA: 0x000C32C4 File Offset: 0x000C14C4
		public SqlDataRecord(params SqlMetaData[] metaData)
		{
			if (metaData == null)
			{
				throw ADP.ArgumentNull("metaData");
			}
			this._columnMetaData = new SqlMetaData[metaData.Length];
			this._columnSmiMetaData = new SmiExtendedMetaData[metaData.Length];
			for (int i = 0; i < this._columnSmiMetaData.Length; i++)
			{
				if (metaData[i] == null)
				{
					throw ADP.ArgumentNull(string.Format("{0}[{1}]", "metaData", i));
				}
				this._columnMetaData[i] = metaData[i];
				this._columnSmiMetaData[i] = MetaDataUtilsSmi.SqlMetaDataToSmiExtendedMetaData(this._columnMetaData[i]);
			}
			this._eventSink = new SmiEventSink_Default();
			SmiMetaData[] columnSmiMetaData = this._columnSmiMetaData;
			this._recordBuffer = new MemoryRecordBuffer(columnSmiMetaData);
			this._usesStringStorageForXml = true;
			this._eventSink.ProcessMessagesAndThrow();
		}

		// Token: 0x06002D48 RID: 11592 RVA: 0x000C3384 File Offset: 0x000C1584
		internal SqlDataRecord(SmiRecordBuffer recordBuffer, params SmiExtendedMetaData[] metaData)
		{
			this._columnMetaData = new SqlMetaData[metaData.Length];
			this._columnSmiMetaData = new SmiExtendedMetaData[metaData.Length];
			for (int i = 0; i < this._columnSmiMetaData.Length; i++)
			{
				this._columnSmiMetaData[i] = metaData[i];
				this._columnMetaData[i] = MetaDataUtilsSmi.SmiExtendedMetaDataToSqlMetaData(this._columnSmiMetaData[i]);
			}
			this._eventSink = new SmiEventSink_Default();
			this._recordBuffer = recordBuffer;
			this._eventSink.ProcessMessagesAndThrow();
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06002D49 RID: 11593 RVA: 0x000C3403 File Offset: 0x000C1603
		internal SmiRecordBuffer RecordBuffer
		{
			get
			{
				return this._recordBuffer;
			}
		}

		// Token: 0x06002D4A RID: 11594 RVA: 0x000C340B File Offset: 0x000C160B
		internal SqlMetaData[] InternalGetMetaData()
		{
			return this._columnMetaData;
		}

		// Token: 0x06002D4B RID: 11595 RVA: 0x000C3413 File Offset: 0x000C1613
		internal SmiExtendedMetaData[] InternalGetSmiMetaData()
		{
			return this._columnSmiMetaData;
		}

		// Token: 0x06002D4C RID: 11596 RVA: 0x000C341B File Offset: 0x000C161B
		internal SmiExtendedMetaData GetSmiMetaData(int ordinal)
		{
			return this._columnSmiMetaData[ordinal];
		}

		// Token: 0x06002D4D RID: 11597 RVA: 0x000C3425 File Offset: 0x000C1625
		internal void ThrowIfInvalidOrdinal(int ordinal)
		{
			if (0 > ordinal || this.FieldCount <= ordinal)
			{
				throw ADP.IndexOutOfRange(ordinal);
			}
		}

		// Token: 0x06002D4E RID: 11598 RVA: 0x000C343B File Offset: 0x000C163B
		private void EnsureSubclassOverride()
		{
			if (this._recordBuffer == null)
			{
				throw SQL.SubclassMustOverride();
			}
		}

		/// <summary>Not supported in this release.</summary>
		/// <returns>
		///   <see cref="T:System.Data.IDataReader" />Always throws an exception.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002D4F RID: 11599 RVA: 0x00060F32 File Offset: 0x0005F132
		IDataReader IDataRecord.GetData(int ordinal)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x04001B47 RID: 6983
		private SmiRecordBuffer _recordBuffer;

		// Token: 0x04001B48 RID: 6984
		private SmiExtendedMetaData[] _columnSmiMetaData;

		// Token: 0x04001B49 RID: 6985
		private SmiEventSink_Default _eventSink;

		// Token: 0x04001B4A RID: 6986
		private SqlMetaData[] _columnMetaData;

		// Token: 0x04001B4B RID: 6987
		private FieldNameLookup _fieldNameLookup;

		// Token: 0x04001B4C RID: 6988
		private bool _usesStringStorageForXml;

		// Token: 0x04001B4D RID: 6989
		private static readonly SmiMetaData s_maxNVarCharForXml = new SmiMetaData(SqlDbType.NVarChar, -1L, SmiMetaData.DefaultNVarChar_NoCollation.Precision, SmiMetaData.DefaultNVarChar_NoCollation.Scale, SmiMetaData.DefaultNVarChar.LocaleId, SmiMetaData.DefaultNVarChar.CompareOptions, null);
	}
}
