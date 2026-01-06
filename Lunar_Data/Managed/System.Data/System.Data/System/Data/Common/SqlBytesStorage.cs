using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x0200035C RID: 860
	internal sealed class SqlBytesStorage : DataStorage
	{
		// Token: 0x060029B5 RID: 10677 RVA: 0x000B74FB File Offset: 0x000B56FB
		public SqlBytesStorage(DataColumn column)
			: base(column, typeof(SqlBytes), SqlBytes.Null, SqlBytes.Null, StorageType.SqlBytes)
		{
		}

		// Token: 0x060029B6 RID: 10678 RVA: 0x000B751C File Offset: 0x000B571C
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
				throw ExprException.Overflow(typeof(SqlBytes));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x060029B7 RID: 10679 RVA: 0x00005AE9 File Offset: 0x00003CE9
		public override int Compare(int recordNo1, int recordNo2)
		{
			return 0;
		}

		// Token: 0x060029B8 RID: 10680 RVA: 0x00005AE9 File Offset: 0x00003CE9
		public override int CompareValueTo(int recordNo, object value)
		{
			return 0;
		}

		// Token: 0x060029B9 RID: 10681 RVA: 0x000B75A4 File Offset: 0x000B57A4
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x060029BA RID: 10682 RVA: 0x000B75B6 File Offset: 0x000B57B6
		public override object Get(int record)
		{
			return this._values[record];
		}

		// Token: 0x060029BB RID: 10683 RVA: 0x000B75C0 File Offset: 0x000B57C0
		public override bool IsNull(int record)
		{
			return this._values[record].IsNull;
		}

		// Token: 0x060029BC RID: 10684 RVA: 0x000B75CF File Offset: 0x000B57CF
		public override void Set(int record, object value)
		{
			if (value == DBNull.Value || value == null)
			{
				this._values[record] = SqlBytes.Null;
				return;
			}
			this._values[record] = (SqlBytes)value;
		}

		// Token: 0x060029BD RID: 10685 RVA: 0x000B75F8 File Offset: 0x000B57F8
		public override void SetCapacity(int capacity)
		{
			SqlBytes[] array = new SqlBytes[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x060029BE RID: 10686 RVA: 0x000B7638 File Offset: 0x000B5838
		public override object ConvertXmlToObject(string s)
		{
			SqlBinary sqlBinary = default(SqlBinary);
			TextReader textReader = new StringReader("<col>" + s + "</col>");
			IXmlSerializable xmlSerializable = sqlBinary;
			using (XmlTextReader xmlTextReader = new XmlTextReader(textReader))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return new SqlBytes((SqlBinary)xmlSerializable);
		}

		// Token: 0x060029BF RID: 10687 RVA: 0x000B76A0 File Offset: 0x000B58A0
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x060029C0 RID: 10688 RVA: 0x000B76F0 File Offset: 0x000B58F0
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlBytes[recordCount];
		}

		// Token: 0x060029C1 RID: 10689 RVA: 0x000B76F8 File Offset: 0x000B58F8
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((SqlBytes[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x060029C2 RID: 10690 RVA: 0x000B771A File Offset: 0x000B591A
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (SqlBytes[])store;
		}

		// Token: 0x0400199D RID: 6557
		private SqlBytes[] _values;
	}
}
