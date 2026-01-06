using System;
using System.Data.SqlTypes;
using System.Diagnostics;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient
{
	// Token: 0x0200022C RID: 556
	internal class TdsRecordBufferSetter : SmiRecordBuffer
	{
		// Token: 0x060019EB RID: 6635 RVA: 0x000820E4 File Offset: 0x000802E4
		internal TdsRecordBufferSetter(TdsParserStateObject stateObj, SmiMetaData md)
		{
			this._fieldSetters = new TdsValueSetter[md.FieldMetaData.Count];
			for (int i = 0; i < md.FieldMetaData.Count; i++)
			{
				this._fieldSetters[i] = new TdsValueSetter(stateObj, md.FieldMetaData[i]);
			}
			this._stateObj = stateObj;
			this._metaData = md;
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x060019EC RID: 6636 RVA: 0x00005AE9 File Offset: 0x00003CE9
		internal override bool CanGet
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x060019ED RID: 6637 RVA: 0x0000CD07 File Offset: 0x0000AF07
		internal override bool CanSet
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060019EE RID: 6638 RVA: 0x0008214B File Offset: 0x0008034B
		public override void SetDBNull(SmiEventSink sink, int ordinal)
		{
			this._fieldSetters[ordinal].SetDBNull();
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x0008215A File Offset: 0x0008035A
		public override void SetBoolean(SmiEventSink sink, int ordinal, bool value)
		{
			this._fieldSetters[ordinal].SetBoolean(value);
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x0008216A File Offset: 0x0008036A
		public override void SetByte(SmiEventSink sink, int ordinal, byte value)
		{
			this._fieldSetters[ordinal].SetByte(value);
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x0008217A File Offset: 0x0008037A
		public override int SetBytes(SmiEventSink sink, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			return this._fieldSetters[ordinal].SetBytes(fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x00082190 File Offset: 0x00080390
		public override void SetBytesLength(SmiEventSink sink, int ordinal, long length)
		{
			this._fieldSetters[ordinal].SetBytesLength(length);
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x000821A0 File Offset: 0x000803A0
		public override int SetChars(SmiEventSink sink, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			return this._fieldSetters[ordinal].SetChars(fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x000821B6 File Offset: 0x000803B6
		public override void SetCharsLength(SmiEventSink sink, int ordinal, long length)
		{
			this._fieldSetters[ordinal].SetCharsLength(length);
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x000821C6 File Offset: 0x000803C6
		public override void SetString(SmiEventSink sink, int ordinal, string value, int offset, int length)
		{
			this._fieldSetters[ordinal].SetString(value, offset, length);
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x000821DA File Offset: 0x000803DA
		public override void SetInt16(SmiEventSink sink, int ordinal, short value)
		{
			this._fieldSetters[ordinal].SetInt16(value);
		}

		// Token: 0x060019F7 RID: 6647 RVA: 0x000821EA File Offset: 0x000803EA
		public override void SetInt32(SmiEventSink sink, int ordinal, int value)
		{
			this._fieldSetters[ordinal].SetInt32(value);
		}

		// Token: 0x060019F8 RID: 6648 RVA: 0x000821FA File Offset: 0x000803FA
		public override void SetInt64(SmiEventSink sink, int ordinal, long value)
		{
			this._fieldSetters[ordinal].SetInt64(value);
		}

		// Token: 0x060019F9 RID: 6649 RVA: 0x0008220A File Offset: 0x0008040A
		public override void SetSingle(SmiEventSink sink, int ordinal, float value)
		{
			this._fieldSetters[ordinal].SetSingle(value);
		}

		// Token: 0x060019FA RID: 6650 RVA: 0x0008221A File Offset: 0x0008041A
		public override void SetDouble(SmiEventSink sink, int ordinal, double value)
		{
			this._fieldSetters[ordinal].SetDouble(value);
		}

		// Token: 0x060019FB RID: 6651 RVA: 0x0008222A File Offset: 0x0008042A
		public override void SetSqlDecimal(SmiEventSink sink, int ordinal, SqlDecimal value)
		{
			this._fieldSetters[ordinal].SetSqlDecimal(value);
		}

		// Token: 0x060019FC RID: 6652 RVA: 0x0008223A File Offset: 0x0008043A
		public override void SetDateTime(SmiEventSink sink, int ordinal, DateTime value)
		{
			this._fieldSetters[ordinal].SetDateTime(value);
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x0008224A File Offset: 0x0008044A
		public override void SetGuid(SmiEventSink sink, int ordinal, Guid value)
		{
			this._fieldSetters[ordinal].SetGuid(value);
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x0008225A File Offset: 0x0008045A
		public override void SetTimeSpan(SmiEventSink sink, int ordinal, TimeSpan value)
		{
			this._fieldSetters[ordinal].SetTimeSpan(value);
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x0008226A File Offset: 0x0008046A
		public override void SetDateTimeOffset(SmiEventSink sink, int ordinal, DateTimeOffset value)
		{
			this._fieldSetters[ordinal].SetDateTimeOffset(value);
		}

		// Token: 0x06001A00 RID: 6656 RVA: 0x0008227A File Offset: 0x0008047A
		public override void SetVariantMetaData(SmiEventSink sink, int ordinal, SmiMetaData metaData)
		{
			this._fieldSetters[ordinal].SetVariantType(metaData);
		}

		// Token: 0x06001A01 RID: 6657 RVA: 0x0008228A File Offset: 0x0008048A
		internal override void NewElement(SmiEventSink sink)
		{
			this._stateObj.WriteByte(1);
		}

		// Token: 0x06001A02 RID: 6658 RVA: 0x00082298 File Offset: 0x00080498
		internal override void EndElements(SmiEventSink sink)
		{
			this._stateObj.WriteByte(0);
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x000094D4 File Offset: 0x000076D4
		[Conditional("DEBUG")]
		private void CheckWritingToColumn(int ordinal)
		{
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x000094D4 File Offset: 0x000076D4
		[Conditional("DEBUG")]
		private void SkipPossibleDefaultedColumns(int targetColumn)
		{
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x000094D4 File Offset: 0x000076D4
		[Conditional("DEBUG")]
		internal void CheckSettingColumn(int ordinal)
		{
		}

		// Token: 0x04001292 RID: 4754
		private TdsValueSetter[] _fieldSetters;

		// Token: 0x04001293 RID: 4755
		private TdsParserStateObject _stateObj;

		// Token: 0x04001294 RID: 4756
		private SmiMetaData _metaData;
	}
}
