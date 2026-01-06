using System;
using System.Globalization;

namespace System.Data.SqlClient
{
	// Token: 0x020001BC RID: 444
	internal sealed class ServerInfo
	{
		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06001588 RID: 5512 RVA: 0x0006AAD8 File Offset: 0x00068CD8
		// (set) Token: 0x06001589 RID: 5513 RVA: 0x0006AAE0 File Offset: 0x00068CE0
		internal string ExtendedServerName { get; private set; }

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x0600158A RID: 5514 RVA: 0x0006AAE9 File Offset: 0x00068CE9
		// (set) Token: 0x0600158B RID: 5515 RVA: 0x0006AAF1 File Offset: 0x00068CF1
		internal string ResolvedServerName { get; private set; }

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x0600158C RID: 5516 RVA: 0x0006AAFA File Offset: 0x00068CFA
		// (set) Token: 0x0600158D RID: 5517 RVA: 0x0006AB02 File Offset: 0x00068D02
		internal string ResolvedDatabaseName { get; private set; }

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x0600158E RID: 5518 RVA: 0x0006AB0B File Offset: 0x00068D0B
		// (set) Token: 0x0600158F RID: 5519 RVA: 0x0006AB13 File Offset: 0x00068D13
		internal string UserProtocol { get; private set; }

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06001590 RID: 5520 RVA: 0x0006AB1C File Offset: 0x00068D1C
		// (set) Token: 0x06001591 RID: 5521 RVA: 0x0006AB24 File Offset: 0x00068D24
		internal string UserServerName
		{
			get
			{
				return this._userServerName;
			}
			private set
			{
				this._userServerName = value;
			}
		}

		// Token: 0x06001592 RID: 5522 RVA: 0x0006AB2D File Offset: 0x00068D2D
		internal ServerInfo(SqlConnectionString userOptions)
			: this(userOptions, userOptions.DataSource)
		{
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x0006AB3C File Offset: 0x00068D3C
		internal ServerInfo(SqlConnectionString userOptions, string serverName)
		{
			this.UserServerName = serverName ?? string.Empty;
			this.UserProtocol = string.Empty;
			this.ResolvedDatabaseName = userOptions.InitialCatalog;
			this.PreRoutingServerName = null;
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x0006AB74 File Offset: 0x00068D74
		internal ServerInfo(SqlConnectionString userOptions, RoutingInfo routing, string preRoutingServerName)
		{
			if (routing == null || routing.ServerName == null)
			{
				this.UserServerName = string.Empty;
			}
			else
			{
				this.UserServerName = string.Format(CultureInfo.InvariantCulture, "{0},{1}", routing.ServerName, routing.Port);
			}
			this.PreRoutingServerName = preRoutingServerName;
			this.UserProtocol = "tcp";
			this.SetDerivedNames(this.UserProtocol, this.UserServerName);
			this.ResolvedDatabaseName = userOptions.InitialCatalog;
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x0006ABF5 File Offset: 0x00068DF5
		internal void SetDerivedNames(string protocol, string serverName)
		{
			if (!string.IsNullOrEmpty(protocol))
			{
				this.ExtendedServerName = protocol + ":" + serverName;
			}
			else
			{
				this.ExtendedServerName = serverName;
			}
			this.ResolvedServerName = serverName;
		}

		// Token: 0x04000E7A RID: 3706
		private string _userServerName;

		// Token: 0x04000E7B RID: 3707
		internal readonly string PreRoutingServerName;
	}
}
