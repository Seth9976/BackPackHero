using System;

namespace System.Data
{
	/// <summary>Provides access to the column values within each row for a DataReader, and is implemented by .NET Framework data providers that access relational databases.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000B1 RID: 177
	public interface IDataRecord
	{
		/// <summary>Gets the number of columns in the current row.</summary>
		/// <returns>When not positioned in a valid recordset, 0; otherwise, the number of columns in the current record. The default is -1.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000B30 RID: 2864
		int FieldCount { get; }

		/// <summary>Gets the column located at the specified index.</summary>
		/// <returns>The column located at the specified index as an <see cref="T:System.Object" />.</returns>
		/// <param name="i">The zero-based index of the column to get. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001F9 RID: 505
		object this[int i] { get; }

		/// <summary>Gets the column with the specified name.</summary>
		/// <returns>The column with the specified name as an <see cref="T:System.Object" />.</returns>
		/// <param name="name">The name of the column to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">No column with the specified name was found. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001FA RID: 506
		object this[string name] { get; }

		/// <summary>Gets the name for the field to find.</summary>
		/// <returns>The name of the field or the empty string (""), if there is no value to return.</returns>
		/// <param name="i">The index of the field to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B33 RID: 2867
		string GetName(int i);

		/// <summary>Gets the data type information for the specified field.</summary>
		/// <returns>The data type information for the specified field.</returns>
		/// <param name="i">The index of the field to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B34 RID: 2868
		string GetDataTypeName(int i);

		/// <summary>Gets the <see cref="T:System.Type" /> information corresponding to the type of <see cref="T:System.Object" /> that would be returned from <see cref="M:System.Data.IDataRecord.GetValue(System.Int32)" />.</summary>
		/// <returns>The <see cref="T:System.Type" /> information corresponding to the type of <see cref="T:System.Object" /> that would be returned from <see cref="M:System.Data.IDataRecord.GetValue(System.Int32)" />.</returns>
		/// <param name="i">The index of the field to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B35 RID: 2869
		Type GetFieldType(int i);

		/// <summary>Return the value of the specified field.</summary>
		/// <returns>The <see cref="T:System.Object" /> which will contain the field value upon return.</returns>
		/// <param name="i">The index of the field to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B36 RID: 2870
		object GetValue(int i);

		/// <summary>Populates an array of objects with the column values of the current record.</summary>
		/// <returns>The number of instances of <see cref="T:System.Object" /> in the array.</returns>
		/// <param name="values">An array of <see cref="T:System.Object" /> to copy the attribute fields into. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B37 RID: 2871
		int GetValues(object[] values);

		/// <summary>Return the index of the named field.</summary>
		/// <returns>The index of the named field.</returns>
		/// <param name="name">The name of the field to find. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B38 RID: 2872
		int GetOrdinal(string name);

		/// <summary>Gets the value of the specified column as a Boolean.</summary>
		/// <returns>The value of the column.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B39 RID: 2873
		bool GetBoolean(int i);

		/// <summary>Gets the 8-bit unsigned integer value of the specified column.</summary>
		/// <returns>The 8-bit unsigned integer value of the specified column.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B3A RID: 2874
		byte GetByte(int i);

		/// <summary>Reads a stream of bytes from the specified column offset into the buffer as an array, starting at the given buffer offset.</summary>
		/// <returns>The actual number of bytes read.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <param name="fieldOffset">The index within the field from which to start the read operation. </param>
		/// <param name="buffer">The buffer into which to read the stream of bytes. </param>
		/// <param name="bufferoffset">The index for <paramref name="buffer" /> to start the read operation. </param>
		/// <param name="length">The number of bytes to read. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B3B RID: 2875
		long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length);

		/// <summary>Gets the character value of the specified column.</summary>
		/// <returns>The character value of the specified column.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B3C RID: 2876
		char GetChar(int i);

		/// <summary>Reads a stream of characters from the specified column offset into the buffer as an array, starting at the given buffer offset.</summary>
		/// <returns>The actual number of characters read.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <param name="fieldoffset">The index within the row from which to start the read operation. </param>
		/// <param name="buffer">The buffer into which to read the stream of bytes. </param>
		/// <param name="bufferoffset">The index for <paramref name="buffer" /> to start the read operation. </param>
		/// <param name="length">The number of bytes to read. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B3D RID: 2877
		long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length);

		/// <summary>Returns the GUID value of the specified field.</summary>
		/// <returns>The GUID value of the specified field.</returns>
		/// <param name="i">The index of the field to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B3E RID: 2878
		Guid GetGuid(int i);

		/// <summary>Gets the 16-bit signed integer value of the specified field.</summary>
		/// <returns>The 16-bit signed integer value of the specified field.</returns>
		/// <param name="i">The index of the field to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B3F RID: 2879
		short GetInt16(int i);

		/// <summary>Gets the 32-bit signed integer value of the specified field.</summary>
		/// <returns>The 32-bit signed integer value of the specified field.</returns>
		/// <param name="i">The index of the field to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B40 RID: 2880
		int GetInt32(int i);

		/// <summary>Gets the 64-bit signed integer value of the specified field.</summary>
		/// <returns>The 64-bit signed integer value of the specified field.</returns>
		/// <param name="i">The index of the field to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B41 RID: 2881
		long GetInt64(int i);

		/// <summary>Gets the single-precision floating point number of the specified field.</summary>
		/// <returns>The single-precision floating point number of the specified field.</returns>
		/// <param name="i">The index of the field to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B42 RID: 2882
		float GetFloat(int i);

		/// <summary>Gets the double-precision floating point number of the specified field.</summary>
		/// <returns>The double-precision floating point number of the specified field.</returns>
		/// <param name="i">The index of the field to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B43 RID: 2883
		double GetDouble(int i);

		/// <summary>Gets the string value of the specified field.</summary>
		/// <returns>The string value of the specified field.</returns>
		/// <param name="i">The index of the field to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B44 RID: 2884
		string GetString(int i);

		/// <summary>Gets the fixed-position numeric value of the specified field.</summary>
		/// <returns>The fixed-position numeric value of the specified field.</returns>
		/// <param name="i">The index of the field to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B45 RID: 2885
		decimal GetDecimal(int i);

		/// <summary>Gets the date and time data value of the specified field.</summary>
		/// <returns>The date and time data value of the specified field.</returns>
		/// <param name="i">The index of the field to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B46 RID: 2886
		DateTime GetDateTime(int i);

		/// <summary>Returns an <see cref="T:System.Data.IDataReader" /> for the specified column ordinal.</summary>
		/// <returns>The <see cref="T:System.Data.IDataReader" /> for the specified column ordinal.</returns>
		/// <param name="i">The index of the field to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B47 RID: 2887
		IDataReader GetData(int i);

		/// <summary>Return whether the specified field is set to null.</summary>
		/// <returns>true if the specified field is set to null; otherwise, false.</returns>
		/// <param name="i">The index of the field to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B48 RID: 2888
		bool IsDBNull(int i);
	}
}
