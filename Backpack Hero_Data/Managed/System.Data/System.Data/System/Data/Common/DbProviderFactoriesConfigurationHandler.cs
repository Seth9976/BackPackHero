using System;
using System.Configuration;
using System.Globalization;
using System.Xml;

namespace System.Data.Common
{
	/// <summary>This type supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000387 RID: 903
	public class DbProviderFactoriesConfigurationHandler : IConfigurationSectionHandler
	{
		/// <summary>This type supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <returns>This type supports the .NET Framework infrastructure and is not intended to be used directly from your code.</returns>
		/// <param name="parent">This type supports the .NET Framework infrastructure and is not intended to be used directly from your code.</param>
		/// <param name="configContext">This type supports the .NET Framework infrastructure and is not intended to be used directly from your code.</param>
		/// <param name="section">This type supports the .NET Framework infrastructure and is not intended to be used directly from your code.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002B60 RID: 11104 RVA: 0x000BF929 File Offset: 0x000BDB29
		public virtual object Create(object parent, object configContext, XmlNode section)
		{
			return DbProviderFactoriesConfigurationHandler.CreateStatic(parent, configContext, section);
		}

		// Token: 0x06002B61 RID: 11105 RVA: 0x000BF934 File Offset: 0x000BDB34
		internal static object CreateStatic(object parent, object configContext, XmlNode section)
		{
			object obj = parent;
			if (section != null)
			{
				obj = HandlerBase.CloneParent(parent as DataSet, false);
				bool flag = false;
				HandlerBase.CheckForUnrecognizedAttributes(section);
				foreach (object obj2 in section.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj2;
					if (!HandlerBase.IsIgnorableAlsoCheckForNonElement(xmlNode))
					{
						string name = xmlNode.Name;
						if (!(name == "DbProviderFactories"))
						{
							throw ADP.ConfigUnrecognizedElement(xmlNode);
						}
						if (flag)
						{
							throw ADP.ConfigSectionsUnique("DbProviderFactories");
						}
						flag = true;
						DbProviderFactoriesConfigurationHandler.HandleProviders(obj as DataSet, configContext, xmlNode, name);
					}
				}
			}
			return obj;
		}

		// Token: 0x06002B62 RID: 11106 RVA: 0x000BF9F0 File Offset: 0x000BDBF0
		private static void HandleProviders(DataSet config, object configContext, XmlNode section, string sectionName)
		{
			DataTableCollection tables = config.Tables;
			DataTable dataTable = tables[sectionName];
			bool flag = dataTable != null;
			dataTable = DbProviderFactoriesConfigurationHandler.DbProviderDictionarySectionHandler.CreateStatic(dataTable, configContext, section);
			if (!flag)
			{
				tables.Add(dataTable);
			}
		}

		// Token: 0x06002B63 RID: 11107 RVA: 0x000BFA24 File Offset: 0x000BDC24
		internal static DataTable CreateProviderDataTable()
		{
			DataColumn dataColumn = new DataColumn("Name", typeof(string));
			dataColumn.ReadOnly = true;
			DataColumn dataColumn2 = new DataColumn("Description", typeof(string));
			dataColumn2.ReadOnly = true;
			DataColumn dataColumn3 = new DataColumn("InvariantName", typeof(string));
			dataColumn3.ReadOnly = true;
			DataColumn dataColumn4 = new DataColumn("AssemblyQualifiedName", typeof(string));
			dataColumn4.ReadOnly = true;
			DataColumn[] array = new DataColumn[] { dataColumn3 };
			DataColumn[] array2 = new DataColumn[] { dataColumn, dataColumn2, dataColumn3, dataColumn4 };
			DataTable dataTable = new DataTable("DbProviderFactories");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.AddRange(array2);
			dataTable.PrimaryKey = array;
			return dataTable;
		}

		// Token: 0x04001A7A RID: 6778
		internal const string sectionName = "system.data";

		// Token: 0x04001A7B RID: 6779
		internal const string providerGroup = "DbProviderFactories";

		// Token: 0x04001A7C RID: 6780
		internal const string odbcProviderName = "Odbc Data Provider";

		// Token: 0x04001A7D RID: 6781
		internal const string odbcProviderDescription = ".Net Framework Data Provider for Odbc";

		// Token: 0x04001A7E RID: 6782
		internal const string oledbProviderName = "OleDb Data Provider";

		// Token: 0x04001A7F RID: 6783
		internal const string oledbProviderDescription = ".Net Framework Data Provider for OleDb";

		// Token: 0x04001A80 RID: 6784
		internal const string oracleclientProviderName = "OracleClient Data Provider";

		// Token: 0x04001A81 RID: 6785
		internal const string oracleclientProviderNamespace = "System.Data.OracleClient";

		// Token: 0x04001A82 RID: 6786
		internal const string oracleclientProviderDescription = ".Net Framework Data Provider for Oracle";

		// Token: 0x04001A83 RID: 6787
		internal const string sqlclientProviderName = "SqlClient Data Provider";

		// Token: 0x04001A84 RID: 6788
		internal const string sqlclientProviderDescription = ".Net Framework Data Provider for SqlServer";

		// Token: 0x04001A85 RID: 6789
		internal const string sqlclientPartialAssemblyQualifiedName = "System.Data.SqlClient.SqlClientFactory, System.Data,";

		// Token: 0x04001A86 RID: 6790
		internal const string oracleclientPartialAssemblyQualifiedName = "System.Data.OracleClient.OracleClientFactory, System.Data.OracleClient,";

		// Token: 0x02000388 RID: 904
		private static class DbProviderDictionarySectionHandler
		{
			// Token: 0x06002B64 RID: 11108 RVA: 0x000BFAF0 File Offset: 0x000BDCF0
			internal static DataTable CreateStatic(DataTable config, object context, XmlNode section)
			{
				if (section != null)
				{
					HandlerBase.CheckForUnrecognizedAttributes(section);
					if (config == null)
					{
						config = DbProviderFactoriesConfigurationHandler.CreateProviderDataTable();
					}
					foreach (object obj in section.ChildNodes)
					{
						XmlNode xmlNode = (XmlNode)obj;
						if (!HandlerBase.IsIgnorableAlsoCheckForNonElement(xmlNode))
						{
							string name = xmlNode.Name;
							if (!(name == "add"))
							{
								if (!(name == "remove"))
								{
									if (!(name == "clear"))
									{
										throw ADP.ConfigUnrecognizedElement(xmlNode);
									}
									DbProviderFactoriesConfigurationHandler.DbProviderDictionarySectionHandler.HandleClear(xmlNode, config);
								}
								else
								{
									DbProviderFactoriesConfigurationHandler.DbProviderDictionarySectionHandler.HandleRemove(xmlNode, config);
								}
							}
							else
							{
								DbProviderFactoriesConfigurationHandler.DbProviderDictionarySectionHandler.HandleAdd(xmlNode, config);
							}
						}
					}
					config.AcceptChanges();
				}
				return config;
			}

			// Token: 0x06002B65 RID: 11109 RVA: 0x000BFBBC File Offset: 0x000BDDBC
			private static void HandleAdd(XmlNode child, DataTable config)
			{
				HandlerBase.CheckForChildNodes(child);
				DataRow dataRow = config.NewRow();
				dataRow[0] = HandlerBase.RemoveAttribute(child, "name", true, false);
				dataRow[1] = HandlerBase.RemoveAttribute(child, "description", true, false);
				dataRow[2] = HandlerBase.RemoveAttribute(child, "invariant", true, false);
				dataRow[3] = HandlerBase.RemoveAttribute(child, "type", true, false);
				HandlerBase.RemoveAttribute(child, "support", false, false);
				HandlerBase.CheckForUnrecognizedAttributes(child);
				config.Rows.Add(dataRow);
			}

			// Token: 0x06002B66 RID: 11110 RVA: 0x000BFC48 File Offset: 0x000BDE48
			private static void HandleRemove(XmlNode child, DataTable config)
			{
				HandlerBase.CheckForChildNodes(child);
				string text = HandlerBase.RemoveAttribute(child, "invariant", true, false);
				HandlerBase.CheckForUnrecognizedAttributes(child);
				DataRow dataRow = config.Rows.Find(text);
				if (dataRow != null)
				{
					dataRow.Delete();
				}
			}

			// Token: 0x06002B67 RID: 11111 RVA: 0x000BFC85 File Offset: 0x000BDE85
			private static void HandleClear(XmlNode child, DataTable config)
			{
				HandlerBase.CheckForChildNodes(child);
				HandlerBase.CheckForUnrecognizedAttributes(child);
				config.Clear();
			}
		}
	}
}
