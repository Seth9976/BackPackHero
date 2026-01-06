using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x0200034D RID: 845
	internal sealed class DoubleStorage : DataStorage
	{
		// Token: 0x06002906 RID: 10502 RVA: 0x000B2C82 File Offset: 0x000B0E82
		internal DoubleStorage(DataColumn column)
			: base(column, typeof(double), 0.0, StorageType.Double)
		{
		}

		// Token: 0x06002907 RID: 10503 RVA: 0x000B2CA8 File Offset: 0x000B0EA8
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Sum:
				{
					double num = 0.0;
					foreach (int num2 in records)
					{
						if (!this.IsNull(num2))
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
				case AggregateType.Mean:
				{
					double num3 = 0.0;
					int num4 = 0;
					foreach (int num5 in records)
					{
						if (!this.IsNull(num5))
						{
							num3 += this._values[num5];
							num4++;
							flag = true;
						}
					}
					if (flag)
					{
						return num3 / (double)num4;
					}
					return this._nullValue;
				}
				case AggregateType.Min:
				{
					double num6 = double.MaxValue;
					foreach (int num7 in records)
					{
						if (!this.IsNull(num7))
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
					double num8 = double.MinValue;
					foreach (int num9 in records)
					{
						if (!this.IsNull(num9))
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
						if (!this.IsNull(num13))
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
				throw ExprException.Overflow(typeof(double));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002908 RID: 10504 RVA: 0x000B2FBC File Offset: 0x000B11BC
		public override int Compare(int recordNo1, int recordNo2)
		{
			double num = this._values[recordNo1];
			double num2 = this._values[recordNo2];
			if (num == 0.0 || num2 == 0.0)
			{
				int num3 = base.CompareBits(recordNo1, recordNo2);
				if (num3 != 0)
				{
					return num3;
				}
			}
			return num.CompareTo(num2);
		}

		// Token: 0x06002909 RID: 10505 RVA: 0x000B300C File Offset: 0x000B120C
		public override int CompareValueTo(int recordNo, object value)
		{
			if (this._nullValue == value)
			{
				if (this.IsNull(recordNo))
				{
					return 0;
				}
				return 1;
			}
			else
			{
				double num = this._values[recordNo];
				if (0.0 == num && this.IsNull(recordNo))
				{
					return -1;
				}
				return num.CompareTo((double)value);
			}
		}

		// Token: 0x0600290A RID: 10506 RVA: 0x000B305C File Offset: 0x000B125C
		public override object ConvertValue(object value)
		{
			if (this._nullValue != value)
			{
				if (value != null)
				{
					value = ((IConvertible)value).ToDouble(base.FormatProvider);
				}
				else
				{
					value = this._nullValue;
				}
			}
			return value;
		}

		// Token: 0x0600290B RID: 10507 RVA: 0x000B308D File Offset: 0x000B128D
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x0600290C RID: 10508 RVA: 0x000B30A8 File Offset: 0x000B12A8
		public override object Get(int record)
		{
			double num = this._values[record];
			if (num != 0.0)
			{
				return num;
			}
			return base.GetBits(record);
		}

		// Token: 0x0600290D RID: 10509 RVA: 0x000B30D8 File Offset: 0x000B12D8
		public override void Set(int record, object value)
		{
			if (this._nullValue == value)
			{
				this._values[record] = 0.0;
				base.SetNullBit(record, true);
				return;
			}
			this._values[record] = ((IConvertible)value).ToDouble(base.FormatProvider);
			base.SetNullBit(record, false);
		}

		// Token: 0x0600290E RID: 10510 RVA: 0x000B312C File Offset: 0x000B132C
		public override void SetCapacity(int capacity)
		{
			double[] array = new double[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
			base.SetCapacity(capacity);
		}

		// Token: 0x0600290F RID: 10511 RVA: 0x000B3172 File Offset: 0x000B1372
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToDouble(s);
		}

		// Token: 0x06002910 RID: 10512 RVA: 0x000B317F File Offset: 0x000B137F
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((double)value);
		}

		// Token: 0x06002911 RID: 10513 RVA: 0x000B318C File Offset: 0x000B138C
		protected override object GetEmptyStorage(int recordCount)
		{
			return new double[recordCount];
		}

		// Token: 0x06002912 RID: 10514 RVA: 0x000B3194 File Offset: 0x000B1394
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((double[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06002913 RID: 10515 RVA: 0x000B31B6 File Offset: 0x000B13B6
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (double[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x04001970 RID: 6512
		private const double defaultValue = 0.0;

		// Token: 0x04001971 RID: 6513
		private double[] _values;
	}
}
