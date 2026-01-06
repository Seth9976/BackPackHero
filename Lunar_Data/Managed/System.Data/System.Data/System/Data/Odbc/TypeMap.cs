using System;
using System.Data.Common;

namespace System.Data.Odbc
{
	// Token: 0x02000283 RID: 643
	internal sealed class TypeMap
	{
		// Token: 0x06001B65 RID: 7013 RVA: 0x00087DB8 File Offset: 0x00085FB8
		private TypeMap(OdbcType odbcType, DbType dbType, Type type, ODBC32.SQL_TYPE sql_type, ODBC32.SQL_C sql_c, ODBC32.SQL_C param_sql_c, int bsize, int csize, bool signType)
		{
			this._odbcType = odbcType;
			this._dbType = dbType;
			this._type = type;
			this._sql_type = sql_type;
			this._sql_c = sql_c;
			this._param_sql_c = param_sql_c;
			this._bufferSize = bsize;
			this._columnSize = csize;
			this._signType = signType;
		}

		// Token: 0x06001B66 RID: 7014 RVA: 0x00087E10 File Offset: 0x00086010
		internal static TypeMap FromOdbcType(OdbcType odbcType)
		{
			switch (odbcType)
			{
			case OdbcType.BigInt:
				return TypeMap.s_bigInt;
			case OdbcType.Binary:
				return TypeMap.s_binary;
			case OdbcType.Bit:
				return TypeMap.s_bit;
			case OdbcType.Char:
				return TypeMap._Char;
			case OdbcType.DateTime:
				return TypeMap.s_dateTime;
			case OdbcType.Decimal:
				return TypeMap.s_decimal;
			case OdbcType.Numeric:
				return TypeMap.s_numeric;
			case OdbcType.Double:
				return TypeMap.s_double;
			case OdbcType.Image:
				return TypeMap._Image;
			case OdbcType.Int:
				return TypeMap.s_int;
			case OdbcType.NChar:
				return TypeMap.s_NChar;
			case OdbcType.NText:
				return TypeMap._NText;
			case OdbcType.NVarChar:
				return TypeMap._NVarChar;
			case OdbcType.Real:
				return TypeMap.s_real;
			case OdbcType.UniqueIdentifier:
				return TypeMap.s_uniqueId;
			case OdbcType.SmallDateTime:
				return TypeMap.s_smallDT;
			case OdbcType.SmallInt:
				return TypeMap.s_smallInt;
			case OdbcType.Text:
				return TypeMap._Text;
			case OdbcType.Timestamp:
				return TypeMap.s_timestamp;
			case OdbcType.TinyInt:
				return TypeMap.s_tinyInt;
			case OdbcType.VarBinary:
				return TypeMap.s_varBinary;
			case OdbcType.VarChar:
				return TypeMap._VarChar;
			case OdbcType.Date:
				return TypeMap.s_date;
			case OdbcType.Time:
				return TypeMap.s_time;
			default:
				throw ODBC.UnknownOdbcType(odbcType);
			}
		}

		// Token: 0x06001B67 RID: 7015 RVA: 0x00087F20 File Offset: 0x00086120
		internal static TypeMap FromDbType(DbType dbType)
		{
			switch (dbType)
			{
			case DbType.AnsiString:
				return TypeMap._VarChar;
			case DbType.Binary:
				return TypeMap.s_varBinary;
			case DbType.Byte:
				return TypeMap.s_tinyInt;
			case DbType.Boolean:
				return TypeMap.s_bit;
			case DbType.Currency:
				return TypeMap.s_decimal;
			case DbType.Date:
				return TypeMap.s_date;
			case DbType.DateTime:
				return TypeMap.s_dateTime;
			case DbType.Decimal:
				return TypeMap.s_decimal;
			case DbType.Double:
				return TypeMap.s_double;
			case DbType.Guid:
				return TypeMap.s_uniqueId;
			case DbType.Int16:
				return TypeMap.s_smallInt;
			case DbType.Int32:
				return TypeMap.s_int;
			case DbType.Int64:
				return TypeMap.s_bigInt;
			case DbType.Single:
				return TypeMap.s_real;
			case DbType.String:
				return TypeMap._NVarChar;
			case DbType.Time:
				return TypeMap.s_time;
			case DbType.AnsiStringFixedLength:
				return TypeMap._Char;
			case DbType.StringFixedLength:
				return TypeMap.s_NChar;
			}
			throw ADP.DbTypeNotSupported(dbType, typeof(OdbcType));
		}

		// Token: 0x06001B68 RID: 7016 RVA: 0x00088014 File Offset: 0x00086214
		internal static TypeMap FromSystemType(Type dataType)
		{
			switch (Type.GetTypeCode(dataType))
			{
			case TypeCode.Empty:
				throw ADP.InvalidDataType(TypeCode.Empty);
			case TypeCode.Object:
				if (dataType == typeof(byte[]))
				{
					return TypeMap.s_varBinary;
				}
				if (dataType == typeof(Guid))
				{
					return TypeMap.s_uniqueId;
				}
				if (dataType == typeof(TimeSpan))
				{
					return TypeMap.s_time;
				}
				if (dataType == typeof(char[]))
				{
					return TypeMap._NVarChar;
				}
				throw ADP.UnknownDataType(dataType);
			case TypeCode.DBNull:
				throw ADP.InvalidDataType(TypeCode.DBNull);
			case TypeCode.Boolean:
				return TypeMap.s_bit;
			case TypeCode.Char:
			case TypeCode.String:
				return TypeMap._NVarChar;
			case TypeCode.SByte:
				return TypeMap.s_smallInt;
			case TypeCode.Byte:
				return TypeMap.s_tinyInt;
			case TypeCode.Int16:
				return TypeMap.s_smallInt;
			case TypeCode.UInt16:
				return TypeMap.s_int;
			case TypeCode.Int32:
				return TypeMap.s_int;
			case TypeCode.UInt32:
				return TypeMap.s_bigInt;
			case TypeCode.Int64:
				return TypeMap.s_bigInt;
			case TypeCode.UInt64:
				return TypeMap.s_numeric;
			case TypeCode.Single:
				return TypeMap.s_real;
			case TypeCode.Double:
				return TypeMap.s_double;
			case TypeCode.Decimal:
				return TypeMap.s_numeric;
			case TypeCode.DateTime:
				return TypeMap.s_dateTime;
			}
			throw ADP.UnknownDataTypeCode(dataType, Type.GetTypeCode(dataType));
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x00088154 File Offset: 0x00086354
		internal static TypeMap FromSqlType(ODBC32.SQL_TYPE sqltype)
		{
			switch (sqltype)
			{
			case ODBC32.SQL_TYPE.SS_TIME_EX:
			case ODBC32.SQL_TYPE.SS_UTCDATETIME:
				throw ODBC.UnknownSQLType(sqltype);
			case ODBC32.SQL_TYPE.SS_XML:
				return TypeMap.s_XML;
			case ODBC32.SQL_TYPE.SS_UDT:
				return TypeMap.s_UDT;
			case ODBC32.SQL_TYPE.SS_VARIANT:
				return TypeMap.s_variant;
			default:
				switch (sqltype)
				{
				case ODBC32.SQL_TYPE.GUID:
					return TypeMap.s_uniqueId;
				case ODBC32.SQL_TYPE.WLONGVARCHAR:
					return TypeMap._NText;
				case ODBC32.SQL_TYPE.WVARCHAR:
					return TypeMap._NVarChar;
				case ODBC32.SQL_TYPE.WCHAR:
					return TypeMap.s_NChar;
				case ODBC32.SQL_TYPE.BIT:
					return TypeMap.s_bit;
				case ODBC32.SQL_TYPE.TINYINT:
					return TypeMap.s_tinyInt;
				case ODBC32.SQL_TYPE.BIGINT:
					return TypeMap.s_bigInt;
				case ODBC32.SQL_TYPE.LONGVARBINARY:
					return TypeMap._Image;
				case ODBC32.SQL_TYPE.VARBINARY:
					return TypeMap.s_varBinary;
				case ODBC32.SQL_TYPE.BINARY:
					return TypeMap.s_binary;
				case ODBC32.SQL_TYPE.LONGVARCHAR:
					return TypeMap._Text;
				case (ODBC32.SQL_TYPE)0:
				case (ODBC32.SQL_TYPE)9:
				case (ODBC32.SQL_TYPE)10:
					goto IL_0146;
				case ODBC32.SQL_TYPE.CHAR:
					return TypeMap._Char;
				case ODBC32.SQL_TYPE.NUMERIC:
					return TypeMap.s_numeric;
				case ODBC32.SQL_TYPE.DECIMAL:
					return TypeMap.s_decimal;
				case ODBC32.SQL_TYPE.INTEGER:
					return TypeMap.s_int;
				case ODBC32.SQL_TYPE.SMALLINT:
					return TypeMap.s_smallInt;
				case ODBC32.SQL_TYPE.FLOAT:
					return TypeMap.s_double;
				case ODBC32.SQL_TYPE.REAL:
					return TypeMap.s_real;
				case ODBC32.SQL_TYPE.DOUBLE:
					return TypeMap.s_double;
				case ODBC32.SQL_TYPE.TIMESTAMP:
					break;
				case ODBC32.SQL_TYPE.VARCHAR:
					return TypeMap._VarChar;
				default:
					switch (sqltype)
					{
					case ODBC32.SQL_TYPE.TYPE_DATE:
						return TypeMap.s_date;
					case ODBC32.SQL_TYPE.TYPE_TIME:
						return TypeMap.s_time;
					case ODBC32.SQL_TYPE.TYPE_TIMESTAMP:
						break;
					default:
						goto IL_0146;
					}
					break;
				}
				return TypeMap.s_dateTime;
				IL_0146:
				throw ODBC.UnknownSQLType(sqltype);
			}
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x000882B0 File Offset: 0x000864B0
		internal static TypeMap UpgradeSignedType(TypeMap typeMap, bool unsigned)
		{
			if (unsigned)
			{
				switch (typeMap._dbType)
				{
				case DbType.Int16:
					return TypeMap.s_int;
				case DbType.Int32:
					return TypeMap.s_bigInt;
				case DbType.Int64:
					return TypeMap.s_decimal;
				default:
					return typeMap;
				}
			}
			else
			{
				if (typeMap._dbType == DbType.Byte)
				{
					return TypeMap.s_smallInt;
				}
				return typeMap;
			}
		}

		// Token: 0x04001507 RID: 5383
		private static readonly TypeMap s_bigInt = new TypeMap(OdbcType.BigInt, DbType.Int64, typeof(long), ODBC32.SQL_TYPE.BIGINT, ODBC32.SQL_C.SBIGINT, ODBC32.SQL_C.SBIGINT, 8, 20, true);

		// Token: 0x04001508 RID: 5384
		private static readonly TypeMap s_binary = new TypeMap(OdbcType.Binary, DbType.Binary, typeof(byte[]), ODBC32.SQL_TYPE.BINARY, ODBC32.SQL_C.BINARY, ODBC32.SQL_C.BINARY, -1, -1, false);

		// Token: 0x04001509 RID: 5385
		private static readonly TypeMap s_bit = new TypeMap(OdbcType.Bit, DbType.Boolean, typeof(bool), ODBC32.SQL_TYPE.BIT, ODBC32.SQL_C.BIT, ODBC32.SQL_C.BIT, 1, 1, false);

		// Token: 0x0400150A RID: 5386
		internal static readonly TypeMap _Char = new TypeMap(OdbcType.Char, DbType.AnsiStringFixedLength, typeof(string), ODBC32.SQL_TYPE.CHAR, ODBC32.SQL_C.WCHAR, ODBC32.SQL_C.CHAR, -1, -1, false);

		// Token: 0x0400150B RID: 5387
		private static readonly TypeMap s_dateTime = new TypeMap(OdbcType.DateTime, DbType.DateTime, typeof(DateTime), ODBC32.SQL_TYPE.TYPE_TIMESTAMP, ODBC32.SQL_C.TYPE_TIMESTAMP, ODBC32.SQL_C.TYPE_TIMESTAMP, 16, 23, false);

		// Token: 0x0400150C RID: 5388
		private static readonly TypeMap s_date = new TypeMap(OdbcType.Date, DbType.Date, typeof(DateTime), ODBC32.SQL_TYPE.TYPE_DATE, ODBC32.SQL_C.TYPE_DATE, ODBC32.SQL_C.TYPE_DATE, 6, 10, false);

		// Token: 0x0400150D RID: 5389
		private static readonly TypeMap s_time = new TypeMap(OdbcType.Time, DbType.Time, typeof(TimeSpan), ODBC32.SQL_TYPE.TYPE_TIME, ODBC32.SQL_C.TYPE_TIME, ODBC32.SQL_C.TYPE_TIME, 6, 12, false);

		// Token: 0x0400150E RID: 5390
		private static readonly TypeMap s_decimal = new TypeMap(OdbcType.Decimal, DbType.Decimal, typeof(decimal), ODBC32.SQL_TYPE.DECIMAL, ODBC32.SQL_C.NUMERIC, ODBC32.SQL_C.NUMERIC, 19, 28, false);

		// Token: 0x0400150F RID: 5391
		private static readonly TypeMap s_double = new TypeMap(OdbcType.Double, DbType.Double, typeof(double), ODBC32.SQL_TYPE.DOUBLE, ODBC32.SQL_C.DOUBLE, ODBC32.SQL_C.DOUBLE, 8, 15, false);

		// Token: 0x04001510 RID: 5392
		internal static readonly TypeMap _Image = new TypeMap(OdbcType.Image, DbType.Binary, typeof(byte[]), ODBC32.SQL_TYPE.LONGVARBINARY, ODBC32.SQL_C.BINARY, ODBC32.SQL_C.BINARY, -1, -1, false);

		// Token: 0x04001511 RID: 5393
		private static readonly TypeMap s_int = new TypeMap(OdbcType.Int, DbType.Int32, typeof(int), ODBC32.SQL_TYPE.INTEGER, ODBC32.SQL_C.SLONG, ODBC32.SQL_C.SLONG, 4, 10, true);

		// Token: 0x04001512 RID: 5394
		private static readonly TypeMap s_NChar = new TypeMap(OdbcType.NChar, DbType.StringFixedLength, typeof(string), ODBC32.SQL_TYPE.WCHAR, ODBC32.SQL_C.WCHAR, ODBC32.SQL_C.WCHAR, -1, -1, false);

		// Token: 0x04001513 RID: 5395
		internal static readonly TypeMap _NText = new TypeMap(OdbcType.NText, DbType.String, typeof(string), ODBC32.SQL_TYPE.WLONGVARCHAR, ODBC32.SQL_C.WCHAR, ODBC32.SQL_C.WCHAR, -1, -1, false);

		// Token: 0x04001514 RID: 5396
		private static readonly TypeMap s_numeric = new TypeMap(OdbcType.Numeric, DbType.Decimal, typeof(decimal), ODBC32.SQL_TYPE.NUMERIC, ODBC32.SQL_C.NUMERIC, ODBC32.SQL_C.NUMERIC, 19, 28, false);

		// Token: 0x04001515 RID: 5397
		internal static readonly TypeMap _NVarChar = new TypeMap(OdbcType.NVarChar, DbType.String, typeof(string), ODBC32.SQL_TYPE.WVARCHAR, ODBC32.SQL_C.WCHAR, ODBC32.SQL_C.WCHAR, -1, -1, false);

		// Token: 0x04001516 RID: 5398
		private static readonly TypeMap s_real = new TypeMap(OdbcType.Real, DbType.Single, typeof(float), ODBC32.SQL_TYPE.REAL, ODBC32.SQL_C.REAL, ODBC32.SQL_C.REAL, 4, 7, false);

		// Token: 0x04001517 RID: 5399
		private static readonly TypeMap s_uniqueId = new TypeMap(OdbcType.UniqueIdentifier, DbType.Guid, typeof(Guid), ODBC32.SQL_TYPE.GUID, ODBC32.SQL_C.GUID, ODBC32.SQL_C.GUID, 16, 36, false);

		// Token: 0x04001518 RID: 5400
		private static readonly TypeMap s_smallDT = new TypeMap(OdbcType.SmallDateTime, DbType.DateTime, typeof(DateTime), ODBC32.SQL_TYPE.TYPE_TIMESTAMP, ODBC32.SQL_C.TYPE_TIMESTAMP, ODBC32.SQL_C.TYPE_TIMESTAMP, 16, 23, false);

		// Token: 0x04001519 RID: 5401
		private static readonly TypeMap s_smallInt = new TypeMap(OdbcType.SmallInt, DbType.Int16, typeof(short), ODBC32.SQL_TYPE.SMALLINT, ODBC32.SQL_C.SSHORT, ODBC32.SQL_C.SSHORT, 2, 5, true);

		// Token: 0x0400151A RID: 5402
		internal static readonly TypeMap _Text = new TypeMap(OdbcType.Text, DbType.AnsiString, typeof(string), ODBC32.SQL_TYPE.LONGVARCHAR, ODBC32.SQL_C.WCHAR, ODBC32.SQL_C.CHAR, -1, -1, false);

		// Token: 0x0400151B RID: 5403
		private static readonly TypeMap s_timestamp = new TypeMap(OdbcType.Timestamp, DbType.Binary, typeof(byte[]), ODBC32.SQL_TYPE.BINARY, ODBC32.SQL_C.BINARY, ODBC32.SQL_C.BINARY, -1, -1, false);

		// Token: 0x0400151C RID: 5404
		private static readonly TypeMap s_tinyInt = new TypeMap(OdbcType.TinyInt, DbType.Byte, typeof(byte), ODBC32.SQL_TYPE.TINYINT, ODBC32.SQL_C.UTINYINT, ODBC32.SQL_C.UTINYINT, 1, 3, true);

		// Token: 0x0400151D RID: 5405
		private static readonly TypeMap s_varBinary = new TypeMap(OdbcType.VarBinary, DbType.Binary, typeof(byte[]), ODBC32.SQL_TYPE.VARBINARY, ODBC32.SQL_C.BINARY, ODBC32.SQL_C.BINARY, -1, -1, false);

		// Token: 0x0400151E RID: 5406
		internal static readonly TypeMap _VarChar = new TypeMap(OdbcType.VarChar, DbType.AnsiString, typeof(string), ODBC32.SQL_TYPE.VARCHAR, ODBC32.SQL_C.WCHAR, ODBC32.SQL_C.CHAR, -1, -1, false);

		// Token: 0x0400151F RID: 5407
		private static readonly TypeMap s_variant = new TypeMap(OdbcType.Binary, DbType.Binary, typeof(object), ODBC32.SQL_TYPE.SS_VARIANT, ODBC32.SQL_C.BINARY, ODBC32.SQL_C.BINARY, -1, -1, false);

		// Token: 0x04001520 RID: 5408
		private static readonly TypeMap s_UDT = new TypeMap(OdbcType.Binary, DbType.Binary, typeof(object), ODBC32.SQL_TYPE.SS_UDT, ODBC32.SQL_C.BINARY, ODBC32.SQL_C.BINARY, -1, -1, false);

		// Token: 0x04001521 RID: 5409
		private static readonly TypeMap s_XML = new TypeMap(OdbcType.Text, DbType.AnsiString, typeof(string), ODBC32.SQL_TYPE.LONGVARCHAR, ODBC32.SQL_C.WCHAR, ODBC32.SQL_C.CHAR, -1, -1, false);

		// Token: 0x04001522 RID: 5410
		internal readonly OdbcType _odbcType;

		// Token: 0x04001523 RID: 5411
		internal readonly DbType _dbType;

		// Token: 0x04001524 RID: 5412
		internal readonly Type _type;

		// Token: 0x04001525 RID: 5413
		internal readonly ODBC32.SQL_TYPE _sql_type;

		// Token: 0x04001526 RID: 5414
		internal readonly ODBC32.SQL_C _sql_c;

		// Token: 0x04001527 RID: 5415
		internal readonly ODBC32.SQL_C _param_sql_c;

		// Token: 0x04001528 RID: 5416
		internal readonly int _bufferSize;

		// Token: 0x04001529 RID: 5417
		internal readonly int _columnSize;

		// Token: 0x0400152A RID: 5418
		internal readonly bool _signType;
	}
}
