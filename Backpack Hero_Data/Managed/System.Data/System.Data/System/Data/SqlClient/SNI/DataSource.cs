using System;
using System.IO;
using System.Text;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x0200024B RID: 587
	internal class DataSource
	{
		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06001AC9 RID: 6857 RVA: 0x0008580B File Offset: 0x00083A0B
		// (set) Token: 0x06001ACA RID: 6858 RVA: 0x00085813 File Offset: 0x00083A13
		internal string ServerName { get; private set; }

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06001ACB RID: 6859 RVA: 0x0008581C File Offset: 0x00083A1C
		// (set) Token: 0x06001ACC RID: 6860 RVA: 0x00085824 File Offset: 0x00083A24
		internal int Port { get; private set; } = -1;

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06001ACD RID: 6861 RVA: 0x0008582D File Offset: 0x00083A2D
		// (set) Token: 0x06001ACE RID: 6862 RVA: 0x00085835 File Offset: 0x00083A35
		public string InstanceName { get; internal set; }

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06001ACF RID: 6863 RVA: 0x0008583E File Offset: 0x00083A3E
		// (set) Token: 0x06001AD0 RID: 6864 RVA: 0x00085846 File Offset: 0x00083A46
		public string PipeName { get; internal set; }

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06001AD1 RID: 6865 RVA: 0x0008584F File Offset: 0x00083A4F
		// (set) Token: 0x06001AD2 RID: 6866 RVA: 0x00085857 File Offset: 0x00083A57
		public string PipeHostName { get; internal set; }

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06001AD3 RID: 6867 RVA: 0x00085860 File Offset: 0x00083A60
		// (set) Token: 0x06001AD4 RID: 6868 RVA: 0x00085868 File Offset: 0x00083A68
		internal bool IsBadDataSource { get; private set; }

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001AD5 RID: 6869 RVA: 0x00085871 File Offset: 0x00083A71
		// (set) Token: 0x06001AD6 RID: 6870 RVA: 0x00085879 File Offset: 0x00083A79
		internal bool IsSsrpRequired { get; private set; }

		// Token: 0x06001AD7 RID: 6871 RVA: 0x00085884 File Offset: 0x00083A84
		private DataSource(string dataSource)
		{
			this._workingDataSource = dataSource.Trim().ToLowerInvariant();
			int num = this._workingDataSource.IndexOf(':');
			this.PopulateProtocol();
			this._dataSourceAfterTrimmingProtocol = ((num > -1 && this.ConnectionProtocol != DataSource.Protocol.None) ? this._workingDataSource.Substring(num + 1).Trim() : this._workingDataSource);
			if (this._dataSourceAfterTrimmingProtocol.Contains("/"))
			{
				if (this.ConnectionProtocol == DataSource.Protocol.None)
				{
					this.ReportSNIError(SNIProviders.INVALID_PROV);
					return;
				}
				if (this.ConnectionProtocol == DataSource.Protocol.NP)
				{
					this.ReportSNIError(SNIProviders.NP_PROV);
					return;
				}
				if (this.ConnectionProtocol == DataSource.Protocol.TCP)
				{
					this.ReportSNIError(SNIProviders.TCP_PROV);
				}
			}
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x0008593C File Offset: 0x00083B3C
		private void PopulateProtocol()
		{
			string[] array = this._workingDataSource.Split(':', StringSplitOptions.None);
			if (array.Length <= 1)
			{
				this.ConnectionProtocol = DataSource.Protocol.None;
				return;
			}
			string text = array[0].Trim();
			if (text == "tcp")
			{
				this.ConnectionProtocol = DataSource.Protocol.TCP;
				return;
			}
			if (text == "np")
			{
				this.ConnectionProtocol = DataSource.Protocol.NP;
				return;
			}
			if (!(text == "admin"))
			{
				this.ConnectionProtocol = DataSource.Protocol.None;
				return;
			}
			this.ConnectionProtocol = DataSource.Protocol.Admin;
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x000859B8 File Offset: 0x00083BB8
		public static string GetLocalDBInstance(string dataSource, out bool error)
		{
			string text = null;
			string[] array = dataSource.ToLowerInvariant().Split('\\', StringSplitOptions.None);
			error = false;
			if (array.Length == 2 && "(localdb)".Equals(array[0].TrimStart()))
			{
				if (string.IsNullOrWhiteSpace(array[1]))
				{
					SNILoadHandle.SingletonInstance.LastError = new SNIError(SNIProviders.INVALID_PROV, 0U, 51U, string.Empty);
					error = true;
					return null;
				}
				text = array[1].Trim();
			}
			return text;
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x00085A28 File Offset: 0x00083C28
		public static DataSource ParseServerName(string dataSource)
		{
			DataSource dataSource2 = new DataSource(dataSource);
			if (dataSource2.IsBadDataSource)
			{
				return null;
			}
			if (dataSource2.InferNamedPipesInformation())
			{
				return dataSource2;
			}
			if (dataSource2.IsBadDataSource)
			{
				return null;
			}
			if (dataSource2.InferConnectionDetails())
			{
				return dataSource2;
			}
			return null;
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x00085A65 File Offset: 0x00083C65
		private void InferLocalServerName()
		{
			if (string.IsNullOrEmpty(this.ServerName) || DataSource.IsLocalHost(this.ServerName))
			{
				this.ServerName = ((this.ConnectionProtocol == DataSource.Protocol.Admin) ? Environment.MachineName : "localhost");
			}
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x00085A9C File Offset: 0x00083C9C
		private bool InferConnectionDetails()
		{
			string[] array = this._dataSourceAfterTrimmingProtocol.Split(new char[] { '\\', ',' });
			this.ServerName = array[0].Trim();
			int num = this._dataSourceAfterTrimmingProtocol.IndexOf(',');
			int num2 = this._dataSourceAfterTrimmingProtocol.IndexOf('\\');
			if (num > -1)
			{
				string text = ((num2 > -1) ? ((num > num2) ? array[2].Trim() : array[1].Trim()) : array[1].Trim());
				if (string.IsNullOrEmpty(text))
				{
					this.ReportSNIError(SNIProviders.INVALID_PROV);
					return false;
				}
				if (this.ConnectionProtocol == DataSource.Protocol.None)
				{
					this.ConnectionProtocol = DataSource.Protocol.TCP;
				}
				else if (this.ConnectionProtocol != DataSource.Protocol.TCP)
				{
					this.ReportSNIError(SNIProviders.INVALID_PROV);
					return false;
				}
				int num3;
				if (!int.TryParse(text, out num3))
				{
					this.ReportSNIError(SNIProviders.TCP_PROV);
					return false;
				}
				if (num3 < 1)
				{
					this.ReportSNIError(SNIProviders.TCP_PROV);
					return false;
				}
				this.Port = num3;
			}
			else if (num2 > -1)
			{
				this.InstanceName = array[1].Trim();
				if (string.IsNullOrWhiteSpace(this.InstanceName))
				{
					this.ReportSNIError(SNIProviders.INVALID_PROV);
					return false;
				}
				if ("mssqlserver".Equals(this.InstanceName))
				{
					this.ReportSNIError(SNIProviders.INVALID_PROV);
					return false;
				}
				this.IsSsrpRequired = true;
			}
			this.InferLocalServerName();
			return true;
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x00085BCF File Offset: 0x00083DCF
		private void ReportSNIError(SNIProviders provider)
		{
			SNILoadHandle.SingletonInstance.LastError = new SNIError(provider, 0U, 25U, string.Empty);
			this.IsBadDataSource = true;
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x00085BF0 File Offset: 0x00083DF0
		private bool InferNamedPipesInformation()
		{
			if (!this._dataSourceAfterTrimmingProtocol.StartsWith("\\\\") && this.ConnectionProtocol != DataSource.Protocol.NP)
			{
				return false;
			}
			if (!this._dataSourceAfterTrimmingProtocol.Contains('\\'))
			{
				this.PipeHostName = (this.ServerName = this._dataSourceAfterTrimmingProtocol);
				this.InferLocalServerName();
				this.PipeName = "sql\\query";
				return true;
			}
			try
			{
				string[] array = this._dataSourceAfterTrimmingProtocol.Split('\\', StringSplitOptions.None);
				if (array.Length < 6)
				{
					this.ReportSNIError(SNIProviders.NP_PROV);
					return false;
				}
				string text = array[2];
				if (string.IsNullOrEmpty(text))
				{
					this.ReportSNIError(SNIProviders.NP_PROV);
					return false;
				}
				if (!"pipe".Equals(array[3]))
				{
					this.ReportSNIError(SNIProviders.NP_PROV);
					return false;
				}
				if (array[4].StartsWith("mssql$"))
				{
					this.InstanceName = array[4].Substring("mssql$".Length);
				}
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 4; i < array.Length - 1; i++)
				{
					stringBuilder.Append(array[i]);
					stringBuilder.Append(Path.DirectorySeparatorChar);
				}
				stringBuilder.Append(array[array.Length - 1]);
				this.PipeName = stringBuilder.ToString();
				if (string.IsNullOrWhiteSpace(this.InstanceName) && !"sql\\query".Equals(this.PipeName))
				{
					this.InstanceName = "pipe" + this.PipeName;
				}
				this.ServerName = (DataSource.IsLocalHost(text) ? Environment.MachineName : text);
				this.PipeHostName = text;
			}
			catch (UriFormatException)
			{
				this.ReportSNIError(SNIProviders.NP_PROV);
				return false;
			}
			if (this.ConnectionProtocol == DataSource.Protocol.None)
			{
				this.ConnectionProtocol = DataSource.Protocol.NP;
			}
			else if (this.ConnectionProtocol != DataSource.Protocol.NP)
			{
				this.ReportSNIError(SNIProviders.NP_PROV);
				return false;
			}
			return true;
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x00085DCC File Offset: 0x00083FCC
		private static bool IsLocalHost(string serverName)
		{
			return ".".Equals(serverName) || "(local)".Equals(serverName) || "localhost".Equals(serverName);
		}

		// Token: 0x0400133C RID: 4924
		private const char CommaSeparator = ',';

		// Token: 0x0400133D RID: 4925
		private const char BackSlashSeparator = '\\';

		// Token: 0x0400133E RID: 4926
		private const string DefaultHostName = "localhost";

		// Token: 0x0400133F RID: 4927
		private const string DefaultSqlServerInstanceName = "mssqlserver";

		// Token: 0x04001340 RID: 4928
		private const string PipeBeginning = "\\\\";

		// Token: 0x04001341 RID: 4929
		private const string PipeToken = "pipe";

		// Token: 0x04001342 RID: 4930
		private const string LocalDbHost = "(localdb)";

		// Token: 0x04001343 RID: 4931
		private const string NamedPipeInstanceNameHeader = "mssql$";

		// Token: 0x04001344 RID: 4932
		private const string DefaultPipeName = "sql\\query";

		// Token: 0x04001345 RID: 4933
		internal DataSource.Protocol ConnectionProtocol = DataSource.Protocol.None;

		// Token: 0x0400134B RID: 4939
		private string _workingDataSource;

		// Token: 0x0400134C RID: 4940
		private string _dataSourceAfterTrimmingProtocol;

		// Token: 0x0200024C RID: 588
		internal enum Protocol
		{
			// Token: 0x04001350 RID: 4944
			TCP,
			// Token: 0x04001351 RID: 4945
			NP,
			// Token: 0x04001352 RID: 4946
			None,
			// Token: 0x04001353 RID: 4947
			Admin
		}
	}
}
