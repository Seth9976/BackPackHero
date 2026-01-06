using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Globalization;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000395 RID: 917
	internal class SmiMetaData
	{
		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06002C25 RID: 11301 RVA: 0x000C13F5 File Offset: 0x000BF5F5
		internal static SmiMetaData DefaultChar
		{
			get
			{
				return new SmiMetaData(SmiMetaData.DefaultChar_NoCollation.SqlDbType, SmiMetaData.DefaultChar_NoCollation.MaxLength, SmiMetaData.DefaultChar_NoCollation.Precision, SmiMetaData.DefaultChar_NoCollation.Scale, (long)CultureInfo.CurrentCulture.LCID, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, null);
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06002C26 RID: 11302 RVA: 0x000C1432 File Offset: 0x000BF632
		internal static SmiMetaData DefaultNChar
		{
			get
			{
				return new SmiMetaData(SmiMetaData.DefaultNChar_NoCollation.SqlDbType, SmiMetaData.DefaultNChar_NoCollation.MaxLength, SmiMetaData.DefaultNChar_NoCollation.Precision, SmiMetaData.DefaultNChar_NoCollation.Scale, (long)CultureInfo.CurrentCulture.LCID, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, null);
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06002C27 RID: 11303 RVA: 0x000C146F File Offset: 0x000BF66F
		internal static SmiMetaData DefaultNText
		{
			get
			{
				return new SmiMetaData(SmiMetaData.DefaultNText_NoCollation.SqlDbType, SmiMetaData.DefaultNText_NoCollation.MaxLength, SmiMetaData.DefaultNText_NoCollation.Precision, SmiMetaData.DefaultNText_NoCollation.Scale, (long)CultureInfo.CurrentCulture.LCID, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, null);
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06002C28 RID: 11304 RVA: 0x000C14AC File Offset: 0x000BF6AC
		internal static SmiMetaData DefaultNVarChar
		{
			get
			{
				return new SmiMetaData(SmiMetaData.DefaultNVarChar_NoCollation.SqlDbType, SmiMetaData.DefaultNVarChar_NoCollation.MaxLength, SmiMetaData.DefaultNVarChar_NoCollation.Precision, SmiMetaData.DefaultNVarChar_NoCollation.Scale, (long)CultureInfo.CurrentCulture.LCID, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, null);
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06002C29 RID: 11305 RVA: 0x000C14E9 File Offset: 0x000BF6E9
		internal static SmiMetaData DefaultText
		{
			get
			{
				return new SmiMetaData(SmiMetaData.DefaultText_NoCollation.SqlDbType, SmiMetaData.DefaultText_NoCollation.MaxLength, SmiMetaData.DefaultText_NoCollation.Precision, SmiMetaData.DefaultText_NoCollation.Scale, (long)CultureInfo.CurrentCulture.LCID, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, null);
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06002C2A RID: 11306 RVA: 0x000C1526 File Offset: 0x000BF726
		internal static SmiMetaData DefaultVarChar
		{
			get
			{
				return new SmiMetaData(SmiMetaData.DefaultVarChar_NoCollation.SqlDbType, SmiMetaData.DefaultVarChar_NoCollation.MaxLength, SmiMetaData.DefaultVarChar_NoCollation.Precision, SmiMetaData.DefaultVarChar_NoCollation.Scale, (long)CultureInfo.CurrentCulture.LCID, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, null);
			}
		}

		// Token: 0x06002C2B RID: 11307 RVA: 0x000C1564 File Offset: 0x000BF764
		internal SmiMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType)
			: this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, false, null, null)
		{
		}

		// Token: 0x06002C2C RID: 11308 RVA: 0x000C1588 File Offset: 0x000BF788
		internal SmiMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, bool isMultiValued, IList<SmiExtendedMetaData> fieldTypes, SmiMetaDataPropertyCollection extendedProperties)
			: this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, null, isMultiValued, fieldTypes, extendedProperties)
		{
		}

		// Token: 0x06002C2D RID: 11309 RVA: 0x000C15B0 File Offset: 0x000BF7B0
		internal SmiMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, string udtAssemblyQualifiedName, bool isMultiValued, IList<SmiExtendedMetaData> fieldTypes, SmiMetaDataPropertyCollection extendedProperties)
		{
			this.SetDefaultsForType(dbType);
			switch (dbType)
			{
			case SqlDbType.Binary:
			case SqlDbType.VarBinary:
				this._maxLength = maxLength;
				break;
			case SqlDbType.Char:
			case SqlDbType.NChar:
			case SqlDbType.NVarChar:
			case SqlDbType.VarChar:
				this._maxLength = maxLength;
				this._localeId = localeId;
				this._compareOptions = compareOptions;
				break;
			case SqlDbType.Decimal:
				this._precision = precision;
				this._scale = scale;
				this._maxLength = (long)((ulong)SmiMetaData.s_maxLenFromPrecision[(int)(precision - 1)]);
				break;
			case SqlDbType.NText:
			case SqlDbType.Text:
				this._localeId = localeId;
				this._compareOptions = compareOptions;
				break;
			case SqlDbType.Udt:
				this._clrType = userDefinedType;
				if (null != userDefinedType)
				{
					this._maxLength = (long)SerializationHelperSql9.GetUdtMaxLength(userDefinedType);
				}
				else
				{
					this._maxLength = maxLength;
				}
				this._udtAssemblyQualifiedName = udtAssemblyQualifiedName;
				break;
			case SqlDbType.Structured:
				if (fieldTypes != null)
				{
					this._fieldMetaData = new List<SmiExtendedMetaData>(fieldTypes).AsReadOnly();
				}
				this._isMultiValued = isMultiValued;
				this._maxLength = (long)this._fieldMetaData.Count;
				break;
			case SqlDbType.Time:
				this._scale = scale;
				this._maxLength = (long)(5 - SmiMetaData.s_maxVarTimeLenOffsetFromScale[(int)scale]);
				break;
			case SqlDbType.DateTime2:
				this._scale = scale;
				this._maxLength = (long)(8 - SmiMetaData.s_maxVarTimeLenOffsetFromScale[(int)scale]);
				break;
			case SqlDbType.DateTimeOffset:
				this._scale = scale;
				this._maxLength = (long)(10 - SmiMetaData.s_maxVarTimeLenOffsetFromScale[(int)scale]);
				break;
			}
			if (extendedProperties != null)
			{
				extendedProperties.SetReadOnly();
				this._extendedProperties = extendedProperties;
			}
		}

		// Token: 0x06002C2E RID: 11310 RVA: 0x000C1790 File Offset: 0x000BF990
		internal bool IsValidMaxLengthForCtorGivenType(SqlDbType dbType, long maxLength)
		{
			bool flag = true;
			switch (dbType)
			{
			case SqlDbType.Binary:
				flag = 0L < maxLength && 8000L >= maxLength;
				break;
			case SqlDbType.Char:
				flag = 0L < maxLength && 8000L >= maxLength;
				break;
			case SqlDbType.NChar:
				flag = 0L < maxLength && 4000L >= maxLength;
				break;
			case SqlDbType.NVarChar:
				flag = -1L == maxLength || (0L < maxLength && 4000L >= maxLength);
				break;
			case SqlDbType.VarBinary:
				flag = -1L == maxLength || (0L < maxLength && 8000L >= maxLength);
				break;
			case SqlDbType.VarChar:
				flag = -1L == maxLength || (0L < maxLength && 8000L >= maxLength);
				break;
			}
			return flag;
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06002C2F RID: 11311 RVA: 0x000C18DA File Offset: 0x000BFADA
		internal SqlCompareOptions CompareOptions
		{
			get
			{
				return this._compareOptions;
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06002C30 RID: 11312 RVA: 0x000C18E2 File Offset: 0x000BFAE2
		internal long LocaleId
		{
			get
			{
				return this._localeId;
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06002C31 RID: 11313 RVA: 0x000C18EA File Offset: 0x000BFAEA
		internal long MaxLength
		{
			get
			{
				return this._maxLength;
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06002C32 RID: 11314 RVA: 0x000C18F2 File Offset: 0x000BFAF2
		internal byte Precision
		{
			get
			{
				return this._precision;
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06002C33 RID: 11315 RVA: 0x000C18FA File Offset: 0x000BFAFA
		internal byte Scale
		{
			get
			{
				return this._scale;
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06002C34 RID: 11316 RVA: 0x000C1902 File Offset: 0x000BFB02
		internal SqlDbType SqlDbType
		{
			get
			{
				return this._databaseType;
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06002C35 RID: 11317 RVA: 0x000C190A File Offset: 0x000BFB0A
		internal Type Type
		{
			get
			{
				if (null == this._clrType && SqlDbType.Udt == this._databaseType && this._udtAssemblyQualifiedName != null)
				{
					this._clrType = Type.GetType(this._udtAssemblyQualifiedName, true);
				}
				return this._clrType;
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06002C36 RID: 11318 RVA: 0x000C1944 File Offset: 0x000BFB44
		internal Type TypeWithoutThrowing
		{
			get
			{
				if (null == this._clrType && SqlDbType.Udt == this._databaseType && this._udtAssemblyQualifiedName != null)
				{
					this._clrType = Type.GetType(this._udtAssemblyQualifiedName, false);
				}
				return this._clrType;
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06002C37 RID: 11319 RVA: 0x000C1980 File Offset: 0x000BFB80
		internal string TypeName
		{
			get
			{
				string text;
				if (SqlDbType.Udt == this._databaseType)
				{
					text = this.Type.FullName;
				}
				else
				{
					text = SmiMetaData.s_typeNameByDatabaseType[(int)this._databaseType];
				}
				return text;
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06002C38 RID: 11320 RVA: 0x000C19B8 File Offset: 0x000BFBB8
		internal string AssemblyQualifiedName
		{
			get
			{
				string text = null;
				if (SqlDbType.Udt == this._databaseType)
				{
					if (this._udtAssemblyQualifiedName == null && this._clrType != null)
					{
						this._udtAssemblyQualifiedName = this._clrType.AssemblyQualifiedName;
					}
					text = this._udtAssemblyQualifiedName;
				}
				return text;
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06002C39 RID: 11321 RVA: 0x000C1A00 File Offset: 0x000BFC00
		internal bool IsMultiValued
		{
			get
			{
				return this._isMultiValued;
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06002C3A RID: 11322 RVA: 0x000C1A08 File Offset: 0x000BFC08
		internal IList<SmiExtendedMetaData> FieldMetaData
		{
			get
			{
				return this._fieldMetaData;
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06002C3B RID: 11323 RVA: 0x000C1A10 File Offset: 0x000BFC10
		internal SmiMetaDataPropertyCollection ExtendedProperties
		{
			get
			{
				return this._extendedProperties;
			}
		}

		// Token: 0x06002C3C RID: 11324 RVA: 0x000C1A18 File Offset: 0x000BFC18
		internal static bool IsSupportedDbType(SqlDbType dbType)
		{
			return (SqlDbType.BigInt <= dbType && SqlDbType.Xml >= dbType) || (SqlDbType.Udt <= dbType && SqlDbType.DateTimeOffset >= dbType);
		}

		// Token: 0x06002C3D RID: 11325 RVA: 0x000C1A34 File Offset: 0x000BFC34
		internal static SmiMetaData GetDefaultForType(SqlDbType dbType)
		{
			return SmiMetaData.s_defaultValues[(int)dbType];
		}

		// Token: 0x06002C3E RID: 11326 RVA: 0x000C1A40 File Offset: 0x000BFC40
		private SmiMetaData(SqlDbType sqlDbType, long maxLength, byte precision, byte scale, SqlCompareOptions compareOptions)
		{
			this._databaseType = sqlDbType;
			this._maxLength = maxLength;
			this._precision = precision;
			this._scale = scale;
			this._compareOptions = compareOptions;
			this._localeId = 0L;
			this._clrType = null;
			this._isMultiValued = false;
			this._fieldMetaData = SmiMetaData.s_emptyFieldList;
			this._extendedProperties = SmiMetaDataPropertyCollection.EmptyInstance;
		}

		// Token: 0x06002C3F RID: 11327 RVA: 0x000C1AA4 File Offset: 0x000BFCA4
		private void SetDefaultsForType(SqlDbType dbType)
		{
			SmiMetaData defaultForType = SmiMetaData.GetDefaultForType(dbType);
			this._databaseType = dbType;
			this._maxLength = defaultForType.MaxLength;
			this._precision = defaultForType.Precision;
			this._scale = defaultForType.Scale;
			this._localeId = defaultForType.LocaleId;
			this._compareOptions = defaultForType.CompareOptions;
			this._clrType = null;
			this._isMultiValued = defaultForType._isMultiValued;
			this._fieldMetaData = defaultForType._fieldMetaData;
			this._extendedProperties = defaultForType._extendedProperties;
		}

		// Token: 0x04001ACE RID: 6862
		private SqlDbType _databaseType;

		// Token: 0x04001ACF RID: 6863
		private long _maxLength;

		// Token: 0x04001AD0 RID: 6864
		private byte _precision;

		// Token: 0x04001AD1 RID: 6865
		private byte _scale;

		// Token: 0x04001AD2 RID: 6866
		private long _localeId;

		// Token: 0x04001AD3 RID: 6867
		private SqlCompareOptions _compareOptions;

		// Token: 0x04001AD4 RID: 6868
		private Type _clrType;

		// Token: 0x04001AD5 RID: 6869
		private string _udtAssemblyQualifiedName;

		// Token: 0x04001AD6 RID: 6870
		private bool _isMultiValued;

		// Token: 0x04001AD7 RID: 6871
		private IList<SmiExtendedMetaData> _fieldMetaData;

		// Token: 0x04001AD8 RID: 6872
		private SmiMetaDataPropertyCollection _extendedProperties;

		// Token: 0x04001AD9 RID: 6873
		internal const long UnlimitedMaxLengthIndicator = -1L;

		// Token: 0x04001ADA RID: 6874
		internal const long MaxUnicodeCharacters = 4000L;

		// Token: 0x04001ADB RID: 6875
		internal const long MaxANSICharacters = 8000L;

		// Token: 0x04001ADC RID: 6876
		internal const long MaxBinaryLength = 8000L;

		// Token: 0x04001ADD RID: 6877
		internal const int MinPrecision = 1;

		// Token: 0x04001ADE RID: 6878
		internal const int MinScale = 0;

		// Token: 0x04001ADF RID: 6879
		internal const int MaxTimeScale = 7;

		// Token: 0x04001AE0 RID: 6880
		internal static readonly DateTime MaxSmallDateTime = new DateTime(2079, 6, 6, 23, 59, 29, 998);

		// Token: 0x04001AE1 RID: 6881
		internal static readonly DateTime MinSmallDateTime = new DateTime(1899, 12, 31, 23, 59, 29, 999);

		// Token: 0x04001AE2 RID: 6882
		internal static readonly SqlMoney MaxSmallMoney = new SqlMoney(214748.3647m);

		// Token: 0x04001AE3 RID: 6883
		internal static readonly SqlMoney MinSmallMoney = new SqlMoney(-214748.3648m);

		// Token: 0x04001AE4 RID: 6884
		internal const SqlCompareOptions DefaultStringCompareOptions = SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth;

		// Token: 0x04001AE5 RID: 6885
		internal const long MaxNameLength = 128L;

		// Token: 0x04001AE6 RID: 6886
		private static readonly IList<SmiExtendedMetaData> s_emptyFieldList = new List<SmiExtendedMetaData>().AsReadOnly();

		// Token: 0x04001AE7 RID: 6887
		private static byte[] s_maxLenFromPrecision = new byte[]
		{
			5, 5, 5, 5, 5, 5, 5, 5, 5, 9,
			9, 9, 9, 9, 9, 9, 9, 9, 9, 13,
			13, 13, 13, 13, 13, 13, 13, 13, 17, 17,
			17, 17, 17, 17, 17, 17, 17, 17
		};

		// Token: 0x04001AE8 RID: 6888
		private static byte[] s_maxVarTimeLenOffsetFromScale = new byte[] { 2, 2, 2, 1, 1, 0, 0, 0 };

		// Token: 0x04001AE9 RID: 6889
		internal static readonly SmiMetaData DefaultBigInt = new SmiMetaData(SqlDbType.BigInt, 8L, 19, 0, SqlCompareOptions.None);

		// Token: 0x04001AEA RID: 6890
		internal static readonly SmiMetaData DefaultBinary = new SmiMetaData(SqlDbType.Binary, 1L, 0, 0, SqlCompareOptions.None);

		// Token: 0x04001AEB RID: 6891
		internal static readonly SmiMetaData DefaultBit = new SmiMetaData(SqlDbType.Bit, 1L, 1, 0, SqlCompareOptions.None);

		// Token: 0x04001AEC RID: 6892
		internal static readonly SmiMetaData DefaultChar_NoCollation = new SmiMetaData(SqlDbType.Char, 1L, 0, 0, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth);

		// Token: 0x04001AED RID: 6893
		internal static readonly SmiMetaData DefaultDateTime = new SmiMetaData(SqlDbType.DateTime, 8L, 23, 3, SqlCompareOptions.None);

		// Token: 0x04001AEE RID: 6894
		internal static readonly SmiMetaData DefaultDecimal = new SmiMetaData(SqlDbType.Decimal, 9L, 18, 0, SqlCompareOptions.None);

		// Token: 0x04001AEF RID: 6895
		internal static readonly SmiMetaData DefaultFloat = new SmiMetaData(SqlDbType.Float, 8L, 53, 0, SqlCompareOptions.None);

		// Token: 0x04001AF0 RID: 6896
		internal static readonly SmiMetaData DefaultImage = new SmiMetaData(SqlDbType.Image, -1L, 0, 0, SqlCompareOptions.None);

		// Token: 0x04001AF1 RID: 6897
		internal static readonly SmiMetaData DefaultInt = new SmiMetaData(SqlDbType.Int, 4L, 10, 0, SqlCompareOptions.None);

		// Token: 0x04001AF2 RID: 6898
		internal static readonly SmiMetaData DefaultMoney = new SmiMetaData(SqlDbType.Money, 8L, 19, 4, SqlCompareOptions.None);

		// Token: 0x04001AF3 RID: 6899
		internal static readonly SmiMetaData DefaultNChar_NoCollation = new SmiMetaData(SqlDbType.NChar, 1L, 0, 0, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth);

		// Token: 0x04001AF4 RID: 6900
		internal static readonly SmiMetaData DefaultNText_NoCollation = new SmiMetaData(SqlDbType.NText, -1L, 0, 0, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth);

		// Token: 0x04001AF5 RID: 6901
		internal static readonly SmiMetaData DefaultNVarChar_NoCollation = new SmiMetaData(SqlDbType.NVarChar, 4000L, 0, 0, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth);

		// Token: 0x04001AF6 RID: 6902
		internal static readonly SmiMetaData DefaultReal = new SmiMetaData(SqlDbType.Real, 4L, 24, 0, SqlCompareOptions.None);

		// Token: 0x04001AF7 RID: 6903
		internal static readonly SmiMetaData DefaultUniqueIdentifier = new SmiMetaData(SqlDbType.UniqueIdentifier, 16L, 0, 0, SqlCompareOptions.None);

		// Token: 0x04001AF8 RID: 6904
		internal static readonly SmiMetaData DefaultSmallDateTime = new SmiMetaData(SqlDbType.SmallDateTime, 4L, 16, 0, SqlCompareOptions.None);

		// Token: 0x04001AF9 RID: 6905
		internal static readonly SmiMetaData DefaultSmallInt = new SmiMetaData(SqlDbType.SmallInt, 2L, 5, 0, SqlCompareOptions.None);

		// Token: 0x04001AFA RID: 6906
		internal static readonly SmiMetaData DefaultSmallMoney = new SmiMetaData(SqlDbType.SmallMoney, 4L, 10, 4, SqlCompareOptions.None);

		// Token: 0x04001AFB RID: 6907
		internal static readonly SmiMetaData DefaultText_NoCollation = new SmiMetaData(SqlDbType.Text, -1L, 0, 0, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth);

		// Token: 0x04001AFC RID: 6908
		internal static readonly SmiMetaData DefaultTimestamp = new SmiMetaData(SqlDbType.Timestamp, 8L, 0, 0, SqlCompareOptions.None);

		// Token: 0x04001AFD RID: 6909
		internal static readonly SmiMetaData DefaultTinyInt = new SmiMetaData(SqlDbType.TinyInt, 1L, 3, 0, SqlCompareOptions.None);

		// Token: 0x04001AFE RID: 6910
		internal static readonly SmiMetaData DefaultVarBinary = new SmiMetaData(SqlDbType.VarBinary, 8000L, 0, 0, SqlCompareOptions.None);

		// Token: 0x04001AFF RID: 6911
		internal static readonly SmiMetaData DefaultVarChar_NoCollation = new SmiMetaData(SqlDbType.VarChar, 8000L, 0, 0, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth);

		// Token: 0x04001B00 RID: 6912
		internal static readonly SmiMetaData DefaultVariant = new SmiMetaData(SqlDbType.Variant, 8016L, 0, 0, SqlCompareOptions.None);

		// Token: 0x04001B01 RID: 6913
		internal static readonly SmiMetaData DefaultXml = new SmiMetaData(SqlDbType.Xml, -1L, 0, 0, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth);

		// Token: 0x04001B02 RID: 6914
		internal static readonly SmiMetaData DefaultUdt_NoType = new SmiMetaData(SqlDbType.Udt, 0L, 0, 0, SqlCompareOptions.None);

		// Token: 0x04001B03 RID: 6915
		internal static readonly SmiMetaData DefaultStructured = new SmiMetaData(SqlDbType.Structured, 0L, 0, 0, SqlCompareOptions.None);

		// Token: 0x04001B04 RID: 6916
		internal static readonly SmiMetaData DefaultDate = new SmiMetaData(SqlDbType.Date, 3L, 10, 0, SqlCompareOptions.None);

		// Token: 0x04001B05 RID: 6917
		internal static readonly SmiMetaData DefaultTime = new SmiMetaData(SqlDbType.Time, 5L, 0, 7, SqlCompareOptions.None);

		// Token: 0x04001B06 RID: 6918
		internal static readonly SmiMetaData DefaultDateTime2 = new SmiMetaData(SqlDbType.DateTime2, 8L, 0, 7, SqlCompareOptions.None);

		// Token: 0x04001B07 RID: 6919
		internal static readonly SmiMetaData DefaultDateTimeOffset = new SmiMetaData(SqlDbType.DateTimeOffset, 10L, 0, 7, SqlCompareOptions.None);

		// Token: 0x04001B08 RID: 6920
		private static SmiMetaData[] s_defaultValues = new SmiMetaData[]
		{
			SmiMetaData.DefaultBigInt,
			SmiMetaData.DefaultBinary,
			SmiMetaData.DefaultBit,
			SmiMetaData.DefaultChar_NoCollation,
			SmiMetaData.DefaultDateTime,
			SmiMetaData.DefaultDecimal,
			SmiMetaData.DefaultFloat,
			SmiMetaData.DefaultImage,
			SmiMetaData.DefaultInt,
			SmiMetaData.DefaultMoney,
			SmiMetaData.DefaultNChar_NoCollation,
			SmiMetaData.DefaultNText_NoCollation,
			SmiMetaData.DefaultNVarChar_NoCollation,
			SmiMetaData.DefaultReal,
			SmiMetaData.DefaultUniqueIdentifier,
			SmiMetaData.DefaultSmallDateTime,
			SmiMetaData.DefaultSmallInt,
			SmiMetaData.DefaultSmallMoney,
			SmiMetaData.DefaultText_NoCollation,
			SmiMetaData.DefaultTimestamp,
			SmiMetaData.DefaultTinyInt,
			SmiMetaData.DefaultVarBinary,
			SmiMetaData.DefaultVarChar_NoCollation,
			SmiMetaData.DefaultVariant,
			SmiMetaData.DefaultNVarChar_NoCollation,
			SmiMetaData.DefaultXml,
			SmiMetaData.DefaultNVarChar_NoCollation,
			SmiMetaData.DefaultNVarChar_NoCollation,
			SmiMetaData.DefaultNVarChar_NoCollation,
			SmiMetaData.DefaultUdt_NoType,
			SmiMetaData.DefaultStructured,
			SmiMetaData.DefaultDate,
			SmiMetaData.DefaultTime,
			SmiMetaData.DefaultDateTime2,
			SmiMetaData.DefaultDateTimeOffset
		};

		// Token: 0x04001B09 RID: 6921
		private static string[] s_typeNameByDatabaseType = new string[]
		{
			"bigint",
			"binary",
			"bit",
			"char",
			"datetime",
			"decimal",
			"float",
			"image",
			"int",
			"money",
			"nchar",
			"ntext",
			"nvarchar",
			"real",
			"uniqueidentifier",
			"smalldatetime",
			"smallint",
			"smallmoney",
			"text",
			"timestamp",
			"tinyint",
			"varbinary",
			"varchar",
			"sql_variant",
			null,
			"xml",
			null,
			null,
			null,
			string.Empty,
			string.Empty,
			"date",
			"time",
			"datetime2",
			"datetimeoffset"
		};
	}
}
