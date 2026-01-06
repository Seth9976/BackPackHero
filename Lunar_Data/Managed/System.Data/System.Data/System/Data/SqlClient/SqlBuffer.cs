using System;
using System.Data.SqlTypes;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Data.SqlClient
{
	// Token: 0x0200013B RID: 315
	internal sealed class SqlBuffer
	{
		// Token: 0x0600102B RID: 4139 RVA: 0x00003D55 File Offset: 0x00001F55
		internal SqlBuffer()
		{
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x0004F7C5 File Offset: 0x0004D9C5
		private SqlBuffer(SqlBuffer value)
		{
			this._isNull = value._isNull;
			this._type = value._type;
			this._value = value._value;
			this._object = value._object;
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x0600102D RID: 4141 RVA: 0x0004F7FD File Offset: 0x0004D9FD
		internal bool IsEmpty
		{
			get
			{
				return this._type == SqlBuffer.StorageType.Empty;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x0600102E RID: 4142 RVA: 0x0004F808 File Offset: 0x0004DA08
		internal bool IsNull
		{
			get
			{
				return this._isNull;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x0600102F RID: 4143 RVA: 0x0004F810 File Offset: 0x0004DA10
		internal SqlBuffer.StorageType VariantInternalStorageType
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06001030 RID: 4144 RVA: 0x0004F818 File Offset: 0x0004DA18
		// (set) Token: 0x06001031 RID: 4145 RVA: 0x0004F840 File Offset: 0x0004DA40
		internal bool Boolean
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.Boolean == this._type)
				{
					return this._value._boolean;
				}
				return (bool)this.Value;
			}
			set
			{
				this._value._boolean = value;
				this._type = SqlBuffer.StorageType.Boolean;
				this._isNull = false;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06001032 RID: 4146 RVA: 0x0004F85C File Offset: 0x0004DA5C
		// (set) Token: 0x06001033 RID: 4147 RVA: 0x0004F884 File Offset: 0x0004DA84
		internal byte Byte
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.Byte == this._type)
				{
					return this._value._byte;
				}
				return (byte)this.Value;
			}
			set
			{
				this._value._byte = value;
				this._type = SqlBuffer.StorageType.Byte;
				this._isNull = false;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06001034 RID: 4148 RVA: 0x0004F8A0 File Offset: 0x0004DAA0
		internal byte[] ByteArray
		{
			get
			{
				this.ThrowIfNull();
				return this.SqlBinary.Value;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06001035 RID: 4149 RVA: 0x0004F8C4 File Offset: 0x0004DAC4
		internal DateTime DateTime
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.Date == this._type)
				{
					return DateTime.MinValue.AddDays((double)this._value._int32);
				}
				if (SqlBuffer.StorageType.DateTime2 == this._type)
				{
					return new DateTime(SqlBuffer.GetTicksFromDateTime2Info(this._value._dateTime2Info));
				}
				if (SqlBuffer.StorageType.DateTime == this._type)
				{
					return SqlTypeWorkarounds.SqlDateTimeToDateTime(this._value._dateTimeInfo.daypart, this._value._dateTimeInfo.timepart);
				}
				return (DateTime)this.Value;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06001036 RID: 4150 RVA: 0x0004F954 File Offset: 0x0004DB54
		internal decimal Decimal
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.Decimal == this._type)
				{
					if (this._value._numericInfo.data4 != 0 || this._value._numericInfo.scale > 28)
					{
						throw new OverflowException(SQLResource.ConversionOverflowMessage);
					}
					return new decimal(this._value._numericInfo.data1, this._value._numericInfo.data2, this._value._numericInfo.data3, !this._value._numericInfo.positive, this._value._numericInfo.scale);
				}
				else
				{
					if (SqlBuffer.StorageType.Money == this._type)
					{
						long num = this._value._int64;
						bool flag = false;
						if (num < 0L)
						{
							flag = true;
							num = -num;
						}
						return new decimal((int)(num & (long)((ulong)(-1))), (int)(num >> 32), 0, flag, 4);
					}
					return (decimal)this.Value;
				}
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06001037 RID: 4151 RVA: 0x0004FA3D File Offset: 0x0004DC3D
		// (set) Token: 0x06001038 RID: 4152 RVA: 0x0004FA65 File Offset: 0x0004DC65
		internal double Double
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.Double == this._type)
				{
					return this._value._double;
				}
				return (double)this.Value;
			}
			set
			{
				this._value._double = value;
				this._type = SqlBuffer.StorageType.Double;
				this._isNull = false;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06001039 RID: 4153 RVA: 0x0004FA84 File Offset: 0x0004DC84
		internal Guid Guid
		{
			get
			{
				this.ThrowIfNull();
				return this.SqlGuid.Value;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x0600103A RID: 4154 RVA: 0x0004FAA5 File Offset: 0x0004DCA5
		// (set) Token: 0x0600103B RID: 4155 RVA: 0x0004FACD File Offset: 0x0004DCCD
		internal short Int16
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.Int16 == this._type)
				{
					return this._value._int16;
				}
				return (short)this.Value;
			}
			set
			{
				this._value._int16 = value;
				this._type = SqlBuffer.StorageType.Int16;
				this._isNull = false;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x0600103C RID: 4156 RVA: 0x0004FAE9 File Offset: 0x0004DCE9
		// (set) Token: 0x0600103D RID: 4157 RVA: 0x0004FB11 File Offset: 0x0004DD11
		internal int Int32
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.Int32 == this._type)
				{
					return this._value._int32;
				}
				return (int)this.Value;
			}
			set
			{
				this._value._int32 = value;
				this._type = SqlBuffer.StorageType.Int32;
				this._isNull = false;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x0600103E RID: 4158 RVA: 0x0004FB2D File Offset: 0x0004DD2D
		// (set) Token: 0x0600103F RID: 4159 RVA: 0x0004FB55 File Offset: 0x0004DD55
		internal long Int64
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.Int64 == this._type)
				{
					return this._value._int64;
				}
				return (long)this.Value;
			}
			set
			{
				this._value._int64 = value;
				this._type = SqlBuffer.StorageType.Int64;
				this._isNull = false;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06001040 RID: 4160 RVA: 0x0004FB71 File Offset: 0x0004DD71
		// (set) Token: 0x06001041 RID: 4161 RVA: 0x0004FB9A File Offset: 0x0004DD9A
		internal float Single
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.Single == this._type)
				{
					return this._value._single;
				}
				return (float)this.Value;
			}
			set
			{
				this._value._single = value;
				this._type = SqlBuffer.StorageType.Single;
				this._isNull = false;
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06001042 RID: 4162 RVA: 0x0004FBB8 File Offset: 0x0004DDB8
		internal string String
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.String == this._type)
				{
					return (string)this._object;
				}
				if (SqlBuffer.StorageType.SqlCachedBuffer == this._type)
				{
					return ((SqlCachedBuffer)this._object).ToString();
				}
				return (string)this.Value;
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06001043 RID: 4163 RVA: 0x0004FC08 File Offset: 0x0004DE08
		internal string KatmaiDateTimeString
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.Date == this._type)
				{
					return this.DateTime.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);
				}
				if (SqlBuffer.StorageType.Time == this._type)
				{
					byte scale = this._value._timeInfo.scale;
					return new DateTime(this._value._timeInfo.ticks).ToString(SqlBuffer.s_katmaiTimeFormatByScale[(int)scale], DateTimeFormatInfo.InvariantInfo);
				}
				if (SqlBuffer.StorageType.DateTime2 == this._type)
				{
					byte scale2 = this._value._dateTime2Info.timeInfo.scale;
					return this.DateTime.ToString(SqlBuffer.s_katmaiDateTime2FormatByScale[(int)scale2], DateTimeFormatInfo.InvariantInfo);
				}
				if (SqlBuffer.StorageType.DateTimeOffset == this._type)
				{
					DateTimeOffset dateTimeOffset = this.DateTimeOffset;
					byte scale3 = this._value._dateTimeOffsetInfo.dateTime2Info.timeInfo.scale;
					return dateTimeOffset.ToString(SqlBuffer.s_katmaiDateTimeOffsetFormatByScale[(int)scale3], DateTimeFormatInfo.InvariantInfo);
				}
				return (string)this.Value;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06001044 RID: 4164 RVA: 0x0004FD0C File Offset: 0x0004DF0C
		internal SqlString KatmaiDateTimeSqlString
		{
			get
			{
				if (SqlBuffer.StorageType.Date != this._type && SqlBuffer.StorageType.Time != this._type && SqlBuffer.StorageType.DateTime2 != this._type && SqlBuffer.StorageType.DateTimeOffset != this._type)
				{
					return (SqlString)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlString.Null;
				}
				return new SqlString(this.KatmaiDateTimeString);
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06001045 RID: 4165 RVA: 0x0004FD66 File Offset: 0x0004DF66
		internal TimeSpan Time
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.Time == this._type)
				{
					return new TimeSpan(this._value._timeInfo.ticks);
				}
				return (TimeSpan)this.Value;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06001046 RID: 4166 RVA: 0x0004FD9C File Offset: 0x0004DF9C
		internal DateTimeOffset DateTimeOffset
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.DateTimeOffset == this._type)
				{
					TimeSpan timeSpan = new TimeSpan(0, (int)this._value._dateTimeOffsetInfo.offset, 0);
					return new DateTimeOffset(SqlBuffer.GetTicksFromDateTime2Info(this._value._dateTimeOffsetInfo.dateTime2Info) + timeSpan.Ticks, timeSpan);
				}
				return (DateTimeOffset)this.Value;
			}
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x0004FE01 File Offset: 0x0004E001
		private static long GetTicksFromDateTime2Info(SqlBuffer.DateTime2Info dateTime2Info)
		{
			return (long)dateTime2Info.date * 864000000000L + dateTime2Info.timeInfo.ticks;
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06001048 RID: 4168 RVA: 0x0004FE20 File Offset: 0x0004E020
		// (set) Token: 0x06001049 RID: 4169 RVA: 0x0004FE43 File Offset: 0x0004E043
		internal SqlBinary SqlBinary
		{
			get
			{
				if (SqlBuffer.StorageType.SqlBinary == this._type)
				{
					return (SqlBinary)this._object;
				}
				return (SqlBinary)this.SqlValue;
			}
			set
			{
				this._object = value;
				this._type = SqlBuffer.StorageType.SqlBinary;
				this._isNull = value.IsNull;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x0600104A RID: 4170 RVA: 0x0004FE66 File Offset: 0x0004E066
		internal SqlBoolean SqlBoolean
		{
			get
			{
				if (SqlBuffer.StorageType.Boolean != this._type)
				{
					return (SqlBoolean)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlBoolean.Null;
				}
				return new SqlBoolean(this._value._boolean);
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x0600104B RID: 4171 RVA: 0x0004FE9B File Offset: 0x0004E09B
		internal SqlByte SqlByte
		{
			get
			{
				if (SqlBuffer.StorageType.Byte != this._type)
				{
					return (SqlByte)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlByte.Null;
				}
				return new SqlByte(this._value._byte);
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x0600104C RID: 4172 RVA: 0x0004FED0 File Offset: 0x0004E0D0
		// (set) Token: 0x0600104D RID: 4173 RVA: 0x0004FF01 File Offset: 0x0004E101
		internal SqlCachedBuffer SqlCachedBuffer
		{
			get
			{
				if (SqlBuffer.StorageType.SqlCachedBuffer != this._type)
				{
					return (SqlCachedBuffer)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlCachedBuffer.Null;
				}
				return (SqlCachedBuffer)this._object;
			}
			set
			{
				this._object = value;
				this._type = SqlBuffer.StorageType.SqlCachedBuffer;
				this._isNull = value.IsNull;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x0600104E RID: 4174 RVA: 0x0004FF1E File Offset: 0x0004E11E
		// (set) Token: 0x0600104F RID: 4175 RVA: 0x0004FF4F File Offset: 0x0004E14F
		internal SqlXml SqlXml
		{
			get
			{
				if (SqlBuffer.StorageType.SqlXml != this._type)
				{
					return (SqlXml)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlXml.Null;
				}
				return (SqlXml)this._object;
			}
			set
			{
				this._object = value;
				this._type = SqlBuffer.StorageType.SqlXml;
				this._isNull = value.IsNull;
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06001050 RID: 4176 RVA: 0x0004FF6C File Offset: 0x0004E16C
		internal SqlDateTime SqlDateTime
		{
			get
			{
				if (SqlBuffer.StorageType.DateTime != this._type)
				{
					return (SqlDateTime)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlDateTime.Null;
				}
				return new SqlDateTime(this._value._dateTimeInfo.daypart, this._value._dateTimeInfo.timepart);
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06001051 RID: 4177 RVA: 0x0004FFC4 File Offset: 0x0004E1C4
		internal SqlDecimal SqlDecimal
		{
			get
			{
				if (SqlBuffer.StorageType.Decimal != this._type)
				{
					return (SqlDecimal)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlDecimal.Null;
				}
				return new SqlDecimal(this._value._numericInfo.precision, this._value._numericInfo.scale, this._value._numericInfo.positive, this._value._numericInfo.data1, this._value._numericInfo.data2, this._value._numericInfo.data3, this._value._numericInfo.data4);
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06001052 RID: 4178 RVA: 0x0005006C File Offset: 0x0004E26C
		internal SqlDouble SqlDouble
		{
			get
			{
				if (SqlBuffer.StorageType.Double != this._type)
				{
					return (SqlDouble)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlDouble.Null;
				}
				return new SqlDouble(this._value._double);
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06001053 RID: 4179 RVA: 0x000500A1 File Offset: 0x0004E2A1
		// (set) Token: 0x06001054 RID: 4180 RVA: 0x000500C4 File Offset: 0x0004E2C4
		internal SqlGuid SqlGuid
		{
			get
			{
				if (SqlBuffer.StorageType.SqlGuid == this._type)
				{
					return (SqlGuid)this._object;
				}
				return (SqlGuid)this.SqlValue;
			}
			set
			{
				this._object = value;
				this._type = SqlBuffer.StorageType.SqlGuid;
				this._isNull = value.IsNull;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06001055 RID: 4181 RVA: 0x000500E7 File Offset: 0x0004E2E7
		internal SqlInt16 SqlInt16
		{
			get
			{
				if (SqlBuffer.StorageType.Int16 != this._type)
				{
					return (SqlInt16)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlInt16.Null;
				}
				return new SqlInt16(this._value._int16);
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06001056 RID: 4182 RVA: 0x0005011C File Offset: 0x0004E31C
		internal SqlInt32 SqlInt32
		{
			get
			{
				if (SqlBuffer.StorageType.Int32 != this._type)
				{
					return (SqlInt32)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlInt32.Null;
				}
				return new SqlInt32(this._value._int32);
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06001057 RID: 4183 RVA: 0x00050151 File Offset: 0x0004E351
		internal SqlInt64 SqlInt64
		{
			get
			{
				if (SqlBuffer.StorageType.Int64 != this._type)
				{
					return (SqlInt64)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlInt64.Null;
				}
				return new SqlInt64(this._value._int64);
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06001058 RID: 4184 RVA: 0x00050186 File Offset: 0x0004E386
		internal SqlMoney SqlMoney
		{
			get
			{
				if (SqlBuffer.StorageType.Money != this._type)
				{
					return (SqlMoney)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlMoney.Null;
				}
				return SqlTypeWorkarounds.SqlMoneyCtor(this._value._int64, 1);
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06001059 RID: 4185 RVA: 0x000501BD File Offset: 0x0004E3BD
		internal SqlSingle SqlSingle
		{
			get
			{
				if (SqlBuffer.StorageType.Single != this._type)
				{
					return (SqlSingle)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlSingle.Null;
				}
				return new SqlSingle(this._value._single);
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x0600105A RID: 4186 RVA: 0x000501F4 File Offset: 0x0004E3F4
		internal SqlString SqlString
		{
			get
			{
				if (SqlBuffer.StorageType.String == this._type)
				{
					if (this.IsNull)
					{
						return SqlString.Null;
					}
					return new SqlString((string)this._object);
				}
				else
				{
					if (SqlBuffer.StorageType.SqlCachedBuffer != this._type)
					{
						return (SqlString)this.SqlValue;
					}
					SqlCachedBuffer sqlCachedBuffer = (SqlCachedBuffer)this._object;
					if (sqlCachedBuffer.IsNull)
					{
						return SqlString.Null;
					}
					return sqlCachedBuffer.ToSqlString();
				}
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x0600105B RID: 4187 RVA: 0x00050260 File Offset: 0x0004E460
		internal object SqlValue
		{
			get
			{
				switch (this._type)
				{
				case SqlBuffer.StorageType.Empty:
					return DBNull.Value;
				case SqlBuffer.StorageType.Boolean:
					return this.SqlBoolean;
				case SqlBuffer.StorageType.Byte:
					return this.SqlByte;
				case SqlBuffer.StorageType.DateTime:
					return this.SqlDateTime;
				case SqlBuffer.StorageType.Decimal:
					return this.SqlDecimal;
				case SqlBuffer.StorageType.Double:
					return this.SqlDouble;
				case SqlBuffer.StorageType.Int16:
					return this.SqlInt16;
				case SqlBuffer.StorageType.Int32:
					return this.SqlInt32;
				case SqlBuffer.StorageType.Int64:
					return this.SqlInt64;
				case SqlBuffer.StorageType.Money:
					return this.SqlMoney;
				case SqlBuffer.StorageType.Single:
					return this.SqlSingle;
				case SqlBuffer.StorageType.String:
					return this.SqlString;
				case SqlBuffer.StorageType.SqlBinary:
				case SqlBuffer.StorageType.SqlGuid:
					return this._object;
				case SqlBuffer.StorageType.SqlCachedBuffer:
				{
					SqlCachedBuffer sqlCachedBuffer = (SqlCachedBuffer)this._object;
					if (sqlCachedBuffer.IsNull)
					{
						return SqlXml.Null;
					}
					return sqlCachedBuffer.ToSqlXml();
				}
				case SqlBuffer.StorageType.SqlXml:
					if (this._isNull)
					{
						return SqlXml.Null;
					}
					return (SqlXml)this._object;
				case SqlBuffer.StorageType.Date:
				case SqlBuffer.StorageType.DateTime2:
					if (this._isNull)
					{
						return DBNull.Value;
					}
					return this.DateTime;
				case SqlBuffer.StorageType.DateTimeOffset:
					if (this._isNull)
					{
						return DBNull.Value;
					}
					return this.DateTimeOffset;
				case SqlBuffer.StorageType.Time:
					if (this._isNull)
					{
						return DBNull.Value;
					}
					return this.Time;
				default:
					return null;
				}
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x0600105C RID: 4188 RVA: 0x000503EC File Offset: 0x0004E5EC
		internal object Value
		{
			get
			{
				if (this.IsNull)
				{
					return DBNull.Value;
				}
				switch (this._type)
				{
				case SqlBuffer.StorageType.Empty:
					return DBNull.Value;
				case SqlBuffer.StorageType.Boolean:
					return this.Boolean;
				case SqlBuffer.StorageType.Byte:
					return this.Byte;
				case SqlBuffer.StorageType.DateTime:
					return this.DateTime;
				case SqlBuffer.StorageType.Decimal:
					return this.Decimal;
				case SqlBuffer.StorageType.Double:
					return this.Double;
				case SqlBuffer.StorageType.Int16:
					return this.Int16;
				case SqlBuffer.StorageType.Int32:
					return this.Int32;
				case SqlBuffer.StorageType.Int64:
					return this.Int64;
				case SqlBuffer.StorageType.Money:
					return this.Decimal;
				case SqlBuffer.StorageType.Single:
					return this.Single;
				case SqlBuffer.StorageType.String:
					return this.String;
				case SqlBuffer.StorageType.SqlBinary:
					return this.ByteArray;
				case SqlBuffer.StorageType.SqlCachedBuffer:
					return ((SqlCachedBuffer)this._object).ToString();
				case SqlBuffer.StorageType.SqlGuid:
					return this.Guid;
				case SqlBuffer.StorageType.SqlXml:
					return ((SqlXml)this._object).Value;
				case SqlBuffer.StorageType.Date:
					return this.DateTime;
				case SqlBuffer.StorageType.DateTime2:
					return this.DateTime;
				case SqlBuffer.StorageType.DateTimeOffset:
					return this.DateTimeOffset;
				case SqlBuffer.StorageType.Time:
					return this.Time;
				default:
					return null;
				}
			}
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x00050554 File Offset: 0x0004E754
		internal Type GetTypeFromStorageType(bool isSqlType)
		{
			if (isSqlType)
			{
				switch (this._type)
				{
				case SqlBuffer.StorageType.Empty:
					return null;
				case SqlBuffer.StorageType.Boolean:
					return typeof(SqlBoolean);
				case SqlBuffer.StorageType.Byte:
					return typeof(SqlByte);
				case SqlBuffer.StorageType.DateTime:
					return typeof(SqlDateTime);
				case SqlBuffer.StorageType.Decimal:
					return typeof(SqlDecimal);
				case SqlBuffer.StorageType.Double:
					return typeof(SqlDouble);
				case SqlBuffer.StorageType.Int16:
					return typeof(SqlInt16);
				case SqlBuffer.StorageType.Int32:
					return typeof(SqlInt32);
				case SqlBuffer.StorageType.Int64:
					return typeof(SqlInt64);
				case SqlBuffer.StorageType.Money:
					return typeof(SqlMoney);
				case SqlBuffer.StorageType.Single:
					return typeof(SqlSingle);
				case SqlBuffer.StorageType.String:
					return typeof(SqlString);
				case SqlBuffer.StorageType.SqlBinary:
					return typeof(object);
				case SqlBuffer.StorageType.SqlCachedBuffer:
					return typeof(SqlString);
				case SqlBuffer.StorageType.SqlGuid:
					return typeof(object);
				case SqlBuffer.StorageType.SqlXml:
					return typeof(SqlXml);
				}
			}
			else
			{
				switch (this._type)
				{
				case SqlBuffer.StorageType.Empty:
					return null;
				case SqlBuffer.StorageType.Boolean:
					return typeof(bool);
				case SqlBuffer.StorageType.Byte:
					return typeof(byte);
				case SqlBuffer.StorageType.DateTime:
					return typeof(DateTime);
				case SqlBuffer.StorageType.Decimal:
					return typeof(decimal);
				case SqlBuffer.StorageType.Double:
					return typeof(double);
				case SqlBuffer.StorageType.Int16:
					return typeof(short);
				case SqlBuffer.StorageType.Int32:
					return typeof(int);
				case SqlBuffer.StorageType.Int64:
					return typeof(long);
				case SqlBuffer.StorageType.Money:
					return typeof(decimal);
				case SqlBuffer.StorageType.Single:
					return typeof(float);
				case SqlBuffer.StorageType.String:
					return typeof(string);
				case SqlBuffer.StorageType.SqlBinary:
					return typeof(byte[]);
				case SqlBuffer.StorageType.SqlCachedBuffer:
					return typeof(string);
				case SqlBuffer.StorageType.SqlGuid:
					return typeof(Guid);
				case SqlBuffer.StorageType.SqlXml:
					return typeof(string);
				}
			}
			return null;
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x0005075C File Offset: 0x0004E95C
		internal static SqlBuffer[] CreateBufferArray(int length)
		{
			SqlBuffer[] array = new SqlBuffer[length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new SqlBuffer();
			}
			return array;
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x00050788 File Offset: 0x0004E988
		internal static SqlBuffer[] CloneBufferArray(SqlBuffer[] values)
		{
			SqlBuffer[] array = new SqlBuffer[values.Length];
			for (int i = 0; i < values.Length; i++)
			{
				array[i] = new SqlBuffer(values[i]);
			}
			return array;
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x000507B8 File Offset: 0x0004E9B8
		internal static void Clear(SqlBuffer[] values)
		{
			if (values != null)
			{
				for (int i = 0; i < values.Length; i++)
				{
					values[i].Clear();
				}
			}
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x000507DE File Offset: 0x0004E9DE
		internal void Clear()
		{
			this._isNull = false;
			this._type = SqlBuffer.StorageType.Empty;
			this._object = null;
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x000507F5 File Offset: 0x0004E9F5
		internal void SetToDateTime(int daypart, int timepart)
		{
			this._value._dateTimeInfo.daypart = daypart;
			this._value._dateTimeInfo.timepart = timepart;
			this._type = SqlBuffer.StorageType.DateTime;
			this._isNull = false;
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x00050828 File Offset: 0x0004EA28
		internal void SetToDecimal(byte precision, byte scale, bool positive, int[] bits)
		{
			this._value._numericInfo.precision = precision;
			this._value._numericInfo.scale = scale;
			this._value._numericInfo.positive = positive;
			this._value._numericInfo.data1 = bits[0];
			this._value._numericInfo.data2 = bits[1];
			this._value._numericInfo.data3 = bits[2];
			this._value._numericInfo.data4 = bits[3];
			this._type = SqlBuffer.StorageType.Decimal;
			this._isNull = false;
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x000508C6 File Offset: 0x0004EAC6
		internal void SetToMoney(long value)
		{
			this._value._int64 = value;
			this._type = SqlBuffer.StorageType.Money;
			this._isNull = false;
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x000508E3 File Offset: 0x0004EAE3
		internal void SetToNullOfType(SqlBuffer.StorageType storageType)
		{
			this._type = storageType;
			this._isNull = true;
			this._object = null;
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x000508FA File Offset: 0x0004EAFA
		internal void SetToString(string value)
		{
			this._object = value;
			this._type = SqlBuffer.StorageType.String;
			this._isNull = false;
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x00050912 File Offset: 0x0004EB12
		internal void SetToDate(byte[] bytes)
		{
			this._type = SqlBuffer.StorageType.Date;
			this._value._int32 = SqlBuffer.GetDateFromByteArray(bytes, 0);
			this._isNull = false;
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x00050938 File Offset: 0x0004EB38
		internal void SetToDate(DateTime date)
		{
			this._type = SqlBuffer.StorageType.Date;
			this._value._int32 = date.Subtract(DateTime.MinValue).Days;
			this._isNull = false;
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x00050973 File Offset: 0x0004EB73
		internal void SetToTime(byte[] bytes, int length, byte scale)
		{
			this._type = SqlBuffer.StorageType.Time;
			SqlBuffer.FillInTimeInfo(ref this._value._timeInfo, bytes, length, scale);
			this._isNull = false;
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x00050997 File Offset: 0x0004EB97
		internal void SetToTime(TimeSpan timeSpan, byte scale)
		{
			this._type = SqlBuffer.StorageType.Time;
			this._value._timeInfo.ticks = timeSpan.Ticks;
			this._value._timeInfo.scale = scale;
			this._isNull = false;
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x000509D0 File Offset: 0x0004EBD0
		internal void SetToDateTime2(byte[] bytes, int length, byte scale)
		{
			this._type = SqlBuffer.StorageType.DateTime2;
			SqlBuffer.FillInTimeInfo(ref this._value._dateTime2Info.timeInfo, bytes, length - 3, scale);
			this._value._dateTime2Info.date = SqlBuffer.GetDateFromByteArray(bytes, length - 3);
			this._isNull = false;
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x00050A20 File Offset: 0x0004EC20
		internal void SetToDateTime2(DateTime dateTime, byte scale)
		{
			this._type = SqlBuffer.StorageType.DateTime2;
			this._value._dateTime2Info.timeInfo.ticks = dateTime.TimeOfDay.Ticks;
			this._value._dateTime2Info.timeInfo.scale = scale;
			this._value._dateTime2Info.date = dateTime.Subtract(DateTime.MinValue).Days;
			this._isNull = false;
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x00050A9C File Offset: 0x0004EC9C
		internal void SetToDateTimeOffset(byte[] bytes, int length, byte scale)
		{
			this._type = SqlBuffer.StorageType.DateTimeOffset;
			SqlBuffer.FillInTimeInfo(ref this._value._dateTimeOffsetInfo.dateTime2Info.timeInfo, bytes, length - 5, scale);
			this._value._dateTimeOffsetInfo.dateTime2Info.date = SqlBuffer.GetDateFromByteArray(bytes, length - 5);
			this._value._dateTimeOffsetInfo.offset = (short)((int)bytes[length - 2] + ((int)bytes[length - 1] << 8));
			this._isNull = false;
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x00050B14 File Offset: 0x0004ED14
		internal void SetToDateTimeOffset(DateTimeOffset dateTimeOffset, byte scale)
		{
			this._type = SqlBuffer.StorageType.DateTimeOffset;
			DateTime utcDateTime = dateTimeOffset.UtcDateTime;
			this._value._dateTimeOffsetInfo.dateTime2Info.timeInfo.ticks = utcDateTime.TimeOfDay.Ticks;
			this._value._dateTimeOffsetInfo.dateTime2Info.timeInfo.scale = scale;
			this._value._dateTimeOffsetInfo.dateTime2Info.date = utcDateTime.Subtract(DateTime.MinValue).Days;
			this._value._dateTimeOffsetInfo.offset = (short)dateTimeOffset.Offset.TotalMinutes;
			this._isNull = false;
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x00050BC8 File Offset: 0x0004EDC8
		private static void FillInTimeInfo(ref SqlBuffer.TimeInfo timeInfo, byte[] timeBytes, int length, byte scale)
		{
			long num = (long)((ulong)timeBytes[0] + ((ulong)timeBytes[1] << 8) + ((ulong)timeBytes[2] << 16));
			if (length > 3)
			{
				num += (long)((long)((ulong)timeBytes[3]) << 24);
			}
			if (length > 4)
			{
				num += (long)((long)((ulong)timeBytes[4]) << 32);
			}
			timeInfo.ticks = num * TdsEnums.TICKS_FROM_SCALE[(int)scale];
			timeInfo.scale = scale;
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x00050C1B File Offset: 0x0004EE1B
		private static int GetDateFromByteArray(byte[] buf, int offset)
		{
			return (int)buf[offset] + ((int)buf[offset + 1] << 8) + ((int)buf[offset + 2] << 16);
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x00050C31 File Offset: 0x0004EE31
		private void ThrowIfNull()
		{
			if (this.IsNull)
			{
				throw new SqlNullValueException();
			}
		}

		// Token: 0x04000A9C RID: 2716
		private bool _isNull;

		// Token: 0x04000A9D RID: 2717
		private SqlBuffer.StorageType _type;

		// Token: 0x04000A9E RID: 2718
		private SqlBuffer.Storage _value;

		// Token: 0x04000A9F RID: 2719
		private object _object;

		// Token: 0x04000AA0 RID: 2720
		private static string[] s_katmaiDateTimeOffsetFormatByScale = new string[] { "yyyy-MM-dd HH:mm:ss zzz", "yyyy-MM-dd HH:mm:ss.f zzz", "yyyy-MM-dd HH:mm:ss.ff zzz", "yyyy-MM-dd HH:mm:ss.fff zzz", "yyyy-MM-dd HH:mm:ss.ffff zzz", "yyyy-MM-dd HH:mm:ss.fffff zzz", "yyyy-MM-dd HH:mm:ss.ffffff zzz", "yyyy-MM-dd HH:mm:ss.fffffff zzz" };

		// Token: 0x04000AA1 RID: 2721
		private static string[] s_katmaiDateTime2FormatByScale = new string[] { "yyyy-MM-dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss.f", "yyyy-MM-dd HH:mm:ss.ff", "yyyy-MM-dd HH:mm:ss.fff", "yyyy-MM-dd HH:mm:ss.ffff", "yyyy-MM-dd HH:mm:ss.fffff", "yyyy-MM-dd HH:mm:ss.ffffff", "yyyy-MM-dd HH:mm:ss.fffffff" };

		// Token: 0x04000AA2 RID: 2722
		private static string[] s_katmaiTimeFormatByScale = new string[] { "HH:mm:ss", "HH:mm:ss.f", "HH:mm:ss.ff", "HH:mm:ss.fff", "HH:mm:ss.ffff", "HH:mm:ss.fffff", "HH:mm:ss.ffffff", "HH:mm:ss.fffffff" };

		// Token: 0x0200013C RID: 316
		internal enum StorageType
		{
			// Token: 0x04000AA4 RID: 2724
			Empty,
			// Token: 0x04000AA5 RID: 2725
			Boolean,
			// Token: 0x04000AA6 RID: 2726
			Byte,
			// Token: 0x04000AA7 RID: 2727
			DateTime,
			// Token: 0x04000AA8 RID: 2728
			Decimal,
			// Token: 0x04000AA9 RID: 2729
			Double,
			// Token: 0x04000AAA RID: 2730
			Int16,
			// Token: 0x04000AAB RID: 2731
			Int32,
			// Token: 0x04000AAC RID: 2732
			Int64,
			// Token: 0x04000AAD RID: 2733
			Money,
			// Token: 0x04000AAE RID: 2734
			Single,
			// Token: 0x04000AAF RID: 2735
			String,
			// Token: 0x04000AB0 RID: 2736
			SqlBinary,
			// Token: 0x04000AB1 RID: 2737
			SqlCachedBuffer,
			// Token: 0x04000AB2 RID: 2738
			SqlGuid,
			// Token: 0x04000AB3 RID: 2739
			SqlXml,
			// Token: 0x04000AB4 RID: 2740
			Date,
			// Token: 0x04000AB5 RID: 2741
			DateTime2,
			// Token: 0x04000AB6 RID: 2742
			DateTimeOffset,
			// Token: 0x04000AB7 RID: 2743
			Time
		}

		// Token: 0x0200013D RID: 317
		internal struct DateTimeInfo
		{
			// Token: 0x04000AB8 RID: 2744
			internal int daypart;

			// Token: 0x04000AB9 RID: 2745
			internal int timepart;
		}

		// Token: 0x0200013E RID: 318
		internal struct NumericInfo
		{
			// Token: 0x04000ABA RID: 2746
			internal int data1;

			// Token: 0x04000ABB RID: 2747
			internal int data2;

			// Token: 0x04000ABC RID: 2748
			internal int data3;

			// Token: 0x04000ABD RID: 2749
			internal int data4;

			// Token: 0x04000ABE RID: 2750
			internal byte precision;

			// Token: 0x04000ABF RID: 2751
			internal byte scale;

			// Token: 0x04000AC0 RID: 2752
			internal bool positive;
		}

		// Token: 0x0200013F RID: 319
		internal struct TimeInfo
		{
			// Token: 0x04000AC1 RID: 2753
			internal long ticks;

			// Token: 0x04000AC2 RID: 2754
			internal byte scale;
		}

		// Token: 0x02000140 RID: 320
		internal struct DateTime2Info
		{
			// Token: 0x04000AC3 RID: 2755
			internal int date;

			// Token: 0x04000AC4 RID: 2756
			internal SqlBuffer.TimeInfo timeInfo;
		}

		// Token: 0x02000141 RID: 321
		internal struct DateTimeOffsetInfo
		{
			// Token: 0x04000AC5 RID: 2757
			internal SqlBuffer.DateTime2Info dateTime2Info;

			// Token: 0x04000AC6 RID: 2758
			internal short offset;
		}

		// Token: 0x02000142 RID: 322
		[StructLayout(LayoutKind.Explicit)]
		internal struct Storage
		{
			// Token: 0x04000AC7 RID: 2759
			[FieldOffset(0)]
			internal bool _boolean;

			// Token: 0x04000AC8 RID: 2760
			[FieldOffset(0)]
			internal byte _byte;

			// Token: 0x04000AC9 RID: 2761
			[FieldOffset(0)]
			internal SqlBuffer.DateTimeInfo _dateTimeInfo;

			// Token: 0x04000ACA RID: 2762
			[FieldOffset(0)]
			internal double _double;

			// Token: 0x04000ACB RID: 2763
			[FieldOffset(0)]
			internal SqlBuffer.NumericInfo _numericInfo;

			// Token: 0x04000ACC RID: 2764
			[FieldOffset(0)]
			internal short _int16;

			// Token: 0x04000ACD RID: 2765
			[FieldOffset(0)]
			internal int _int32;

			// Token: 0x04000ACE RID: 2766
			[FieldOffset(0)]
			internal long _int64;

			// Token: 0x04000ACF RID: 2767
			[FieldOffset(0)]
			internal float _single;

			// Token: 0x04000AD0 RID: 2768
			[FieldOffset(0)]
			internal SqlBuffer.TimeInfo _timeInfo;

			// Token: 0x04000AD1 RID: 2769
			[FieldOffset(0)]
			internal SqlBuffer.DateTime2Info _dateTime2Info;

			// Token: 0x04000AD2 RID: 2770
			[FieldOffset(0)]
			internal SqlBuffer.DateTimeOffsetInfo _dateTimeOffsetInfo;
		}
	}
}
