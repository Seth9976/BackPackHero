using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000365 RID: 869
	internal sealed class SqlMoneyStorage : DataStorage
	{
		// Token: 0x06002A3A RID: 10810 RVA: 0x000B9EBC File Offset: 0x000B80BC
		public SqlMoneyStorage(DataColumn column)
			: base(column, typeof(SqlMoney), SqlMoney.Null, SqlMoney.Null, StorageType.SqlMoney)
		{
		}

		// Token: 0x06002A3B RID: 10811 RVA: 0x000B9EE8 File Offset: 0x000B80E8
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Sum:
				{
					SqlDecimal sqlDecimal = 0L;
					foreach (int num in records)
					{
						if (!this.IsNull(num))
						{
							sqlDecimal += this._values[num];
							flag = true;
						}
					}
					if (flag)
					{
						return sqlDecimal;
					}
					return this._nullValue;
				}
				case AggregateType.Mean:
				{
					SqlDecimal sqlDecimal2 = 0L;
					int num2 = 0;
					foreach (int num3 in records)
					{
						if (!this.IsNull(num3))
						{
							sqlDecimal2 += this._values[num3].ToSqlDecimal();
							num2++;
							flag = true;
						}
					}
					if (flag)
					{
						0L;
						return (sqlDecimal2 / (long)num2).ToSqlMoney();
					}
					return this._nullValue;
				}
				case AggregateType.Min:
				{
					SqlMoney sqlMoney = SqlMoney.MaxValue;
					foreach (int num4 in records)
					{
						if (!this.IsNull(num4))
						{
							if (SqlMoney.LessThan(this._values[num4], sqlMoney).IsTrue)
							{
								sqlMoney = this._values[num4];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlMoney;
					}
					return this._nullValue;
				}
				case AggregateType.Max:
				{
					SqlMoney sqlMoney2 = SqlMoney.MinValue;
					foreach (int num5 in records)
					{
						if (!this.IsNull(num5))
						{
							if (SqlMoney.GreaterThan(this._values[num5], sqlMoney2).IsTrue)
							{
								sqlMoney2 = this._values[num5];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlMoney2;
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
					int num6 = 0;
					for (int l = 0; l < records.Length; l++)
					{
						if (!this.IsNull(records[l]))
						{
							num6++;
						}
					}
					return num6;
				}
				case AggregateType.Var:
				case AggregateType.StDev:
				{
					int num6 = 0;
					SqlDouble sqlDouble = 0.0;
					0.0;
					SqlDouble sqlDouble2 = 0.0;
					SqlDouble sqlDouble3 = 0.0;
					foreach (int num7 in records)
					{
						if (!this.IsNull(num7))
						{
							sqlDouble2 += this._values[num7].ToSqlDouble();
							sqlDouble3 += this._values[num7].ToSqlDouble() * this._values[num7].ToSqlDouble();
							num6++;
						}
					}
					if (num6 <= 1)
					{
						return this._nullValue;
					}
					sqlDouble = (double)num6 * sqlDouble3 - sqlDouble2 * sqlDouble2;
					SqlBoolean sqlBoolean = sqlDouble / (sqlDouble2 * sqlDouble2) < 1E-15;
					if (sqlBoolean ? sqlBoolean : (sqlBoolean | (sqlDouble < 0.0)))
					{
						sqlDouble = 0.0;
					}
					else
					{
						sqlDouble /= (double)(num6 * (num6 - 1));
					}
					if (kind == AggregateType.StDev)
					{
						return Math.Sqrt(sqlDouble.Value);
					}
					return sqlDouble;
				}
				}
			}
			catch (OverflowException)
			{
				throw ExprException.Overflow(typeof(SqlMoney));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002A3C RID: 10812 RVA: 0x000BA328 File Offset: 0x000B8528
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this._values[recordNo1].CompareTo(this._values[recordNo2]);
		}

		// Token: 0x06002A3D RID: 10813 RVA: 0x000BA347 File Offset: 0x000B8547
		public override int CompareValueTo(int recordNo, object value)
		{
			return this._values[recordNo].CompareTo((SqlMoney)value);
		}

		// Token: 0x06002A3E RID: 10814 RVA: 0x000BA360 File Offset: 0x000B8560
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlMoney(value);
			}
			return this._nullValue;
		}

		// Token: 0x06002A3F RID: 10815 RVA: 0x000BA377 File Offset: 0x000B8577
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06002A40 RID: 10816 RVA: 0x000BA391 File Offset: 0x000B8591
		public override object Get(int record)
		{
			return this._values[record];
		}

		// Token: 0x06002A41 RID: 10817 RVA: 0x000BA3A4 File Offset: 0x000B85A4
		public override bool IsNull(int record)
		{
			return this._values[record].IsNull;
		}

		// Token: 0x06002A42 RID: 10818 RVA: 0x000BA3B7 File Offset: 0x000B85B7
		public override void Set(int record, object value)
		{
			this._values[record] = SqlConvert.ConvertToSqlMoney(value);
		}

		// Token: 0x06002A43 RID: 10819 RVA: 0x000BA3CC File Offset: 0x000B85CC
		public override void SetCapacity(int capacity)
		{
			SqlMoney[] array = new SqlMoney[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x06002A44 RID: 10820 RVA: 0x000BA40C File Offset: 0x000B860C
		public override object ConvertXmlToObject(string s)
		{
			SqlMoney sqlMoney = default(SqlMoney);
			TextReader textReader = new StringReader("<col>" + s + "</col>");
			IXmlSerializable xmlSerializable = sqlMoney;
			using (XmlTextReader xmlTextReader = new XmlTextReader(textReader))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlMoney)xmlSerializable;
		}

		// Token: 0x06002A45 RID: 10821 RVA: 0x000BA474 File Offset: 0x000B8674
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06002A46 RID: 10822 RVA: 0x000BA4C4 File Offset: 0x000B86C4
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlMoney[recordCount];
		}

		// Token: 0x06002A47 RID: 10823 RVA: 0x000BA4CC File Offset: 0x000B86CC
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((SqlMoney[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06002A48 RID: 10824 RVA: 0x000BA4F6 File Offset: 0x000B86F6
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (SqlMoney[])store;
		}

		// Token: 0x040019A6 RID: 6566
		private SqlMoney[] _values;
	}
}
