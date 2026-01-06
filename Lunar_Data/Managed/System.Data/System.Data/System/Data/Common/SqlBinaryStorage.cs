using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x0200035A RID: 858
	internal sealed class SqlBinaryStorage : DataStorage
	{
		// Token: 0x06002997 RID: 10647 RVA: 0x000B6C23 File Offset: 0x000B4E23
		public SqlBinaryStorage(DataColumn column)
			: base(column, typeof(SqlBinary), SqlBinary.Null, SqlBinary.Null, StorageType.SqlBinary)
		{
		}

		// Token: 0x06002998 RID: 10648 RVA: 0x000B6C4C File Offset: 0x000B4E4C
		public override object Aggregate(int[] records, AggregateType kind)
		{
			try
			{
				if (kind != AggregateType.First)
				{
					if (kind == AggregateType.Count)
					{
						int num = 0;
						for (int i = 0; i < records.Length; i++)
						{
							if (!this.IsNull(records[i]))
							{
								num++;
							}
						}
						return num;
					}
				}
				else
				{
					if (records.Length != 0)
					{
						return this._values[records[0]];
					}
					return null;
				}
			}
			catch (OverflowException)
			{
				throw ExprException.Overflow(typeof(SqlBinary));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002999 RID: 10649 RVA: 0x000B6CDC File Offset: 0x000B4EDC
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this._values[recordNo1].CompareTo(this._values[recordNo2]);
		}

		// Token: 0x0600299A RID: 10650 RVA: 0x000B6CFB File Offset: 0x000B4EFB
		public override int CompareValueTo(int recordNo, object value)
		{
			return this._values[recordNo].CompareTo((SqlBinary)value);
		}

		// Token: 0x0600299B RID: 10651 RVA: 0x000B6D14 File Offset: 0x000B4F14
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlBinary(value);
			}
			return this._nullValue;
		}

		// Token: 0x0600299C RID: 10652 RVA: 0x000B6D2B File Offset: 0x000B4F2B
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x0600299D RID: 10653 RVA: 0x000B6D45 File Offset: 0x000B4F45
		public override object Get(int record)
		{
			return this._values[record];
		}

		// Token: 0x0600299E RID: 10654 RVA: 0x000B6D58 File Offset: 0x000B4F58
		public override bool IsNull(int record)
		{
			return this._values[record].IsNull;
		}

		// Token: 0x0600299F RID: 10655 RVA: 0x000B6D6B File Offset: 0x000B4F6B
		public override void Set(int record, object value)
		{
			this._values[record] = SqlConvert.ConvertToSqlBinary(value);
		}

		// Token: 0x060029A0 RID: 10656 RVA: 0x000B6D80 File Offset: 0x000B4F80
		public override void SetCapacity(int capacity)
		{
			SqlBinary[] array = new SqlBinary[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x060029A1 RID: 10657 RVA: 0x000B6DC0 File Offset: 0x000B4FC0
		public override object ConvertXmlToObject(string s)
		{
			SqlBinary sqlBinary = default(SqlBinary);
			TextReader textReader = new StringReader("<col>" + s + "</col>");
			IXmlSerializable xmlSerializable = sqlBinary;
			using (XmlTextReader xmlTextReader = new XmlTextReader(textReader))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlBinary)xmlSerializable;
		}

		// Token: 0x060029A2 RID: 10658 RVA: 0x000B6E28 File Offset: 0x000B5028
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x060029A3 RID: 10659 RVA: 0x000B6E78 File Offset: 0x000B5078
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlBinary[recordCount];
		}

		// Token: 0x060029A4 RID: 10660 RVA: 0x000B6E80 File Offset: 0x000B5080
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((SqlBinary[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x060029A5 RID: 10661 RVA: 0x000B6EAA File Offset: 0x000B50AA
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (SqlBinary[])store;
		}

		// Token: 0x0400199B RID: 6555
		private SqlBinary[] _values;
	}
}
