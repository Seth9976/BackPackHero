using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000370 RID: 880
	internal sealed class TimeSpanStorage : DataStorage
	{
		// Token: 0x06002AAD RID: 10925 RVA: 0x000BC04B File Offset: 0x000BA24B
		public TimeSpanStorage(DataColumn column)
			: base(column, typeof(TimeSpan), TimeSpanStorage.s_defaultValue, StorageType.TimeSpan)
		{
		}

		// Token: 0x06002AAE RID: 10926 RVA: 0x000BC06C File Offset: 0x000BA26C
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Sum:
				{
					decimal num = 0m;
					foreach (int num2 in records)
					{
						if (!this.IsNull(num2))
						{
							num += this._values[num2].Ticks;
							flag = true;
						}
					}
					if (flag)
					{
						return TimeSpan.FromTicks((long)Math.Round(num));
					}
					return null;
				}
				case AggregateType.Mean:
				{
					decimal num3 = 0m;
					int num4 = 0;
					foreach (int num5 in records)
					{
						if (!this.IsNull(num5))
						{
							num3 += this._values[num5].Ticks;
							num4++;
						}
					}
					if (num4 > 0)
					{
						return TimeSpan.FromTicks((long)Math.Round(num3 / num4));
					}
					return null;
				}
				case AggregateType.Min:
				{
					TimeSpan timeSpan = TimeSpan.MaxValue;
					foreach (int num6 in records)
					{
						if (!this.IsNull(num6))
						{
							timeSpan = ((TimeSpan.Compare(this._values[num6], timeSpan) < 0) ? this._values[num6] : timeSpan);
							flag = true;
						}
					}
					if (flag)
					{
						return timeSpan;
					}
					return this._nullValue;
				}
				case AggregateType.Max:
				{
					TimeSpan timeSpan2 = TimeSpan.MinValue;
					foreach (int num7 in records)
					{
						if (!this.IsNull(num7))
						{
							timeSpan2 = ((TimeSpan.Compare(this._values[num7], timeSpan2) >= 0) ? this._values[num7] : timeSpan2);
							flag = true;
						}
					}
					if (flag)
					{
						return timeSpan2;
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
				case AggregateType.StDev:
				{
					int num8 = 0;
					decimal num9 = 0m;
					foreach (int num10 in records)
					{
						if (!this.IsNull(num10))
						{
							num9 += this._values[num10].Ticks;
							num8++;
						}
					}
					if (num8 > 1)
					{
						double num11 = 0.0;
						decimal num12 = num9 / num8;
						foreach (int num13 in records)
						{
							if (!this.IsNull(num13))
							{
								double num14 = (double)(this._values[num13].Ticks - num12);
								num11 += num14 * num14;
							}
						}
						ulong num15 = (ulong)Math.Round(Math.Sqrt(num11 / (double)(num8 - 1)));
						if (num15 > 9223372036854775807UL)
						{
							num15 = 9223372036854775807UL;
						}
						return TimeSpan.FromTicks((long)num15);
					}
					return null;
				}
				}
			}
			catch (OverflowException)
			{
				throw ExprException.Overflow(typeof(TimeSpan));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002AAF RID: 10927 RVA: 0x000BC410 File Offset: 0x000BA610
		public override int Compare(int recordNo1, int recordNo2)
		{
			TimeSpan timeSpan = this._values[recordNo1];
			TimeSpan timeSpan2 = this._values[recordNo2];
			if (timeSpan == TimeSpanStorage.s_defaultValue || timeSpan2 == TimeSpanStorage.s_defaultValue)
			{
				int num = base.CompareBits(recordNo1, recordNo2);
				if (num != 0)
				{
					return num;
				}
			}
			return TimeSpan.Compare(timeSpan, timeSpan2);
		}

		// Token: 0x06002AB0 RID: 10928 RVA: 0x000BC468 File Offset: 0x000BA668
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
				TimeSpan timeSpan = this._values[recordNo];
				if (TimeSpanStorage.s_defaultValue == timeSpan && this.IsNull(recordNo))
				{
					return -1;
				}
				return timeSpan.CompareTo((TimeSpan)value);
			}
		}

		// Token: 0x06002AB1 RID: 10929 RVA: 0x000BC4C0 File Offset: 0x000BA6C0
		private static TimeSpan ConvertToTimeSpan(object value)
		{
			Type type = value.GetType();
			if (type == typeof(string))
			{
				return TimeSpan.Parse((string)value);
			}
			if (type == typeof(int))
			{
				return new TimeSpan((long)((int)value));
			}
			if (type == typeof(long))
			{
				return new TimeSpan((long)value);
			}
			return (TimeSpan)value;
		}

		// Token: 0x06002AB2 RID: 10930 RVA: 0x000BC535 File Offset: 0x000BA735
		public override object ConvertValue(object value)
		{
			if (this._nullValue != value)
			{
				if (value != null)
				{
					value = TimeSpanStorage.ConvertToTimeSpan(value);
				}
				else
				{
					value = this._nullValue;
				}
			}
			return value;
		}

		// Token: 0x06002AB3 RID: 10931 RVA: 0x000BC55B File Offset: 0x000BA75B
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06002AB4 RID: 10932 RVA: 0x000BC580 File Offset: 0x000BA780
		public override object Get(int record)
		{
			TimeSpan timeSpan = this._values[record];
			if (timeSpan != TimeSpanStorage.s_defaultValue)
			{
				return timeSpan;
			}
			return base.GetBits(record);
		}

		// Token: 0x06002AB5 RID: 10933 RVA: 0x000BC5B5 File Offset: 0x000BA7B5
		public override void Set(int record, object value)
		{
			if (this._nullValue == value)
			{
				this._values[record] = TimeSpanStorage.s_defaultValue;
				base.SetNullBit(record, true);
				return;
			}
			this._values[record] = TimeSpanStorage.ConvertToTimeSpan(value);
			base.SetNullBit(record, false);
		}

		// Token: 0x06002AB6 RID: 10934 RVA: 0x000BC5F4 File Offset: 0x000BA7F4
		public override void SetCapacity(int capacity)
		{
			TimeSpan[] array = new TimeSpan[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
			base.SetCapacity(capacity);
		}

		// Token: 0x06002AB7 RID: 10935 RVA: 0x000BC63A File Offset: 0x000BA83A
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToTimeSpan(s);
		}

		// Token: 0x06002AB8 RID: 10936 RVA: 0x000BC647 File Offset: 0x000BA847
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((TimeSpan)value);
		}

		// Token: 0x06002AB9 RID: 10937 RVA: 0x000BC654 File Offset: 0x000BA854
		protected override object GetEmptyStorage(int recordCount)
		{
			return new TimeSpan[recordCount];
		}

		// Token: 0x06002ABA RID: 10938 RVA: 0x000BC65C File Offset: 0x000BA85C
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((TimeSpan[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06002ABB RID: 10939 RVA: 0x000BC686 File Offset: 0x000BA886
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (TimeSpan[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x040019D7 RID: 6615
		private static readonly TimeSpan s_defaultValue = TimeSpan.Zero;

		// Token: 0x040019D8 RID: 6616
		private TimeSpan[] _values;
	}
}
