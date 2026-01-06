using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000351 RID: 849
	internal sealed class Int32Storage : DataStorage
	{
		// Token: 0x06002923 RID: 10531 RVA: 0x000B36E6 File Offset: 0x000B18E6
		internal Int32Storage(DataColumn column)
			: base(column, typeof(int), 0, StorageType.Int32)
		{
		}

		// Token: 0x06002924 RID: 10532 RVA: 0x000B3704 File Offset: 0x000B1904
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
								num += unchecked((long)this._values[num2]);
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
					long num3 = 0L;
					int num4 = 0;
					foreach (int num5 in records)
					{
						if (base.HasValue(num5))
						{
							checked
							{
								num3 += unchecked((long)this._values[num5]);
							}
							num4++;
							flag = true;
						}
					}
					checked
					{
						if (flag)
						{
							return (int)(num3 / unchecked((long)num4));
						}
						return this._nullValue;
					}
				}
				case AggregateType.Min:
				{
					int num6 = int.MaxValue;
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
					int num8 = int.MinValue;
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
				throw ExprException.Overflow(typeof(int));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002925 RID: 10533 RVA: 0x000B3A2C File Offset: 0x000B1C2C
		public override int Compare(int recordNo1, int recordNo2)
		{
			int num = this._values[recordNo1];
			int num2 = this._values[recordNo2];
			if (num == 0 || num2 == 0)
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

		// Token: 0x06002926 RID: 10534 RVA: 0x000B3A6C File Offset: 0x000B1C6C
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
				int num = this._values[recordNo];
				if (num == 0 && !base.HasValue(recordNo))
				{
					return -1;
				}
				return num.CompareTo((int)value);
			}
		}

		// Token: 0x06002927 RID: 10535 RVA: 0x000B3AB3 File Offset: 0x000B1CB3
		public override object ConvertValue(object value)
		{
			if (this._nullValue != value)
			{
				if (value != null)
				{
					value = ((IConvertible)value).ToInt32(base.FormatProvider);
				}
				else
				{
					value = this._nullValue;
				}
			}
			return value;
		}

		// Token: 0x06002928 RID: 10536 RVA: 0x000B3AE4 File Offset: 0x000B1CE4
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06002929 RID: 10537 RVA: 0x000B3B00 File Offset: 0x000B1D00
		public override object Get(int record)
		{
			int num = this._values[record];
			if (num != 0)
			{
				return num;
			}
			return base.GetBits(record);
		}

		// Token: 0x0600292A RID: 10538 RVA: 0x000B3B27 File Offset: 0x000B1D27
		public override void Set(int record, object value)
		{
			if (this._nullValue == value)
			{
				this._values[record] = 0;
				base.SetNullBit(record, true);
				return;
			}
			this._values[record] = ((IConvertible)value).ToInt32(base.FormatProvider);
			base.SetNullBit(record, false);
		}

		// Token: 0x0600292B RID: 10539 RVA: 0x000B3B68 File Offset: 0x000B1D68
		public override void SetCapacity(int capacity)
		{
			int[] array = new int[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
			base.SetCapacity(capacity);
		}

		// Token: 0x0600292C RID: 10540 RVA: 0x000B3BAE File Offset: 0x000B1DAE
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToInt32(s);
		}

		// Token: 0x0600292D RID: 10541 RVA: 0x000B3BBB File Offset: 0x000B1DBB
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((int)value);
		}

		// Token: 0x0600292E RID: 10542 RVA: 0x000B3BC8 File Offset: 0x000B1DC8
		protected override object GetEmptyStorage(int recordCount)
		{
			return new int[recordCount];
		}

		// Token: 0x0600292F RID: 10543 RVA: 0x000B3BD0 File Offset: 0x000B1DD0
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((int[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, !base.HasValue(record));
		}

		// Token: 0x06002930 RID: 10544 RVA: 0x000B3BF5 File Offset: 0x000B1DF5
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (int[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x0400197A RID: 6522
		private const int defaultValue = 0;

		// Token: 0x0400197B RID: 6523
		private int[] _values;
	}
}
