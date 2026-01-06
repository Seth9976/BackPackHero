using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x0200036B RID: 875
	internal sealed class SingleStorage : DataStorage
	{
		// Token: 0x06002A7A RID: 10874 RVA: 0x000BB401 File Offset: 0x000B9601
		public SingleStorage(DataColumn column)
			: base(column, typeof(float), 0f, StorageType.Single)
		{
		}

		// Token: 0x06002A7B RID: 10875 RVA: 0x000BB420 File Offset: 0x000B9620
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Sum:
				{
					float num = 0f;
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
							num3 += (double)this._values[num5];
							num4++;
							flag = true;
						}
					}
					if (flag)
					{
						return (float)(num3 / (double)num4);
					}
					return this._nullValue;
				}
				case AggregateType.Min:
				{
					float num6 = float.MaxValue;
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
					float num8 = float.MinValue;
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
				throw ExprException.Overflow(typeof(float));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002A7C RID: 10876 RVA: 0x000BB72C File Offset: 0x000B992C
		public override int Compare(int recordNo1, int recordNo2)
		{
			float num = this._values[recordNo1];
			float num2 = this._values[recordNo2];
			if (num == 0f || num2 == 0f)
			{
				int num3 = base.CompareBits(recordNo1, recordNo2);
				if (num3 != 0)
				{
					return num3;
				}
			}
			return num.CompareTo(num2);
		}

		// Token: 0x06002A7D RID: 10877 RVA: 0x000BB774 File Offset: 0x000B9974
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
				float num = this._values[recordNo];
				if (0f == num && this.IsNull(recordNo))
				{
					return -1;
				}
				return num.CompareTo((float)value);
			}
		}

		// Token: 0x06002A7E RID: 10878 RVA: 0x000BB7C0 File Offset: 0x000B99C0
		public override object ConvertValue(object value)
		{
			if (this._nullValue != value)
			{
				if (value != null)
				{
					value = ((IConvertible)value).ToSingle(base.FormatProvider);
				}
				else
				{
					value = this._nullValue;
				}
			}
			return value;
		}

		// Token: 0x06002A7F RID: 10879 RVA: 0x000BB7F1 File Offset: 0x000B99F1
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06002A80 RID: 10880 RVA: 0x000BB80C File Offset: 0x000B9A0C
		public override object Get(int record)
		{
			float num = this._values[record];
			if (num != 0f)
			{
				return num;
			}
			return base.GetBits(record);
		}

		// Token: 0x06002A81 RID: 10881 RVA: 0x000BB838 File Offset: 0x000B9A38
		public override void Set(int record, object value)
		{
			if (this._nullValue == value)
			{
				this._values[record] = 0f;
				base.SetNullBit(record, true);
				return;
			}
			this._values[record] = ((IConvertible)value).ToSingle(base.FormatProvider);
			base.SetNullBit(record, false);
		}

		// Token: 0x06002A82 RID: 10882 RVA: 0x000BB888 File Offset: 0x000B9A88
		public override void SetCapacity(int capacity)
		{
			float[] array = new float[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
			base.SetCapacity(capacity);
		}

		// Token: 0x06002A83 RID: 10883 RVA: 0x000BB8CE File Offset: 0x000B9ACE
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToSingle(s);
		}

		// Token: 0x06002A84 RID: 10884 RVA: 0x000BB8DB File Offset: 0x000B9ADB
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((float)value);
		}

		// Token: 0x06002A85 RID: 10885 RVA: 0x000BB8E8 File Offset: 0x000B9AE8
		protected override object GetEmptyStorage(int recordCount)
		{
			return new float[recordCount];
		}

		// Token: 0x06002A86 RID: 10886 RVA: 0x000BB8F0 File Offset: 0x000B9AF0
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((float[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06002A87 RID: 10887 RVA: 0x000BB912 File Offset: 0x000B9B12
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (float[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x040019C9 RID: 6601
		private const float defaultValue = 0f;

		// Token: 0x040019CA RID: 6602
		private float[] _values;
	}
}
