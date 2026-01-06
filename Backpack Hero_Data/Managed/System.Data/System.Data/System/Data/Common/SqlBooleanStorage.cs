using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000368 RID: 872
	internal sealed class SqlBooleanStorage : DataStorage
	{
		// Token: 0x06002A69 RID: 10857 RVA: 0x000BAF38 File Offset: 0x000B9138
		public SqlBooleanStorage(DataColumn column)
			: base(column, typeof(SqlBoolean), SqlBoolean.Null, SqlBoolean.Null, StorageType.SqlBoolean)
		{
		}

		// Token: 0x06002A6A RID: 10858 RVA: 0x000BAF64 File Offset: 0x000B9164
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Min:
				{
					SqlBoolean sqlBoolean = true;
					foreach (int num in records)
					{
						if (!this.IsNull(num))
						{
							sqlBoolean = SqlBoolean.And(this._values[num], sqlBoolean);
							flag = true;
						}
					}
					if (flag)
					{
						return sqlBoolean;
					}
					return this._nullValue;
				}
				case AggregateType.Max:
				{
					SqlBoolean sqlBoolean2 = false;
					foreach (int num2 in records)
					{
						if (!this.IsNull(num2))
						{
							sqlBoolean2 = SqlBoolean.Or(this._values[num2], sqlBoolean2);
							flag = true;
						}
					}
					if (flag)
					{
						return sqlBoolean2;
					}
					return this._nullValue;
				}
				case AggregateType.First:
					if (records.Length != 0)
					{
						return this._values[records[0]];
					}
					return this._nullValue;
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
				throw ExprException.Overflow(typeof(SqlBoolean));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002A6B RID: 10859 RVA: 0x000BB0D4 File Offset: 0x000B92D4
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this._values[recordNo1].CompareTo(this._values[recordNo2]);
		}

		// Token: 0x06002A6C RID: 10860 RVA: 0x000BB0F3 File Offset: 0x000B92F3
		public override int CompareValueTo(int recordNo, object value)
		{
			return this._values[recordNo].CompareTo((SqlBoolean)value);
		}

		// Token: 0x06002A6D RID: 10861 RVA: 0x000BB10C File Offset: 0x000B930C
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlBoolean(value);
			}
			return this._nullValue;
		}

		// Token: 0x06002A6E RID: 10862 RVA: 0x000BB123 File Offset: 0x000B9323
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06002A6F RID: 10863 RVA: 0x000BB13D File Offset: 0x000B933D
		public override object Get(int record)
		{
			return this._values[record];
		}

		// Token: 0x06002A70 RID: 10864 RVA: 0x000BB150 File Offset: 0x000B9350
		public override bool IsNull(int record)
		{
			return this._values[record].IsNull;
		}

		// Token: 0x06002A71 RID: 10865 RVA: 0x000BB163 File Offset: 0x000B9363
		public override void Set(int record, object value)
		{
			this._values[record] = SqlConvert.ConvertToSqlBoolean(value);
		}

		// Token: 0x06002A72 RID: 10866 RVA: 0x000BB178 File Offset: 0x000B9378
		public override void SetCapacity(int capacity)
		{
			SqlBoolean[] array = new SqlBoolean[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x06002A73 RID: 10867 RVA: 0x000BB1B8 File Offset: 0x000B93B8
		public override object ConvertXmlToObject(string s)
		{
			SqlBoolean sqlBoolean = default(SqlBoolean);
			TextReader textReader = new StringReader("<col>" + s + "</col>");
			IXmlSerializable xmlSerializable = sqlBoolean;
			using (XmlTextReader xmlTextReader = new XmlTextReader(textReader))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlBoolean)xmlSerializable;
		}

		// Token: 0x06002A74 RID: 10868 RVA: 0x000BB220 File Offset: 0x000B9420
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06002A75 RID: 10869 RVA: 0x000BB270 File Offset: 0x000B9470
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlBoolean[recordCount];
		}

		// Token: 0x06002A76 RID: 10870 RVA: 0x000BB278 File Offset: 0x000B9478
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((SqlBoolean[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06002A77 RID: 10871 RVA: 0x000BB2A2 File Offset: 0x000B94A2
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (SqlBoolean[])store;
		}

		// Token: 0x040019A9 RID: 6569
		private SqlBoolean[] _values;
	}
}
