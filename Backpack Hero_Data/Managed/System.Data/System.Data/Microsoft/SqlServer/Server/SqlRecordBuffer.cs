using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x020003A7 RID: 935
	internal sealed class SqlRecordBuffer
	{
		// Token: 0x06002D51 RID: 11601 RVA: 0x000C3484 File Offset: 0x000C1684
		internal SqlRecordBuffer(SmiMetaData metaData)
		{
			this._isNull = true;
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06002D52 RID: 11602 RVA: 0x000C3493 File Offset: 0x000C1693
		internal bool IsNull
		{
			get
			{
				return this._isNull;
			}
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06002D53 RID: 11603 RVA: 0x000C349B File Offset: 0x000C169B
		// (set) Token: 0x06002D54 RID: 11604 RVA: 0x000C34A8 File Offset: 0x000C16A8
		internal bool Boolean
		{
			get
			{
				return this._value._boolean;
			}
			set
			{
				this._value._boolean = value;
				this._type = SqlRecordBuffer.StorageType.Boolean;
				this._isNull = false;
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06002D55 RID: 11605 RVA: 0x000C34C4 File Offset: 0x000C16C4
		// (set) Token: 0x06002D56 RID: 11606 RVA: 0x000C34D1 File Offset: 0x000C16D1
		internal byte Byte
		{
			get
			{
				return this._value._byte;
			}
			set
			{
				this._value._byte = value;
				this._type = SqlRecordBuffer.StorageType.Byte;
				this._isNull = false;
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06002D57 RID: 11607 RVA: 0x000C34ED File Offset: 0x000C16ED
		// (set) Token: 0x06002D58 RID: 11608 RVA: 0x000C34FA File Offset: 0x000C16FA
		internal DateTime DateTime
		{
			get
			{
				return this._value._dateTime;
			}
			set
			{
				this._value._dateTime = value;
				this._type = SqlRecordBuffer.StorageType.DateTime;
				this._isNull = false;
				if (this._isMetaSet)
				{
					this._isMetaSet = false;
					return;
				}
				this._metadata = null;
			}
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06002D59 RID: 11609 RVA: 0x000C352D File Offset: 0x000C172D
		// (set) Token: 0x06002D5A RID: 11610 RVA: 0x000C353A File Offset: 0x000C173A
		internal DateTimeOffset DateTimeOffset
		{
			get
			{
				return this._value._dateTimeOffset;
			}
			set
			{
				this._value._dateTimeOffset = value;
				this._type = SqlRecordBuffer.StorageType.DateTimeOffset;
				this._isNull = false;
			}
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06002D5B RID: 11611 RVA: 0x000C3556 File Offset: 0x000C1756
		// (set) Token: 0x06002D5C RID: 11612 RVA: 0x000C3563 File Offset: 0x000C1763
		internal double Double
		{
			get
			{
				return this._value._double;
			}
			set
			{
				this._value._double = value;
				this._type = SqlRecordBuffer.StorageType.Double;
				this._isNull = false;
			}
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06002D5D RID: 11613 RVA: 0x000C357F File Offset: 0x000C177F
		// (set) Token: 0x06002D5E RID: 11614 RVA: 0x000C358C File Offset: 0x000C178C
		internal Guid Guid
		{
			get
			{
				return this._value._guid;
			}
			set
			{
				this._value._guid = value;
				this._type = SqlRecordBuffer.StorageType.Guid;
				this._isNull = false;
			}
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06002D5F RID: 11615 RVA: 0x000C35A8 File Offset: 0x000C17A8
		// (set) Token: 0x06002D60 RID: 11616 RVA: 0x000C35B5 File Offset: 0x000C17B5
		internal short Int16
		{
			get
			{
				return this._value._int16;
			}
			set
			{
				this._value._int16 = value;
				this._type = SqlRecordBuffer.StorageType.Int16;
				this._isNull = false;
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06002D61 RID: 11617 RVA: 0x000C35D1 File Offset: 0x000C17D1
		// (set) Token: 0x06002D62 RID: 11618 RVA: 0x000C35DE File Offset: 0x000C17DE
		internal int Int32
		{
			get
			{
				return this._value._int32;
			}
			set
			{
				this._value._int32 = value;
				this._type = SqlRecordBuffer.StorageType.Int32;
				this._isNull = false;
			}
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06002D63 RID: 11619 RVA: 0x000C35FB File Offset: 0x000C17FB
		// (set) Token: 0x06002D64 RID: 11620 RVA: 0x000C3608 File Offset: 0x000C1808
		internal long Int64
		{
			get
			{
				return this._value._int64;
			}
			set
			{
				this._value._int64 = value;
				this._type = SqlRecordBuffer.StorageType.Int64;
				this._isNull = false;
				if (this._isMetaSet)
				{
					this._isMetaSet = false;
					return;
				}
				this._metadata = null;
			}
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06002D65 RID: 11621 RVA: 0x000C363C File Offset: 0x000C183C
		// (set) Token: 0x06002D66 RID: 11622 RVA: 0x000C3649 File Offset: 0x000C1849
		internal float Single
		{
			get
			{
				return this._value._single;
			}
			set
			{
				this._value._single = value;
				this._type = SqlRecordBuffer.StorageType.Single;
				this._isNull = false;
			}
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06002D67 RID: 11623 RVA: 0x000C3668 File Offset: 0x000C1868
		// (set) Token: 0x06002D68 RID: 11624 RVA: 0x000C36C8 File Offset: 0x000C18C8
		internal string String
		{
			get
			{
				if (SqlRecordBuffer.StorageType.String == this._type)
				{
					return (string)this._object;
				}
				if (SqlRecordBuffer.StorageType.CharArray == this._type)
				{
					return new string((char[])this._object, 0, (int)this.CharsLength);
				}
				return new SqlXml(new MemoryStream((byte[])this._object, false)).Value;
			}
			set
			{
				this._object = value;
				this._value._int64 = (long)value.Length;
				this._type = SqlRecordBuffer.StorageType.String;
				this._isNull = false;
				if (this._isMetaSet)
				{
					this._isMetaSet = false;
					return;
				}
				this._metadata = null;
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06002D69 RID: 11625 RVA: 0x000C3714 File Offset: 0x000C1914
		// (set) Token: 0x06002D6A RID: 11626 RVA: 0x000C3721 File Offset: 0x000C1921
		internal SqlDecimal SqlDecimal
		{
			get
			{
				return (SqlDecimal)this._object;
			}
			set
			{
				this._object = value;
				this._type = SqlRecordBuffer.StorageType.SqlDecimal;
				this._isNull = false;
			}
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06002D6B RID: 11627 RVA: 0x000C373E File Offset: 0x000C193E
		// (set) Token: 0x06002D6C RID: 11628 RVA: 0x000C374B File Offset: 0x000C194B
		internal TimeSpan TimeSpan
		{
			get
			{
				return this._value._timeSpan;
			}
			set
			{
				this._value._timeSpan = value;
				this._type = SqlRecordBuffer.StorageType.TimeSpan;
				this._isNull = false;
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06002D6D RID: 11629 RVA: 0x000C3768 File Offset: 0x000C1968
		// (set) Token: 0x06002D6E RID: 11630 RVA: 0x000C3785 File Offset: 0x000C1985
		internal long BytesLength
		{
			get
			{
				if (SqlRecordBuffer.StorageType.String == this._type)
				{
					this.ConvertXmlStringToByteArray();
				}
				return this._value._int64;
			}
			set
			{
				if (value == 0L)
				{
					this._value._int64 = value;
					this._object = Array.Empty<byte>();
					this._type = SqlRecordBuffer.StorageType.ByteArray;
					this._isNull = false;
					return;
				}
				this._value._int64 = value;
			}
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06002D6F RID: 11631 RVA: 0x000C35FB File Offset: 0x000C17FB
		// (set) Token: 0x06002D70 RID: 11632 RVA: 0x000C37BC File Offset: 0x000C19BC
		internal long CharsLength
		{
			get
			{
				return this._value._int64;
			}
			set
			{
				if (value == 0L)
				{
					this._value._int64 = value;
					this._object = Array.Empty<char>();
					this._type = SqlRecordBuffer.StorageType.CharArray;
					this._isNull = false;
					return;
				}
				this._value._int64 = value;
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06002D71 RID: 11633 RVA: 0x000C37F4 File Offset: 0x000C19F4
		// (set) Token: 0x06002D72 RID: 11634 RVA: 0x000C38F6 File Offset: 0x000C1AF6
		internal SmiMetaData VariantType
		{
			get
			{
				switch (this._type)
				{
				case SqlRecordBuffer.StorageType.Boolean:
					return SmiMetaData.DefaultBit;
				case SqlRecordBuffer.StorageType.Byte:
					return SmiMetaData.DefaultTinyInt;
				case SqlRecordBuffer.StorageType.ByteArray:
					return SmiMetaData.DefaultVarBinary;
				case SqlRecordBuffer.StorageType.CharArray:
					return SmiMetaData.DefaultNVarChar;
				case SqlRecordBuffer.StorageType.DateTime:
					return this._metadata ?? SmiMetaData.DefaultDateTime;
				case SqlRecordBuffer.StorageType.DateTimeOffset:
					return SmiMetaData.DefaultDateTimeOffset;
				case SqlRecordBuffer.StorageType.Double:
					return SmiMetaData.DefaultFloat;
				case SqlRecordBuffer.StorageType.Guid:
					return SmiMetaData.DefaultUniqueIdentifier;
				case SqlRecordBuffer.StorageType.Int16:
					return SmiMetaData.DefaultSmallInt;
				case SqlRecordBuffer.StorageType.Int32:
					return SmiMetaData.DefaultInt;
				case SqlRecordBuffer.StorageType.Int64:
					return this._metadata ?? SmiMetaData.DefaultBigInt;
				case SqlRecordBuffer.StorageType.Single:
					return SmiMetaData.DefaultReal;
				case SqlRecordBuffer.StorageType.String:
					return this._metadata ?? SmiMetaData.DefaultNVarChar;
				case SqlRecordBuffer.StorageType.SqlDecimal:
					return new SmiMetaData(SqlDbType.Decimal, 17L, ((SqlDecimal)this._object).Precision, ((SqlDecimal)this._object).Scale, 0L, SqlCompareOptions.None, null);
				case SqlRecordBuffer.StorageType.TimeSpan:
					return SmiMetaData.DefaultTime;
				default:
					return null;
				}
			}
			set
			{
				this._metadata = value;
				this._isMetaSet = true;
			}
		}

		// Token: 0x06002D73 RID: 11635 RVA: 0x000C3908 File Offset: 0x000C1B08
		internal int GetBytes(long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			int num = (int)fieldOffset;
			if (SqlRecordBuffer.StorageType.String == this._type)
			{
				this.ConvertXmlStringToByteArray();
			}
			Buffer.BlockCopy((byte[])this._object, num, buffer, bufferOffset, length);
			return length;
		}

		// Token: 0x06002D74 RID: 11636 RVA: 0x000C3940 File Offset: 0x000C1B40
		internal int GetChars(long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			int num = (int)fieldOffset;
			if (SqlRecordBuffer.StorageType.CharArray == this._type)
			{
				Array.Copy((char[])this._object, num, buffer, bufferOffset, length);
			}
			else
			{
				((string)this._object).CopyTo(num, buffer, bufferOffset, length);
			}
			return length;
		}

		// Token: 0x06002D75 RID: 11637 RVA: 0x000C3988 File Offset: 0x000C1B88
		internal int SetBytes(long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			int num = (int)fieldOffset;
			if (this.IsNull || SqlRecordBuffer.StorageType.ByteArray != this._type)
			{
				if (num != 0)
				{
					throw ADP.ArgumentOutOfRange("fieldOffset");
				}
				this._object = new byte[length];
				this._type = SqlRecordBuffer.StorageType.ByteArray;
				this._isNull = false;
				this.BytesLength = (long)length;
			}
			else
			{
				if ((long)num > this.BytesLength)
				{
					throw ADP.ArgumentOutOfRange("fieldOffset");
				}
				if ((long)(num + length) > this.BytesLength)
				{
					int num2 = ((byte[])this._object).Length;
					if (num + length > num2)
					{
						byte[] array = new byte[Math.Max(num + length, 2 * num2)];
						Buffer.BlockCopy((byte[])this._object, 0, array, 0, (int)this.BytesLength);
						this._object = array;
					}
					this.BytesLength = (long)(num + length);
				}
			}
			Buffer.BlockCopy(buffer, bufferOffset, (byte[])this._object, num, length);
			return length;
		}

		// Token: 0x06002D76 RID: 11638 RVA: 0x000C3A6C File Offset: 0x000C1C6C
		internal int SetChars(long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			int num = (int)fieldOffset;
			if (this.IsNull || (SqlRecordBuffer.StorageType.CharArray != this._type && SqlRecordBuffer.StorageType.String != this._type))
			{
				if (num != 0)
				{
					throw ADP.ArgumentOutOfRange("fieldOffset");
				}
				this._object = new char[length];
				this._type = SqlRecordBuffer.StorageType.CharArray;
				this._isNull = false;
				this.CharsLength = (long)length;
			}
			else
			{
				if ((long)num > this.CharsLength)
				{
					throw ADP.ArgumentOutOfRange("fieldOffset");
				}
				if (SqlRecordBuffer.StorageType.String == this._type)
				{
					this._object = ((string)this._object).ToCharArray();
					this._type = SqlRecordBuffer.StorageType.CharArray;
				}
				if ((long)(num + length) > this.CharsLength)
				{
					int num2 = ((char[])this._object).Length;
					if (num + length > num2)
					{
						char[] array = new char[Math.Max(num + length, 2 * num2)];
						Array.Copy((char[])this._object, 0, array, 0, (int)this.CharsLength);
						this._object = array;
					}
					this.CharsLength = (long)(num + length);
				}
			}
			Array.Copy(buffer, bufferOffset, (char[])this._object, num, length);
			return length;
		}

		// Token: 0x06002D77 RID: 11639 RVA: 0x000C3B81 File Offset: 0x000C1D81
		internal void SetNull()
		{
			this._isNull = true;
		}

		// Token: 0x06002D78 RID: 11640 RVA: 0x000C3B8C File Offset: 0x000C1D8C
		private void ConvertXmlStringToByteArray()
		{
			string text = (string)this._object;
			byte[] array = new byte[2 + Encoding.Unicode.GetByteCount(text)];
			array[0] = byte.MaxValue;
			array[1] = 254;
			Encoding.Unicode.GetBytes(text, 0, text.Length, array, 2);
			this._object = array;
			this._value._int64 = (long)array.Length;
			this._type = SqlRecordBuffer.StorageType.ByteArray;
		}

		// Token: 0x04001B4E RID: 6990
		private bool _isNull;

		// Token: 0x04001B4F RID: 6991
		private SqlRecordBuffer.StorageType _type;

		// Token: 0x04001B50 RID: 6992
		private SqlRecordBuffer.Storage _value;

		// Token: 0x04001B51 RID: 6993
		private object _object;

		// Token: 0x04001B52 RID: 6994
		private SmiMetaData _metadata;

		// Token: 0x04001B53 RID: 6995
		private bool _isMetaSet;

		// Token: 0x020003A8 RID: 936
		internal enum StorageType
		{
			// Token: 0x04001B55 RID: 6997
			Boolean,
			// Token: 0x04001B56 RID: 6998
			Byte,
			// Token: 0x04001B57 RID: 6999
			ByteArray,
			// Token: 0x04001B58 RID: 7000
			CharArray,
			// Token: 0x04001B59 RID: 7001
			DateTime,
			// Token: 0x04001B5A RID: 7002
			DateTimeOffset,
			// Token: 0x04001B5B RID: 7003
			Double,
			// Token: 0x04001B5C RID: 7004
			Guid,
			// Token: 0x04001B5D RID: 7005
			Int16,
			// Token: 0x04001B5E RID: 7006
			Int32,
			// Token: 0x04001B5F RID: 7007
			Int64,
			// Token: 0x04001B60 RID: 7008
			Single,
			// Token: 0x04001B61 RID: 7009
			String,
			// Token: 0x04001B62 RID: 7010
			SqlDecimal,
			// Token: 0x04001B63 RID: 7011
			TimeSpan
		}

		// Token: 0x020003A9 RID: 937
		[StructLayout(LayoutKind.Explicit)]
		internal struct Storage
		{
			// Token: 0x04001B64 RID: 7012
			[FieldOffset(0)]
			internal bool _boolean;

			// Token: 0x04001B65 RID: 7013
			[FieldOffset(0)]
			internal byte _byte;

			// Token: 0x04001B66 RID: 7014
			[FieldOffset(0)]
			internal DateTime _dateTime;

			// Token: 0x04001B67 RID: 7015
			[FieldOffset(0)]
			internal DateTimeOffset _dateTimeOffset;

			// Token: 0x04001B68 RID: 7016
			[FieldOffset(0)]
			internal double _double;

			// Token: 0x04001B69 RID: 7017
			[FieldOffset(0)]
			internal Guid _guid;

			// Token: 0x04001B6A RID: 7018
			[FieldOffset(0)]
			internal short _int16;

			// Token: 0x04001B6B RID: 7019
			[FieldOffset(0)]
			internal int _int32;

			// Token: 0x04001B6C RID: 7020
			[FieldOffset(0)]
			internal long _int64;

			// Token: 0x04001B6D RID: 7021
			[FieldOffset(0)]
			internal float _single;

			// Token: 0x04001B6E RID: 7022
			[FieldOffset(0)]
			internal TimeSpan _timeSpan;
		}
	}
}
