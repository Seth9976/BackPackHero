using System;
using System.Collections;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Numerics;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000330 RID: 816
	internal abstract class DataStorage
	{
		// Token: 0x06002676 RID: 9846 RVA: 0x000ACD11 File Offset: 0x000AAF11
		protected DataStorage(DataColumn column, Type type, object defaultValue, StorageType storageType)
			: this(column, type, defaultValue, DBNull.Value, false, storageType)
		{
		}

		// Token: 0x06002677 RID: 9847 RVA: 0x000ACD24 File Offset: 0x000AAF24
		protected DataStorage(DataColumn column, Type type, object defaultValue, object nullValue, StorageType storageType)
			: this(column, type, defaultValue, nullValue, false, storageType)
		{
		}

		// Token: 0x06002678 RID: 9848 RVA: 0x000ACD34 File Offset: 0x000AAF34
		protected DataStorage(DataColumn column, Type type, object defaultValue, object nullValue, bool isICloneable, StorageType storageType)
		{
			this._column = column;
			this._table = column.Table;
			this._dataType = type;
			this._storageTypeCode = storageType;
			this._defaultValue = defaultValue;
			this._nullValue = nullValue;
			this._isCloneable = isICloneable;
			this._isCustomDefinedType = DataStorage.IsTypeCustomType(this._storageTypeCode);
			this._isStringType = StorageType.String == this._storageTypeCode || StorageType.SqlString == this._storageTypeCode;
			this._isValueType = DataStorage.DetermineIfValueType(this._storageTypeCode, type);
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06002679 RID: 9849 RVA: 0x000ACDC0 File Offset: 0x000AAFC0
		internal DataSetDateTime DateTimeMode
		{
			get
			{
				return this._column.DateTimeMode;
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x0600267A RID: 9850 RVA: 0x000ACDCD File Offset: 0x000AAFCD
		internal IFormatProvider FormatProvider
		{
			get
			{
				return this._table.FormatProvider;
			}
		}

		// Token: 0x0600267B RID: 9851 RVA: 0x000ACDDA File Offset: 0x000AAFDA
		public virtual object Aggregate(int[] recordNos, AggregateType kind)
		{
			if (AggregateType.Count == kind)
			{
				return this.AggregateCount(recordNos);
			}
			return null;
		}

		// Token: 0x0600267C RID: 9852 RVA: 0x000ACDEC File Offset: 0x000AAFEC
		public object AggregateCount(int[] recordNos)
		{
			int num = 0;
			for (int i = 0; i < recordNos.Length; i++)
			{
				if (!this._dbNullBits.Get(recordNos[i]))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600267D RID: 9853 RVA: 0x000ACE24 File Offset: 0x000AB024
		protected int CompareBits(int recordNo1, int recordNo2)
		{
			bool flag = this._dbNullBits.Get(recordNo1);
			bool flag2 = this._dbNullBits.Get(recordNo2);
			if (!(flag ^ flag2))
			{
				return 0;
			}
			if (flag)
			{
				return -1;
			}
			return 1;
		}

		// Token: 0x0600267E RID: 9854
		public abstract int Compare(int recordNo1, int recordNo2);

		// Token: 0x0600267F RID: 9855
		public abstract int CompareValueTo(int recordNo1, object value);

		// Token: 0x06002680 RID: 9856 RVA: 0x0000567E File Offset: 0x0000387E
		public virtual object ConvertValue(object value)
		{
			return value;
		}

		// Token: 0x06002681 RID: 9857 RVA: 0x000ACE58 File Offset: 0x000AB058
		protected void CopyBits(int srcRecordNo, int dstRecordNo)
		{
			this._dbNullBits.Set(dstRecordNo, this._dbNullBits.Get(srcRecordNo));
		}

		// Token: 0x06002682 RID: 9858
		public abstract void Copy(int recordNo1, int recordNo2);

		// Token: 0x06002683 RID: 9859
		public abstract object Get(int recordNo);

		// Token: 0x06002684 RID: 9860 RVA: 0x000ACE72 File Offset: 0x000AB072
		protected object GetBits(int recordNo)
		{
			if (this._dbNullBits.Get(recordNo))
			{
				return this._nullValue;
			}
			return this._defaultValue;
		}

		// Token: 0x06002685 RID: 9861 RVA: 0x000ACE8F File Offset: 0x000AB08F
		public virtual int GetStringLength(int record)
		{
			return int.MaxValue;
		}

		// Token: 0x06002686 RID: 9862 RVA: 0x000ACE96 File Offset: 0x000AB096
		protected bool HasValue(int recordNo)
		{
			return !this._dbNullBits.Get(recordNo);
		}

		// Token: 0x06002687 RID: 9863 RVA: 0x000ACEA7 File Offset: 0x000AB0A7
		public virtual bool IsNull(int recordNo)
		{
			return this._dbNullBits.Get(recordNo);
		}

		// Token: 0x06002688 RID: 9864
		public abstract void Set(int recordNo, object value);

		// Token: 0x06002689 RID: 9865 RVA: 0x000ACEB5 File Offset: 0x000AB0B5
		protected void SetNullBit(int recordNo, bool flag)
		{
			this._dbNullBits.Set(recordNo, flag);
		}

		// Token: 0x0600268A RID: 9866 RVA: 0x000ACEC4 File Offset: 0x000AB0C4
		public virtual void SetCapacity(int capacity)
		{
			if (this._dbNullBits == null)
			{
				this._dbNullBits = new BitArray(capacity);
				return;
			}
			this._dbNullBits.Length = capacity;
		}

		// Token: 0x0600268B RID: 9867
		public abstract object ConvertXmlToObject(string s);

		// Token: 0x0600268C RID: 9868 RVA: 0x000ACEE7 File Offset: 0x000AB0E7
		public virtual object ConvertXmlToObject(XmlReader xmlReader, XmlRootAttribute xmlAttrib)
		{
			return this.ConvertXmlToObject(xmlReader.Value);
		}

		// Token: 0x0600268D RID: 9869
		public abstract string ConvertObjectToXml(object value);

		// Token: 0x0600268E RID: 9870 RVA: 0x000ACEF5 File Offset: 0x000AB0F5
		public virtual void ConvertObjectToXml(object value, XmlWriter xmlWriter, XmlRootAttribute xmlAttrib)
		{
			xmlWriter.WriteString(this.ConvertObjectToXml(value));
		}

		// Token: 0x0600268F RID: 9871 RVA: 0x000ACF04 File Offset: 0x000AB104
		public static DataStorage CreateStorage(DataColumn column, Type dataType, StorageType typeCode)
		{
			if (typeCode != StorageType.Empty || !(null != dataType))
			{
				switch (typeCode)
				{
				case StorageType.Empty:
					throw ExceptionBuilder.InvalidStorageType(TypeCode.Empty);
				case StorageType.DBNull:
					throw ExceptionBuilder.InvalidStorageType(TypeCode.DBNull);
				case StorageType.Boolean:
					return new BooleanStorage(column);
				case StorageType.Char:
					return new CharStorage(column);
				case StorageType.SByte:
					return new SByteStorage(column);
				case StorageType.Byte:
					return new ByteStorage(column);
				case StorageType.Int16:
					return new Int16Storage(column);
				case StorageType.UInt16:
					return new UInt16Storage(column);
				case StorageType.Int32:
					return new Int32Storage(column);
				case StorageType.UInt32:
					return new UInt32Storage(column);
				case StorageType.Int64:
					return new Int64Storage(column);
				case StorageType.UInt64:
					return new UInt64Storage(column);
				case StorageType.Single:
					return new SingleStorage(column);
				case StorageType.Double:
					return new DoubleStorage(column);
				case StorageType.Decimal:
					return new DecimalStorage(column);
				case StorageType.DateTime:
					return new DateTimeStorage(column);
				case StorageType.TimeSpan:
					return new TimeSpanStorage(column);
				case StorageType.String:
					return new StringStorage(column);
				case StorageType.Guid:
					return new ObjectStorage(column, dataType);
				case StorageType.ByteArray:
					return new ObjectStorage(column, dataType);
				case StorageType.CharArray:
					return new ObjectStorage(column, dataType);
				case StorageType.Type:
					return new ObjectStorage(column, dataType);
				case StorageType.DateTimeOffset:
					return new DateTimeOffsetStorage(column);
				case StorageType.BigInteger:
					return new BigIntegerStorage(column);
				case StorageType.Uri:
					return new ObjectStorage(column, dataType);
				case StorageType.SqlBinary:
					return new SqlBinaryStorage(column);
				case StorageType.SqlBoolean:
					return new SqlBooleanStorage(column);
				case StorageType.SqlByte:
					return new SqlByteStorage(column);
				case StorageType.SqlBytes:
					return new SqlBytesStorage(column);
				case StorageType.SqlChars:
					return new SqlCharsStorage(column);
				case StorageType.SqlDateTime:
					return new SqlDateTimeStorage(column);
				case StorageType.SqlDecimal:
					return new SqlDecimalStorage(column);
				case StorageType.SqlDouble:
					return new SqlDoubleStorage(column);
				case StorageType.SqlGuid:
					return new SqlGuidStorage(column);
				case StorageType.SqlInt16:
					return new SqlInt16Storage(column);
				case StorageType.SqlInt32:
					return new SqlInt32Storage(column);
				case StorageType.SqlInt64:
					return new SqlInt64Storage(column);
				case StorageType.SqlMoney:
					return new SqlMoneyStorage(column);
				case StorageType.SqlSingle:
					return new SqlSingleStorage(column);
				case StorageType.SqlString:
					return new SqlStringStorage(column);
				}
				return new ObjectStorage(column, dataType);
			}
			if (typeof(INullable).IsAssignableFrom(dataType))
			{
				return new SqlUdtStorage(column, dataType);
			}
			return new ObjectStorage(column, dataType);
		}

		// Token: 0x06002690 RID: 9872 RVA: 0x000AD110 File Offset: 0x000AB310
		internal static StorageType GetStorageType(Type dataType)
		{
			for (int i = 0; i < DataStorage.s_storageClassType.Length; i++)
			{
				if (dataType == DataStorage.s_storageClassType[i])
				{
					return (StorageType)i;
				}
			}
			TypeCode typeCode = Type.GetTypeCode(dataType);
			if (TypeCode.Object != typeCode)
			{
				return (StorageType)typeCode;
			}
			return StorageType.Empty;
		}

		// Token: 0x06002691 RID: 9873 RVA: 0x000AD14E File Offset: 0x000AB34E
		internal static Type GetTypeStorage(StorageType storageType)
		{
			return DataStorage.s_storageClassType[(int)storageType];
		}

		// Token: 0x06002692 RID: 9874 RVA: 0x000AD157 File Offset: 0x000AB357
		internal static bool IsTypeCustomType(Type type)
		{
			return DataStorage.IsTypeCustomType(DataStorage.GetStorageType(type));
		}

		// Token: 0x06002693 RID: 9875 RVA: 0x000AD164 File Offset: 0x000AB364
		internal static bool IsTypeCustomType(StorageType typeCode)
		{
			return StorageType.Object == typeCode || typeCode == StorageType.Empty || StorageType.CharArray == typeCode;
		}

		// Token: 0x06002694 RID: 9876 RVA: 0x000AD174 File Offset: 0x000AB374
		internal static bool IsSqlType(StorageType storageType)
		{
			return StorageType.SqlBinary <= storageType;
		}

		// Token: 0x06002695 RID: 9877 RVA: 0x000AD180 File Offset: 0x000AB380
		public static bool IsSqlType(Type dataType)
		{
			for (int i = 26; i < DataStorage.s_storageClassType.Length; i++)
			{
				if (dataType == DataStorage.s_storageClassType[i])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002696 RID: 9878 RVA: 0x000AD1B4 File Offset: 0x000AB3B4
		private static bool DetermineIfValueType(StorageType typeCode, Type dataType)
		{
			bool flag;
			switch (typeCode)
			{
			case StorageType.Boolean:
			case StorageType.Char:
			case StorageType.SByte:
			case StorageType.Byte:
			case StorageType.Int16:
			case StorageType.UInt16:
			case StorageType.Int32:
			case StorageType.UInt32:
			case StorageType.Int64:
			case StorageType.UInt64:
			case StorageType.Single:
			case StorageType.Double:
			case StorageType.Decimal:
			case StorageType.DateTime:
			case StorageType.TimeSpan:
			case StorageType.Guid:
			case StorageType.DateTimeOffset:
			case StorageType.BigInteger:
			case StorageType.SqlBinary:
			case StorageType.SqlBoolean:
			case StorageType.SqlByte:
			case StorageType.SqlDateTime:
			case StorageType.SqlDecimal:
			case StorageType.SqlDouble:
			case StorageType.SqlGuid:
			case StorageType.SqlInt16:
			case StorageType.SqlInt32:
			case StorageType.SqlInt64:
			case StorageType.SqlMoney:
			case StorageType.SqlSingle:
			case StorageType.SqlString:
				flag = true;
				break;
			case StorageType.String:
			case StorageType.ByteArray:
			case StorageType.CharArray:
			case StorageType.Type:
			case StorageType.Uri:
			case StorageType.SqlBytes:
			case StorageType.SqlChars:
				flag = false;
				break;
			default:
				flag = dataType.IsValueType;
				break;
			}
			return flag;
		}

		// Token: 0x06002697 RID: 9879 RVA: 0x000AD274 File Offset: 0x000AB474
		internal static void ImplementsInterfaces(StorageType typeCode, Type dataType, out bool sqlType, out bool nullable, out bool xmlSerializable, out bool changeTracking, out bool revertibleChangeTracking)
		{
			if (DataStorage.IsSqlType(typeCode))
			{
				sqlType = true;
				nullable = true;
				changeTracking = false;
				revertibleChangeTracking = false;
				xmlSerializable = true;
				return;
			}
			if (typeCode != StorageType.Empty)
			{
				sqlType = false;
				nullable = false;
				changeTracking = false;
				revertibleChangeTracking = false;
				xmlSerializable = false;
				return;
			}
			Tuple<bool, bool, bool, bool> orAdd = DataStorage.s_typeImplementsInterface.GetOrAdd(dataType, DataStorage.s_inspectTypeForInterfaces);
			sqlType = false;
			nullable = orAdd.Item1;
			changeTracking = orAdd.Item2;
			revertibleChangeTracking = orAdd.Item3;
			xmlSerializable = orAdd.Item4;
		}

		// Token: 0x06002698 RID: 9880 RVA: 0x000AD2EC File Offset: 0x000AB4EC
		private static Tuple<bool, bool, bool, bool> InspectTypeForInterfaces(Type dataType)
		{
			return new Tuple<bool, bool, bool, bool>(typeof(INullable).IsAssignableFrom(dataType), typeof(IChangeTracking).IsAssignableFrom(dataType), typeof(IRevertibleChangeTracking).IsAssignableFrom(dataType), typeof(IXmlSerializable).IsAssignableFrom(dataType));
		}

		// Token: 0x06002699 RID: 9881 RVA: 0x000AD33E File Offset: 0x000AB53E
		internal static bool ImplementsINullableValue(StorageType typeCode, Type dataType)
		{
			return typeCode == StorageType.Empty && dataType.IsGenericType && dataType.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		// Token: 0x0600269A RID: 9882 RVA: 0x000AD362 File Offset: 0x000AB562
		public static bool IsObjectNull(object value)
		{
			return value == null || DBNull.Value == value || DataStorage.IsObjectSqlNull(value);
		}

		// Token: 0x0600269B RID: 9883 RVA: 0x000AD378 File Offset: 0x000AB578
		public static bool IsObjectSqlNull(object value)
		{
			INullable nullable = value as INullable;
			return nullable != null && nullable.IsNull;
		}

		// Token: 0x0600269C RID: 9884 RVA: 0x000AD397 File Offset: 0x000AB597
		internal object GetEmptyStorageInternal(int recordCount)
		{
			return this.GetEmptyStorage(recordCount);
		}

		// Token: 0x0600269D RID: 9885 RVA: 0x000AD3A0 File Offset: 0x000AB5A0
		internal void CopyValueInternal(int record, object store, BitArray nullbits, int storeIndex)
		{
			this.CopyValue(record, store, nullbits, storeIndex);
		}

		// Token: 0x0600269E RID: 9886 RVA: 0x000AD3AD File Offset: 0x000AB5AD
		internal void SetStorageInternal(object store, BitArray nullbits)
		{
			this.SetStorage(store, nullbits);
		}

		// Token: 0x0600269F RID: 9887
		protected abstract object GetEmptyStorage(int recordCount);

		// Token: 0x060026A0 RID: 9888
		protected abstract void CopyValue(int record, object store, BitArray nullbits, int storeIndex);

		// Token: 0x060026A1 RID: 9889
		protected abstract void SetStorage(object store, BitArray nullbits);

		// Token: 0x060026A2 RID: 9890 RVA: 0x000AD3B7 File Offset: 0x000AB5B7
		protected void SetNullStorage(BitArray nullbits)
		{
			this._dbNullBits = nullbits;
		}

		// Token: 0x060026A3 RID: 9891 RVA: 0x000AD3C0 File Offset: 0x000AB5C0
		internal static Type GetType(string value)
		{
			Type type = Type.GetType(value);
			if (null == type && "System.Numerics.BigInteger" == value)
			{
				type = typeof(BigInteger);
			}
			ObjectStorage.VerifyIDynamicMetaObjectProvider(type);
			return type;
		}

		// Token: 0x060026A4 RID: 9892 RVA: 0x000AD3FC File Offset: 0x000AB5FC
		internal static string GetQualifiedName(Type type)
		{
			ObjectStorage.VerifyIDynamicMetaObjectProvider(type);
			return type.AssemblyQualifiedName;
		}

		// Token: 0x040018E5 RID: 6373
		private static readonly Type[] s_storageClassType = new Type[]
		{
			null,
			typeof(object),
			typeof(DBNull),
			typeof(bool),
			typeof(char),
			typeof(sbyte),
			typeof(byte),
			typeof(short),
			typeof(ushort),
			typeof(int),
			typeof(uint),
			typeof(long),
			typeof(ulong),
			typeof(float),
			typeof(double),
			typeof(decimal),
			typeof(DateTime),
			typeof(TimeSpan),
			typeof(string),
			typeof(Guid),
			typeof(byte[]),
			typeof(char[]),
			typeof(Type),
			typeof(DateTimeOffset),
			typeof(BigInteger),
			typeof(Uri),
			typeof(SqlBinary),
			typeof(SqlBoolean),
			typeof(SqlByte),
			typeof(SqlBytes),
			typeof(SqlChars),
			typeof(SqlDateTime),
			typeof(SqlDecimal),
			typeof(SqlDouble),
			typeof(SqlGuid),
			typeof(SqlInt16),
			typeof(SqlInt32),
			typeof(SqlInt64),
			typeof(SqlMoney),
			typeof(SqlSingle),
			typeof(SqlString)
		};

		// Token: 0x040018E6 RID: 6374
		internal readonly DataColumn _column;

		// Token: 0x040018E7 RID: 6375
		internal readonly DataTable _table;

		// Token: 0x040018E8 RID: 6376
		internal readonly Type _dataType;

		// Token: 0x040018E9 RID: 6377
		internal readonly StorageType _storageTypeCode;

		// Token: 0x040018EA RID: 6378
		private BitArray _dbNullBits;

		// Token: 0x040018EB RID: 6379
		private readonly object _defaultValue;

		// Token: 0x040018EC RID: 6380
		internal readonly object _nullValue;

		// Token: 0x040018ED RID: 6381
		internal readonly bool _isCloneable;

		// Token: 0x040018EE RID: 6382
		internal readonly bool _isCustomDefinedType;

		// Token: 0x040018EF RID: 6383
		internal readonly bool _isStringType;

		// Token: 0x040018F0 RID: 6384
		internal readonly bool _isValueType;

		// Token: 0x040018F1 RID: 6385
		private static readonly Func<Type, Tuple<bool, bool, bool, bool>> s_inspectTypeForInterfaces = new Func<Type, Tuple<bool, bool, bool, bool>>(DataStorage.InspectTypeForInterfaces);

		// Token: 0x040018F2 RID: 6386
		private static readonly ConcurrentDictionary<Type, Tuple<bool, bool, bool, bool>> s_typeImplementsInterface = new ConcurrentDictionary<Type, Tuple<bool, bool, bool, bool>>();
	}
}
