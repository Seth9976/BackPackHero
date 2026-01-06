using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000398 RID: 920
	internal class SmiStorageMetaData : SmiExtendedMetaData
	{
		// Token: 0x06002C4B RID: 11339 RVA: 0x000C2194 File Offset: 0x000C0394
		internal SmiStorageMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, bool allowsDBNull, string serverName, string catalogName, string schemaName, string tableName, string columnName, SqlBoolean isKey, bool isIdentity)
			: this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, false, null, null, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3, allowsDBNull, serverName, catalogName, schemaName, tableName, columnName, isKey, isIdentity)
		{
		}

		// Token: 0x06002C4C RID: 11340 RVA: 0x000C21D0 File Offset: 0x000C03D0
		internal SmiStorageMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, bool isMultiValued, IList<SmiExtendedMetaData> fieldMetaData, SmiMetaDataPropertyCollection extendedProperties, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, bool allowsDBNull, string serverName, string catalogName, string schemaName, string tableName, string columnName, SqlBoolean isKey, bool isIdentity)
			: this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, null, isMultiValued, fieldMetaData, extendedProperties, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3, allowsDBNull, serverName, catalogName, schemaName, tableName, columnName, isKey, isIdentity, false)
		{
		}

		// Token: 0x06002C4D RID: 11341 RVA: 0x000C2210 File Offset: 0x000C0410
		internal SmiStorageMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, string udtAssemblyQualifiedName, bool isMultiValued, IList<SmiExtendedMetaData> fieldMetaData, SmiMetaDataPropertyCollection extendedProperties, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, bool allowsDBNull, string serverName, string catalogName, string schemaName, string tableName, string columnName, SqlBoolean isKey, bool isIdentity, bool isColumnSet)
			: base(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, udtAssemblyQualifiedName, isMultiValued, fieldMetaData, extendedProperties, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3)
		{
			this._allowsDBNull = allowsDBNull;
			this._serverName = serverName;
			this._catalogName = catalogName;
			this._schemaName = schemaName;
			this._tableName = tableName;
			this._columnName = columnName;
			this._isKey = isKey;
			this._isIdentity = isIdentity;
			this._isColumnSet = isColumnSet;
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06002C4E RID: 11342 RVA: 0x000C2286 File Offset: 0x000C0486
		internal bool AllowsDBNull
		{
			get
			{
				return this._allowsDBNull;
			}
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06002C4F RID: 11343 RVA: 0x000C228E File Offset: 0x000C048E
		internal string ServerName
		{
			get
			{
				return this._serverName;
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06002C50 RID: 11344 RVA: 0x000C2296 File Offset: 0x000C0496
		internal string CatalogName
		{
			get
			{
				return this._catalogName;
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06002C51 RID: 11345 RVA: 0x000C229E File Offset: 0x000C049E
		internal string SchemaName
		{
			get
			{
				return this._schemaName;
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06002C52 RID: 11346 RVA: 0x000C22A6 File Offset: 0x000C04A6
		internal string TableName
		{
			get
			{
				return this._tableName;
			}
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06002C53 RID: 11347 RVA: 0x000C22AE File Offset: 0x000C04AE
		internal string ColumnName
		{
			get
			{
				return this._columnName;
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06002C54 RID: 11348 RVA: 0x000C22B6 File Offset: 0x000C04B6
		internal SqlBoolean IsKey
		{
			get
			{
				return this._isKey;
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06002C55 RID: 11349 RVA: 0x000C22BE File Offset: 0x000C04BE
		internal bool IsIdentity
		{
			get
			{
				return this._isIdentity;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06002C56 RID: 11350 RVA: 0x000C22C6 File Offset: 0x000C04C6
		internal bool IsColumnSet
		{
			get
			{
				return this._isColumnSet;
			}
		}

		// Token: 0x04001B0F RID: 6927
		private bool _allowsDBNull;

		// Token: 0x04001B10 RID: 6928
		private string _serverName;

		// Token: 0x04001B11 RID: 6929
		private string _catalogName;

		// Token: 0x04001B12 RID: 6930
		private string _schemaName;

		// Token: 0x04001B13 RID: 6931
		private string _tableName;

		// Token: 0x04001B14 RID: 6932
		private string _columnName;

		// Token: 0x04001B15 RID: 6933
		private SqlBoolean _isKey;

		// Token: 0x04001B16 RID: 6934
		private bool _isIdentity;

		// Token: 0x04001B17 RID: 6935
		private bool _isColumnSet;
	}
}
