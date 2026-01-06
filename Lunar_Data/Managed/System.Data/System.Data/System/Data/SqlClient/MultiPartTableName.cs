using System;
using System.Data.Common;

namespace System.Data.SqlClient
{
	// Token: 0x02000219 RID: 537
	internal struct MultiPartTableName
	{
		// Token: 0x060018E4 RID: 6372 RVA: 0x0007DB71 File Offset: 0x0007BD71
		internal MultiPartTableName(string[] parts)
		{
			this._multipartName = null;
			this._serverName = parts[0];
			this._catalogName = parts[1];
			this._schemaName = parts[2];
			this._tableName = parts[3];
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x0007DB9E File Offset: 0x0007BD9E
		internal MultiPartTableName(string multipartName)
		{
			this._multipartName = multipartName;
			this._serverName = null;
			this._catalogName = null;
			this._schemaName = null;
			this._tableName = null;
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x060018E6 RID: 6374 RVA: 0x0007DBC3 File Offset: 0x0007BDC3
		// (set) Token: 0x060018E7 RID: 6375 RVA: 0x0007DBD1 File Offset: 0x0007BDD1
		internal string ServerName
		{
			get
			{
				this.ParseMultipartName();
				return this._serverName;
			}
			set
			{
				this._serverName = value;
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x060018E8 RID: 6376 RVA: 0x0007DBDA File Offset: 0x0007BDDA
		// (set) Token: 0x060018E9 RID: 6377 RVA: 0x0007DBE8 File Offset: 0x0007BDE8
		internal string CatalogName
		{
			get
			{
				this.ParseMultipartName();
				return this._catalogName;
			}
			set
			{
				this._catalogName = value;
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x060018EA RID: 6378 RVA: 0x0007DBF1 File Offset: 0x0007BDF1
		// (set) Token: 0x060018EB RID: 6379 RVA: 0x0007DBFF File Offset: 0x0007BDFF
		internal string SchemaName
		{
			get
			{
				this.ParseMultipartName();
				return this._schemaName;
			}
			set
			{
				this._schemaName = value;
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x060018EC RID: 6380 RVA: 0x0007DC08 File Offset: 0x0007BE08
		// (set) Token: 0x060018ED RID: 6381 RVA: 0x0007DC16 File Offset: 0x0007BE16
		internal string TableName
		{
			get
			{
				this.ParseMultipartName();
				return this._tableName;
			}
			set
			{
				this._tableName = value;
			}
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x0007DC20 File Offset: 0x0007BE20
		private void ParseMultipartName()
		{
			if (this._multipartName != null)
			{
				string[] array = MultipartIdentifier.ParseMultipartIdentifier(this._multipartName, "[\"", "]\"", "Processing of results from SQL Server failed because of an invalid multipart name", false);
				this._serverName = array[0];
				this._catalogName = array[1];
				this._schemaName = array[2];
				this._tableName = array[3];
				this._multipartName = null;
			}
		}

		// Token: 0x04001209 RID: 4617
		private string _multipartName;

		// Token: 0x0400120A RID: 4618
		private string _serverName;

		// Token: 0x0400120B RID: 4619
		private string _catalogName;

		// Token: 0x0400120C RID: 4620
		private string _schemaName;

		// Token: 0x0400120D RID: 4621
		private string _tableName;

		// Token: 0x0400120E RID: 4622
		internal static readonly MultiPartTableName Null = new MultiPartTableName(new string[4]);
	}
}
