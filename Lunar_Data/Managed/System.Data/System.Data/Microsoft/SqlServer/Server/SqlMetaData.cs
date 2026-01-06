using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Globalization;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Specifies and retrieves metadata information from parameters and columns of <see cref="T:Microsoft.SqlServer.Server.SqlDataRecord" /> objects. This class cannot be inherited.</summary>
	// Token: 0x020003AB RID: 939
	public sealed class SqlMetaData
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name and type.</summary>
		/// <param name="name">The name of the column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="Name" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">A SqlDbType that is not allowed was passed to the constructor as <paramref name="dbType" />.</exception>
		// Token: 0x06002E2A RID: 11818 RVA: 0x000C7945 File Offset: 0x000C5B45
		public SqlMetaData(string name, SqlDbType dbType)
		{
			this.Construct(name, dbType, false, false, SortOrder.Unspecified, -1);
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, and default server. This form of the constructor supports table-valued parameters by allowing you to specify if the column is unique in the table-valued parameter, the sort order for the column, and the ordinal of the sort column.</summary>
		/// <param name="name">The name of the column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="useServerDefault">Specifes whether this column should use the default server value.</param>
		/// <param name="isUniqueKey">Specifies if the column in the table-valued parameter is unique.</param>
		/// <param name="columnSortOrder">Specifies the sort order for a column.</param>
		/// <param name="sortOrdinal">Specifies the ordinal of the sort column.</param>
		// Token: 0x06002E2B RID: 11819 RVA: 0x000C7959 File Offset: 0x000C5B59
		public SqlMetaData(string name, SqlDbType dbType, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.Construct(name, dbType, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, and maximum length.</summary>
		/// <param name="name">The name of the column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="maxLength">The maximum length of the specified type.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="Name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">A SqlDbType that is not allowed was passed to the constructor as <paramref name="dbType" />.</exception>
		// Token: 0x06002E2C RID: 11820 RVA: 0x000C7970 File Offset: 0x000C5B70
		public SqlMetaData(string name, SqlDbType dbType, long maxLength)
		{
			this.Construct(name, dbType, maxLength, false, false, SortOrder.Unspecified, -1);
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, maximum length, and server default. This form of the constructor supports table-valued parameters by allowing you to specify if the column is unique in the table-valued parameter, the sort order for the column, and the ordinal of the sort column.</summary>
		/// <param name="name">The name of the column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="maxLength">The maximum length of the specified type.</param>
		/// <param name="useServerDefault">Specifes whether this column should use the default server value.</param>
		/// <param name="isUniqueKey">Specifies if the column in the table-valued parameter is unique.</param>
		/// <param name="columnSortOrder">Specifies the sort order for a column.</param>
		/// <param name="sortOrdinal">Specifies the ordinal of the sort column.</param>
		// Token: 0x06002E2D RID: 11821 RVA: 0x000C7985 File Offset: 0x000C5B85
		public SqlMetaData(string name, SqlDbType dbType, long maxLength, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.Construct(name, dbType, maxLength, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, and user-defined type (UDT).</summary>
		/// <param name="name">The name of the column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="userDefinedType">A <see cref="T:System.Type" /> instance that points to the UDT.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="Name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">A SqlDbType that is not allowed was passed to the constructor as <paramref name="dbType" />, or <paramref name="userDefinedType" /> points to a type that does not have <see cref="T:Microsoft.SqlServer.Server.SqlUserDefinedTypeAttribute" /> declared. </exception>
		// Token: 0x06002E2E RID: 11822 RVA: 0x000C79A0 File Offset: 0x000C5BA0
		public SqlMetaData(string name, SqlDbType dbType, Type userDefinedType)
		{
			this.Construct(name, dbType, userDefinedType, null, false, false, SortOrder.Unspecified, -1);
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, user-defined type (UDT), and SQLServer type.</summary>
		/// <param name="name">The name of the column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="userDefinedType">A <see cref="T:System.Type" /> instance that points to the UDT.</param>
		/// <param name="serverTypeName">The SQL Server type name for <paramref name="userDefinedType" />.</param>
		// Token: 0x06002E2F RID: 11823 RVA: 0x000C79C4 File Offset: 0x000C5BC4
		public SqlMetaData(string name, SqlDbType dbType, Type userDefinedType, string serverTypeName)
		{
			this.Construct(name, dbType, userDefinedType, serverTypeName, false, false, SortOrder.Unspecified, -1);
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, user-defined type, SQL Server type, and server default. This form of the constructor supports table-valued parameters by allowing you to specify if the column is unique in the table-valued parameter, the sort order for the column, and the ordinal of the sort column.</summary>
		/// <param name="name">The name of the column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="userDefinedType">A <see cref="T:System.Type" /> instance that points to the UDT.</param>
		/// <param name="serverTypeName">The SQL Server type name for <paramref name="userDefinedType" />.</param>
		/// <param name="useServerDefault">Specifes whether this column should use the default server value.</param>
		/// <param name="isUniqueKey">Specifies if the column in the table-valued parameter is unique.</param>
		/// <param name="columnSortOrder">Specifies the sort order for a column.</param>
		/// <param name="sortOrdinal">Specifies the ordinal of the sort column.</param>
		// Token: 0x06002E30 RID: 11824 RVA: 0x000C79E8 File Offset: 0x000C5BE8
		public SqlMetaData(string name, SqlDbType dbType, Type userDefinedType, string serverTypeName, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.Construct(name, dbType, userDefinedType, serverTypeName, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, precision, and scale.</summary>
		/// <param name="name">The name of the parameter or column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="precision">The precision of the parameter or column.</param>
		/// <param name="scale">The scale of the parameter or column.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="Name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">A SqlDbType that is not allowed was passed to the constructor as <paramref name="dbType" />, or <paramref name="scale" /> was greater than <paramref name="precision" />. </exception>
		// Token: 0x06002E31 RID: 11825 RVA: 0x000C7A10 File Offset: 0x000C5C10
		public SqlMetaData(string name, SqlDbType dbType, byte precision, byte scale)
		{
			this.Construct(name, dbType, precision, scale, false, false, SortOrder.Unspecified, -1);
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, precision, scale, and server default. This form of the constructor supports table-valued parameters by allowing you to specify if the column is unique in the table-valued parameter, the sort order for the column, and the ordinal of the sort column.</summary>
		/// <param name="name">The name of the column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="precision">The precision of the parameter or column.</param>
		/// <param name="scale">The scale of the parameter or column.</param>
		/// <param name="useServerDefault">Specifes whether this column should use the default server value.</param>
		/// <param name="isUniqueKey">Specifies if the column in the table-valued parameter is unique.</param>
		/// <param name="columnSortOrder">Specifies the sort order for a column.</param>
		/// <param name="sortOrdinal">Specifies the ordinal of the sort column.</param>
		// Token: 0x06002E32 RID: 11826 RVA: 0x000C7A34 File Offset: 0x000C5C34
		public SqlMetaData(string name, SqlDbType dbType, byte precision, byte scale, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.Construct(name, dbType, precision, scale, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, maximum length, locale, and compare options.</summary>
		/// <param name="name">The name of the parameter or column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="maxLength">The maximum length of the specified type. </param>
		/// <param name="locale">The locale ID of the parameter or column.</param>
		/// <param name="compareOptions">The comparison rules of the parameter or column.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="Name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">A SqlDbType that is not allowed was passed to the constructor as <paramref name="dbType" />.</exception>
		// Token: 0x06002E33 RID: 11827 RVA: 0x000C7A5C File Offset: 0x000C5C5C
		public SqlMetaData(string name, SqlDbType dbType, long maxLength, long locale, SqlCompareOptions compareOptions)
		{
			this.Construct(name, dbType, maxLength, locale, compareOptions, false, false, SortOrder.Unspecified, -1);
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, maximum length, locale, compare options, and server default. This form of the constructor supports table-valued parameters by allowing you to specify if the column is unique in the table-valued parameter, the sort order for the column, and the ordinal of the sort column.</summary>
		/// <param name="name">The name of the column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="maxLength">The maximum length of the specified type.</param>
		/// <param name="locale">The locale ID of the parameter or column.</param>
		/// <param name="compareOptions">The comparison rules of the parameter or column.</param>
		/// <param name="useServerDefault">Specifes whether this column should use the default server value.</param>
		/// <param name="isUniqueKey">Specifies if the column in the table-valued parameter is unique.</param>
		/// <param name="columnSortOrder">Specifies the sort order for a column.</param>
		/// <param name="sortOrdinal">Specifies the ordinal of the sort column.</param>
		// Token: 0x06002E34 RID: 11828 RVA: 0x000C7A80 File Offset: 0x000C5C80
		public SqlMetaData(string name, SqlDbType dbType, long maxLength, long locale, SqlCompareOptions compareOptions, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.Construct(name, dbType, maxLength, locale, compareOptions, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, database name, owning schema, object name, and default server. This form of the constructor supports table-valued parameters by allowing you to specify if the column is unique in the table-valued parameter, the sort order for the column, and the ordinal of the sort column.</summary>
		/// <param name="name">The name of the column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="database">The database name of the XML schema collection of a typed XML instance.</param>
		/// <param name="owningSchema">The relational schema name of the XML schema collection of a typed XML instance.</param>
		/// <param name="objectName">The name of the XML schema collection of a typed XML instance.</param>
		/// <param name="useServerDefault">Specifes whether this column should use the default server value.</param>
		/// <param name="isUniqueKey">Specifies if the column in the table-valued parameter is unique.</param>
		/// <param name="columnSortOrder">Specifies the sort order for a column.</param>
		/// <param name="sortOrdinal">Specifies the ordinal of the sort column.</param>
		// Token: 0x06002E35 RID: 11829 RVA: 0x000C7AA8 File Offset: 0x000C5CA8
		public SqlMetaData(string name, SqlDbType dbType, string database, string owningSchema, string objectName, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.Construct(name, dbType, database, owningSchema, objectName, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, maximum length, precision, scale, locale ID, compare options, and user-defined type (UDT).</summary>
		/// <param name="name">The name of the column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="maxLength">The maximum length of the specified type.</param>
		/// <param name="precision">The precision of the parameter or column.</param>
		/// <param name="scale">The scale of the parameter or column.</param>
		/// <param name="locale">The locale ID of the parameter or column.</param>
		/// <param name="compareOptions">The comparison rules of the parameter or column.</param>
		/// <param name="userDefinedType">A <see cref="T:System.Type" /> instance that points to the UDT.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="Name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">A SqlDbType that is not allowed was passed to the constructor as <paramref name="dbType" />, or <paramref name="userDefinedType" /> points to a type that does not have <see cref="T:Microsoft.SqlServer.Server.SqlUserDefinedTypeAttribute" /> declared.</exception>
		// Token: 0x06002E36 RID: 11830 RVA: 0x000C7AD0 File Offset: 0x000C5CD0
		public SqlMetaData(string name, SqlDbType dbType, long maxLength, byte precision, byte scale, long locale, SqlCompareOptions compareOptions, Type userDefinedType)
			: this(name, dbType, maxLength, precision, scale, locale, compareOptions, userDefinedType, false, false, SortOrder.Unspecified, -1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, maximum length, precision, scale, locale ID, compare options, and user-defined type (UDT). This form of the constructor supports table-valued parameters by allowing you to specify if the column is unique in the table-valued parameter, the sort order for the column, and the ordinal of the sort column.</summary>
		/// <param name="name">The name of the column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="maxLength">The maximum length of the specified type.</param>
		/// <param name="precision">The precision of the parameter or column.</param>
		/// <param name="scale">The scale of the parameter or column.</param>
		/// <param name="localeId">The locale ID of the parameter or column.</param>
		/// <param name="compareOptions">The comparison rules of the parameter or column.</param>
		/// <param name="userDefinedType">A <see cref="T:System.Type" /> instance that points to the UDT.</param>
		/// <param name="useServerDefault">Specifes whether this column should use the default server value.</param>
		/// <param name="isUniqueKey">Specifies if the column in the table-valued parameter is unique.</param>
		/// <param name="columnSortOrder">Specifies the sort order for a column.</param>
		/// <param name="sortOrdinal">Specifies the ordinal of the sort column.</param>
		// Token: 0x06002E37 RID: 11831 RVA: 0x000C7AF4 File Offset: 0x000C5CF4
		public SqlMetaData(string name, SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			switch (dbType)
			{
			case SqlDbType.BigInt:
			case SqlDbType.Bit:
			case SqlDbType.DateTime:
			case SqlDbType.Float:
			case SqlDbType.Image:
			case SqlDbType.Int:
			case SqlDbType.Money:
			case SqlDbType.Real:
			case SqlDbType.UniqueIdentifier:
			case SqlDbType.SmallDateTime:
			case SqlDbType.SmallInt:
			case SqlDbType.SmallMoney:
			case SqlDbType.Timestamp:
			case SqlDbType.TinyInt:
			case SqlDbType.Xml:
			case SqlDbType.Date:
				this.Construct(name, dbType, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
				return;
			case SqlDbType.Binary:
			case SqlDbType.VarBinary:
				this.Construct(name, dbType, maxLength, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
				return;
			case SqlDbType.Char:
			case SqlDbType.NChar:
			case SqlDbType.NVarChar:
			case SqlDbType.VarChar:
				this.Construct(name, dbType, maxLength, localeId, compareOptions, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
				return;
			case SqlDbType.Decimal:
			case SqlDbType.Time:
			case SqlDbType.DateTime2:
			case SqlDbType.DateTimeOffset:
				this.Construct(name, dbType, precision, scale, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
				return;
			case SqlDbType.NText:
			case SqlDbType.Text:
				this.Construct(name, dbType, SqlMetaData.Max, localeId, compareOptions, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
				return;
			case SqlDbType.Variant:
				this.Construct(name, dbType, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
				return;
			case SqlDbType.Udt:
				this.Construct(name, dbType, userDefinedType, "", useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
				return;
			}
			SQL.InvalidSqlDbTypeForConstructor(dbType);
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, database name, owning schema, and object name.</summary>
		/// <param name="name">The name of the column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="database">The database name of the XML schema collection of a typed XML instance.</param>
		/// <param name="owningSchema">The relational schema name of the XML schema collection of a typed XML instance.</param>
		/// <param name="objectName">The name of the XML schema collection of a typed XML instance.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="Name" /> is null, or <paramref name="objectName" /> is null when <paramref name="database" /> and <paramref name="owningSchema" /> are non-null.</exception>
		/// <exception cref="T:System.ArgumentException">A SqlDbType that is not allowed was passed to the constructor as <paramref name="dbType" />.</exception>
		// Token: 0x06002E38 RID: 11832 RVA: 0x000C7C38 File Offset: 0x000C5E38
		public SqlMetaData(string name, SqlDbType dbType, string database, string owningSchema, string objectName)
		{
			this.Construct(name, dbType, database, owningSchema, objectName, false, false, SortOrder.Unspecified, -1);
		}

		// Token: 0x06002E39 RID: 11833 RVA: 0x000C7C5C File Offset: 0x000C5E5C
		internal SqlMetaData(string name, SqlDbType sqlDBType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, string xmlSchemaCollectionDatabase, string xmlSchemaCollectionOwningSchema, string xmlSchemaCollectionName, bool partialLength, Type udtType)
		{
			this.AssertNameIsValid(name);
			this._strName = name;
			this._sqlDbType = sqlDBType;
			this._lMaxLength = maxLength;
			this._bPrecision = precision;
			this._bScale = scale;
			this._lLocale = localeId;
			this._eCompareOptions = compareOptions;
			this._xmlSchemaCollectionDatabase = xmlSchemaCollectionDatabase;
			this._xmlSchemaCollectionOwningSchema = xmlSchemaCollectionOwningSchema;
			this._xmlSchemaCollectionName = xmlSchemaCollectionName;
			this._bPartialLength = partialLength;
			this._udtType = udtType;
		}

		// Token: 0x06002E3A RID: 11834 RVA: 0x000C7CD4 File Offset: 0x000C5ED4
		private SqlMetaData(string name, SqlDbType sqlDbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, bool partialLength)
		{
			this.AssertNameIsValid(name);
			this._strName = name;
			this._sqlDbType = sqlDbType;
			this._lMaxLength = maxLength;
			this._bPrecision = precision;
			this._bScale = scale;
			this._lLocale = localeId;
			this._eCompareOptions = compareOptions;
			this._bPartialLength = partialLength;
			this._udtType = null;
		}

		/// <summary>Gets the comparison rules used for the column or parameter.</summary>
		/// <returns>The comparison rules used for the column or parameter as a <see cref="T:System.Data.SqlTypes.SqlCompareOptions" />.</returns>
		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06002E3B RID: 11835 RVA: 0x000C7D32 File Offset: 0x000C5F32
		public SqlCompareOptions CompareOptions
		{
			get
			{
				return this._eCompareOptions;
			}
		}

		/// <summary>Gets the data type of the column or parameter.</summary>
		/// <returns>The data type of the column or parameter as a <see cref="T:System.Data.DbType" />.</returns>
		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06002E3C RID: 11836 RVA: 0x000C7D3A File Offset: 0x000C5F3A
		public DbType DbType
		{
			get
			{
				return SqlMetaData.sxm_rgSqlDbTypeToDbType[(int)this._sqlDbType];
			}
		}

		/// <summary>Indicates if the column in the table-valued parameter is unique.</summary>
		/// <returns>A Boolean value.</returns>
		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06002E3D RID: 11837 RVA: 0x000C7D48 File Offset: 0x000C5F48
		public bool IsUniqueKey
		{
			get
			{
				return this._isUniqueKey;
			}
		}

		/// <summary>Gets the locale ID of the column or parameter.</summary>
		/// <returns>The locale ID of the column or parameter as a <see cref="T:System.Int64" />.</returns>
		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06002E3E RID: 11838 RVA: 0x000C7D50 File Offset: 0x000C5F50
		public long LocaleId
		{
			get
			{
				return this._lLocale;
			}
		}

		/// <summary>Gets the length of text, ntext, and image data types. </summary>
		/// <returns>The length of text, ntext, and image data types.</returns>
		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06002E3F RID: 11839 RVA: 0x000C7D58 File Offset: 0x000C5F58
		public static long Max
		{
			get
			{
				return -1L;
			}
		}

		/// <summary>Gets the maximum length of the column or parameter.</summary>
		/// <returns>The maximum length of the column or parameter as a <see cref="T:System.Int64" />.</returns>
		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06002E40 RID: 11840 RVA: 0x000C7D5C File Offset: 0x000C5F5C
		public long MaxLength
		{
			get
			{
				return this._lMaxLength;
			}
		}

		/// <summary>Gets the name of the column or parameter.</summary>
		/// <returns>The name of the column or parameter as a <see cref="T:System.String" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="Name" /> specified in the constructor is longer than 128 characters. </exception>
		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06002E41 RID: 11841 RVA: 0x000C7D64 File Offset: 0x000C5F64
		public string Name
		{
			get
			{
				return this._strName;
			}
		}

		/// <summary>Gets the precision of the column or parameter.</summary>
		/// <returns>The precision of the column or parameter as a <see cref="T:System.Byte" />.</returns>
		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06002E42 RID: 11842 RVA: 0x000C7D6C File Offset: 0x000C5F6C
		public byte Precision
		{
			get
			{
				return this._bPrecision;
			}
		}

		/// <summary>Gets the scale of the column or parameter.</summary>
		/// <returns>The scale of the column or parameter.</returns>
		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06002E43 RID: 11843 RVA: 0x000C7D74 File Offset: 0x000C5F74
		public byte Scale
		{
			get
			{
				return this._bScale;
			}
		}

		/// <summary>Returns the sort order for a column.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SortOrder" /> object.</returns>
		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06002E44 RID: 11844 RVA: 0x000C7D7C File Offset: 0x000C5F7C
		public SortOrder SortOrder
		{
			get
			{
				return this._columnSortOrder;
			}
		}

		/// <summary>Returns the ordinal of the sort column.</summary>
		/// <returns>The ordinal of the sort column.</returns>
		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06002E45 RID: 11845 RVA: 0x000C7D84 File Offset: 0x000C5F84
		public int SortOrdinal
		{
			get
			{
				return this._sortOrdinal;
			}
		}

		/// <summary>Gets the data type of the column or parameter.</summary>
		/// <returns>The data type of the column or parameter as a <see cref="T:System.Data.DbType" />.</returns>
		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06002E46 RID: 11846 RVA: 0x000C7D8C File Offset: 0x000C5F8C
		public SqlDbType SqlDbType
		{
			get
			{
				return this._sqlDbType;
			}
		}

		/// <summary>Gets the common language runtime (CLR) type of a user-defined type (UDT).</summary>
		/// <returns>The CLR type name of a user-defined type as a <see cref="T:System.Type" />.</returns>
		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06002E47 RID: 11847 RVA: 0x000C7D94 File Offset: 0x000C5F94
		public Type Type
		{
			get
			{
				return this._udtType;
			}
		}

		/// <summary>Gets the three-part name of the user-defined type (UDT) or the SQL Server type represented by the instance.</summary>
		/// <returns>The name of the UDT or SQL Server type as a <see cref="T:System.String" />.</returns>
		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06002E48 RID: 11848 RVA: 0x000C7D9C File Offset: 0x000C5F9C
		public string TypeName
		{
			get
			{
				if (this._serverTypeName != null)
				{
					return this._serverTypeName;
				}
				if (this.SqlDbType == SqlDbType.Udt)
				{
					return this.UdtTypeName;
				}
				return SqlMetaData.sxm_rgDefaults[(int)this.SqlDbType].Name;
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06002E49 RID: 11849 RVA: 0x000C7DCF File Offset: 0x000C5FCF
		internal string ServerTypeName
		{
			get
			{
				return this._serverTypeName;
			}
		}

		/// <summary>Reports whether this column should use the default server value.</summary>
		/// <returns>A Boolean value.</returns>
		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06002E4A RID: 11850 RVA: 0x000C7DD7 File Offset: 0x000C5FD7
		public bool UseServerDefault
		{
			get
			{
				return this._useServerDefault;
			}
		}

		/// <summary>Gets the name of the database where the schema collection for this XML instance is located.</summary>
		/// <returns>The name of the database where the schema collection for this XML instance is located as a <see cref="T:System.String" />.</returns>
		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06002E4B RID: 11851 RVA: 0x000C7DDF File Offset: 0x000C5FDF
		public string XmlSchemaCollectionDatabase
		{
			get
			{
				return this._xmlSchemaCollectionDatabase;
			}
		}

		/// <summary>Gets the name of the schema collection for this XML instance.</summary>
		/// <returns>The name of the schema collection for this XML instance as a <see cref="T:System.String" />.</returns>
		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06002E4C RID: 11852 RVA: 0x000C7DE7 File Offset: 0x000C5FE7
		public string XmlSchemaCollectionName
		{
			get
			{
				return this._xmlSchemaCollectionName;
			}
		}

		/// <summary>Gets the owning relational schema where the schema collection for this XML instance is located.</summary>
		/// <returns>The owning relational schema where the schema collection for this XML instance is located as a <see cref="T:System.String" />.</returns>
		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06002E4D RID: 11853 RVA: 0x000C7DEF File Offset: 0x000C5FEF
		public string XmlSchemaCollectionOwningSchema
		{
			get
			{
				return this._xmlSchemaCollectionOwningSchema;
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06002E4E RID: 11854 RVA: 0x000C7DF7 File Offset: 0x000C5FF7
		internal bool IsPartialLength
		{
			get
			{
				return this._bPartialLength;
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06002E4F RID: 11855 RVA: 0x000C7DFF File Offset: 0x000C5FFF
		internal string UdtTypeName
		{
			get
			{
				if (this.SqlDbType != SqlDbType.Udt)
				{
					return null;
				}
				if (this._udtType == null)
				{
					return null;
				}
				return this._udtType.FullName;
			}
		}

		// Token: 0x06002E50 RID: 11856 RVA: 0x000C7E28 File Offset: 0x000C6028
		private void Construct(string name, SqlDbType dbType, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.AssertNameIsValid(name);
			this.ValidateSortOrder(columnSortOrder, sortOrdinal);
			if (dbType != SqlDbType.BigInt && SqlDbType.Bit != dbType && SqlDbType.DateTime != dbType && SqlDbType.Date != dbType && SqlDbType.DateTime2 != dbType && SqlDbType.DateTimeOffset != dbType && SqlDbType.Decimal != dbType && SqlDbType.Float != dbType && SqlDbType.Image != dbType && SqlDbType.Int != dbType && SqlDbType.Money != dbType && SqlDbType.NText != dbType && SqlDbType.Real != dbType && SqlDbType.SmallDateTime != dbType && SqlDbType.SmallInt != dbType && SqlDbType.SmallMoney != dbType && SqlDbType.Text != dbType && SqlDbType.Time != dbType && SqlDbType.Timestamp != dbType && SqlDbType.TinyInt != dbType && SqlDbType.UniqueIdentifier != dbType && SqlDbType.Variant != dbType && SqlDbType.Xml != dbType)
			{
				throw SQL.InvalidSqlDbTypeForConstructor(dbType);
			}
			this.SetDefaultsForType(dbType);
			if (SqlDbType.NText == dbType || SqlDbType.Text == dbType)
			{
				this._lLocale = (long)CultureInfo.CurrentCulture.LCID;
			}
			this._strName = name;
			this._useServerDefault = useServerDefault;
			this._isUniqueKey = isUniqueKey;
			this._columnSortOrder = columnSortOrder;
			this._sortOrdinal = sortOrdinal;
		}

		// Token: 0x06002E51 RID: 11857 RVA: 0x000C7F00 File Offset: 0x000C6100
		private void Construct(string name, SqlDbType dbType, long maxLength, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.AssertNameIsValid(name);
			this.ValidateSortOrder(columnSortOrder, sortOrdinal);
			long num = 0L;
			if (SqlDbType.Char == dbType)
			{
				if (maxLength > 8000L || maxLength < 0L)
				{
					throw ADP.Argument(SR.GetString("Specified length '{0}' is out of range.", new object[] { maxLength.ToString(CultureInfo.InvariantCulture) }), "maxLength");
				}
				num = (long)CultureInfo.CurrentCulture.LCID;
			}
			else if (SqlDbType.VarChar == dbType)
			{
				if ((maxLength > 8000L || maxLength < 0L) && maxLength != SqlMetaData.Max)
				{
					throw ADP.Argument(SR.GetString("Specified length '{0}' is out of range.", new object[] { maxLength.ToString(CultureInfo.InvariantCulture) }), "maxLength");
				}
				num = (long)CultureInfo.CurrentCulture.LCID;
			}
			else if (SqlDbType.NChar == dbType)
			{
				if (maxLength > 4000L || maxLength < 0L)
				{
					throw ADP.Argument(SR.GetString("Specified length '{0}' is out of range.", new object[] { maxLength.ToString(CultureInfo.InvariantCulture) }), "maxLength");
				}
				num = (long)CultureInfo.CurrentCulture.LCID;
			}
			else if (SqlDbType.NVarChar == dbType)
			{
				if ((maxLength > 4000L || maxLength < 0L) && maxLength != SqlMetaData.Max)
				{
					throw ADP.Argument(SR.GetString("Specified length '{0}' is out of range.", new object[] { maxLength.ToString(CultureInfo.InvariantCulture) }), "maxLength");
				}
				num = (long)CultureInfo.CurrentCulture.LCID;
			}
			else if (SqlDbType.NText == dbType || SqlDbType.Text == dbType)
			{
				if (SqlMetaData.Max != maxLength)
				{
					throw ADP.Argument(SR.GetString("Specified length '{0}' is out of range.", new object[] { maxLength.ToString(CultureInfo.InvariantCulture) }), "maxLength");
				}
				num = (long)CultureInfo.CurrentCulture.LCID;
			}
			else if (SqlDbType.Binary == dbType)
			{
				if (maxLength > 8000L || maxLength < 0L)
				{
					throw ADP.Argument(SR.GetString("Specified length '{0}' is out of range.", new object[] { maxLength.ToString(CultureInfo.InvariantCulture) }), "maxLength");
				}
			}
			else if (SqlDbType.VarBinary == dbType)
			{
				if ((maxLength > 8000L || maxLength < 0L) && maxLength != SqlMetaData.Max)
				{
					throw ADP.Argument(SR.GetString("Specified length '{0}' is out of range.", new object[] { maxLength.ToString(CultureInfo.InvariantCulture) }), "maxLength");
				}
			}
			else
			{
				if (SqlDbType.Image != dbType)
				{
					throw SQL.InvalidSqlDbTypeForConstructor(dbType);
				}
				if (SqlMetaData.Max != maxLength)
				{
					throw ADP.Argument(SR.GetString("Specified length '{0}' is out of range.", new object[] { maxLength.ToString(CultureInfo.InvariantCulture) }), "maxLength");
				}
			}
			this.SetDefaultsForType(dbType);
			this._strName = name;
			this._lMaxLength = maxLength;
			this._lLocale = num;
			this._useServerDefault = useServerDefault;
			this._isUniqueKey = isUniqueKey;
			this._columnSortOrder = columnSortOrder;
			this._sortOrdinal = sortOrdinal;
		}

		// Token: 0x06002E52 RID: 11858 RVA: 0x000C81B4 File Offset: 0x000C63B4
		private void Construct(string name, SqlDbType dbType, long maxLength, long locale, SqlCompareOptions compareOptions, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.AssertNameIsValid(name);
			this.ValidateSortOrder(columnSortOrder, sortOrdinal);
			if (SqlDbType.Char == dbType)
			{
				if (maxLength > 8000L || maxLength < 0L)
				{
					throw ADP.Argument(SR.GetString("Specified length '{0}' is out of range.", new object[] { maxLength.ToString(CultureInfo.InvariantCulture) }), "maxLength");
				}
			}
			else if (SqlDbType.VarChar == dbType)
			{
				if ((maxLength > 8000L || maxLength < 0L) && maxLength != SqlMetaData.Max)
				{
					throw ADP.Argument(SR.GetString("Specified length '{0}' is out of range.", new object[] { maxLength.ToString(CultureInfo.InvariantCulture) }), "maxLength");
				}
			}
			else if (SqlDbType.NChar == dbType)
			{
				if (maxLength > 4000L || maxLength < 0L)
				{
					throw ADP.Argument(SR.GetString("Specified length '{0}' is out of range.", new object[] { maxLength.ToString(CultureInfo.InvariantCulture) }), "maxLength");
				}
			}
			else if (SqlDbType.NVarChar == dbType)
			{
				if ((maxLength > 4000L || maxLength < 0L) && maxLength != SqlMetaData.Max)
				{
					throw ADP.Argument(SR.GetString("Specified length '{0}' is out of range.", new object[] { maxLength.ToString(CultureInfo.InvariantCulture) }), "maxLength");
				}
			}
			else
			{
				if (SqlDbType.NText != dbType && SqlDbType.Text != dbType)
				{
					throw SQL.InvalidSqlDbTypeForConstructor(dbType);
				}
				if (SqlMetaData.Max != maxLength)
				{
					throw ADP.Argument(SR.GetString("Specified length '{0}' is out of range.", new object[] { maxLength.ToString(CultureInfo.InvariantCulture) }), "maxLength");
				}
			}
			if (SqlCompareOptions.BinarySort != compareOptions && (~(SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreNonSpace | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth) & compareOptions) != SqlCompareOptions.None)
			{
				throw ADP.InvalidEnumerationValue(typeof(SqlCompareOptions), (int)compareOptions);
			}
			this.SetDefaultsForType(dbType);
			this._strName = name;
			this._lMaxLength = maxLength;
			this._lLocale = locale;
			this._eCompareOptions = compareOptions;
			this._useServerDefault = useServerDefault;
			this._isUniqueKey = isUniqueKey;
			this._columnSortOrder = columnSortOrder;
			this._sortOrdinal = sortOrdinal;
		}

		// Token: 0x06002E53 RID: 11859 RVA: 0x000C838C File Offset: 0x000C658C
		private void Construct(string name, SqlDbType dbType, byte precision, byte scale, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.AssertNameIsValid(name);
			this.ValidateSortOrder(columnSortOrder, sortOrdinal);
			if (SqlDbType.Decimal == dbType)
			{
				if (precision > SqlDecimal.MaxPrecision || scale > precision)
				{
					throw SQL.PrecisionValueOutOfRange(precision);
				}
				if (scale > SqlDecimal.MaxScale)
				{
					throw SQL.ScaleValueOutOfRange(scale);
				}
			}
			else
			{
				if (SqlDbType.Time != dbType && SqlDbType.DateTime2 != dbType && SqlDbType.DateTimeOffset != dbType)
				{
					throw SQL.InvalidSqlDbTypeForConstructor(dbType);
				}
				if (scale > 7)
				{
					throw SQL.TimeScaleValueOutOfRange(scale);
				}
			}
			this.SetDefaultsForType(dbType);
			this._strName = name;
			this._bPrecision = precision;
			this._bScale = scale;
			if (SqlDbType.Decimal == dbType)
			{
				this._lMaxLength = (long)((ulong)SqlMetaData.s_maxLenFromPrecision[(int)(precision - 1)]);
			}
			else
			{
				this._lMaxLength -= (long)((ulong)SqlMetaData.s_maxVarTimeLenOffsetFromScale[(int)scale]);
			}
			this._useServerDefault = useServerDefault;
			this._isUniqueKey = isUniqueKey;
			this._columnSortOrder = columnSortOrder;
			this._sortOrdinal = sortOrdinal;
		}

		// Token: 0x06002E54 RID: 11860 RVA: 0x000C8460 File Offset: 0x000C6660
		private void Construct(string name, SqlDbType dbType, Type userDefinedType, string serverTypeName, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.AssertNameIsValid(name);
			this.ValidateSortOrder(columnSortOrder, sortOrdinal);
			if (SqlDbType.Udt != dbType)
			{
				throw SQL.InvalidSqlDbTypeForConstructor(dbType);
			}
			if (null == userDefinedType)
			{
				throw ADP.ArgumentNull("userDefinedType");
			}
			this.SetDefaultsForType(SqlDbType.Udt);
			this._strName = name;
			this._lMaxLength = (long)SerializationHelperSql9.GetUdtMaxLength(userDefinedType);
			this._udtType = userDefinedType;
			this._serverTypeName = serverTypeName;
			this._useServerDefault = useServerDefault;
			this._isUniqueKey = isUniqueKey;
			this._columnSortOrder = columnSortOrder;
			this._sortOrdinal = sortOrdinal;
		}

		// Token: 0x06002E55 RID: 11861 RVA: 0x000C84EC File Offset: 0x000C66EC
		private void Construct(string name, SqlDbType dbType, string database, string owningSchema, string objectName, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.AssertNameIsValid(name);
			this.ValidateSortOrder(columnSortOrder, sortOrdinal);
			if (SqlDbType.Xml != dbType)
			{
				throw SQL.InvalidSqlDbTypeForConstructor(dbType);
			}
			if ((database != null || owningSchema != null) && objectName == null)
			{
				throw ADP.ArgumentNull("objectName");
			}
			this.SetDefaultsForType(SqlDbType.Xml);
			this._strName = name;
			this._xmlSchemaCollectionDatabase = database;
			this._xmlSchemaCollectionOwningSchema = owningSchema;
			this._xmlSchemaCollectionName = objectName;
			this._useServerDefault = useServerDefault;
			this._isUniqueKey = isUniqueKey;
			this._columnSortOrder = columnSortOrder;
			this._sortOrdinal = sortOrdinal;
		}

		// Token: 0x06002E56 RID: 11862 RVA: 0x000C8572 File Offset: 0x000C6772
		private void AssertNameIsValid(string name)
		{
			if (name == null)
			{
				throw ADP.ArgumentNull("name");
			}
			if (128L < (long)name.Length)
			{
				throw SQL.NameTooLong("name");
			}
		}

		// Token: 0x06002E57 RID: 11863 RVA: 0x000C859C File Offset: 0x000C679C
		private void ValidateSortOrder(SortOrder columnSortOrder, int sortOrdinal)
		{
			if (SortOrder.Unspecified != columnSortOrder && columnSortOrder != SortOrder.Ascending && SortOrder.Descending != columnSortOrder)
			{
				throw SQL.InvalidSortOrder(columnSortOrder);
			}
			if (SortOrder.Unspecified == columnSortOrder != (-1 == sortOrdinal))
			{
				throw SQL.MustSpecifyBothSortOrderAndOrdinal(columnSortOrder, sortOrdinal);
			}
		}

		/// <summary>Validates the specified <see cref="T:System.Int16" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Int16" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002E58 RID: 11864 RVA: 0x000C85C2 File Offset: 0x000C67C2
		public short Adjust(short value)
		{
			if (SqlDbType.SmallInt != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Int32" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Int32" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002E59 RID: 11865 RVA: 0x000C85D4 File Offset: 0x000C67D4
		public int Adjust(int value)
		{
			if (SqlDbType.Int != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Int64" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Int64" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002E5A RID: 11866 RVA: 0x000C85E5 File Offset: 0x000C67E5
		public long Adjust(long value)
		{
			if (this.SqlDbType != SqlDbType.BigInt)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Single" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Single" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002E5B RID: 11867 RVA: 0x000C85F5 File Offset: 0x000C67F5
		public float Adjust(float value)
		{
			if (SqlDbType.Real != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Double" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Double" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002E5C RID: 11868 RVA: 0x000C8607 File Offset: 0x000C6807
		public double Adjust(double value)
		{
			if (SqlDbType.Float != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.String" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.String" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002E5D RID: 11869 RVA: 0x000C8618 File Offset: 0x000C6818
		public string Adjust(string value)
		{
			if (SqlDbType.Char == this.SqlDbType || SqlDbType.NChar == this.SqlDbType)
			{
				if (value != null && (long)value.Length < this.MaxLength)
				{
					value = value.PadRight((int)this.MaxLength);
				}
			}
			else if (SqlDbType.VarChar != this.SqlDbType && SqlDbType.NVarChar != this.SqlDbType && SqlDbType.Text != this.SqlDbType && SqlDbType.NText != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (value == null)
			{
				return null;
			}
			if ((long)value.Length > this.MaxLength && SqlMetaData.Max != this.MaxLength)
			{
				value = value.Remove((int)this.MaxLength, (int)((long)value.Length - this.MaxLength));
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Decimal" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Decimal" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002E5E RID: 11870 RVA: 0x000C86C8 File Offset: 0x000C68C8
		public decimal Adjust(decimal value)
		{
			if (SqlDbType.Decimal != this.SqlDbType && SqlDbType.Money != this.SqlDbType && SqlDbType.SmallMoney != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (SqlDbType.Decimal != this.SqlDbType)
			{
				this.VerifyMoneyRange(new SqlMoney(value));
				return value;
			}
			return this.InternalAdjustSqlDecimal(new SqlDecimal(value)).Value;
		}

		/// <summary>Validates the specified <see cref="T:System.DateTime" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.DateTime" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002E5F RID: 11871 RVA: 0x000C8724 File Offset: 0x000C6924
		public DateTime Adjust(DateTime value)
		{
			if (SqlDbType.DateTime == this.SqlDbType || SqlDbType.SmallDateTime == this.SqlDbType)
			{
				this.VerifyDateTimeRange(value);
			}
			else
			{
				if (SqlDbType.DateTime2 == this.SqlDbType)
				{
					return new DateTime(this.InternalAdjustTimeTicks(value.Ticks));
				}
				if (SqlDbType.Date == this.SqlDbType)
				{
					return value.Date;
				}
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Guid" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Guid" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002E60 RID: 11872 RVA: 0x000C8782 File Offset: 0x000C6982
		public Guid Adjust(Guid value)
		{
			if (SqlDbType.UniqueIdentifier != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlBoolean" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002E61 RID: 11873 RVA: 0x000C8794 File Offset: 0x000C6994
		public SqlBoolean Adjust(SqlBoolean value)
		{
			if (SqlDbType.Bit != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlByte" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlByte" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002E62 RID: 11874 RVA: 0x000C87A5 File Offset: 0x000C69A5
		public SqlByte Adjust(SqlByte value)
		{
			if (SqlDbType.TinyInt != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlInt16" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlInt16" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002E63 RID: 11875 RVA: 0x000C85C2 File Offset: 0x000C67C2
		public SqlInt16 Adjust(SqlInt16 value)
		{
			if (SqlDbType.SmallInt != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlInt32" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlInt32" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002E64 RID: 11876 RVA: 0x000C85D4 File Offset: 0x000C67D4
		public SqlInt32 Adjust(SqlInt32 value)
		{
			if (SqlDbType.Int != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlInt64" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlInt64" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002E65 RID: 11877 RVA: 0x000C85E5 File Offset: 0x000C67E5
		public SqlInt64 Adjust(SqlInt64 value)
		{
			if (this.SqlDbType != SqlDbType.BigInt)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlSingle" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlSingle" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002E66 RID: 11878 RVA: 0x000C85F5 File Offset: 0x000C67F5
		public SqlSingle Adjust(SqlSingle value)
		{
			if (SqlDbType.Real != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlDouble" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlDouble" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002E67 RID: 11879 RVA: 0x000C8607 File Offset: 0x000C6807
		public SqlDouble Adjust(SqlDouble value)
		{
			if (SqlDbType.Float != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlMoney" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlMoney" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002E68 RID: 11880 RVA: 0x000C87B7 File Offset: 0x000C69B7
		public SqlMoney Adjust(SqlMoney value)
		{
			if (SqlDbType.Money != this.SqlDbType && SqlDbType.SmallMoney != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (!value.IsNull)
			{
				this.VerifyMoneyRange(value);
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlDateTime" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlDateTime" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002E69 RID: 11881 RVA: 0x000C87E3 File Offset: 0x000C69E3
		public SqlDateTime Adjust(SqlDateTime value)
		{
			if (SqlDbType.DateTime != this.SqlDbType && SqlDbType.SmallDateTime != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (!value.IsNull)
			{
				this.VerifyDateTimeRange(value.Value);
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlDecimal" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002E6A RID: 11882 RVA: 0x000C8814 File Offset: 0x000C6A14
		public SqlDecimal Adjust(SqlDecimal value)
		{
			if (SqlDbType.Decimal != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return this.InternalAdjustSqlDecimal(value);
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlString" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlString" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002E6B RID: 11883 RVA: 0x000C882C File Offset: 0x000C6A2C
		public SqlString Adjust(SqlString value)
		{
			if (SqlDbType.Char == this.SqlDbType || SqlDbType.NChar == this.SqlDbType)
			{
				if (!value.IsNull && (long)value.Value.Length < this.MaxLength)
				{
					return new SqlString(value.Value.PadRight((int)this.MaxLength));
				}
			}
			else if (SqlDbType.VarChar != this.SqlDbType && SqlDbType.NVarChar != this.SqlDbType && SqlDbType.Text != this.SqlDbType && SqlDbType.NText != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (value.IsNull)
			{
				return value;
			}
			if ((long)value.Value.Length > this.MaxLength && SqlMetaData.Max != this.MaxLength)
			{
				value = new SqlString(value.Value.Remove((int)this.MaxLength, (int)((long)value.Value.Length - this.MaxLength)));
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlBinary" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlBinary" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002E6C RID: 11884 RVA: 0x000C8910 File Offset: 0x000C6B10
		public SqlBinary Adjust(SqlBinary value)
		{
			if (SqlDbType.Binary == this.SqlDbType || SqlDbType.Timestamp == this.SqlDbType)
			{
				if (!value.IsNull && (long)value.Length < this.MaxLength)
				{
					byte[] value2 = value.Value;
					byte[] array = new byte[this.MaxLength];
					Buffer.BlockCopy(value2, 0, array, 0, value2.Length);
					Array.Clear(array, value2.Length, array.Length - value2.Length);
					return new SqlBinary(array);
				}
			}
			else if (SqlDbType.VarBinary != this.SqlDbType && SqlDbType.Image != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (value.IsNull)
			{
				return value;
			}
			if ((long)value.Length > this.MaxLength && SqlMetaData.Max != this.MaxLength)
			{
				Array value3 = value.Value;
				byte[] array2 = new byte[this.MaxLength];
				Buffer.BlockCopy(value3, 0, array2, 0, (int)this.MaxLength);
				value = new SqlBinary(array2);
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlGuid" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlGuid" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002E6D RID: 11885 RVA: 0x000C8782 File Offset: 0x000C6982
		public SqlGuid Adjust(SqlGuid value)
		{
			if (SqlDbType.UniqueIdentifier != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlChars" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlChars" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002E6E RID: 11886 RVA: 0x000C89F0 File Offset: 0x000C6BF0
		public SqlChars Adjust(SqlChars value)
		{
			if (SqlDbType.Char == this.SqlDbType || SqlDbType.NChar == this.SqlDbType)
			{
				if (value != null && !value.IsNull)
				{
					long length = value.Length;
					if (length < this.MaxLength)
					{
						if (value.MaxLength < this.MaxLength)
						{
							char[] array = new char[(int)this.MaxLength];
							Buffer.BlockCopy(value.Buffer, 0, array, 0, (int)length);
							value = new SqlChars(array);
						}
						char[] buffer = value.Buffer;
						for (long num = length; num < this.MaxLength; num += 1L)
						{
							buffer[(int)(checked((IntPtr)num))] = ' ';
						}
						value.SetLength(this.MaxLength);
						return value;
					}
				}
			}
			else if (SqlDbType.VarChar != this.SqlDbType && SqlDbType.NVarChar != this.SqlDbType && SqlDbType.Text != this.SqlDbType && SqlDbType.NText != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (value == null || value.IsNull)
			{
				return value;
			}
			if (value.Length > this.MaxLength && SqlMetaData.Max != this.MaxLength)
			{
				value.SetLength(this.MaxLength);
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlBytes" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlBytes" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002E6F RID: 11887 RVA: 0x000C8AFC File Offset: 0x000C6CFC
		public SqlBytes Adjust(SqlBytes value)
		{
			if (SqlDbType.Binary == this.SqlDbType || SqlDbType.Timestamp == this.SqlDbType)
			{
				if (value != null && !value.IsNull)
				{
					int num = (int)value.Length;
					if ((long)num < this.MaxLength)
					{
						if (value.MaxLength < this.MaxLength)
						{
							byte[] array = new byte[this.MaxLength];
							Buffer.BlockCopy(value.Buffer, 0, array, 0, num);
							value = new SqlBytes(array);
						}
						byte[] buffer = value.Buffer;
						Array.Clear(buffer, num, buffer.Length - num);
						value.SetLength(this.MaxLength);
						return value;
					}
				}
			}
			else if (SqlDbType.VarBinary != this.SqlDbType && SqlDbType.Image != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (value == null || value.IsNull)
			{
				return value;
			}
			if (value.Length > this.MaxLength && SqlMetaData.Max != this.MaxLength)
			{
				value.SetLength(this.MaxLength);
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlXml" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlXml" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002E70 RID: 11888 RVA: 0x000C8BDC File Offset: 0x000C6DDC
		public SqlXml Adjust(SqlXml value)
		{
			if (SqlDbType.Xml != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.TimeSpan" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as an array of <see cref="T:System.TimeSpan" /> values.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002E71 RID: 11889 RVA: 0x000C8BEE File Offset: 0x000C6DEE
		public TimeSpan Adjust(TimeSpan value)
		{
			if (SqlDbType.Time != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			this.VerifyTimeRange(value);
			return new TimeSpan(this.InternalAdjustTimeTicks(value.Ticks));
		}

		/// <summary>Validates the specified <see cref="T:System.DateTimeOffset" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as an array of <see cref="T:System.DateTimeOffset" /> values.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002E72 RID: 11890 RVA: 0x000C8C18 File Offset: 0x000C6E18
		public DateTimeOffset Adjust(DateTimeOffset value)
		{
			if (SqlDbType.DateTimeOffset != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return new DateTimeOffset(this.InternalAdjustTimeTicks(value.Ticks), value.Offset);
		}

		/// <summary>Validates the specified <see cref="T:System.Object" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Object" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002E73 RID: 11891 RVA: 0x000C8C44 File Offset: 0x000C6E44
		public object Adjust(object value)
		{
			if (value == null)
			{
				return null;
			}
			Type type = value.GetType();
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Empty:
				throw ADP.InvalidDataType(TypeCode.Empty);
			case TypeCode.Object:
				if (type == typeof(byte[]))
				{
					return this.Adjust((byte[])value);
				}
				if (type == typeof(char[]))
				{
					return this.Adjust((char[])value);
				}
				if (type == typeof(Guid))
				{
					return this.Adjust((Guid)value);
				}
				if (type == typeof(object))
				{
					throw ADP.InvalidDataType(TypeCode.UInt64);
				}
				if (type == typeof(SqlBinary))
				{
					return this.Adjust((SqlBinary)value);
				}
				if (type == typeof(SqlBoolean))
				{
					return this.Adjust((SqlBoolean)value);
				}
				if (type == typeof(SqlByte))
				{
					return this.Adjust((SqlByte)value);
				}
				if (type == typeof(SqlDateTime))
				{
					return this.Adjust((SqlDateTime)value);
				}
				if (type == typeof(SqlDouble))
				{
					return this.Adjust((SqlDouble)value);
				}
				if (type == typeof(SqlGuid))
				{
					return this.Adjust((SqlGuid)value);
				}
				if (type == typeof(SqlInt16))
				{
					return this.Adjust((SqlInt16)value);
				}
				if (type == typeof(SqlInt32))
				{
					return this.Adjust((SqlInt32)value);
				}
				if (type == typeof(SqlInt64))
				{
					return this.Adjust((SqlInt64)value);
				}
				if (type == typeof(SqlMoney))
				{
					return this.Adjust((SqlMoney)value);
				}
				if (type == typeof(SqlDecimal))
				{
					return this.Adjust((SqlDecimal)value);
				}
				if (type == typeof(SqlSingle))
				{
					return this.Adjust((SqlSingle)value);
				}
				if (type == typeof(SqlString))
				{
					return this.Adjust((SqlString)value);
				}
				if (type == typeof(SqlChars))
				{
					return this.Adjust((SqlChars)value);
				}
				if (type == typeof(SqlBytes))
				{
					return this.Adjust((SqlBytes)value);
				}
				if (type == typeof(SqlXml))
				{
					return this.Adjust((SqlXml)value);
				}
				if (type == typeof(TimeSpan))
				{
					return this.Adjust((TimeSpan)value);
				}
				if (type == typeof(DateTimeOffset))
				{
					return this.Adjust((DateTimeOffset)value);
				}
				throw ADP.UnknownDataType(type);
			case TypeCode.DBNull:
				return value;
			case TypeCode.Boolean:
				return this.Adjust((bool)value);
			case TypeCode.Char:
				return this.Adjust((char)value);
			case TypeCode.SByte:
				throw ADP.InvalidDataType(TypeCode.SByte);
			case TypeCode.Byte:
				return this.Adjust((byte)value);
			case TypeCode.Int16:
				return this.Adjust((short)value);
			case TypeCode.UInt16:
				throw ADP.InvalidDataType(TypeCode.UInt16);
			case TypeCode.Int32:
				return this.Adjust((int)value);
			case TypeCode.UInt32:
				throw ADP.InvalidDataType(TypeCode.UInt32);
			case TypeCode.Int64:
				return this.Adjust((long)value);
			case TypeCode.UInt64:
				throw ADP.InvalidDataType(TypeCode.UInt64);
			case TypeCode.Single:
				return this.Adjust((float)value);
			case TypeCode.Double:
				return this.Adjust((double)value);
			case TypeCode.Decimal:
				return this.Adjust((decimal)value);
			case TypeCode.DateTime:
				return this.Adjust((DateTime)value);
			case TypeCode.String:
				return this.Adjust((string)value);
			}
			throw ADP.UnknownDataTypeCode(type, Type.GetTypeCode(type));
		}

		/// <summary>Infers the metadata from the specified object and returns it as a <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</summary>
		/// <returns>The inferred metadata as a <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</returns>
		/// <param name="value">The object used from which the metadata is inferred.</param>
		/// <param name="name">The name assigned to the returned <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentNullException">The v<paramref name="alue" /> is null. </exception>
		// Token: 0x06002E74 RID: 11892 RVA: 0x000C9164 File Offset: 0x000C7364
		public static SqlMetaData InferFromValue(object value, string name)
		{
			if (value == null)
			{
				throw ADP.ArgumentNull("value");
			}
			Type type = value.GetType();
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Empty:
				throw ADP.InvalidDataType(TypeCode.Empty);
			case TypeCode.Object:
				if (type == typeof(byte[]))
				{
					long num = (long)((byte[])value).Length;
					if (num < 1L)
					{
						num = 1L;
					}
					if (8000L < num)
					{
						num = SqlMetaData.Max;
					}
					return new SqlMetaData(name, SqlDbType.VarBinary, num);
				}
				if (type == typeof(char[]))
				{
					long num2 = (long)((char[])value).Length;
					if (num2 < 1L)
					{
						num2 = 1L;
					}
					if (4000L < num2)
					{
						num2 = SqlMetaData.Max;
					}
					return new SqlMetaData(name, SqlDbType.NVarChar, num2);
				}
				if (type == typeof(Guid))
				{
					return new SqlMetaData(name, SqlDbType.UniqueIdentifier);
				}
				if (type == typeof(object))
				{
					return new SqlMetaData(name, SqlDbType.Variant);
				}
				if (type == typeof(SqlBinary))
				{
					SqlBinary sqlBinary = (SqlBinary)value;
					long num3;
					if (!sqlBinary.IsNull)
					{
						num3 = (long)sqlBinary.Length;
						if (num3 < 1L)
						{
							num3 = 1L;
						}
						if (8000L < num3)
						{
							num3 = SqlMetaData.Max;
						}
					}
					else
					{
						num3 = SqlMetaData.sxm_rgDefaults[21].MaxLength;
					}
					return new SqlMetaData(name, SqlDbType.VarBinary, num3);
				}
				if (type == typeof(SqlBoolean))
				{
					return new SqlMetaData(name, SqlDbType.Bit);
				}
				if (type == typeof(SqlByte))
				{
					return new SqlMetaData(name, SqlDbType.TinyInt);
				}
				if (type == typeof(SqlDateTime))
				{
					return new SqlMetaData(name, SqlDbType.DateTime);
				}
				if (type == typeof(SqlDouble))
				{
					return new SqlMetaData(name, SqlDbType.Float);
				}
				if (type == typeof(SqlGuid))
				{
					return new SqlMetaData(name, SqlDbType.UniqueIdentifier);
				}
				if (type == typeof(SqlInt16))
				{
					return new SqlMetaData(name, SqlDbType.SmallInt);
				}
				if (type == typeof(SqlInt32))
				{
					return new SqlMetaData(name, SqlDbType.Int);
				}
				if (type == typeof(SqlInt64))
				{
					return new SqlMetaData(name, SqlDbType.BigInt);
				}
				if (type == typeof(SqlMoney))
				{
					return new SqlMetaData(name, SqlDbType.Money);
				}
				if (type == typeof(SqlDecimal))
				{
					SqlDecimal sqlDecimal = (SqlDecimal)value;
					byte b;
					byte b2;
					if (!sqlDecimal.IsNull)
					{
						b = sqlDecimal.Precision;
						b2 = sqlDecimal.Scale;
					}
					else
					{
						b = SqlMetaData.sxm_rgDefaults[5].Precision;
						b2 = SqlMetaData.sxm_rgDefaults[5].Scale;
					}
					return new SqlMetaData(name, SqlDbType.Decimal, b, b2);
				}
				if (type == typeof(SqlSingle))
				{
					return new SqlMetaData(name, SqlDbType.Real);
				}
				if (type == typeof(SqlString))
				{
					SqlString sqlString = (SqlString)value;
					if (!sqlString.IsNull)
					{
						long num4 = (long)sqlString.Value.Length;
						if (num4 < 1L)
						{
							num4 = 1L;
						}
						if (num4 > 4000L)
						{
							num4 = SqlMetaData.Max;
						}
						return new SqlMetaData(name, SqlDbType.NVarChar, num4, (long)sqlString.LCID, sqlString.SqlCompareOptions);
					}
					return new SqlMetaData(name, SqlDbType.NVarChar, SqlMetaData.sxm_rgDefaults[12].MaxLength);
				}
				else
				{
					if (type == typeof(SqlChars))
					{
						SqlChars sqlChars = (SqlChars)value;
						long num5;
						if (!sqlChars.IsNull)
						{
							num5 = sqlChars.Length;
							if (num5 < 1L)
							{
								num5 = 1L;
							}
							if (num5 > 4000L)
							{
								num5 = SqlMetaData.Max;
							}
						}
						else
						{
							num5 = SqlMetaData.sxm_rgDefaults[12].MaxLength;
						}
						return new SqlMetaData(name, SqlDbType.NVarChar, num5);
					}
					if (type == typeof(SqlBytes))
					{
						SqlBytes sqlBytes = (SqlBytes)value;
						long num6;
						if (!sqlBytes.IsNull)
						{
							num6 = sqlBytes.Length;
							if (num6 < 1L)
							{
								num6 = 1L;
							}
							else if (8000L < num6)
							{
								num6 = SqlMetaData.Max;
							}
						}
						else
						{
							num6 = SqlMetaData.sxm_rgDefaults[21].MaxLength;
						}
						return new SqlMetaData(name, SqlDbType.VarBinary, num6);
					}
					if (type == typeof(SqlXml))
					{
						return new SqlMetaData(name, SqlDbType.Xml);
					}
					if (type == typeof(TimeSpan))
					{
						return new SqlMetaData(name, SqlDbType.Time, 0, SqlMetaData.InferScaleFromTimeTicks(((TimeSpan)value).Ticks));
					}
					if (type == typeof(DateTimeOffset))
					{
						return new SqlMetaData(name, SqlDbType.DateTimeOffset, 0, SqlMetaData.InferScaleFromTimeTicks(((DateTimeOffset)value).Ticks));
					}
					throw ADP.UnknownDataType(type);
				}
				break;
			case TypeCode.DBNull:
				throw ADP.InvalidDataType(TypeCode.DBNull);
			case TypeCode.Boolean:
				return new SqlMetaData(name, SqlDbType.Bit);
			case TypeCode.Char:
				return new SqlMetaData(name, SqlDbType.NVarChar, 1L);
			case TypeCode.SByte:
				throw ADP.InvalidDataType(TypeCode.SByte);
			case TypeCode.Byte:
				return new SqlMetaData(name, SqlDbType.TinyInt);
			case TypeCode.Int16:
				return new SqlMetaData(name, SqlDbType.SmallInt);
			case TypeCode.UInt16:
				throw ADP.InvalidDataType(TypeCode.UInt16);
			case TypeCode.Int32:
				return new SqlMetaData(name, SqlDbType.Int);
			case TypeCode.UInt32:
				throw ADP.InvalidDataType(TypeCode.UInt32);
			case TypeCode.Int64:
				return new SqlMetaData(name, SqlDbType.BigInt);
			case TypeCode.UInt64:
				throw ADP.InvalidDataType(TypeCode.UInt64);
			case TypeCode.Single:
				return new SqlMetaData(name, SqlDbType.Real);
			case TypeCode.Double:
				return new SqlMetaData(name, SqlDbType.Float);
			case TypeCode.Decimal:
			{
				SqlDecimal sqlDecimal2 = new SqlDecimal((decimal)value);
				return new SqlMetaData(name, SqlDbType.Decimal, sqlDecimal2.Precision, sqlDecimal2.Scale);
			}
			case TypeCode.DateTime:
				return new SqlMetaData(name, SqlDbType.DateTime);
			case TypeCode.String:
			{
				long num7 = (long)((string)value).Length;
				if (num7 < 1L)
				{
					num7 = 1L;
				}
				if (4000L < num7)
				{
					num7 = SqlMetaData.Max;
				}
				return new SqlMetaData(name, SqlDbType.NVarChar, num7);
			}
			}
			throw ADP.UnknownDataTypeCode(type, Type.GetTypeCode(type));
		}

		/// <summary>Validates the specified <see cref="T:System.Boolean" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Boolean" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002E75 RID: 11893 RVA: 0x000C8794 File Offset: 0x000C6994
		public bool Adjust(bool value)
		{
			if (SqlDbType.Bit != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Byte" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Byte" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002E76 RID: 11894 RVA: 0x000C87A5 File Offset: 0x000C69A5
		public byte Adjust(byte value)
		{
			if (SqlDbType.TinyInt != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified array of <see cref="T:System.Byte" /> values against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as an array of <see cref="T:System.Byte" /> values.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002E77 RID: 11895 RVA: 0x000C97B8 File Offset: 0x000C79B8
		public byte[] Adjust(byte[] value)
		{
			if (SqlDbType.Binary == this.SqlDbType || SqlDbType.Timestamp == this.SqlDbType)
			{
				if (value != null && (long)value.Length < this.MaxLength)
				{
					byte[] array = new byte[this.MaxLength];
					Buffer.BlockCopy(value, 0, array, 0, value.Length);
					Array.Clear(array, value.Length, array.Length - value.Length);
					return array;
				}
			}
			else if (SqlDbType.VarBinary != this.SqlDbType && SqlDbType.Image != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (value == null)
			{
				return null;
			}
			if ((long)value.Length > this.MaxLength && SqlMetaData.Max != this.MaxLength)
			{
				byte[] array2 = new byte[this.MaxLength];
				Buffer.BlockCopy(value, 0, array2, 0, (int)this.MaxLength);
				value = array2;
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Char" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Char" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002E78 RID: 11896 RVA: 0x000C986C File Offset: 0x000C7A6C
		public char Adjust(char value)
		{
			if (SqlDbType.Char == this.SqlDbType || SqlDbType.NChar == this.SqlDbType)
			{
				if (1L != this.MaxLength)
				{
					SqlMetaData.ThrowInvalidType();
				}
			}
			else if (1L > this.MaxLength || (SqlDbType.VarChar != this.SqlDbType && SqlDbType.NVarChar != this.SqlDbType && SqlDbType.Text != this.SqlDbType && SqlDbType.NText != this.SqlDbType))
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified array of <see cref="T:System.Char" /> values against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as an array <see cref="T:System.Char" /> values.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002E79 RID: 11897 RVA: 0x000C98D8 File Offset: 0x000C7AD8
		public char[] Adjust(char[] value)
		{
			if (SqlDbType.Char == this.SqlDbType || SqlDbType.NChar == this.SqlDbType)
			{
				if (value != null)
				{
					long num = (long)value.Length;
					if (num < this.MaxLength)
					{
						char[] array = new char[(int)this.MaxLength];
						Buffer.BlockCopy(value, 0, array, 0, (int)num);
						for (long num2 = num; num2 < (long)array.Length; num2 += 1L)
						{
							array[(int)(checked((IntPtr)num2))] = ' ';
						}
						return array;
					}
				}
			}
			else if (SqlDbType.VarChar != this.SqlDbType && SqlDbType.NVarChar != this.SqlDbType && SqlDbType.Text != this.SqlDbType && SqlDbType.NText != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (value == null)
			{
				return null;
			}
			if ((long)value.Length > this.MaxLength && SqlMetaData.Max != this.MaxLength)
			{
				char[] array2 = new char[this.MaxLength];
				Buffer.BlockCopy(value, 0, array2, 0, (int)this.MaxLength);
				value = array2;
			}
			return value;
		}

		// Token: 0x06002E7A RID: 11898 RVA: 0x000C99A8 File Offset: 0x000C7BA8
		internal static SqlMetaData GetPartialLengthMetaData(SqlMetaData md)
		{
			if (md.IsPartialLength)
			{
				return md;
			}
			if (md.SqlDbType == SqlDbType.Xml)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (md.SqlDbType == SqlDbType.NVarChar || md.SqlDbType == SqlDbType.VarChar || md.SqlDbType == SqlDbType.VarBinary)
			{
				return new SqlMetaData(md.Name, md.SqlDbType, SqlMetaData.Max, 0, 0, md.LocaleId, md.CompareOptions, null, null, null, true, md.Type);
			}
			return md;
		}

		// Token: 0x06002E7B RID: 11899 RVA: 0x000C9A1C File Offset: 0x000C7C1C
		private static void ThrowInvalidType()
		{
			throw ADP.InvalidMetaDataValue();
		}

		// Token: 0x06002E7C RID: 11900 RVA: 0x000C9A23 File Offset: 0x000C7C23
		private void VerifyDateTimeRange(DateTime value)
		{
			if (SqlDbType.SmallDateTime == this.SqlDbType && (SqlMetaData.s_dtSmallMax < value || SqlMetaData.s_dtSmallMin > value))
			{
				SqlMetaData.ThrowInvalidType();
			}
		}

		// Token: 0x06002E7D RID: 11901 RVA: 0x000C9A50 File Offset: 0x000C7C50
		private void VerifyMoneyRange(SqlMoney value)
		{
			if (SqlDbType.SmallMoney == this.SqlDbType && ((SqlMetaData.s_smSmallMax < value).Value || (SqlMetaData.s_smSmallMin > value).Value))
			{
				SqlMetaData.ThrowInvalidType();
			}
		}

		// Token: 0x06002E7E RID: 11902 RVA: 0x000C9A98 File Offset: 0x000C7C98
		private SqlDecimal InternalAdjustSqlDecimal(SqlDecimal value)
		{
			if (!value.IsNull && (value.Precision != this.Precision || value.Scale != this.Scale))
			{
				if (value.Scale != this.Scale)
				{
					value = SqlDecimal.AdjustScale(value, (int)(this.Scale - value.Scale), false);
				}
				return SqlDecimal.ConvertToPrecScale(value, (int)this.Precision, (int)this.Scale);
			}
			return value;
		}

		// Token: 0x06002E7F RID: 11903 RVA: 0x000C9B06 File Offset: 0x000C7D06
		private void VerifyTimeRange(TimeSpan value)
		{
			if (SqlDbType.Time == this.SqlDbType && (SqlMetaData.s_timeMin > value || value > SqlMetaData.s_timeMax))
			{
				SqlMetaData.ThrowInvalidType();
			}
		}

		// Token: 0x06002E80 RID: 11904 RVA: 0x000C9B31 File Offset: 0x000C7D31
		private long InternalAdjustTimeTicks(long ticks)
		{
			return ticks / SqlMetaData.s_unitTicksFromScale[(int)this.Scale] * SqlMetaData.s_unitTicksFromScale[(int)this.Scale];
		}

		// Token: 0x06002E81 RID: 11905 RVA: 0x000C9B50 File Offset: 0x000C7D50
		private static byte InferScaleFromTimeTicks(long ticks)
		{
			for (byte b = 0; b < 7; b += 1)
			{
				if (ticks / SqlMetaData.s_unitTicksFromScale[(int)b] * SqlMetaData.s_unitTicksFromScale[(int)b] == ticks)
				{
					return b;
				}
			}
			return 7;
		}

		// Token: 0x06002E82 RID: 11906 RVA: 0x000C9B84 File Offset: 0x000C7D84
		private void SetDefaultsForType(SqlDbType dbType)
		{
			if (SqlDbType.BigInt <= dbType && SqlDbType.DateTimeOffset >= dbType)
			{
				SqlMetaData sqlMetaData = SqlMetaData.sxm_rgDefaults[(int)dbType];
				this._sqlDbType = dbType;
				this._lMaxLength = sqlMetaData.MaxLength;
				this._bPrecision = sqlMetaData.Precision;
				this._bScale = sqlMetaData.Scale;
				this._lLocale = sqlMetaData.LocaleId;
				this._eCompareOptions = sqlMetaData.CompareOptions;
			}
		}

		// Token: 0x04001B7D RID: 7037
		private string _strName;

		// Token: 0x04001B7E RID: 7038
		private long _lMaxLength;

		// Token: 0x04001B7F RID: 7039
		private SqlDbType _sqlDbType;

		// Token: 0x04001B80 RID: 7040
		private byte _bPrecision;

		// Token: 0x04001B81 RID: 7041
		private byte _bScale;

		// Token: 0x04001B82 RID: 7042
		private long _lLocale;

		// Token: 0x04001B83 RID: 7043
		private SqlCompareOptions _eCompareOptions;

		// Token: 0x04001B84 RID: 7044
		private string _xmlSchemaCollectionDatabase;

		// Token: 0x04001B85 RID: 7045
		private string _xmlSchemaCollectionOwningSchema;

		// Token: 0x04001B86 RID: 7046
		private string _xmlSchemaCollectionName;

		// Token: 0x04001B87 RID: 7047
		private string _serverTypeName;

		// Token: 0x04001B88 RID: 7048
		private bool _bPartialLength;

		// Token: 0x04001B89 RID: 7049
		private Type _udtType;

		// Token: 0x04001B8A RID: 7050
		private bool _useServerDefault;

		// Token: 0x04001B8B RID: 7051
		private bool _isUniqueKey;

		// Token: 0x04001B8C RID: 7052
		private SortOrder _columnSortOrder;

		// Token: 0x04001B8D RID: 7053
		private int _sortOrdinal;

		// Token: 0x04001B8E RID: 7054
		private const long x_lMax = -1L;

		// Token: 0x04001B8F RID: 7055
		private const long x_lServerMaxUnicode = 4000L;

		// Token: 0x04001B90 RID: 7056
		private const long x_lServerMaxANSI = 8000L;

		// Token: 0x04001B91 RID: 7057
		private const long x_lServerMaxBinary = 8000L;

		// Token: 0x04001B92 RID: 7058
		private const bool x_defaultUseServerDefault = false;

		// Token: 0x04001B93 RID: 7059
		private const bool x_defaultIsUniqueKey = false;

		// Token: 0x04001B94 RID: 7060
		private const SortOrder x_defaultColumnSortOrder = SortOrder.Unspecified;

		// Token: 0x04001B95 RID: 7061
		private const int x_defaultSortOrdinal = -1;

		// Token: 0x04001B96 RID: 7062
		private const SqlCompareOptions x_eDefaultStringCompareOptions = SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth;

		// Token: 0x04001B97 RID: 7063
		private static byte[] s_maxLenFromPrecision = new byte[]
		{
			5, 5, 5, 5, 5, 5, 5, 5, 5, 9,
			9, 9, 9, 9, 9, 9, 9, 9, 9, 13,
			13, 13, 13, 13, 13, 13, 13, 13, 17, 17,
			17, 17, 17, 17, 17, 17, 17, 17
		};

		// Token: 0x04001B98 RID: 7064
		private const byte MaxTimeScale = 7;

		// Token: 0x04001B99 RID: 7065
		private static byte[] s_maxVarTimeLenOffsetFromScale = new byte[] { 2, 2, 2, 1, 1, 0, 0, 0 };

		// Token: 0x04001B9A RID: 7066
		private static readonly DateTime s_dtSmallMax = new DateTime(2079, 6, 6, 23, 59, 29, 998);

		// Token: 0x04001B9B RID: 7067
		private static readonly DateTime s_dtSmallMin = new DateTime(1899, 12, 31, 23, 59, 29, 999);

		// Token: 0x04001B9C RID: 7068
		private static readonly SqlMoney s_smSmallMax = new SqlMoney(214748.3647m);

		// Token: 0x04001B9D RID: 7069
		private static readonly SqlMoney s_smSmallMin = new SqlMoney(-214748.3648m);

		// Token: 0x04001B9E RID: 7070
		private static readonly TimeSpan s_timeMin = TimeSpan.Zero;

		// Token: 0x04001B9F RID: 7071
		private static readonly TimeSpan s_timeMax = new TimeSpan(863999999999L);

		// Token: 0x04001BA0 RID: 7072
		private static readonly long[] s_unitTicksFromScale = new long[] { 10000000L, 1000000L, 100000L, 10000L, 1000L, 100L, 10L, 1L };

		// Token: 0x04001BA1 RID: 7073
		private static DbType[] sxm_rgSqlDbTypeToDbType = new DbType[]
		{
			DbType.Int64,
			DbType.Binary,
			DbType.Boolean,
			DbType.AnsiString,
			DbType.DateTime,
			DbType.Decimal,
			DbType.Double,
			DbType.Binary,
			DbType.Int32,
			DbType.Currency,
			DbType.String,
			DbType.String,
			DbType.String,
			DbType.Single,
			DbType.Guid,
			DbType.DateTime,
			DbType.Int16,
			DbType.Currency,
			DbType.AnsiString,
			DbType.Binary,
			DbType.Byte,
			DbType.Binary,
			DbType.AnsiString,
			DbType.Object,
			DbType.Object,
			DbType.Xml,
			DbType.String,
			DbType.String,
			DbType.String,
			DbType.Object,
			DbType.Object,
			DbType.Date,
			DbType.Time,
			DbType.DateTime2,
			DbType.DateTimeOffset
		};

		// Token: 0x04001BA2 RID: 7074
		internal static SqlMetaData[] sxm_rgDefaults = new SqlMetaData[]
		{
			new SqlMetaData("bigint", SqlDbType.BigInt, 8L, 19, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("binary", SqlDbType.Binary, 1L, 0, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("bit", SqlDbType.Bit, 1L, 1, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("char", SqlDbType.Char, 1L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("datetime", SqlDbType.DateTime, 8L, 23, 3, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("decimal", SqlDbType.Decimal, 9L, 18, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("float", SqlDbType.Float, 8L, 53, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("image", SqlDbType.Image, -1L, 0, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("int", SqlDbType.Int, 4L, 10, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("money", SqlDbType.Money, 8L, 19, 4, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("nchar", SqlDbType.NChar, 1L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("ntext", SqlDbType.NText, -1L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("nvarchar", SqlDbType.NVarChar, 4000L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("real", SqlDbType.Real, 4L, 24, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("uniqueidentifier", SqlDbType.UniqueIdentifier, 16L, 0, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("smalldatetime", SqlDbType.SmallDateTime, 4L, 16, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("smallint", SqlDbType.SmallInt, 2L, 5, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("smallmoney", SqlDbType.SmallMoney, 4L, 10, 4, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("text", SqlDbType.Text, -1L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("timestamp", SqlDbType.Timestamp, 8L, 0, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("tinyint", SqlDbType.TinyInt, 1L, 3, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("varbinary", SqlDbType.VarBinary, 8000L, 0, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("varchar", SqlDbType.VarChar, 8000L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("sql_variant", SqlDbType.Variant, 8016L, 0, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("nvarchar", SqlDbType.NVarChar, 1L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("xml", SqlDbType.Xml, -1L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, true),
			new SqlMetaData("nvarchar", SqlDbType.NVarChar, 1L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("nvarchar", SqlDbType.NVarChar, 4000L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("nvarchar", SqlDbType.NVarChar, 4000L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("udt", SqlDbType.Udt, 0L, 0, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("table", SqlDbType.Structured, 0L, 0, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("date", SqlDbType.Date, 3L, 10, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("time", SqlDbType.Time, 5L, 0, 7, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("datetime2", SqlDbType.DateTime2, 8L, 0, 7, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("datetimeoffset", SqlDbType.DateTimeOffset, 10L, 0, 7, 0L, SqlCompareOptions.None, false)
		};
	}
}
