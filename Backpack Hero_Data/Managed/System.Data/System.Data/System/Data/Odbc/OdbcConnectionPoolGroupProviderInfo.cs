using System;
using System.Data.ProviderBase;

namespace System.Data.Odbc
{
	// Token: 0x0200028B RID: 651
	internal sealed class OdbcConnectionPoolGroupProviderInfo : DbConnectionPoolGroupProviderInfo
	{
		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06001C41 RID: 7233 RVA: 0x0008AF88 File Offset: 0x00089188
		// (set) Token: 0x06001C42 RID: 7234 RVA: 0x0008AF90 File Offset: 0x00089190
		internal string DriverName
		{
			get
			{
				return this._driverName;
			}
			set
			{
				this._driverName = value;
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06001C43 RID: 7235 RVA: 0x0008AF99 File Offset: 0x00089199
		// (set) Token: 0x06001C44 RID: 7236 RVA: 0x0008AFA1 File Offset: 0x000891A1
		internal string DriverVersion
		{
			get
			{
				return this._driverVersion;
			}
			set
			{
				this._driverVersion = value;
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06001C45 RID: 7237 RVA: 0x0008AFAA File Offset: 0x000891AA
		internal bool HasQuoteChar
		{
			get
			{
				return this._hasQuoteChar;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06001C46 RID: 7238 RVA: 0x0008AFB2 File Offset: 0x000891B2
		internal bool HasEscapeChar
		{
			get
			{
				return this._hasEscapeChar;
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001C47 RID: 7239 RVA: 0x0008AFBA File Offset: 0x000891BA
		// (set) Token: 0x06001C48 RID: 7240 RVA: 0x0008AFC2 File Offset: 0x000891C2
		internal string QuoteChar
		{
			get
			{
				return this._quoteChar;
			}
			set
			{
				this._quoteChar = value;
				this._hasQuoteChar = true;
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06001C49 RID: 7241 RVA: 0x0008AFD2 File Offset: 0x000891D2
		// (set) Token: 0x06001C4A RID: 7242 RVA: 0x0008AFDA File Offset: 0x000891DA
		internal char EscapeChar
		{
			get
			{
				return this._escapeChar;
			}
			set
			{
				this._escapeChar = value;
				this._hasEscapeChar = true;
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06001C4B RID: 7243 RVA: 0x0008AFEA File Offset: 0x000891EA
		// (set) Token: 0x06001C4C RID: 7244 RVA: 0x0008AFF2 File Offset: 0x000891F2
		internal bool IsV3Driver
		{
			get
			{
				return this._isV3Driver;
			}
			set
			{
				this._isV3Driver = value;
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06001C4D RID: 7245 RVA: 0x0008AFFB File Offset: 0x000891FB
		// (set) Token: 0x06001C4E RID: 7246 RVA: 0x0008B003 File Offset: 0x00089203
		internal int SupportedSQLTypes
		{
			get
			{
				return this._supportedSQLTypes;
			}
			set
			{
				this._supportedSQLTypes = value;
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06001C4F RID: 7247 RVA: 0x0008B00C File Offset: 0x0008920C
		// (set) Token: 0x06001C50 RID: 7248 RVA: 0x0008B014 File Offset: 0x00089214
		internal int TestedSQLTypes
		{
			get
			{
				return this._testedSQLTypes;
			}
			set
			{
				this._testedSQLTypes = value;
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06001C51 RID: 7249 RVA: 0x0008B01D File Offset: 0x0008921D
		// (set) Token: 0x06001C52 RID: 7250 RVA: 0x0008B025 File Offset: 0x00089225
		internal int RestrictedSQLBindTypes
		{
			get
			{
				return this._restrictedSQLBindTypes;
			}
			set
			{
				this._restrictedSQLBindTypes = value;
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06001C53 RID: 7251 RVA: 0x0008B02E File Offset: 0x0008922E
		// (set) Token: 0x06001C54 RID: 7252 RVA: 0x0008B036 File Offset: 0x00089236
		internal bool NoCurrentCatalog
		{
			get
			{
				return this._noCurrentCatalog;
			}
			set
			{
				this._noCurrentCatalog = value;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06001C55 RID: 7253 RVA: 0x0008B03F File Offset: 0x0008923F
		// (set) Token: 0x06001C56 RID: 7254 RVA: 0x0008B047 File Offset: 0x00089247
		internal bool NoConnectionDead
		{
			get
			{
				return this._noConnectionDead;
			}
			set
			{
				this._noConnectionDead = value;
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06001C57 RID: 7255 RVA: 0x0008B050 File Offset: 0x00089250
		// (set) Token: 0x06001C58 RID: 7256 RVA: 0x0008B058 File Offset: 0x00089258
		internal bool NoQueryTimeout
		{
			get
			{
				return this._noQueryTimeout;
			}
			set
			{
				this._noQueryTimeout = value;
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001C59 RID: 7257 RVA: 0x0008B061 File Offset: 0x00089261
		// (set) Token: 0x06001C5A RID: 7258 RVA: 0x0008B069 File Offset: 0x00089269
		internal bool NoSqlSoptSSNoBrowseTable
		{
			get
			{
				return this._noSqlSoptSSNoBrowseTable;
			}
			set
			{
				this._noSqlSoptSSNoBrowseTable = value;
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06001C5B RID: 7259 RVA: 0x0008B072 File Offset: 0x00089272
		// (set) Token: 0x06001C5C RID: 7260 RVA: 0x0008B07A File Offset: 0x0008927A
		internal bool NoSqlSoptSSHiddenColumns
		{
			get
			{
				return this._noSqlSoptSSHiddenColumns;
			}
			set
			{
				this._noSqlSoptSSHiddenColumns = value;
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06001C5D RID: 7261 RVA: 0x0008B083 File Offset: 0x00089283
		// (set) Token: 0x06001C5E RID: 7262 RVA: 0x0008B08B File Offset: 0x0008928B
		internal bool NoSqlCASSColumnKey
		{
			get
			{
				return this._noSqlCASSColumnKey;
			}
			set
			{
				this._noSqlCASSColumnKey = value;
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06001C5F RID: 7263 RVA: 0x0008B094 File Offset: 0x00089294
		// (set) Token: 0x06001C60 RID: 7264 RVA: 0x0008B09C File Offset: 0x0008929C
		internal bool NoSqlPrimaryKeys
		{
			get
			{
				return this._noSqlPrimaryKeys;
			}
			set
			{
				this._noSqlPrimaryKeys = value;
			}
		}

		// Token: 0x04001554 RID: 5460
		private string _driverName;

		// Token: 0x04001555 RID: 5461
		private string _driverVersion;

		// Token: 0x04001556 RID: 5462
		private string _quoteChar;

		// Token: 0x04001557 RID: 5463
		private char _escapeChar;

		// Token: 0x04001558 RID: 5464
		private bool _hasQuoteChar;

		// Token: 0x04001559 RID: 5465
		private bool _hasEscapeChar;

		// Token: 0x0400155A RID: 5466
		private bool _isV3Driver;

		// Token: 0x0400155B RID: 5467
		private int _supportedSQLTypes;

		// Token: 0x0400155C RID: 5468
		private int _testedSQLTypes;

		// Token: 0x0400155D RID: 5469
		private int _restrictedSQLBindTypes;

		// Token: 0x0400155E RID: 5470
		private bool _noCurrentCatalog;

		// Token: 0x0400155F RID: 5471
		private bool _noConnectionDead;

		// Token: 0x04001560 RID: 5472
		private bool _noQueryTimeout;

		// Token: 0x04001561 RID: 5473
		private bool _noSqlSoptSSNoBrowseTable;

		// Token: 0x04001562 RID: 5474
		private bool _noSqlSoptSSHiddenColumns;

		// Token: 0x04001563 RID: 5475
		private bool _noSqlCASSColumnKey;

		// Token: 0x04001564 RID: 5476
		private bool _noSqlPrimaryKeys;
	}
}
