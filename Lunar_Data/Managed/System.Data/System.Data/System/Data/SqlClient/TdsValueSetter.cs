using System;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Text;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient
{
	// Token: 0x0200022D RID: 557
	internal class TdsValueSetter
	{
		// Token: 0x06001A06 RID: 6662 RVA: 0x000822A6 File Offset: 0x000804A6
		internal TdsValueSetter(TdsParserStateObject stateObj, SmiMetaData md)
		{
			this._stateObj = stateObj;
			this._metaData = md;
			this._isPlp = MetaDataUtilsSmi.IsPlpFormat(md);
			this._plpUnknownSent = false;
			this._encoder = null;
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x000822D8 File Offset: 0x000804D8
		internal void SetDBNull()
		{
			if (this._isPlp)
			{
				this._stateObj.Parser.WriteUnsignedLong(ulong.MaxValue, this._stateObj);
				return;
			}
			switch (this._metaData.SqlDbType)
			{
			case SqlDbType.BigInt:
			case SqlDbType.Bit:
			case SqlDbType.DateTime:
			case SqlDbType.Decimal:
			case SqlDbType.Float:
			case SqlDbType.Int:
			case SqlDbType.Money:
			case SqlDbType.Real:
			case SqlDbType.UniqueIdentifier:
			case SqlDbType.SmallDateTime:
			case SqlDbType.SmallInt:
			case SqlDbType.SmallMoney:
			case SqlDbType.TinyInt:
			case SqlDbType.Date:
			case SqlDbType.Time:
			case SqlDbType.DateTime2:
			case SqlDbType.DateTimeOffset:
				this._stateObj.WriteByte(0);
				return;
			case SqlDbType.Binary:
			case SqlDbType.Char:
			case SqlDbType.Image:
			case SqlDbType.NChar:
			case SqlDbType.NText:
			case SqlDbType.NVarChar:
			case SqlDbType.Text:
			case SqlDbType.Timestamp:
			case SqlDbType.VarBinary:
			case SqlDbType.VarChar:
				this._stateObj.Parser.WriteShort(65535, this._stateObj);
				return;
			case SqlDbType.Variant:
				this._stateObj.Parser.WriteInt(0, this._stateObj);
				break;
			case (SqlDbType)24:
			case SqlDbType.Xml:
			case (SqlDbType)26:
			case (SqlDbType)27:
			case (SqlDbType)28:
			case SqlDbType.Udt:
			case SqlDbType.Structured:
				break;
			default:
				return;
			}
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x000823E8 File Offset: 0x000805E8
		internal void SetBoolean(bool value)
		{
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantHeader(3, 50, 0, this._stateObj);
			}
			else
			{
				this._stateObj.WriteByte((byte)this._metaData.MaxLength);
			}
			if (value)
			{
				this._stateObj.WriteByte(1);
				return;
			}
			this._stateObj.WriteByte(0);
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x00082454 File Offset: 0x00080654
		internal void SetByte(byte value)
		{
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantHeader(3, 48, 0, this._stateObj);
			}
			else
			{
				this._stateObj.WriteByte((byte)this._metaData.MaxLength);
			}
			this._stateObj.WriteByte(value);
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x000824AF File Offset: 0x000806AF
		internal int SetBytes(long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			this.SetBytesNoOffsetHandling(fieldOffset, buffer, bufferOffset, length);
			return length;
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x000824C0 File Offset: 0x000806C0
		private void SetBytesNoOffsetHandling(long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			if (this._isPlp)
			{
				if (!this._plpUnknownSent)
				{
					this._stateObj.Parser.WriteUnsignedLong(18446744073709551614UL, this._stateObj);
					this._plpUnknownSent = true;
				}
				this._stateObj.Parser.WriteInt(length, this._stateObj);
				this._stateObj.WriteByteArray(buffer, length, bufferOffset, true, null);
				return;
			}
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantHeader(4 + length, 165, 2, this._stateObj);
			}
			this._stateObj.Parser.WriteShort(length, this._stateObj);
			this._stateObj.WriteByteArray(buffer, length, bufferOffset, true, null);
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x00082584 File Offset: 0x00080784
		internal void SetBytesLength(long length)
		{
			if (length == 0L)
			{
				if (this._isPlp)
				{
					this._stateObj.Parser.WriteLong(0L, this._stateObj);
					this._plpUnknownSent = true;
				}
				else
				{
					if (SqlDbType.Variant == this._metaData.SqlDbType)
					{
						this._stateObj.Parser.WriteSqlVariantHeader(4, 165, 2, this._stateObj);
					}
					this._stateObj.Parser.WriteShort(0, this._stateObj);
				}
			}
			if (this._plpUnknownSent)
			{
				this._stateObj.Parser.WriteInt(0, this._stateObj);
				this._plpUnknownSent = false;
			}
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x00082628 File Offset: 0x00080828
		internal int SetChars(long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			if (MetaDataUtilsSmi.IsAnsiType(this._metaData.SqlDbType))
			{
				if (this._encoder == null)
				{
					this._encoder = this._stateObj.Parser._defaultEncoding.GetEncoder();
				}
				byte[] array = new byte[this._encoder.GetByteCount(buffer, bufferOffset, length, false)];
				this._encoder.GetBytes(buffer, bufferOffset, length, array, 0, false);
				this.SetBytesNoOffsetHandling(fieldOffset, array, 0, array.Length);
			}
			else if (this._isPlp)
			{
				if (!this._plpUnknownSent)
				{
					this._stateObj.Parser.WriteUnsignedLong(18446744073709551614UL, this._stateObj);
					this._plpUnknownSent = true;
				}
				this._stateObj.Parser.WriteInt(length * 2, this._stateObj);
				this._stateObj.Parser.WriteCharArray(buffer, length, bufferOffset, this._stateObj, true);
			}
			else if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantValue(new string(buffer, bufferOffset, length), length, 0, this._stateObj, true);
			}
			else
			{
				this._stateObj.Parser.WriteShort(length * 2, this._stateObj);
				this._stateObj.Parser.WriteCharArray(buffer, length, bufferOffset, this._stateObj, true);
			}
			return length;
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x0008277C File Offset: 0x0008097C
		internal void SetCharsLength(long length)
		{
			if (length == 0L)
			{
				if (this._isPlp)
				{
					this._stateObj.Parser.WriteLong(0L, this._stateObj);
					this._plpUnknownSent = true;
				}
				else
				{
					this._stateObj.Parser.WriteShort(0, this._stateObj);
				}
			}
			if (this._plpUnknownSent)
			{
				this._stateObj.Parser.WriteInt(0, this._stateObj);
				this._plpUnknownSent = false;
			}
			this._encoder = null;
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x000827FC File Offset: 0x000809FC
		internal void SetString(string value, int offset, int length)
		{
			if (MetaDataUtilsSmi.IsAnsiType(this._metaData.SqlDbType))
			{
				byte[] array;
				if (offset == 0 && value.Length <= length)
				{
					array = this._stateObj.Parser._defaultEncoding.GetBytes(value);
				}
				else
				{
					char[] array2 = value.ToCharArray(offset, length);
					array = this._stateObj.Parser._defaultEncoding.GetBytes(array2);
				}
				this.SetBytes(0L, array, 0, array.Length);
				this.SetBytesLength((long)array.Length);
				return;
			}
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				SqlCollation sqlCollation = new SqlCollation();
				sqlCollation.LCID = checked((int)this._variantType.LocaleId);
				sqlCollation.SqlCompareOptions = this._variantType.CompareOptions;
				if (length * 2 > 8000)
				{
					byte[] array3;
					if (offset == 0 && value.Length <= length)
					{
						array3 = this._stateObj.Parser._defaultEncoding.GetBytes(value);
					}
					else
					{
						array3 = this._stateObj.Parser._defaultEncoding.GetBytes(value.ToCharArray(offset, length));
					}
					this._stateObj.Parser.WriteSqlVariantHeader(9 + array3.Length, 167, 7, this._stateObj);
					this._stateObj.Parser.WriteUnsignedInt(sqlCollation.info, this._stateObj);
					this._stateObj.WriteByte(sqlCollation.sortId);
					this._stateObj.Parser.WriteShort(array3.Length, this._stateObj);
					this._stateObj.WriteByteArray(array3, array3.Length, 0, true, null);
				}
				else
				{
					this._stateObj.Parser.WriteSqlVariantHeader(9 + length * 2, 231, 7, this._stateObj);
					this._stateObj.Parser.WriteUnsignedInt(sqlCollation.info, this._stateObj);
					this._stateObj.WriteByte(sqlCollation.sortId);
					this._stateObj.Parser.WriteShort(length * 2, this._stateObj);
					this._stateObj.Parser.WriteString(value, length, offset, this._stateObj, true);
				}
				this._variantType = null;
				return;
			}
			if (this._isPlp)
			{
				this._stateObj.Parser.WriteLong((long)(length * 2), this._stateObj);
				this._stateObj.Parser.WriteInt(length * 2, this._stateObj);
				this._stateObj.Parser.WriteString(value, length, offset, this._stateObj, true);
				if (length != 0)
				{
					this._stateObj.Parser.WriteInt(0, this._stateObj);
					return;
				}
			}
			else
			{
				this._stateObj.Parser.WriteShort(length * 2, this._stateObj);
				this._stateObj.Parser.WriteString(value, length, offset, this._stateObj, true);
			}
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x00082AB4 File Offset: 0x00080CB4
		internal void SetInt16(short value)
		{
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantHeader(4, 52, 0, this._stateObj);
			}
			else
			{
				this._stateObj.WriteByte((byte)this._metaData.MaxLength);
			}
			this._stateObj.Parser.WriteShort((int)value, this._stateObj);
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x00082B1C File Offset: 0x00080D1C
		internal void SetInt32(int value)
		{
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantHeader(6, 56, 0, this._stateObj);
			}
			else
			{
				this._stateObj.WriteByte((byte)this._metaData.MaxLength);
			}
			this._stateObj.Parser.WriteInt(value, this._stateObj);
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x00082B84 File Offset: 0x00080D84
		internal void SetInt64(long value)
		{
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				if (this._variantType == null)
				{
					this._stateObj.Parser.WriteSqlVariantHeader(10, 127, 0, this._stateObj);
					this._stateObj.Parser.WriteLong(value, this._stateObj);
					return;
				}
				this._stateObj.Parser.WriteSqlVariantHeader(10, 60, 0, this._stateObj);
				this._stateObj.Parser.WriteInt((int)(value >> 32), this._stateObj);
				this._stateObj.Parser.WriteInt((int)value, this._stateObj);
				this._variantType = null;
				return;
			}
			else
			{
				this._stateObj.WriteByte((byte)this._metaData.MaxLength);
				if (SqlDbType.SmallMoney == this._metaData.SqlDbType)
				{
					this._stateObj.Parser.WriteInt((int)value, this._stateObj);
					return;
				}
				if (SqlDbType.Money == this._metaData.SqlDbType)
				{
					this._stateObj.Parser.WriteInt((int)(value >> 32), this._stateObj);
					this._stateObj.Parser.WriteInt((int)value, this._stateObj);
					return;
				}
				this._stateObj.Parser.WriteLong(value, this._stateObj);
				return;
			}
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x00082CD0 File Offset: 0x00080ED0
		internal void SetSingle(float value)
		{
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantHeader(6, 59, 0, this._stateObj);
			}
			else
			{
				this._stateObj.WriteByte((byte)this._metaData.MaxLength);
			}
			this._stateObj.Parser.WriteFloat(value, this._stateObj);
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x00082D38 File Offset: 0x00080F38
		internal void SetDouble(double value)
		{
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantHeader(10, 62, 0, this._stateObj);
			}
			else
			{
				this._stateObj.WriteByte((byte)this._metaData.MaxLength);
			}
			this._stateObj.Parser.WriteDouble(value, this._stateObj);
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x00082DA0 File Offset: 0x00080FA0
		internal void SetSqlDecimal(SqlDecimal value)
		{
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantHeader(21, 108, 2, this._stateObj);
				this._stateObj.WriteByte(value.Precision);
				this._stateObj.WriteByte(value.Scale);
				this._stateObj.Parser.WriteSqlDecimal(value, this._stateObj);
				return;
			}
			this._stateObj.WriteByte(checked((byte)MetaType.MetaDecimal.FixedLength));
			this._stateObj.Parser.WriteSqlDecimal(SqlDecimal.ConvertToPrecScale(value, (int)this._metaData.Precision, (int)this._metaData.Scale), this._stateObj);
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x00082E5C File Offset: 0x0008105C
		internal void SetDateTime(DateTime value)
		{
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				if (this._variantType != null && this._variantType.SqlDbType == SqlDbType.DateTime2)
				{
					this._stateObj.Parser.WriteSqlVariantDateTime2(value, this._stateObj);
				}
				else if (this._variantType != null && this._variantType.SqlDbType == SqlDbType.Date)
				{
					this._stateObj.Parser.WriteSqlVariantDate(value, this._stateObj);
				}
				else
				{
					TdsDateTime tdsDateTime = MetaType.FromDateTime(value, 8);
					this._stateObj.Parser.WriteSqlVariantHeader(10, 61, 0, this._stateObj);
					this._stateObj.Parser.WriteInt(tdsDateTime.days, this._stateObj);
					this._stateObj.Parser.WriteInt(tdsDateTime.time, this._stateObj);
				}
				this._variantType = null;
				return;
			}
			this._stateObj.WriteByte((byte)this._metaData.MaxLength);
			if (SqlDbType.SmallDateTime == this._metaData.SqlDbType)
			{
				TdsDateTime tdsDateTime2 = MetaType.FromDateTime(value, (byte)this._metaData.MaxLength);
				this._stateObj.Parser.WriteShort(tdsDateTime2.days, this._stateObj);
				this._stateObj.Parser.WriteShort(tdsDateTime2.time, this._stateObj);
				return;
			}
			if (SqlDbType.DateTime == this._metaData.SqlDbType)
			{
				TdsDateTime tdsDateTime3 = MetaType.FromDateTime(value, (byte)this._metaData.MaxLength);
				this._stateObj.Parser.WriteInt(tdsDateTime3.days, this._stateObj);
				this._stateObj.Parser.WriteInt(tdsDateTime3.time, this._stateObj);
				return;
			}
			int days = value.Subtract(DateTime.MinValue).Days;
			if (SqlDbType.DateTime2 == this._metaData.SqlDbType)
			{
				long num = value.TimeOfDay.Ticks / TdsEnums.TICKS_FROM_SCALE[(int)this._metaData.Scale];
				this._stateObj.WriteByteArray(BitConverter.GetBytes(num), (int)this._metaData.MaxLength - 3, 0, true, null);
			}
			this._stateObj.WriteByteArray(BitConverter.GetBytes(days), 3, 0, true, null);
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x00083090 File Offset: 0x00081290
		internal void SetGuid(Guid value)
		{
			byte[] array = value.ToByteArray();
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantHeader(18, 36, 0, this._stateObj);
			}
			else
			{
				this._stateObj.WriteByte((byte)this._metaData.MaxLength);
			}
			this._stateObj.WriteByteArray(array, array.Length, 0, true, null);
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x000830FC File Offset: 0x000812FC
		internal void SetTimeSpan(TimeSpan value)
		{
			byte b;
			byte b2;
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				b = SmiMetaData.DefaultTime.Scale;
				b2 = (byte)SmiMetaData.DefaultTime.MaxLength;
				this._stateObj.Parser.WriteSqlVariantHeader(8, 41, 1, this._stateObj);
				this._stateObj.WriteByte(b);
			}
			else
			{
				b = this._metaData.Scale;
				b2 = (byte)this._metaData.MaxLength;
				this._stateObj.WriteByte(b2);
			}
			long num = value.Ticks / TdsEnums.TICKS_FROM_SCALE[(int)b];
			this._stateObj.WriteByteArray(BitConverter.GetBytes(num), (int)b2, 0, true, null);
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x000831A4 File Offset: 0x000813A4
		internal void SetDateTimeOffset(DateTimeOffset value)
		{
			byte b;
			byte b2;
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				SmiMetaData defaultDateTimeOffset = SmiMetaData.DefaultDateTimeOffset;
				b = MetaType.MetaDateTimeOffset.Scale;
				b2 = (byte)defaultDateTimeOffset.MaxLength;
				this._stateObj.Parser.WriteSqlVariantHeader(13, 43, 1, this._stateObj);
				this._stateObj.WriteByte(b);
			}
			else
			{
				b = this._metaData.Scale;
				b2 = (byte)this._metaData.MaxLength;
				this._stateObj.WriteByte(b2);
			}
			DateTime utcDateTime = value.UtcDateTime;
			long num = utcDateTime.TimeOfDay.Ticks / TdsEnums.TICKS_FROM_SCALE[(int)b];
			int days = utcDateTime.Subtract(DateTime.MinValue).Days;
			short num2 = (short)value.Offset.TotalMinutes;
			this._stateObj.WriteByteArray(BitConverter.GetBytes(num), (int)(b2 - 5), 0, true, null);
			this._stateObj.WriteByteArray(BitConverter.GetBytes(days), 3, 0, true, null);
			this._stateObj.WriteByte((byte)(num2 & 255));
			this._stateObj.WriteByte((byte)((num2 >> 8) & 255));
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x000832C9 File Offset: 0x000814C9
		internal void SetVariantType(SmiMetaData value)
		{
			this._variantType = value;
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x000094D4 File Offset: 0x000076D4
		[Conditional("DEBUG")]
		private void CheckSettingOffset(long offset)
		{
		}

		// Token: 0x04001295 RID: 4757
		private TdsParserStateObject _stateObj;

		// Token: 0x04001296 RID: 4758
		private SmiMetaData _metaData;

		// Token: 0x04001297 RID: 4759
		private bool _isPlp;

		// Token: 0x04001298 RID: 4760
		private bool _plpUnknownSent;

		// Token: 0x04001299 RID: 4761
		private Encoder _encoder;

		// Token: 0x0400129A RID: 4762
		private SmiMetaData _variantType;
	}
}
