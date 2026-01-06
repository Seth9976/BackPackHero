using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x0200035E RID: 862
	internal sealed class SqlDateTimeStorage : DataStorage
	{
		// Token: 0x060029D1 RID: 10705 RVA: 0x000B7954 File Offset: 0x000B5B54
		public SqlDateTimeStorage(DataColumn column)
			: base(column, typeof(SqlDateTime), SqlDateTime.Null, SqlDateTime.Null, StorageType.SqlDateTime)
		{
		}

		// Token: 0x060029D2 RID: 10706 RVA: 0x000B7980 File Offset: 0x000B5B80
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Min:
				{
					SqlDateTime sqlDateTime = SqlDateTime.MaxValue;
					foreach (int num in records)
					{
						if (!this.IsNull(num))
						{
							if (SqlDateTime.LessThan(this._values[num], sqlDateTime).IsTrue)
							{
								sqlDateTime = this._values[num];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlDateTime;
					}
					return this._nullValue;
				}
				case AggregateType.Max:
				{
					SqlDateTime sqlDateTime2 = SqlDateTime.MinValue;
					foreach (int num2 in records)
					{
						if (!this.IsNull(num2))
						{
							if (SqlDateTime.GreaterThan(this._values[num2], sqlDateTime2).IsTrue)
							{
								sqlDateTime2 = this._values[num2];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlDateTime2;
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
					int num3 = 0;
					for (int k = 0; k < records.Length; k++)
					{
						if (!this.IsNull(records[k]))
						{
							num3++;
						}
					}
					return num3;
				}
				}
			}
			catch (OverflowException)
			{
				throw ExprException.Overflow(typeof(SqlDateTime));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x060029D3 RID: 10707 RVA: 0x000B7B18 File Offset: 0x000B5D18
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this._values[recordNo1].CompareTo(this._values[recordNo2]);
		}

		// Token: 0x060029D4 RID: 10708 RVA: 0x000B7B37 File Offset: 0x000B5D37
		public override int CompareValueTo(int recordNo, object value)
		{
			return this._values[recordNo].CompareTo((SqlDateTime)value);
		}

		// Token: 0x060029D5 RID: 10709 RVA: 0x000B7B50 File Offset: 0x000B5D50
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlDateTime(value);
			}
			return this._nullValue;
		}

		// Token: 0x060029D6 RID: 10710 RVA: 0x000B7B67 File Offset: 0x000B5D67
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x060029D7 RID: 10711 RVA: 0x000B7B81 File Offset: 0x000B5D81
		public override object Get(int record)
		{
			return this._values[record];
		}

		// Token: 0x060029D8 RID: 10712 RVA: 0x000B7B94 File Offset: 0x000B5D94
		public override bool IsNull(int record)
		{
			return this._values[record].IsNull;
		}

		// Token: 0x060029D9 RID: 10713 RVA: 0x000B7BA7 File Offset: 0x000B5DA7
		public override void Set(int record, object value)
		{
			this._values[record] = SqlConvert.ConvertToSqlDateTime(value);
		}

		// Token: 0x060029DA RID: 10714 RVA: 0x000B7BBC File Offset: 0x000B5DBC
		public override void SetCapacity(int capacity)
		{
			SqlDateTime[] array = new SqlDateTime[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x060029DB RID: 10715 RVA: 0x000B7BFC File Offset: 0x000B5DFC
		public override object ConvertXmlToObject(string s)
		{
			SqlDateTime sqlDateTime = default(SqlDateTime);
			TextReader textReader = new StringReader("<col>" + s + "</col>");
			IXmlSerializable xmlSerializable = sqlDateTime;
			using (XmlTextReader xmlTextReader = new XmlTextReader(textReader))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlDateTime)xmlSerializable;
		}

		// Token: 0x060029DC RID: 10716 RVA: 0x000B7C64 File Offset: 0x000B5E64
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x060029DD RID: 10717 RVA: 0x000B7CB4 File Offset: 0x000B5EB4
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlDateTime[recordCount];
		}

		// Token: 0x060029DE RID: 10718 RVA: 0x000B7CBC File Offset: 0x000B5EBC
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((SqlDateTime[])store)[storeIndex] = this._values[record];
			nullbits.Set(record, this.IsNull(record));
		}

		// Token: 0x060029DF RID: 10719 RVA: 0x000B7CE5 File Offset: 0x000B5EE5
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (SqlDateTime[])store;
		}

		// Token: 0x0400199F RID: 6559
		private SqlDateTime[] _values;
	}
}
