using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Data.SqlTypes;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x0200036C RID: 876
	internal sealed class SqlUdtStorage : DataStorage
	{
		// Token: 0x06002A88 RID: 10888 RVA: 0x000BB927 File Offset: 0x000B9B27
		public SqlUdtStorage(DataColumn column, Type type)
			: this(column, type, SqlUdtStorage.GetStaticNullForUdtType(type))
		{
		}

		// Token: 0x06002A89 RID: 10889 RVA: 0x000BB938 File Offset: 0x000B9B38
		private SqlUdtStorage(DataColumn column, Type type, object nullValue)
			: base(column, type, nullValue, nullValue, typeof(ICloneable).IsAssignableFrom(type), DataStorage.GetStorageType(type))
		{
			this._implementsIXmlSerializable = typeof(IXmlSerializable).IsAssignableFrom(type);
			this._implementsIComparable = typeof(IComparable).IsAssignableFrom(type);
		}

		// Token: 0x06002A8A RID: 10890 RVA: 0x000BB994 File Offset: 0x000B9B94
		internal static object GetStaticNullForUdtType(Type type)
		{
			return SqlUdtStorage.s_typeToNull.GetOrAdd(type, delegate(Type t)
			{
				PropertyInfo property = type.GetProperty("Null", BindingFlags.Static | BindingFlags.Public);
				if (property != null)
				{
					return property.GetValue(null, null);
				}
				FieldInfo field = type.GetField("Null", BindingFlags.Static | BindingFlags.Public);
				if (field != null)
				{
					return field.GetValue(null);
				}
				throw ExceptionBuilder.INullableUDTwithoutStaticNull(type.AssemblyQualifiedName);
			});
		}

		// Token: 0x06002A8B RID: 10891 RVA: 0x000BB9CA File Offset: 0x000B9BCA
		public override bool IsNull(int record)
		{
			return ((INullable)this._values[record]).IsNull;
		}

		// Token: 0x06002A8C RID: 10892 RVA: 0x000A82B4 File Offset: 0x000A64B4
		public override object Aggregate(int[] records, AggregateType kind)
		{
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002A8D RID: 10893 RVA: 0x000BB9DE File Offset: 0x000B9BDE
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this.CompareValueTo(recordNo1, this._values[recordNo2]);
		}

		// Token: 0x06002A8E RID: 10894 RVA: 0x000BB9F0 File Offset: 0x000B9BF0
		public override int CompareValueTo(int recordNo1, object value)
		{
			if (DBNull.Value == value)
			{
				value = this._nullValue;
			}
			if (this._implementsIComparable)
			{
				return ((IComparable)this._values[recordNo1]).CompareTo(value);
			}
			if (this._nullValue != value)
			{
				throw ExceptionBuilder.IComparableNotImplemented(this._dataType.AssemblyQualifiedName);
			}
			if (!((INullable)this._values[recordNo1]).IsNull)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06002A8F RID: 10895 RVA: 0x000BBA5A File Offset: 0x000B9C5A
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06002A90 RID: 10896 RVA: 0x000BBA74 File Offset: 0x000B9C74
		public override object Get(int recordNo)
		{
			return this._values[recordNo];
		}

		// Token: 0x06002A91 RID: 10897 RVA: 0x000BBA80 File Offset: 0x000B9C80
		public override void Set(int recordNo, object value)
		{
			if (DBNull.Value == value)
			{
				this._values[recordNo] = this._nullValue;
				base.SetNullBit(recordNo, true);
				return;
			}
			if (value == null)
			{
				if (this._isValueType)
				{
					throw ExceptionBuilder.StorageSetFailed();
				}
				this._values[recordNo] = this._nullValue;
				base.SetNullBit(recordNo, true);
				return;
			}
			else
			{
				if (!this._dataType.IsInstanceOfType(value))
				{
					throw ExceptionBuilder.StorageSetFailed();
				}
				this._values[recordNo] = value;
				base.SetNullBit(recordNo, false);
				return;
			}
		}

		// Token: 0x06002A92 RID: 10898 RVA: 0x000BBAFC File Offset: 0x000B9CFC
		public override void SetCapacity(int capacity)
		{
			object[] array = new object[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
			base.SetCapacity(capacity);
		}

		// Token: 0x06002A93 RID: 10899 RVA: 0x000BBB44 File Offset: 0x000B9D44
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override object ConvertXmlToObject(string s)
		{
			if (this._implementsIXmlSerializable)
			{
				object obj = Activator.CreateInstance(this._dataType, true);
				using (XmlTextReader xmlTextReader = new XmlTextReader(new StringReader("<col>" + s + "</col>")))
				{
					((IXmlSerializable)obj).ReadXml(xmlTextReader);
				}
				return obj;
			}
			StringReader stringReader = new StringReader(s);
			return ObjectStorage.GetXmlSerializer(this._dataType).Deserialize(stringReader);
		}

		// Token: 0x06002A94 RID: 10900 RVA: 0x000BBBC4 File Offset: 0x000B9DC4
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override object ConvertXmlToObject(XmlReader xmlReader, XmlRootAttribute xmlAttrib)
		{
			if (xmlAttrib == null)
			{
				string text = xmlReader.GetAttribute("InstanceType", "urn:schemas-microsoft-com:xml-msdata");
				if (text == null)
				{
					string attribute = xmlReader.GetAttribute("InstanceType", "http://www.w3.org/2001/XMLSchema-instance");
					if (attribute != null)
					{
						text = XSDSchema.XsdtoClr(attribute).FullName;
					}
				}
				object obj = Activator.CreateInstance((text == null) ? this._dataType : Type.GetType(text), true);
				((IXmlSerializable)obj).ReadXml(xmlReader);
				return obj;
			}
			return ObjectStorage.GetXmlSerializer(this._dataType, xmlAttrib).Deserialize(xmlReader);
		}

		// Token: 0x06002A95 RID: 10901 RVA: 0x000BBC40 File Offset: 0x000B9E40
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			if (this._implementsIXmlSerializable)
			{
				using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
				{
					((IXmlSerializable)value).WriteXml(xmlTextWriter);
					goto IL_0045;
				}
			}
			ObjectStorage.GetXmlSerializer(value.GetType()).Serialize(stringWriter, value);
			IL_0045:
			return stringWriter.ToString();
		}

		// Token: 0x06002A96 RID: 10902 RVA: 0x000BBCA8 File Offset: 0x000B9EA8
		public override void ConvertObjectToXml(object value, XmlWriter xmlWriter, XmlRootAttribute xmlAttrib)
		{
			if (xmlAttrib == null)
			{
				((IXmlSerializable)value).WriteXml(xmlWriter);
				return;
			}
			ObjectStorage.GetXmlSerializer(this._dataType, xmlAttrib).Serialize(xmlWriter, value);
		}

		// Token: 0x06002A97 RID: 10903 RVA: 0x000B4B59 File Offset: 0x000B2D59
		protected override object GetEmptyStorage(int recordCount)
		{
			return new object[recordCount];
		}

		// Token: 0x06002A98 RID: 10904 RVA: 0x000BBCCD File Offset: 0x000B9ECD
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((object[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06002A99 RID: 10905 RVA: 0x000BBCEF File Offset: 0x000B9EEF
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (object[])store;
		}

		// Token: 0x040019CB RID: 6603
		private object[] _values;

		// Token: 0x040019CC RID: 6604
		private readonly bool _implementsIXmlSerializable;

		// Token: 0x040019CD RID: 6605
		private readonly bool _implementsIComparable;

		// Token: 0x040019CE RID: 6606
		private static readonly ConcurrentDictionary<Type, object> s_typeToNull = new ConcurrentDictionary<Type, object>();
	}
}
