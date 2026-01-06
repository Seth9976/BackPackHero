using System;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200038F RID: 911
	internal interface ITypedSettersV3
	{
		// Token: 0x06002BC6 RID: 11206
		void SetVariantMetaData(SmiEventSink sink, int ordinal, SmiMetaData metaData);

		// Token: 0x06002BC7 RID: 11207
		void SetDBNull(SmiEventSink sink, int ordinal);

		// Token: 0x06002BC8 RID: 11208
		void SetBoolean(SmiEventSink sink, int ordinal, bool value);

		// Token: 0x06002BC9 RID: 11209
		void SetByte(SmiEventSink sink, int ordinal, byte value);

		// Token: 0x06002BCA RID: 11210
		int SetBytes(SmiEventSink sink, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length);

		// Token: 0x06002BCB RID: 11211
		void SetBytesLength(SmiEventSink sink, int ordinal, long length);

		// Token: 0x06002BCC RID: 11212
		int SetChars(SmiEventSink sink, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length);

		// Token: 0x06002BCD RID: 11213
		void SetCharsLength(SmiEventSink sink, int ordinal, long length);

		// Token: 0x06002BCE RID: 11214
		void SetString(SmiEventSink sink, int ordinal, string value, int offset, int length);

		// Token: 0x06002BCF RID: 11215
		void SetInt16(SmiEventSink sink, int ordinal, short value);

		// Token: 0x06002BD0 RID: 11216
		void SetInt32(SmiEventSink sink, int ordinal, int value);

		// Token: 0x06002BD1 RID: 11217
		void SetInt64(SmiEventSink sink, int ordinal, long value);

		// Token: 0x06002BD2 RID: 11218
		void SetSingle(SmiEventSink sink, int ordinal, float value);

		// Token: 0x06002BD3 RID: 11219
		void SetDouble(SmiEventSink sink, int ordinal, double value);

		// Token: 0x06002BD4 RID: 11220
		void SetSqlDecimal(SmiEventSink sink, int ordinal, SqlDecimal value);

		// Token: 0x06002BD5 RID: 11221
		void SetDateTime(SmiEventSink sink, int ordinal, DateTime value);

		// Token: 0x06002BD6 RID: 11222
		void SetGuid(SmiEventSink sink, int ordinal, Guid value);
	}
}
