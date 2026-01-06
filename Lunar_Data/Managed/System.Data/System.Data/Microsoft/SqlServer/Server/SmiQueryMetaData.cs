using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000399 RID: 921
	internal class SmiQueryMetaData : SmiStorageMetaData
	{
		// Token: 0x06002C57 RID: 11351 RVA: 0x000C22D0 File Offset: 0x000C04D0
		internal SmiQueryMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, bool allowsDBNull, string serverName, string catalogName, string schemaName, string tableName, string columnName, SqlBoolean isKey, bool isIdentity, bool isReadOnly, SqlBoolean isExpression, SqlBoolean isAliased, SqlBoolean isHidden)
			: this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, false, null, null, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3, allowsDBNull, serverName, catalogName, schemaName, tableName, columnName, isKey, isIdentity, isReadOnly, isExpression, isAliased, isHidden)
		{
		}

		// Token: 0x06002C58 RID: 11352 RVA: 0x000C2314 File Offset: 0x000C0514
		internal SmiQueryMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, bool isMultiValued, IList<SmiExtendedMetaData> fieldMetaData, SmiMetaDataPropertyCollection extendedProperties, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, bool allowsDBNull, string serverName, string catalogName, string schemaName, string tableName, string columnName, SqlBoolean isKey, bool isIdentity, bool isReadOnly, SqlBoolean isExpression, SqlBoolean isAliased, SqlBoolean isHidden)
			: this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, null, isMultiValued, fieldMetaData, extendedProperties, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3, allowsDBNull, serverName, catalogName, schemaName, tableName, columnName, isKey, isIdentity, false, isReadOnly, isExpression, isAliased, isHidden)
		{
		}

		// Token: 0x06002C59 RID: 11353 RVA: 0x000C235C File Offset: 0x000C055C
		internal SmiQueryMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, string udtAssemblyQualifiedName, bool isMultiValued, IList<SmiExtendedMetaData> fieldMetaData, SmiMetaDataPropertyCollection extendedProperties, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, bool allowsDBNull, string serverName, string catalogName, string schemaName, string tableName, string columnName, SqlBoolean isKey, bool isIdentity, bool isColumnSet, bool isReadOnly, SqlBoolean isExpression, SqlBoolean isAliased, SqlBoolean isHidden)
			: base(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, udtAssemblyQualifiedName, isMultiValued, fieldMetaData, extendedProperties, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3, allowsDBNull, serverName, catalogName, schemaName, tableName, columnName, isKey, isIdentity, isColumnSet)
		{
			this._isReadOnly = isReadOnly;
			this._isExpression = isExpression;
			this._isAliased = isAliased;
			this._isHidden = isHidden;
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06002C5A RID: 11354 RVA: 0x000C23BC File Offset: 0x000C05BC
		internal bool IsReadOnly
		{
			get
			{
				return this._isReadOnly;
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06002C5B RID: 11355 RVA: 0x000C23C4 File Offset: 0x000C05C4
		internal SqlBoolean IsExpression
		{
			get
			{
				return this._isExpression;
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06002C5C RID: 11356 RVA: 0x000C23CC File Offset: 0x000C05CC
		internal SqlBoolean IsAliased
		{
			get
			{
				return this._isAliased;
			}
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06002C5D RID: 11357 RVA: 0x000C23D4 File Offset: 0x000C05D4
		internal SqlBoolean IsHidden
		{
			get
			{
				return this._isHidden;
			}
		}

		// Token: 0x04001B18 RID: 6936
		private bool _isReadOnly;

		// Token: 0x04001B19 RID: 6937
		private SqlBoolean _isExpression;

		// Token: 0x04001B1A RID: 6938
		private SqlBoolean _isAliased;

		// Token: 0x04001B1B RID: 6939
		private SqlBoolean _isHidden;
	}
}
