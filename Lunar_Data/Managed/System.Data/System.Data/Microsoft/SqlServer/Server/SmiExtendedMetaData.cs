using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000396 RID: 918
	internal class SmiExtendedMetaData : SmiMetaData
	{
		// Token: 0x06002C41 RID: 11329 RVA: 0x000C2060 File Offset: 0x000C0260
		internal SmiExtendedMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3)
			: this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, false, null, null, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3)
		{
		}

		// Token: 0x06002C42 RID: 11330 RVA: 0x000C208C File Offset: 0x000C028C
		internal SmiExtendedMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, bool isMultiValued, IList<SmiExtendedMetaData> fieldMetaData, SmiMetaDataPropertyCollection extendedProperties, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3)
			: this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, null, isMultiValued, fieldMetaData, extendedProperties, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3)
		{
		}

		// Token: 0x06002C43 RID: 11331 RVA: 0x000C20BC File Offset: 0x000C02BC
		internal SmiExtendedMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, string udtAssemblyQualifiedName, bool isMultiValued, IList<SmiExtendedMetaData> fieldMetaData, SmiMetaDataPropertyCollection extendedProperties, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3)
			: base(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, udtAssemblyQualifiedName, isMultiValued, fieldMetaData, extendedProperties)
		{
			this._name = name;
			this._typeSpecificNamePart1 = typeSpecificNamePart1;
			this._typeSpecificNamePart2 = typeSpecificNamePart2;
			this._typeSpecificNamePart3 = typeSpecificNamePart3;
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06002C44 RID: 11332 RVA: 0x000C2102 File Offset: 0x000C0302
		internal string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06002C45 RID: 11333 RVA: 0x000C210A File Offset: 0x000C030A
		internal string TypeSpecificNamePart1
		{
			get
			{
				return this._typeSpecificNamePart1;
			}
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06002C46 RID: 11334 RVA: 0x000C2112 File Offset: 0x000C0312
		internal string TypeSpecificNamePart2
		{
			get
			{
				return this._typeSpecificNamePart2;
			}
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06002C47 RID: 11335 RVA: 0x000C211A File Offset: 0x000C031A
		internal string TypeSpecificNamePart3
		{
			get
			{
				return this._typeSpecificNamePart3;
			}
		}

		// Token: 0x04001B0A RID: 6922
		private string _name;

		// Token: 0x04001B0B RID: 6923
		private string _typeSpecificNamePart1;

		// Token: 0x04001B0C RID: 6924
		private string _typeSpecificNamePart2;

		// Token: 0x04001B0D RID: 6925
		private string _typeSpecificNamePart3;
	}
}
