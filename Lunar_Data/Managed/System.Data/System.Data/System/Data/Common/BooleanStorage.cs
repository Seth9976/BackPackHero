using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000320 RID: 800
	internal sealed class BooleanStorage : DataStorage
	{
		// Token: 0x0600252F RID: 9519 RVA: 0x000A881F File Offset: 0x000A6A1F
		internal BooleanStorage(DataColumn column)
			: base(column, typeof(bool), false, StorageType.Boolean)
		{
		}

		// Token: 0x06002530 RID: 9520 RVA: 0x000A883C File Offset: 0x000A6A3C
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Min:
				{
					bool flag2 = true;
					foreach (int num in records)
					{
						if (!this.IsNull(num))
						{
							flag2 = this._values[num] && flag2;
							flag = true;
						}
					}
					if (flag)
					{
						return flag2;
					}
					return this._nullValue;
				}
				case AggregateType.Max:
				{
					bool flag3 = false;
					foreach (int num2 in records)
					{
						if (!this.IsNull(num2))
						{
							flag3 = this._values[num2] || flag3;
							flag = true;
						}
					}
					if (flag)
					{
						return flag3;
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
				}
			}
			catch (OverflowException)
			{
				throw ExprException.Overflow(typeof(bool));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x000A8958 File Offset: 0x000A6B58
		public override int Compare(int recordNo1, int recordNo2)
		{
			bool flag = this._values[recordNo1];
			bool flag2 = this._values[recordNo2];
			if (!flag || !flag2)
			{
				int num = base.CompareBits(recordNo1, recordNo2);
				if (num != 0)
				{
					return num;
				}
			}
			return flag.CompareTo(flag2);
		}

		// Token: 0x06002532 RID: 9522 RVA: 0x000A8994 File Offset: 0x000A6B94
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
				bool flag = this._values[recordNo];
				if (!flag && this.IsNull(recordNo))
				{
					return -1;
				}
				return flag.CompareTo((bool)value);
			}
		}

		// Token: 0x06002533 RID: 9523 RVA: 0x000A89DB File Offset: 0x000A6BDB
		public override object ConvertValue(object value)
		{
			if (this._nullValue != value)
			{
				if (value != null)
				{
					value = ((IConvertible)value).ToBoolean(base.FormatProvider);
				}
				else
				{
					value = this._nullValue;
				}
			}
			return value;
		}

		// Token: 0x06002534 RID: 9524 RVA: 0x000A8A0C File Offset: 0x000A6C0C
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06002535 RID: 9525 RVA: 0x000A8A28 File Offset: 0x000A6C28
		public override object Get(int record)
		{
			bool flag = this._values[record];
			if (flag)
			{
				return flag;
			}
			return base.GetBits(record);
		}

		// Token: 0x06002536 RID: 9526 RVA: 0x000A8A4F File Offset: 0x000A6C4F
		public override void Set(int record, object value)
		{
			if (this._nullValue == value)
			{
				this._values[record] = false;
				base.SetNullBit(record, true);
				return;
			}
			this._values[record] = ((IConvertible)value).ToBoolean(base.FormatProvider);
			base.SetNullBit(record, false);
		}

		// Token: 0x06002537 RID: 9527 RVA: 0x000A8A90 File Offset: 0x000A6C90
		public override void SetCapacity(int capacity)
		{
			bool[] array = new bool[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
			base.SetCapacity(capacity);
		}

		// Token: 0x06002538 RID: 9528 RVA: 0x000A8AD6 File Offset: 0x000A6CD6
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToBoolean(s);
		}

		// Token: 0x06002539 RID: 9529 RVA: 0x000A8AE3 File Offset: 0x000A6CE3
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((bool)value);
		}

		// Token: 0x0600253A RID: 9530 RVA: 0x000A8AF0 File Offset: 0x000A6CF0
		protected override object GetEmptyStorage(int recordCount)
		{
			return new bool[recordCount];
		}

		// Token: 0x0600253B RID: 9531 RVA: 0x000A8AF8 File Offset: 0x000A6CF8
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((bool[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x0600253C RID: 9532 RVA: 0x000A8B1A File Offset: 0x000A6D1A
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (bool[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x04001848 RID: 6216
		private const bool defaultValue = false;

		// Token: 0x04001849 RID: 6217
		private bool[] _values;
	}
}
