using System;
using System.Collections;

namespace System.Data.Common
{
	// Token: 0x0200036E RID: 878
	internal sealed class StringStorage : DataStorage
	{
		// Token: 0x06002A9D RID: 10909 RVA: 0x000BBD72 File Offset: 0x000B9F72
		public StringStorage(DataColumn column)
			: base(column, typeof(string), string.Empty, StorageType.String)
		{
		}

		// Token: 0x06002A9E RID: 10910 RVA: 0x000BBD8C File Offset: 0x000B9F8C
		public override object Aggregate(int[] recordNos, AggregateType kind)
		{
			switch (kind)
			{
			case AggregateType.Min:
			{
				int num = -1;
				int i;
				for (i = 0; i < recordNos.Length; i++)
				{
					if (!this.IsNull(recordNos[i]))
					{
						num = recordNos[i];
						break;
					}
				}
				if (num >= 0)
				{
					for (i++; i < recordNos.Length; i++)
					{
						if (!this.IsNull(recordNos[i]) && this.Compare(num, recordNos[i]) > 0)
						{
							num = recordNos[i];
						}
					}
					return this.Get(num);
				}
				return this._nullValue;
			}
			case AggregateType.Max:
			{
				int num2 = -1;
				int i;
				for (i = 0; i < recordNos.Length; i++)
				{
					if (!this.IsNull(recordNos[i]))
					{
						num2 = recordNos[i];
						break;
					}
				}
				if (num2 >= 0)
				{
					for (i++; i < recordNos.Length; i++)
					{
						if (this.Compare(num2, recordNos[i]) < 0)
						{
							num2 = recordNos[i];
						}
					}
					return this.Get(num2);
				}
				return this._nullValue;
			}
			case AggregateType.Count:
			{
				int num3 = 0;
				for (int i = 0; i < recordNos.Length; i++)
				{
					if (this._values[recordNos[i]] != null)
					{
						num3++;
					}
				}
				return num3;
			}
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002A9F RID: 10911 RVA: 0x000BBEA0 File Offset: 0x000BA0A0
		public override int Compare(int recordNo1, int recordNo2)
		{
			string text = this._values[recordNo1];
			string text2 = this._values[recordNo2];
			if (text == text2)
			{
				return 0;
			}
			if (text == null)
			{
				return -1;
			}
			if (text2 == null)
			{
				return 1;
			}
			return this._table.Compare(text, text2);
		}

		// Token: 0x06002AA0 RID: 10912 RVA: 0x000BBEDC File Offset: 0x000BA0DC
		public override int CompareValueTo(int recordNo, object value)
		{
			string text = this._values[recordNo];
			if (text == null)
			{
				if (this._nullValue == value)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (this._nullValue == value)
				{
					return 1;
				}
				return this._table.Compare(text, (string)value);
			}
		}

		// Token: 0x06002AA1 RID: 10913 RVA: 0x000BBF1F File Offset: 0x000BA11F
		public override object ConvertValue(object value)
		{
			if (this._nullValue != value)
			{
				if (value != null)
				{
					value = value.ToString();
				}
				else
				{
					value = this._nullValue;
				}
			}
			return value;
		}

		// Token: 0x06002AA2 RID: 10914 RVA: 0x000BBF40 File Offset: 0x000BA140
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06002AA3 RID: 10915 RVA: 0x000BBF54 File Offset: 0x000BA154
		public override object Get(int recordNo)
		{
			string text = this._values[recordNo];
			if (text != null)
			{
				return text;
			}
			return this._nullValue;
		}

		// Token: 0x06002AA4 RID: 10916 RVA: 0x000BBF78 File Offset: 0x000BA178
		public override int GetStringLength(int record)
		{
			string text = this._values[record];
			if (text == null)
			{
				return 0;
			}
			return text.Length;
		}

		// Token: 0x06002AA5 RID: 10917 RVA: 0x000BBF99 File Offset: 0x000BA199
		public override bool IsNull(int record)
		{
			return this._values[record] == null;
		}

		// Token: 0x06002AA6 RID: 10918 RVA: 0x000BBFA6 File Offset: 0x000BA1A6
		public override void Set(int record, object value)
		{
			if (this._nullValue == value)
			{
				this._values[record] = null;
				return;
			}
			this._values[record] = value.ToString();
		}

		// Token: 0x06002AA7 RID: 10919 RVA: 0x000BBFCC File Offset: 0x000BA1CC
		public override void SetCapacity(int capacity)
		{
			string[] array = new string[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x06002AA8 RID: 10920 RVA: 0x0000567E File Offset: 0x0000387E
		public override object ConvertXmlToObject(string s)
		{
			return s;
		}

		// Token: 0x06002AA9 RID: 10921 RVA: 0x000BC00B File Offset: 0x000BA20B
		public override string ConvertObjectToXml(object value)
		{
			return (string)value;
		}

		// Token: 0x06002AAA RID: 10922 RVA: 0x000BC013 File Offset: 0x000BA213
		protected override object GetEmptyStorage(int recordCount)
		{
			return new string[recordCount];
		}

		// Token: 0x06002AAB RID: 10923 RVA: 0x000BC01B File Offset: 0x000BA21B
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((string[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06002AAC RID: 10924 RVA: 0x000BC03D File Offset: 0x000BA23D
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (string[])store;
		}

		// Token: 0x040019D0 RID: 6608
		private string[] _values;
	}
}
