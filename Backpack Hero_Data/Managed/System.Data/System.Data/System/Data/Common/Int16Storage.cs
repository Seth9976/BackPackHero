using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000350 RID: 848
	internal sealed class Int16Storage : DataStorage
	{
		// Token: 0x06002915 RID: 10517 RVA: 0x000B31CB File Offset: 0x000B13CB
		internal Int16Storage(DataColumn column)
			: base(column, typeof(short), 0, StorageType.Int16)
		{
		}

		// Token: 0x06002916 RID: 10518 RVA: 0x000B31E8 File Offset: 0x000B13E8
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
							return (short)(num3 / unchecked((long)num4));
						}
						return this._nullValue;
					}
				}
				case AggregateType.Min:
				{
					short num6 = short.MaxValue;
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
					short num8 = short.MinValue;
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
				throw ExprException.Overflow(typeof(short));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002917 RID: 10519 RVA: 0x000B3510 File Offset: 0x000B1710
		public override int Compare(int recordNo1, int recordNo2)
		{
			short num = this._values[recordNo1];
			short num2 = this._values[recordNo2];
			if (num == 0 || num2 == 0)
			{
				int num3 = base.CompareBits(recordNo1, recordNo2);
				if (num3 != 0)
				{
					return num3;
				}
			}
			return (int)(num - num2);
		}

		// Token: 0x06002918 RID: 10520 RVA: 0x000B3548 File Offset: 0x000B1748
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
				short num = this._values[recordNo];
				if (num == 0 && !base.HasValue(recordNo))
				{
					return -1;
				}
				return num.CompareTo((short)value);
			}
		}

		// Token: 0x06002919 RID: 10521 RVA: 0x000B358F File Offset: 0x000B178F
		public override object ConvertValue(object value)
		{
			if (this._nullValue != value)
			{
				if (value != null)
				{
					value = ((IConvertible)value).ToInt16(base.FormatProvider);
				}
				else
				{
					value = this._nullValue;
				}
			}
			return value;
		}

		// Token: 0x0600291A RID: 10522 RVA: 0x000B35C0 File Offset: 0x000B17C0
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x0600291B RID: 10523 RVA: 0x000B35DC File Offset: 0x000B17DC
		public override object Get(int record)
		{
			short num = this._values[record];
			if (num != 0)
			{
				return num;
			}
			return base.GetBits(record);
		}

		// Token: 0x0600291C RID: 10524 RVA: 0x000B3603 File Offset: 0x000B1803
		public override void Set(int record, object value)
		{
			if (this._nullValue == value)
			{
				this._values[record] = 0;
				base.SetNullBit(record, true);
				return;
			}
			this._values[record] = ((IConvertible)value).ToInt16(base.FormatProvider);
			base.SetNullBit(record, false);
		}

		// Token: 0x0600291D RID: 10525 RVA: 0x000B3644 File Offset: 0x000B1844
		public override void SetCapacity(int capacity)
		{
			short[] array = new short[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
			base.SetCapacity(capacity);
		}

		// Token: 0x0600291E RID: 10526 RVA: 0x000B368A File Offset: 0x000B188A
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToInt16(s);
		}

		// Token: 0x0600291F RID: 10527 RVA: 0x000B3697 File Offset: 0x000B1897
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((short)value);
		}

		// Token: 0x06002920 RID: 10528 RVA: 0x000B36A4 File Offset: 0x000B18A4
		protected override object GetEmptyStorage(int recordCount)
		{
			return new short[recordCount];
		}

		// Token: 0x06002921 RID: 10529 RVA: 0x000B36AC File Offset: 0x000B18AC
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((short[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, !base.HasValue(record));
		}

		// Token: 0x06002922 RID: 10530 RVA: 0x000B36D1 File Offset: 0x000B18D1
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (short[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x04001978 RID: 6520
		private const short defaultValue = 0;

		// Token: 0x04001979 RID: 6521
		private short[] _values;
	}
}
