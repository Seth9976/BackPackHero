using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000371 RID: 881
	internal sealed class UInt16Storage : DataStorage
	{
		// Token: 0x06002ABD RID: 10941 RVA: 0x000BC6A7 File Offset: 0x000BA8A7
		public UInt16Storage(DataColumn column)
			: base(column, typeof(ushort), UInt16Storage.s_defaultValue, StorageType.UInt16)
		{
		}

		// Token: 0x06002ABE RID: 10942 RVA: 0x000BC6C8 File Offset: 0x000BA8C8
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Sum:
				{
					ulong num = (ulong)UInt16Storage.s_defaultValue;
					checked
					{
						foreach (int num2 in records)
						{
							if (base.HasValue(num2))
							{
								num += unchecked((ulong)this._values[num2]);
								flag = true;
							}
						}
						if (flag)
						{
							return num;
						}
						return this._nullValue;
					}
				}
				case AggregateType.Mean:
				{
					long num3 = (long)((ulong)UInt16Storage.s_defaultValue);
					int num4 = 0;
					foreach (int num5 in records)
					{
						if (base.HasValue(num5))
						{
							checked
							{
								num3 += (long)(unchecked((ulong)this._values[num5]));
							}
							num4++;
							flag = true;
						}
					}
					checked
					{
						if (flag)
						{
							return (ushort)(num3 / unchecked((long)num4));
						}
						return this._nullValue;
					}
				}
				case AggregateType.Min:
				{
					ushort num6 = ushort.MaxValue;
					foreach (int num7 in records)
					{
						if (base.HasValue(num7))
						{
							num6 = Math.Min(this._values[num7], num6);
							flag = true;
						}
					}
					if (flag)
					{
						return num6;
					}
					return this._nullValue;
				}
				case AggregateType.Max:
				{
					ushort num8 = 0;
					foreach (int num9 in records)
					{
						if (base.HasValue(num9))
						{
							num8 = Math.Max(this._values[num9], num8);
							flag = true;
						}
					}
					if (flag)
					{
						return num8;
					}
					return this._nullValue;
				}
				case AggregateType.First:
					if (records.Length != 0)
					{
						return this._values[records[0]];
					}
					return null;
				case AggregateType.Count:
				{
					int num10 = 0;
					for (int l = 0; l < records.Length; l++)
					{
						if (base.HasValue(records[l]))
						{
							num10++;
						}
					}
					return num10;
				}
				case AggregateType.Var:
				case AggregateType.StDev:
				{
					int num10 = 0;
					double num11 = 0.0;
					double num12 = 0.0;
					foreach (int num13 in records)
					{
						if (base.HasValue(num13))
						{
							num11 += (double)this._values[num13];
							num12 += (double)this._values[num13] * (double)this._values[num13];
							num10++;
						}
					}
					if (num10 <= 1)
					{
						return this._nullValue;
					}
					double num14 = (double)num10 * num12 - num11 * num11;
					if (num14 / (num11 * num11) < 1E-15 || num14 < 0.0)
					{
						num14 = 0.0;
					}
					else
					{
						num14 /= (double)(num10 * (num10 - 1));
					}
					if (kind == AggregateType.StDev)
					{
						return Math.Sqrt(num14);
					}
					return num14;
				}
				}
			}
			catch (OverflowException)
			{
				throw ExprException.Overflow(typeof(ushort));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002ABF RID: 10943 RVA: 0x000BC9F4 File Offset: 0x000BABF4
		public override int Compare(int recordNo1, int recordNo2)
		{
			ushort num = this._values[recordNo1];
			ushort num2 = this._values[recordNo2];
			if (num == UInt16Storage.s_defaultValue || num2 == UInt16Storage.s_defaultValue)
			{
				int num3 = base.CompareBits(recordNo1, recordNo2);
				if (num3 != 0)
				{
					return num3;
				}
			}
			return (int)(num - num2);
		}

		// Token: 0x06002AC0 RID: 10944 RVA: 0x000BCA34 File Offset: 0x000BAC34
		public override int CompareValueTo(int recordNo, object value)
		{
			if (this._nullValue == value)
			{
				if (!base.HasValue(recordNo))
				{
					return 0;
				}
				return 1;
			}
			else
			{
				ushort num = this._values[recordNo];
				if (UInt16Storage.s_defaultValue == num && !base.HasValue(recordNo))
				{
					return -1;
				}
				return num.CompareTo((ushort)value);
			}
		}

		// Token: 0x06002AC1 RID: 10945 RVA: 0x000BCA80 File Offset: 0x000BAC80
		public override object ConvertValue(object value)
		{
			if (this._nullValue != value)
			{
				if (value != null)
				{
					value = ((IConvertible)value).ToUInt16(base.FormatProvider);
				}
				else
				{
					value = this._nullValue;
				}
			}
			return value;
		}

		// Token: 0x06002AC2 RID: 10946 RVA: 0x000BCAB1 File Offset: 0x000BACB1
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06002AC3 RID: 10947 RVA: 0x000BCACC File Offset: 0x000BACCC
		public override object Get(int record)
		{
			ushort num = this._values[record];
			if (!num.Equals(UInt16Storage.s_defaultValue))
			{
				return num;
			}
			return base.GetBits(record);
		}

		// Token: 0x06002AC4 RID: 10948 RVA: 0x000BCB00 File Offset: 0x000BAD00
		public override void Set(int record, object value)
		{
			if (this._nullValue == value)
			{
				this._values[record] = UInt16Storage.s_defaultValue;
				base.SetNullBit(record, true);
				return;
			}
			this._values[record] = ((IConvertible)value).ToUInt16(base.FormatProvider);
			base.SetNullBit(record, false);
		}

		// Token: 0x06002AC5 RID: 10949 RVA: 0x000BCB50 File Offset: 0x000BAD50
		public override void SetCapacity(int capacity)
		{
			ushort[] array = new ushort[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
			base.SetCapacity(capacity);
		}

		// Token: 0x06002AC6 RID: 10950 RVA: 0x000BCB96 File Offset: 0x000BAD96
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToUInt16(s);
		}

		// Token: 0x06002AC7 RID: 10951 RVA: 0x000BCBA3 File Offset: 0x000BADA3
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((ushort)value);
		}

		// Token: 0x06002AC8 RID: 10952 RVA: 0x000BCBB0 File Offset: 0x000BADB0
		protected override object GetEmptyStorage(int recordCount)
		{
			return new ushort[recordCount];
		}

		// Token: 0x06002AC9 RID: 10953 RVA: 0x000BCBB8 File Offset: 0x000BADB8
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((ushort[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, !base.HasValue(record));
		}

		// Token: 0x06002ACA RID: 10954 RVA: 0x000BCBDD File Offset: 0x000BADDD
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (ushort[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x040019D9 RID: 6617
		private static readonly ushort s_defaultValue;

		// Token: 0x040019DA RID: 6618
		private ushort[] _values;
	}
}
