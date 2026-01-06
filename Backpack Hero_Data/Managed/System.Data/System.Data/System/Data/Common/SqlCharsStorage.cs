using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x0200035D RID: 861
	internal sealed class SqlCharsStorage : DataStorage
	{
		// Token: 0x060029C3 RID: 10691 RVA: 0x000B7728 File Offset: 0x000B5928
		public SqlCharsStorage(DataColumn column)
			: base(column, typeof(SqlChars), SqlChars.Null, SqlChars.Null, StorageType.SqlChars)
		{
		}

		// Token: 0x060029C4 RID: 10692 RVA: 0x000B7748 File Offset: 0x000B5948
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
				throw ExprException.Overflow(typeof(SqlChars));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x060029C5 RID: 10693 RVA: 0x00005AE9 File Offset: 0x00003CE9
		public override int Compare(int recordNo1, int recordNo2)
		{
			return 0;
		}

		// Token: 0x060029C6 RID: 10694 RVA: 0x00005AE9 File Offset: 0x00003CE9
		public override int CompareValueTo(int recordNo, object value)
		{
			return 0;
		}

		// Token: 0x060029C7 RID: 10695 RVA: 0x000B77D0 File Offset: 0x000B59D0
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x060029C8 RID: 10696 RVA: 0x000B77E2 File Offset: 0x000B59E2
		public override object Get(int record)
		{
			return this._values[record];
		}

		// Token: 0x060029C9 RID: 10697 RVA: 0x000B77EC File Offset: 0x000B59EC
		public override bool IsNull(int record)
		{
			return this._values[record].IsNull;
		}

		// Token: 0x060029CA RID: 10698 RVA: 0x000B77FB File Offset: 0x000B59FB
		public override void Set(int record, object value)
		{
			if (value == DBNull.Value || value == null)
			{
				this._values[record] = SqlChars.Null;
				return;
			}
			this._values[record] = (SqlChars)value;
		}

		// Token: 0x060029CB RID: 10699 RVA: 0x000B7824 File Offset: 0x000B5A24
		public override void SetCapacity(int capacity)
		{
			SqlChars[] array = new SqlChars[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x060029CC RID: 10700 RVA: 0x000B7864 File Offset: 0x000B5A64
		public override object ConvertXmlToObject(string s)
		{
			SqlString sqlString = default(SqlString);
			TextReader textReader = new StringReader("<col>" + s + "</col>");
			IXmlSerializable xmlSerializable = sqlString;
			using (XmlTextReader xmlTextReader = new XmlTextReader(textReader))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return new SqlChars((SqlString)xmlSerializable);
		}

		// Token: 0x060029CD RID: 10701 RVA: 0x000B78CC File Offset: 0x000B5ACC
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x060029CE RID: 10702 RVA: 0x000B791C File Offset: 0x000B5B1C
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlChars[recordCount];
		}

		// Token: 0x060029CF RID: 10703 RVA: 0x000B7924 File Offset: 0x000B5B24
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((SqlChars[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x060029D0 RID: 10704 RVA: 0x000B7946 File Offset: 0x000B5B46
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (SqlChars[])store;
		}

		// Token: 0x0400199E RID: 6558
		private SqlChars[] _values;
	}
}
