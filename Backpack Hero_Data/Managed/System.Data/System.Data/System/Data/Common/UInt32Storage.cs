using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000372 RID: 882
	internal sealed class UInt32Storage : DataStorage
	{
		// Token: 0x06002ACB RID: 10955 RVA: 0x000BCBF2 File Offset: 0x000BADF2
		public UInt32Storage(DataColumn column)
			: base(column, typeof(uint), UInt32Storage.s_defaultValue, StorageType.UInt32)
		{
		}

		// Token: 0x06002ACC RID: 10956 RVA: 0x000BCC14 File Offset: 0x000BAE14
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Sum:
				{
					ulong num = (ulong)UInt32Storage.s_defaultValue;
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
					long num3 = (long)((ulong)UInt32Storage.s_defaultValue);
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
							return (uint)(num3 / unchecked((long)num4));
						}
						return this._nullValue;
					}
				}
				case AggregateType.Min:
				{
					uint num6 = uint.MaxValue;
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
					uint num8 = 0U;
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
				throw ExprException.Overflow(typeof(uint));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002ACD RID: 10957 RVA: 0x000BCF40 File Offset: 0x000BB140
		public override int Compare(int recordNo1, int recordNo2)
		{
			uint num = this._values[recordNo1];
			uint num2 = this._values[recordNo2];
			if (num == UInt32Storage.s_defaultValue || num2 == UInt32Storage.s_defaultValue)
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

		// Token: 0x06002ACE RID: 10958 RVA: 0x000BCF8C File Offset: 0x000BB18C
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
				uint num = this._values[recordNo];
				if (UInt32Storage.s_defaultValue == num && !base.HasValue(recordNo))
				{
					return -1;
				}
				return num.CompareTo((uint)value);
			}
		}

		// Token: 0x06002ACF RID: 10959 RVA: 0x000BCFD8 File Offset: 0x000BB1D8
		public override object ConvertValue(object value)
		{
			if (this._nullValue != value)
			{
				if (value != null)
				{
					value = ((IConvertible)value).ToUInt32(base.FormatProvider);
				}
				else
				{
					value = this._nullValue;
				}
			}
			return value;
		}

		// Token: 0x06002AD0 RID: 10960 RVA: 0x000BD009 File Offset: 0x000BB209
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06002AD1 RID: 10961 RVA: 0x000BD024 File Offset: 0x000BB224
		public override object Get(int record)
		{
			uint num = this._values[record];
			if (!num.Equals(UInt32Storage.s_defaultValue))
			{
				return num;
			}
			return base.GetBits(record);
		}

		// Token: 0x06002AD2 RID: 10962 RVA: 0x000BD058 File Offset: 0x000BB258
		public override void Set(int record, object value)
		{
			if (this._nullValue == value)
			{
				this._values[record] = UInt32Storage.s_defaultValue;
				base.SetNullBit(record, true);
				return;
			}
			this._values[record] = ((IConvertible)value).ToUInt32(base.FormatProvider);
			base.SetNullBit(record, false);
		}

		// Token: 0x06002AD3 RID: 10963 RVA: 0x000BD0A8 File Offset: 0x000BB2A8
		public override void SetCapacity(int capacity)
		{
			uint[] array = new uint[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
			base.SetCapacity(capacity);
		}

		// Token: 0x06002AD4 RID: 10964 RVA: 0x000BD0EE File Offset: 0x000BB2EE
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToUInt32(s);
		}

		// Token: 0x06002AD5 RID: 10965 RVA: 0x000BD0FB File Offset: 0x000BB2FB
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((uint)value);
		}

		// Token: 0x06002AD6 RID: 10966 RVA: 0x000BD108 File Offset: 0x000BB308
		protected override object GetEmptyStorage(int recordCount)
		{
			return new uint[recordCount];
		}

		// Token: 0x06002AD7 RID: 10967 RVA: 0x000BD110 File Offset: 0x000BB310
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((uint[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, !base.HasValue(record));
		}

		// Token: 0x06002AD8 RID: 10968 RVA: 0x000BD135 File Offset: 0x000BB335
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (uint[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x040019DB RID: 6619
		private static readonly uint s_defaultValue;

		// Token: 0x040019DC RID: 6620
		private uint[] _values;
	}
}
