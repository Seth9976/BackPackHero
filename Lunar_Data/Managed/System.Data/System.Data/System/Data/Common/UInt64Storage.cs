using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000373 RID: 883
	internal sealed class UInt64Storage : DataStorage
	{
		// Token: 0x06002AD9 RID: 10969 RVA: 0x000BD14A File Offset: 0x000BB34A
		public UInt64Storage(DataColumn column)
			: base(column, typeof(ulong), UInt64Storage.s_defaultValue, StorageType.UInt64)
		{
		}

		// Token: 0x06002ADA RID: 10970 RVA: 0x000BD16C File Offset: 0x000BB36C
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Sum:
				{
					ulong num = UInt64Storage.s_defaultValue;
					checked
					{
						foreach (int num2 in records)
						{
							if (base.HasValue(num2))
							{
								num += this._values[num2];
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
					decimal num3 = UInt64Storage.s_defaultValue;
					int num4 = 0;
					foreach (int num5 in records)
					{
						if (base.HasValue(num5))
						{
							num3 += this._values[num5];
							num4++;
							flag = true;
						}
					}
					if (flag)
					{
						return (ulong)(num3 / num4);
					}
					return this._nullValue;
				}
				case AggregateType.Min:
				{
					ulong num6 = ulong.MaxValue;
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
					ulong num8 = 0UL;
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
					return base.Aggregate(records, kind);
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
							num11 += this._values[num13];
							num12 += this._values[num13] * this._values[num13];
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
				throw ExprException.Overflow(typeof(ulong));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002ADB RID: 10971 RVA: 0x000BD48C File Offset: 0x000BB68C
		public override int Compare(int recordNo1, int recordNo2)
		{
			ulong num = this._values[recordNo1];
			ulong num2 = this._values[recordNo2];
			if (num.Equals(UInt64Storage.s_defaultValue) || num2.Equals(UInt64Storage.s_defaultValue))
			{
				int num3 = base.CompareBits(recordNo1, recordNo2);
				if (num3 != 0)
				{
					return num3;
				}
			}
			if (num < num2)
			{
				return -1;
			}
			if (num <= num2)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06002ADC RID: 10972 RVA: 0x000BD4E4 File Offset: 0x000BB6E4
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
				ulong num = this._values[recordNo];
				if (UInt64Storage.s_defaultValue == num && !base.HasValue(recordNo))
				{
					return -1;
				}
				return num.CompareTo((ulong)value);
			}
		}

		// Token: 0x06002ADD RID: 10973 RVA: 0x000BD530 File Offset: 0x000BB730
		public override object ConvertValue(object value)
		{
			if (this._nullValue != value)
			{
				if (value != null)
				{
					value = ((IConvertible)value).ToUInt64(base.FormatProvider);
				}
				else
				{
					value = this._nullValue;
				}
			}
			return value;
		}

		// Token: 0x06002ADE RID: 10974 RVA: 0x000BD561 File Offset: 0x000BB761
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06002ADF RID: 10975 RVA: 0x000BD57C File Offset: 0x000BB77C
		public override object Get(int record)
		{
			ulong num = this._values[record];
			if (!num.Equals(UInt64Storage.s_defaultValue))
			{
				return num;
			}
			return base.GetBits(record);
		}

		// Token: 0x06002AE0 RID: 10976 RVA: 0x000BD5B0 File Offset: 0x000BB7B0
		public override void Set(int record, object value)
		{
			if (this._nullValue == value)
			{
				this._values[record] = UInt64Storage.s_defaultValue;
				base.SetNullBit(record, true);
				return;
			}
			this._values[record] = ((IConvertible)value).ToUInt64(base.FormatProvider);
			base.SetNullBit(record, false);
		}

		// Token: 0x06002AE1 RID: 10977 RVA: 0x000BD600 File Offset: 0x000BB800
		public override void SetCapacity(int capacity)
		{
			ulong[] array = new ulong[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
			base.SetCapacity(capacity);
		}

		// Token: 0x06002AE2 RID: 10978 RVA: 0x000BD646 File Offset: 0x000BB846
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToUInt64(s);
		}

		// Token: 0x06002AE3 RID: 10979 RVA: 0x000BD653 File Offset: 0x000BB853
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((ulong)value);
		}

		// Token: 0x06002AE4 RID: 10980 RVA: 0x000BD660 File Offset: 0x000BB860
		protected override object GetEmptyStorage(int recordCount)
		{
			return new ulong[recordCount];
		}

		// Token: 0x06002AE5 RID: 10981 RVA: 0x000BD668 File Offset: 0x000BB868
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((ulong[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, !base.HasValue(record));
		}

		// Token: 0x06002AE6 RID: 10982 RVA: 0x000BD68D File Offset: 0x000BB88D
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (ulong[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x040019DD RID: 6621
		private static readonly ulong s_defaultValue;

		// Token: 0x040019DE RID: 6622
		private ulong[] _values;
	}
}
