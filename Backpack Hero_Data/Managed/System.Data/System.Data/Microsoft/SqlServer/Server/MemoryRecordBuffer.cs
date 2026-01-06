using System;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000390 RID: 912
	internal sealed class MemoryRecordBuffer : SmiRecordBuffer
	{
		// Token: 0x06002BD7 RID: 11223 RVA: 0x000BFD70 File Offset: 0x000BDF70
		internal MemoryRecordBuffer(SmiMetaData[] metaData)
		{
			this._buffer = new SqlRecordBuffer[metaData.Length];
			for (int i = 0; i < this._buffer.Length; i++)
			{
				this._buffer[i] = new SqlRecordBuffer(metaData[i]);
			}
		}

		// Token: 0x06002BD8 RID: 11224 RVA: 0x000BFDB4 File Offset: 0x000BDFB4
		public override bool IsDBNull(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].IsNull;
		}

		// Token: 0x06002BD9 RID: 11225 RVA: 0x000BFDC3 File Offset: 0x000BDFC3
		public override SmiMetaData GetVariantType(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].VariantType;
		}

		// Token: 0x06002BDA RID: 11226 RVA: 0x000BFDD2 File Offset: 0x000BDFD2
		public override bool GetBoolean(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Boolean;
		}

		// Token: 0x06002BDB RID: 11227 RVA: 0x000BFDE1 File Offset: 0x000BDFE1
		public override byte GetByte(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Byte;
		}

		// Token: 0x06002BDC RID: 11228 RVA: 0x000BFDF0 File Offset: 0x000BDFF0
		public override long GetBytesLength(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].BytesLength;
		}

		// Token: 0x06002BDD RID: 11229 RVA: 0x000BFDFF File Offset: 0x000BDFFF
		public override int GetBytes(SmiEventSink sink, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			return this._buffer[ordinal].GetBytes(fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x06002BDE RID: 11230 RVA: 0x000BFE15 File Offset: 0x000BE015
		public override long GetCharsLength(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].CharsLength;
		}

		// Token: 0x06002BDF RID: 11231 RVA: 0x000BFE24 File Offset: 0x000BE024
		public override int GetChars(SmiEventSink sink, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			return this._buffer[ordinal].GetChars(fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x06002BE0 RID: 11232 RVA: 0x000BFE3A File Offset: 0x000BE03A
		public override string GetString(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].String;
		}

		// Token: 0x06002BE1 RID: 11233 RVA: 0x000BFE49 File Offset: 0x000BE049
		public override short GetInt16(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Int16;
		}

		// Token: 0x06002BE2 RID: 11234 RVA: 0x000BFE58 File Offset: 0x000BE058
		public override int GetInt32(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Int32;
		}

		// Token: 0x06002BE3 RID: 11235 RVA: 0x000BFE67 File Offset: 0x000BE067
		public override long GetInt64(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Int64;
		}

		// Token: 0x06002BE4 RID: 11236 RVA: 0x000BFE76 File Offset: 0x000BE076
		public override float GetSingle(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Single;
		}

		// Token: 0x06002BE5 RID: 11237 RVA: 0x000BFE85 File Offset: 0x000BE085
		public override double GetDouble(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Double;
		}

		// Token: 0x06002BE6 RID: 11238 RVA: 0x000BFE94 File Offset: 0x000BE094
		public override SqlDecimal GetSqlDecimal(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].SqlDecimal;
		}

		// Token: 0x06002BE7 RID: 11239 RVA: 0x000BFEA3 File Offset: 0x000BE0A3
		public override DateTime GetDateTime(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].DateTime;
		}

		// Token: 0x06002BE8 RID: 11240 RVA: 0x000BFEB2 File Offset: 0x000BE0B2
		public override Guid GetGuid(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Guid;
		}

		// Token: 0x06002BE9 RID: 11241 RVA: 0x000BFEC1 File Offset: 0x000BE0C1
		public override TimeSpan GetTimeSpan(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].TimeSpan;
		}

		// Token: 0x06002BEA RID: 11242 RVA: 0x000BFED0 File Offset: 0x000BE0D0
		public override DateTimeOffset GetDateTimeOffset(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].DateTimeOffset;
		}

		// Token: 0x06002BEB RID: 11243 RVA: 0x000BFEDF File Offset: 0x000BE0DF
		public override void SetDBNull(SmiEventSink sink, int ordinal)
		{
			this._buffer[ordinal].SetNull();
		}

		// Token: 0x06002BEC RID: 11244 RVA: 0x000BFEEE File Offset: 0x000BE0EE
		public override void SetBoolean(SmiEventSink sink, int ordinal, bool value)
		{
			this._buffer[ordinal].Boolean = value;
		}

		// Token: 0x06002BED RID: 11245 RVA: 0x000BFEFE File Offset: 0x000BE0FE
		public override void SetByte(SmiEventSink sink, int ordinal, byte value)
		{
			this._buffer[ordinal].Byte = value;
		}

		// Token: 0x06002BEE RID: 11246 RVA: 0x000BFF0E File Offset: 0x000BE10E
		public override int SetBytes(SmiEventSink sink, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			return this._buffer[ordinal].SetBytes(fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x06002BEF RID: 11247 RVA: 0x000BFF24 File Offset: 0x000BE124
		public override void SetBytesLength(SmiEventSink sink, int ordinal, long length)
		{
			this._buffer[ordinal].BytesLength = length;
		}

		// Token: 0x06002BF0 RID: 11248 RVA: 0x000BFF34 File Offset: 0x000BE134
		public override int SetChars(SmiEventSink sink, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			return this._buffer[ordinal].SetChars(fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x06002BF1 RID: 11249 RVA: 0x000BFF4A File Offset: 0x000BE14A
		public override void SetCharsLength(SmiEventSink sink, int ordinal, long length)
		{
			this._buffer[ordinal].CharsLength = length;
		}

		// Token: 0x06002BF2 RID: 11250 RVA: 0x000BFF5A File Offset: 0x000BE15A
		public override void SetString(SmiEventSink sink, int ordinal, string value, int offset, int length)
		{
			this._buffer[ordinal].String = value.Substring(offset, length);
		}

		// Token: 0x06002BF3 RID: 11251 RVA: 0x000BFF73 File Offset: 0x000BE173
		public override void SetInt16(SmiEventSink sink, int ordinal, short value)
		{
			this._buffer[ordinal].Int16 = value;
		}

		// Token: 0x06002BF4 RID: 11252 RVA: 0x000BFF83 File Offset: 0x000BE183
		public override void SetInt32(SmiEventSink sink, int ordinal, int value)
		{
			this._buffer[ordinal].Int32 = value;
		}

		// Token: 0x06002BF5 RID: 11253 RVA: 0x000BFF93 File Offset: 0x000BE193
		public override void SetInt64(SmiEventSink sink, int ordinal, long value)
		{
			this._buffer[ordinal].Int64 = value;
		}

		// Token: 0x06002BF6 RID: 11254 RVA: 0x000BFFA3 File Offset: 0x000BE1A3
		public override void SetSingle(SmiEventSink sink, int ordinal, float value)
		{
			this._buffer[ordinal].Single = value;
		}

		// Token: 0x06002BF7 RID: 11255 RVA: 0x000BFFB3 File Offset: 0x000BE1B3
		public override void SetDouble(SmiEventSink sink, int ordinal, double value)
		{
			this._buffer[ordinal].Double = value;
		}

		// Token: 0x06002BF8 RID: 11256 RVA: 0x000BFFC3 File Offset: 0x000BE1C3
		public override void SetSqlDecimal(SmiEventSink sink, int ordinal, SqlDecimal value)
		{
			this._buffer[ordinal].SqlDecimal = value;
		}

		// Token: 0x06002BF9 RID: 11257 RVA: 0x000BFFD3 File Offset: 0x000BE1D3
		public override void SetDateTime(SmiEventSink sink, int ordinal, DateTime value)
		{
			this._buffer[ordinal].DateTime = value;
		}

		// Token: 0x06002BFA RID: 11258 RVA: 0x000BFFE3 File Offset: 0x000BE1E3
		public override void SetGuid(SmiEventSink sink, int ordinal, Guid value)
		{
			this._buffer[ordinal].Guid = value;
		}

		// Token: 0x06002BFB RID: 11259 RVA: 0x000BFFF3 File Offset: 0x000BE1F3
		public override void SetTimeSpan(SmiEventSink sink, int ordinal, TimeSpan value)
		{
			this._buffer[ordinal].TimeSpan = value;
		}

		// Token: 0x06002BFC RID: 11260 RVA: 0x000C0003 File Offset: 0x000BE203
		public override void SetDateTimeOffset(SmiEventSink sink, int ordinal, DateTimeOffset value)
		{
			this._buffer[ordinal].DateTimeOffset = value;
		}

		// Token: 0x06002BFD RID: 11261 RVA: 0x000C0013 File Offset: 0x000BE213
		public override void SetVariantMetaData(SmiEventSink sink, int ordinal, SmiMetaData metaData)
		{
			this._buffer[ordinal].VariantType = metaData;
		}

		// Token: 0x04001AC2 RID: 6850
		private SqlRecordBuffer[] _buffer;
	}
}
