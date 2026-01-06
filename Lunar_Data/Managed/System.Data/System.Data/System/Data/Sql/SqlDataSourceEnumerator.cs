using System;
using System.Data.Common;
using System.Globalization;

namespace System.Data.Sql
{
	/// <summary>Provides a mechanism for enumerating all available instances of SQL Server within the local network.</summary>
	// Token: 0x02000126 RID: 294
	public sealed class SqlDataSourceEnumerator : DbDataSourceEnumerator
	{
		// Token: 0x06000FEB RID: 4075 RVA: 0x0004F2E7 File Offset: 0x0004D4E7
		private SqlDataSourceEnumerator()
		{
		}

		/// <summary>Gets an instance of the <see cref="T:System.Data.Sql.SqlDataSourceEnumerator" />, which can be used to retrieve information about available SQL Server instances.</summary>
		/// <returns>An instance of the <see cref="T:System.Data.Sql.SqlDataSourceEnumerator" /> used to retrieve information about available SQL Server instances.</returns>
		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000FEC RID: 4076 RVA: 0x0004F2EF File Offset: 0x0004D4EF
		public static SqlDataSourceEnumerator Instance
		{
			get
			{
				return SqlDataSourceEnumerator.SingletonInstance;
			}
		}

		/// <summary>Retrieves a <see cref="T:System.Data.DataTable" /> containing information about all visible SQL Server 2000 or SQL Server 2005 instances.</summary>
		/// <returns>Returns a <see cref="T:System.Data.DataTable" /> containing information about the visible SQL Server instances.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000FED RID: 4077 RVA: 0x0004F2F6 File Offset: 0x0004D4F6
		public override DataTable GetDataSources()
		{
			this.timeoutTime = 0L;
			throw new NotImplementedException();
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x0004F308 File Offset: 0x0004D508
		private static DataTable ParseServerEnumString(string serverInstances)
		{
			DataTable dataTable = new DataTable("SqlDataSources");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add("ServerName", typeof(string));
			dataTable.Columns.Add("InstanceName", typeof(string));
			dataTable.Columns.Add("IsClustered", typeof(string));
			dataTable.Columns.Add("Version", typeof(string));
			string text = null;
			string text2 = null;
			string text3 = null;
			string text4 = null;
			string[] array = serverInstances.Split('\0', StringSplitOptions.None);
			for (int i = 0; i < array.Length; i++)
			{
				string text5 = array[i].Trim('\0');
				if (text5.Length != 0)
				{
					foreach (string text6 in text5.Split(';', StringSplitOptions.None))
					{
						if (text == null)
						{
							foreach (string text7 in text6.Split('\\', StringSplitOptions.None))
							{
								if (text == null)
								{
									text = text7;
								}
								else
								{
									text2 = text7;
								}
							}
						}
						else if (text3 == null)
						{
							text3 = text6.Substring(SqlDataSourceEnumerator._clusterLength);
						}
						else
						{
							text4 = text6.Substring(SqlDataSourceEnumerator._versionLength);
						}
					}
					string text8 = "ServerName='" + text + "'";
					if (!ADP.IsEmpty(text2))
					{
						text8 = text8 + " AND InstanceName='" + text2 + "'";
					}
					if (dataTable.Select(text8).Length == 0)
					{
						DataRow dataRow = dataTable.NewRow();
						dataRow[0] = text;
						dataRow[1] = text2;
						dataRow[2] = text3;
						dataRow[3] = text4;
						dataTable.Rows.Add(dataRow);
					}
					text = null;
					text2 = null;
					text3 = null;
					text4 = null;
				}
			}
			foreach (object obj in dataTable.Columns)
			{
				((DataColumn)obj).ReadOnly = true;
			}
			return dataTable;
		}

		// Token: 0x04000A19 RID: 2585
		private static readonly SqlDataSourceEnumerator SingletonInstance = new SqlDataSourceEnumerator();

		// Token: 0x04000A1A RID: 2586
		internal const string ServerName = "ServerName";

		// Token: 0x04000A1B RID: 2587
		internal const string InstanceName = "InstanceName";

		// Token: 0x04000A1C RID: 2588
		internal const string IsClustered = "IsClustered";

		// Token: 0x04000A1D RID: 2589
		internal const string Version = "Version";

		// Token: 0x04000A1E RID: 2590
		private long timeoutTime;

		// Token: 0x04000A1F RID: 2591
		private static string _Version = "Version:";

		// Token: 0x04000A20 RID: 2592
		private static string _Cluster = "Clustered:";

		// Token: 0x04000A21 RID: 2593
		private static int _clusterLength = SqlDataSourceEnumerator._Cluster.Length;

		// Token: 0x04000A22 RID: 2594
		private static int _versionLength = SqlDataSourceEnumerator._Version.Length;
	}
}
