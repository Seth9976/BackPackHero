using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000358 RID: 856
	internal sealed class SByteStorage : DataStorage
	{
		// Token: 0x06002976 RID: 10614 RVA: 0x000B50E5 File Offset: 0x000B32E5
		public SByteStorage(DataColumn column)
			: base(column, typeof(sbyte), 0, StorageType.SByte)
		{
		}

		// Token: 0x06002977 RID: 10615 RVA: 0x000B5100 File Offset: 0x000B3300
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			checked
			{
				try
				{
					switch (kind)
					{
					case AggregateType.Sum:
					{
						long num = 0L;
						foreach (int num2 in records)
						{
							if (!this.IsNull(num2))
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
					case AggregateType.Mean:
					{
						long num3 = 0L;
						int num4 = 0;
						foreach (int num5 in records)
						{
							if (!this.IsNull(num5))
							{
								num3 += unchecked((long)this._values[num5]);
								unchecked
								{
									num4++;
									flag = true;
								}
							}
						}
						if (flag)
						{
							return (sbyte)(num3 / unchecked((long)num4));
						}
						return this._nullValue;
					}
					case AggregateType.Min:
					{
						sbyte b = sbyte.MaxValue;
						foreach (int num6 in records)
						{
							if (!this.IsNull(num6))
							{
								b = Math.Min(this._values[num6], b);
								flag = true;
							}
						}
						if (flag)
						{
							return b;
						}
						return this._nullValue;
					}
					case AggregateType.Max:
					{
						sbyte b2 = sbyte.MinValue;
						foreach (int num7 in records)
						{
							if (!this.IsNull(num7))
							{
								b2 = Math.Max(this._values[num7], b2);
								flag = true;
							}
						}
						if (flag)
						{
							return b2;
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
						int num8 = 0;
						double num9 = 0.0;
						double num10 = 0.0;
						unchecked
						{
							foreach (int num11 in records)
							{
								if (!this.IsNull(num11))
								{
									num9 += (double)this._values[num11];
									num10 += (double)this._values[num11] * (double)this._values[num11];
									num8++;
								}
							}
							if (num8 <= 1)
							{
								return this._nullValue;
							}
							double num12 = (double)num8 * num10 - num9 * num9;
							if (num12 / (num9 * num9) < 1E-15 || num12 < 0.0)
							{
								num12 = 0.0;
							}
							else
							{
								num12 /= (double)(num8 * (num8 - 1));
							}
							if (kind == AggregateType.StDev)
							{
								return Math.Sqrt(num12);
							}
							return num12;
						}
					}
					}
				}
				catch (OverflowException)
				{
					throw ExprException.Overflow(typeof(sbyte));
				}
				throw ExceptionBuilder.AggregateException(kind, this._dataType);
			}
		}

		// Token: 0x06002978 RID: 10616 RVA: 0x000B53FC File Offset: 0x000B35FC
		public override int Compare(int recordNo1, int recordNo2)
		{
			sbyte b = this._values[recordNo1];
			sbyte b2 = this._values[recordNo2];
			if (b.Equals(0) || b2.Equals(0))
			{
				int num = base.CompareBits(recordNo1, recordNo2);
				if (num != 0)
				{
					return num;
				}
			}
			return b.CompareTo(b2);
		}

		// Token: 0x06002979 RID: 10617 RVA: 0x000B5448 File Offset: 0x000B3648
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
				sbyte b = this._values[recordNo];
				if (b == 0 && this.IsNull(recordNo))
				{
					return -1;
				}
				return b.CompareTo((sbyte)value);
			}
		}

		// Token: 0x0600297A RID: 10618 RVA: 0x000B548F File Offset: 0x000B368F
		public override object ConvertValue(object value)
		{
			if (this._nullValue != value)
			{
				if (value != null)
				{
					value = ((IConvertible)value).ToSByte(base.FormatProvider);
				}
				else
				{
					value = this._nullValue;
				}
			}
			return value;
		}

		// Token: 0x0600297B RID: 10619 RVA: 0x000B54C0 File Offset: 0x000B36C0
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x0600297C RID: 10620 RVA: 0x000B54DC File Offset: 0x000B36DC
		public override object Get(int record)
		{
			sbyte b = this._values[record];
			if (!b.Equals(0))
			{
				return b;
			}
			return base.GetBits(record);
		}

		// Token: 0x0600297D RID: 10621 RVA: 0x000B550A File Offset: 0x000B370A
		public override void Set(int record, object value)
		{
			if (this._nullValue == value)
			{
				this._values[record] = 0;
				base.SetNullBit(record, true);
				return;
			}
			this._values[record] = ((IConvertible)value).ToSByte(base.FormatProvider);
			base.SetNullBit(record, false);
		}

		// Token: 0x0600297E RID: 10622 RVA: 0x000B5548 File Offset: 0x000B3748
		public override void SetCapacity(int capacity)
		{
			sbyte[] array = new sbyte[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
			base.SetCapacity(capacity);
		}

		// Token: 0x0600297F RID: 10623 RVA: 0x000B558E File Offset: 0x000B378E
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToSByte(s);
		}

		// Token: 0x06002980 RID: 10624 RVA: 0x000B559B File Offset: 0x000B379B
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((sbyte)value);
		}

		// Token: 0x06002981 RID: 10625 RVA: 0x000B55A8 File Offset: 0x000B37A8
		protected override object GetEmptyStorage(int recordCount)
		{
			return new sbyte[recordCount];
		}

		// Token: 0x06002982 RID: 10626 RVA: 0x000B55B0 File Offset: 0x000B37B0
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((sbyte[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06002983 RID: 10627 RVA: 0x000B55D2 File Offset: 0x000B37D2
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (sbyte[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x04001999 RID: 6553
		private const sbyte defaultValue = 0;

		// Token: 0x0400199A RID: 6554
		private sbyte[] _values;
	}
}
