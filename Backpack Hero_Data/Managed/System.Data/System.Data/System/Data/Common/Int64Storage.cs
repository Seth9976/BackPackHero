using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000352 RID: 850
	internal sealed class Int64Storage : DataStorage
	{
		// Token: 0x06002931 RID: 10545 RVA: 0x000B3C0A File Offset: 0x000B1E0A
		internal Int64Storage(DataColumn column)
			: base(column, typeof(long), 0L, StorageType.Int64)
		{
		}

		// Token: 0x06002932 RID: 10546 RVA: 0x000B3C28 File Offset: 0x000B1E28
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Sum:
				{
					long num = 0L;
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
					decimal num3 = 0m;
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
						return (long)(num3 / num4);
					}
					return this._nullValue;
				}
				case AggregateType.Min:
				{
					long num6 = long.MaxValue;
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
					long num8 = long.MinValue;
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
				throw ExprException.Overflow(typeof(long));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002933 RID: 10547 RVA: 0x000B3F4C File Offset: 0x000B214C
		public override int Compare(int recordNo1, int recordNo2)
		{
			long num = this._values[recordNo1];
			long num2 = this._values[recordNo2];
			if (num == 0L || num2 == 0L)
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

		// Token: 0x06002934 RID: 10548 RVA: 0x000B3F8C File Offset: 0x000B218C
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
				long num = this._values[recordNo];
				if (num == 0L && !base.HasValue(recordNo))
				{
					return -1;
				}
				return num.CompareTo((long)value);
			}
		}

		// Token: 0x06002935 RID: 10549 RVA: 0x000B3FD3 File Offset: 0x000B21D3
		public override object ConvertValue(object value)
		{
			if (this._nullValue != value)
			{
				if (value != null)
				{
					value = ((IConvertible)value).ToInt64(base.FormatProvider);
				}
				else
				{
					value = this._nullValue;
				}
			}
			return value;
		}

		// Token: 0x06002936 RID: 10550 RVA: 0x000B4004 File Offset: 0x000B2204
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06002937 RID: 10551 RVA: 0x000B4020 File Offset: 0x000B2220
		public override object Get(int record)
		{
			long num = this._values[record];
			if (num != 0L)
			{
				return num;
			}
			return base.GetBits(record);
		}

		// Token: 0x06002938 RID: 10552 RVA: 0x000B4047 File Offset: 0x000B2247
		public override void Set(int record, object value)
		{
			if (this._nullValue == value)
			{
				this._values[record] = 0L;
				base.SetNullBit(record, true);
				return;
			}
			this._values[record] = ((IConvertible)value).ToInt64(base.FormatProvider);
			base.SetNullBit(record, false);
		}

		// Token: 0x06002939 RID: 10553 RVA: 0x000B4088 File Offset: 0x000B2288
		public override void SetCapacity(int capacity)
		{
			long[] array = new long[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
			base.SetCapacity(capacity);
		}

		// Token: 0x0600293A RID: 10554 RVA: 0x000B40CE File Offset: 0x000B22CE
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToInt64(s);
		}

		// Token: 0x0600293B RID: 10555 RVA: 0x000B40DB File Offset: 0x000B22DB
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((long)value);
		}

		// Token: 0x0600293C RID: 10556 RVA: 0x000B40E8 File Offset: 0x000B22E8
		protected override object GetEmptyStorage(int recordCount)
		{
			return new long[recordCount];
		}

		// Token: 0x0600293D RID: 10557 RVA: 0x000B40F0 File Offset: 0x000B22F0
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((long[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, !base.HasValue(record));
		}

		// Token: 0x0600293E RID: 10558 RVA: 0x000B4115 File Offset: 0x000B2315
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (long[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x0400197C RID: 6524
		private const long defaultValue = 0L;

		// Token: 0x0400197D RID: 6525
		private long[] _values;
	}
}
