using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000361 RID: 865
	internal sealed class SqlGuidStorage : DataStorage
	{
		// Token: 0x060029FE RID: 10750 RVA: 0x000B895C File Offset: 0x000B6B5C
		public SqlGuidStorage(DataColumn column)
			: base(column, typeof(SqlGuid), SqlGuid.Null, SqlGuid.Null, StorageType.SqlGuid)
		{
		}

		// Token: 0x060029FF RID: 10751 RVA: 0x000B8988 File Offset: 0x000B6B88
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
				throw ExprException.Overflow(typeof(SqlGuid));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002A00 RID: 10752 RVA: 0x000B8A18 File Offset: 0x000B6C18
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this._values[recordNo1].CompareTo(this._values[recordNo2]);
		}

		// Token: 0x06002A01 RID: 10753 RVA: 0x000B8A37 File Offset: 0x000B6C37
		public override int CompareValueTo(int recordNo, object value)
		{
			return this._values[recordNo].CompareTo((SqlGuid)value);
		}

		// Token: 0x06002A02 RID: 10754 RVA: 0x000B8A50 File Offset: 0x000B6C50
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlGuid(value);
			}
			return this._nullValue;
		}

		// Token: 0x06002A03 RID: 10755 RVA: 0x000B8A67 File Offset: 0x000B6C67
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06002A04 RID: 10756 RVA: 0x000B8A81 File Offset: 0x000B6C81
		public override object Get(int record)
		{
			return this._values[record];
		}

		// Token: 0x06002A05 RID: 10757 RVA: 0x000B8A94 File Offset: 0x000B6C94
		public override bool IsNull(int record)
		{
			return this._values[record].IsNull;
		}

		// Token: 0x06002A06 RID: 10758 RVA: 0x000B8AA7 File Offset: 0x000B6CA7
		public override void Set(int record, object value)
		{
			this._values[record] = SqlConvert.ConvertToSqlGuid(value);
		}

		// Token: 0x06002A07 RID: 10759 RVA: 0x000B8ABC File Offset: 0x000B6CBC
		public override void SetCapacity(int capacity)
		{
			SqlGuid[] array = new SqlGuid[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x06002A08 RID: 10760 RVA: 0x000B8AFC File Offset: 0x000B6CFC
		public override object ConvertXmlToObject(string s)
		{
			SqlGuid sqlGuid = default(SqlGuid);
			TextReader textReader = new StringReader("<col>" + s + "</col>");
			IXmlSerializable xmlSerializable = sqlGuid;
			using (XmlTextReader xmlTextReader = new XmlTextReader(textReader))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlGuid)xmlSerializable;
		}

		// Token: 0x06002A09 RID: 10761 RVA: 0x000B8B64 File Offset: 0x000B6D64
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06002A0A RID: 10762 RVA: 0x000B8BB4 File Offset: 0x000B6DB4
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlGuid[recordCount];
		}

		// Token: 0x06002A0B RID: 10763 RVA: 0x000B8BBC File Offset: 0x000B6DBC
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((SqlGuid[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06002A0C RID: 10764 RVA: 0x000B8BE6 File Offset: 0x000B6DE6
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (SqlGuid[])store;
		}

		// Token: 0x040019A2 RID: 6562
		private SqlGuid[] _values;
	}
}
