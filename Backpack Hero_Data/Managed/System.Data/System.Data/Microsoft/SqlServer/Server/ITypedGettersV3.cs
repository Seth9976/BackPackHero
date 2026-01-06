using System;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200038D RID: 909
	internal interface ITypedGettersV3
	{
		// Token: 0x06002B91 RID: 11153
		bool IsDBNull(SmiEventSink sink, int ordinal);

		// Token: 0x06002B92 RID: 11154
		SmiMetaData GetVariantType(SmiEventSink sink, int ordinal);

		// Token: 0x06002B93 RID: 11155
		bool GetBoolean(SmiEventSink sink, int ordinal);

		// Token: 0x06002B94 RID: 11156
		byte GetByte(SmiEventSink sink, int ordinal);

		// Token: 0x06002B95 RID: 11157
		long GetBytesLength(SmiEventSink sink, int ordinal);

		// Token: 0x06002B96 RID: 11158
		int GetBytes(SmiEventSink sink, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length);

		// Token: 0x06002B97 RID: 11159
		long GetCharsLength(SmiEventSink sink, int ordinal);

		// Token: 0x06002B98 RID: 11160
		int GetChars(SmiEventSink sink, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length);

		// Token: 0x06002B99 RID: 11161
		string GetString(SmiEventSink sink, int ordinal);

		// Token: 0x06002B9A RID: 11162
		short GetInt16(SmiEventSink sink, int ordinal);

		// Token: 0x06002B9B RID: 11163
		int GetInt32(SmiEventSink sink, int ordinal);

		// Token: 0x06002B9C RID: 11164
		long GetInt64(SmiEventSink sink, int ordinal);

		// Token: 0x06002B9D RID: 11165
		float GetSingle(SmiEventSink sink, int ordinal);

		// Token: 0x06002B9E RID: 11166
		double GetDouble(SmiEventSink sink, int ordinal);

		// Token: 0x06002B9F RID: 11167
		SqlDecimal GetSqlDecimal(SmiEventSink sink, int ordinal);

		// Token: 0x06002BA0 RID: 11168
		DateTime GetDateTime(SmiEventSink sink, int ordinal);

		// Token: 0x06002BA1 RID: 11169
		Guid GetGuid(SmiEventSink sink, int ordinal);
	}
}
