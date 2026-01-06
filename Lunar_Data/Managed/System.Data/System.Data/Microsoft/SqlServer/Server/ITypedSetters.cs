using System;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200038E RID: 910
	internal interface ITypedSetters
	{
		// Token: 0x06002BA2 RID: 11170
		void SetDBNull(int ordinal);

		// Token: 0x06002BA3 RID: 11171
		void SetBoolean(int ordinal, bool value);

		// Token: 0x06002BA4 RID: 11172
		void SetByte(int ordinal, byte value);

		// Token: 0x06002BA5 RID: 11173
		void SetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length);

		// Token: 0x06002BA6 RID: 11174
		void SetChar(int ordinal, char value);

		// Token: 0x06002BA7 RID: 11175
		void SetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length);

		// Token: 0x06002BA8 RID: 11176
		void SetInt16(int ordinal, short value);

		// Token: 0x06002BA9 RID: 11177
		void SetInt32(int ordinal, int value);

		// Token: 0x06002BAA RID: 11178
		void SetInt64(int ordinal, long value);

		// Token: 0x06002BAB RID: 11179
		void SetFloat(int ordinal, float value);

		// Token: 0x06002BAC RID: 11180
		void SetDouble(int ordinal, double value);

		// Token: 0x06002BAD RID: 11181
		[Obsolete("Not supported as of SMI v2.  Will be removed when v1 support dropped.  Use setter with offset.")]
		void SetString(int ordinal, string value);

		// Token: 0x06002BAE RID: 11182
		void SetString(int ordinal, string value, int offset);

		// Token: 0x06002BAF RID: 11183
		void SetDecimal(int ordinal, decimal value);

		// Token: 0x06002BB0 RID: 11184
		void SetDateTime(int ordinal, DateTime value);

		// Token: 0x06002BB1 RID: 11185
		void SetGuid(int ordinal, Guid value);

		// Token: 0x06002BB2 RID: 11186
		void SetSqlBoolean(int ordinal, SqlBoolean value);

		// Token: 0x06002BB3 RID: 11187
		void SetSqlByte(int ordinal, SqlByte value);

		// Token: 0x06002BB4 RID: 11188
		void SetSqlInt16(int ordinal, SqlInt16 value);

		// Token: 0x06002BB5 RID: 11189
		void SetSqlInt32(int ordinal, SqlInt32 value);

		// Token: 0x06002BB6 RID: 11190
		void SetSqlInt64(int ordinal, SqlInt64 value);

		// Token: 0x06002BB7 RID: 11191
		void SetSqlSingle(int ordinal, SqlSingle value);

		// Token: 0x06002BB8 RID: 11192
		void SetSqlDouble(int ordinal, SqlDouble value);

		// Token: 0x06002BB9 RID: 11193
		void SetSqlMoney(int ordinal, SqlMoney value);

		// Token: 0x06002BBA RID: 11194
		void SetSqlDateTime(int ordinal, SqlDateTime value);

		// Token: 0x06002BBB RID: 11195
		void SetSqlDecimal(int ordinal, SqlDecimal value);

		// Token: 0x06002BBC RID: 11196
		[Obsolete("Not supported as of SMI v2.  Will be removed when v1 support dropped.  Use setter with offset.")]
		void SetSqlString(int ordinal, SqlString value);

		// Token: 0x06002BBD RID: 11197
		void SetSqlString(int ordinal, SqlString value, int offset);

		// Token: 0x06002BBE RID: 11198
		[Obsolete("Not supported as of SMI v2.  Will be removed when v1 support dropped.  Use setter with offset.")]
		void SetSqlBinary(int ordinal, SqlBinary value);

		// Token: 0x06002BBF RID: 11199
		void SetSqlBinary(int ordinal, SqlBinary value, int offset);

		// Token: 0x06002BC0 RID: 11200
		void SetSqlGuid(int ordinal, SqlGuid value);

		// Token: 0x06002BC1 RID: 11201
		[Obsolete("Not supported as of SMI v2.  Will be removed when v1 support dropped.  Use setter with offset.")]
		void SetSqlChars(int ordinal, SqlChars value);

		// Token: 0x06002BC2 RID: 11202
		void SetSqlChars(int ordinal, SqlChars value, int offset);

		// Token: 0x06002BC3 RID: 11203
		[Obsolete("Not supported as of SMI v2.  Will be removed when v1 support dropped.  Use setter with offset.")]
		void SetSqlBytes(int ordinal, SqlBytes value);

		// Token: 0x06002BC4 RID: 11204
		void SetSqlBytes(int ordinal, SqlBytes value, int offset);

		// Token: 0x06002BC5 RID: 11205
		void SetSqlXml(int ordinal, SqlXml value);
	}
}
