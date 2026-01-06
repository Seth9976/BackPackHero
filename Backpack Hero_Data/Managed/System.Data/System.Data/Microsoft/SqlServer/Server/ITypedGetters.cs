using System;
using System.Data;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200038C RID: 908
	internal interface ITypedGetters
	{
		// Token: 0x06002B6E RID: 11118
		bool IsDBNull(int ordinal);

		// Token: 0x06002B6F RID: 11119
		SqlDbType GetVariantType(int ordinal);

		// Token: 0x06002B70 RID: 11120
		bool GetBoolean(int ordinal);

		// Token: 0x06002B71 RID: 11121
		byte GetByte(int ordinal);

		// Token: 0x06002B72 RID: 11122
		long GetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length);

		// Token: 0x06002B73 RID: 11123
		char GetChar(int ordinal);

		// Token: 0x06002B74 RID: 11124
		long GetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length);

		// Token: 0x06002B75 RID: 11125
		short GetInt16(int ordinal);

		// Token: 0x06002B76 RID: 11126
		int GetInt32(int ordinal);

		// Token: 0x06002B77 RID: 11127
		long GetInt64(int ordinal);

		// Token: 0x06002B78 RID: 11128
		float GetFloat(int ordinal);

		// Token: 0x06002B79 RID: 11129
		double GetDouble(int ordinal);

		// Token: 0x06002B7A RID: 11130
		string GetString(int ordinal);

		// Token: 0x06002B7B RID: 11131
		decimal GetDecimal(int ordinal);

		// Token: 0x06002B7C RID: 11132
		DateTime GetDateTime(int ordinal);

		// Token: 0x06002B7D RID: 11133
		Guid GetGuid(int ordinal);

		// Token: 0x06002B7E RID: 11134
		SqlBoolean GetSqlBoolean(int ordinal);

		// Token: 0x06002B7F RID: 11135
		SqlByte GetSqlByte(int ordinal);

		// Token: 0x06002B80 RID: 11136
		SqlInt16 GetSqlInt16(int ordinal);

		// Token: 0x06002B81 RID: 11137
		SqlInt32 GetSqlInt32(int ordinal);

		// Token: 0x06002B82 RID: 11138
		SqlInt64 GetSqlInt64(int ordinal);

		// Token: 0x06002B83 RID: 11139
		SqlSingle GetSqlSingle(int ordinal);

		// Token: 0x06002B84 RID: 11140
		SqlDouble GetSqlDouble(int ordinal);

		// Token: 0x06002B85 RID: 11141
		SqlMoney GetSqlMoney(int ordinal);

		// Token: 0x06002B86 RID: 11142
		SqlDateTime GetSqlDateTime(int ordinal);

		// Token: 0x06002B87 RID: 11143
		SqlDecimal GetSqlDecimal(int ordinal);

		// Token: 0x06002B88 RID: 11144
		SqlString GetSqlString(int ordinal);

		// Token: 0x06002B89 RID: 11145
		SqlBinary GetSqlBinary(int ordinal);

		// Token: 0x06002B8A RID: 11146
		SqlGuid GetSqlGuid(int ordinal);

		// Token: 0x06002B8B RID: 11147
		SqlChars GetSqlChars(int ordinal);

		// Token: 0x06002B8C RID: 11148
		SqlBytes GetSqlBytes(int ordinal);

		// Token: 0x06002B8D RID: 11149
		SqlXml GetSqlXml(int ordinal);

		// Token: 0x06002B8E RID: 11150
		SqlBytes GetSqlBytesRef(int ordinal);

		// Token: 0x06002B8F RID: 11151
		SqlChars GetSqlCharsRef(int ordinal);

		// Token: 0x06002B90 RID: 11152
		SqlXml GetSqlXmlRef(int ordinal);
	}
}
