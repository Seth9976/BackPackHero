using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace System.Data.Common
{
	/// <summary>Represents a set of static methods for creating one or more instances of <see cref="T:System.Data.Common.DbProviderFactory" /> classes.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000383 RID: 899
	public static class DbProviderFactories
	{
		/// <summary>Returns an instance of a <see cref="T:System.Data.Common.DbProviderFactory" />.</summary>
		/// <returns>An instance of a <see cref="T:System.Data.Common.DbProviderFactory" /> for a specified provider name.</returns>
		/// <param name="providerInvariantName">Invariant name of a provider.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002B45 RID: 11077 RVA: 0x000BF1FC File Offset: 0x000BD3FC
		public static DbProviderFactory GetFactory(string providerInvariantName)
		{
			return DbProviderFactories.GetFactory(providerInvariantName, true);
		}

		// Token: 0x06002B46 RID: 11078 RVA: 0x000BF208 File Offset: 0x000BD408
		public static DbProviderFactory GetFactory(string providerInvariantName, bool throwOnError)
		{
			if (throwOnError)
			{
				ADP.CheckArgumentLength(providerInvariantName, "providerInvariantName");
			}
			DataTable providerTable = DbProviderFactories.GetProviderTable();
			if (providerTable != null)
			{
				DataRow dataRow = providerTable.Rows.Find(providerInvariantName);
				if (dataRow != null)
				{
					return DbProviderFactories.GetFactory(dataRow);
				}
			}
			if (throwOnError)
			{
				throw ADP.ConfigProviderNotFound();
			}
			return null;
		}

		/// <summary>Returns an instance of a <see cref="T:System.Data.Common.DbProviderFactory" />.</summary>
		/// <returns>An instance of a <see cref="T:System.Data.Common.DbProviderFactory" /> for a specified <see cref="T:System.Data.DataRow" />.</returns>
		/// <param name="providerRow">
		///   <see cref="T:System.Data.DataRow" /> containing the provider's configuration information.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002B47 RID: 11079 RVA: 0x000BF250 File Offset: 0x000BD450
		public static DbProviderFactory GetFactory(DataRow providerRow)
		{
			ADP.CheckArgumentNull(providerRow, "providerRow");
			DataColumn dataColumn = providerRow.Table.Columns["AssemblyQualifiedName"];
			if (dataColumn != null)
			{
				string text = providerRow[dataColumn] as string;
				if (!ADP.IsEmpty(text))
				{
					Type type = Type.GetType(text);
					if (null != type)
					{
						FieldInfo field = type.GetField("Instance", BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public);
						if (null != field && field.FieldType.IsSubclassOf(typeof(DbProviderFactory)))
						{
							object value = field.GetValue(null);
							if (value != null)
							{
								return (DbProviderFactory)value;
							}
						}
						throw ADP.ConfigProviderInvalid();
					}
					throw ADP.ConfigProviderNotInstalled();
				}
			}
			throw ADP.ConfigProviderMissing();
		}

		/// <summary>Returns an instance of a <see cref="T:System.Data.Common.DbProviderFactory" />.</summary>
		/// <returns>An instance of a <see cref="T:System.Data.Common.DbProviderFactory" /> for a specified connection.</returns>
		/// <param name="connection">The connection used.</param>
		// Token: 0x06002B48 RID: 11080 RVA: 0x000BF2FA File Offset: 0x000BD4FA
		public static DbProviderFactory GetFactory(DbConnection connection)
		{
			ADP.CheckArgumentNull(connection, "connection");
			return connection.ProviderFactory;
		}

		/// <summary>Returns a <see cref="T:System.Data.DataTable" /> that contains information about all installed providers that implement <see cref="T:System.Data.Common.DbProviderFactory" />.</summary>
		/// <returns>Returns a <see cref="T:System.Data.DataTable" /> containing <see cref="T:System.Data.DataRow" /> objects that contain the following data. Column ordinalColumn nameDescription0NameHuman-readable name for the data provider.1DescriptionHuman-readable description of the data provider.2InvariantNameName that can be used programmatically to refer to the data provider.3AssemblyQualifiedNameFully qualified name of the factory class, which contains enough information to instantiate the object.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002B49 RID: 11081 RVA: 0x000BF310 File Offset: 0x000BD510
		public static DataTable GetFactoryClasses()
		{
			DataTable dataTable = DbProviderFactories.GetProviderTable();
			if (dataTable != null)
			{
				dataTable = dataTable.Copy();
			}
			else
			{
				dataTable = DbProviderFactoriesConfigurationHandler.CreateProviderDataTable();
			}
			return dataTable;
		}

		// Token: 0x06002B4A RID: 11082 RVA: 0x000BF338 File Offset: 0x000BD538
		private static DataTable IncludeFrameworkFactoryClasses(DataTable configDataTable)
		{
			DataTable dataTable = DbProviderFactoriesConfigurationHandler.CreateProviderDataTable();
			string text = typeof(SqlClientFactory).AssemblyQualifiedName.ToString().Replace("System.Data.SqlClient.SqlClientFactory, System.Data,", "System.Data.OracleClient.OracleClientFactory, System.Data.OracleClient,");
			DbProviderFactoryConfigSection[] array = new DbProviderFactoryConfigSection[]
			{
				new DbProviderFactoryConfigSection(typeof(OdbcFactory), "Odbc Data Provider", ".Net Framework Data Provider for Odbc"),
				new DbProviderFactoryConfigSection(typeof(OleDbFactory), "OleDb Data Provider", ".Net Framework Data Provider for OleDb"),
				new DbProviderFactoryConfigSection("OracleClient Data Provider", "System.Data.OracleClient", ".Net Framework Data Provider for Oracle", text),
				new DbProviderFactoryConfigSection(typeof(SqlClientFactory), "SqlClient Data Provider", ".Net Framework Data Provider for SqlServer")
			};
			for (int i = 0; i < array.Length; i++)
			{
				if (!array[i].IsNull())
				{
					bool flag = false;
					if (i == 2)
					{
						Type type = Type.GetType(array[i].AssemblyQualifiedName);
						if (type != null)
						{
							FieldInfo field = type.GetField("Instance", BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public);
							if (null != field && field.FieldType.IsSubclassOf(typeof(DbProviderFactory)))
							{
								object value = field.GetValue(null);
								if (value != null)
								{
									flag = true;
								}
							}
						}
					}
					else
					{
						flag = true;
					}
					if (flag)
					{
						DataRow dataRow = dataTable.NewRow();
						dataRow["Name"] = array[i].Name;
						dataRow["InvariantName"] = array[i].InvariantName;
						dataRow["Description"] = array[i].Description;
						dataRow["AssemblyQualifiedName"] = array[i].AssemblyQualifiedName;
						dataTable.Rows.Add(dataRow);
					}
				}
			}
			int num = 0;
			while (configDataTable != null && num < configDataTable.Rows.Count)
			{
				try
				{
					bool flag2 = false;
					if (configDataTable.Rows[num]["AssemblyQualifiedName"].ToString().ToLowerInvariant().Contains("System.Data.OracleClient".ToString().ToLowerInvariant()))
					{
						Type type2 = Type.GetType(configDataTable.Rows[num]["AssemblyQualifiedName"].ToString());
						if (type2 != null)
						{
							FieldInfo field2 = type2.GetField("Instance", BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public);
							if (null != field2 && field2.FieldType.IsSubclassOf(typeof(DbProviderFactory)))
							{
								object value2 = field2.GetValue(null);
								if (value2 != null)
								{
									flag2 = true;
								}
							}
						}
					}
					else
					{
						flag2 = true;
					}
					if (flag2)
					{
						dataTable.Rows.Add(configDataTable.Rows[num].ItemArray);
					}
				}
				catch (ConstraintException)
				{
				}
				num++;
			}
			return dataTable;
		}

		// Token: 0x06002B4B RID: 11083 RVA: 0x000BF5E0 File Offset: 0x000BD7E0
		private static DataTable GetProviderTable()
		{
			DbProviderFactories.Initialize();
			return DbProviderFactories._providerTable;
		}

		// Token: 0x06002B4C RID: 11084 RVA: 0x000BF5EC File Offset: 0x000BD7EC
		private static void Initialize()
		{
			if (ConnectionState.Open != DbProviderFactories._initState)
			{
				object lockobj = DbProviderFactories._lockobj;
				lock (lockobj)
				{
					ConnectionState initState = DbProviderFactories._initState;
					if (initState != ConnectionState.Closed)
					{
						if (initState - ConnectionState.Open > 1)
						{
						}
					}
					else
					{
						DbProviderFactories._initState = ConnectionState.Connecting;
						try
						{
							DataSet dataSet = global::System.Configuration.PrivilegedConfigurationManager.GetSection("system.data") as DataSet;
							DbProviderFactories._providerTable = ((dataSet != null) ? DbProviderFactories.IncludeFrameworkFactoryClasses(dataSet.Tables["DbProviderFactories"]) : DbProviderFactories.IncludeFrameworkFactoryClasses(null));
						}
						finally
						{
							DbProviderFactories._initState = ConnectionState.Open;
						}
					}
				}
			}
		}

		// Token: 0x06002B4D RID: 11085 RVA: 0x000BF690 File Offset: 0x000BD890
		public static bool TryGetFactory(string providerInvariantName, out DbProviderFactory factory)
		{
			factory = DbProviderFactories.GetFactory(providerInvariantName, false);
			return factory != null;
		}

		// Token: 0x06002B4E RID: 11086 RVA: 0x000BF6A0 File Offset: 0x000BD8A0
		public static IEnumerable<string> GetProviderInvariantNames()
		{
			return DbProviderFactories._registeredFactories.Keys.ToList<string>();
		}

		// Token: 0x06002B4F RID: 11087 RVA: 0x000BF6B1 File Offset: 0x000BD8B1
		public static void RegisterFactory(string providerInvariantName, string factoryTypeAssemblyQualifiedName)
		{
			ADP.CheckArgumentLength(providerInvariantName, "providerInvariantName");
			ADP.CheckArgumentLength(factoryTypeAssemblyQualifiedName, "factoryTypeAssemblyQualifiedName");
			DbProviderFactories._registeredFactories[providerInvariantName] = new DbProviderFactories.ProviderRegistration(factoryTypeAssemblyQualifiedName, null);
		}

		// Token: 0x06002B50 RID: 11088 RVA: 0x000BF6DC File Offset: 0x000BD8DC
		private static DbProviderFactory GetFactoryInstance(Type providerFactoryClass)
		{
			ADP.CheckArgumentNull(providerFactoryClass, "providerFactoryClass");
			if (!providerFactoryClass.IsSubclassOf(typeof(DbProviderFactory)))
			{
				throw ADP.Argument(SR.Format("The type '{0}' doesn't inherit from DbProviderFactory.", providerFactoryClass.FullName));
			}
			FieldInfo field = providerFactoryClass.GetField("Instance", BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public);
			if (null == field)
			{
				throw ADP.InvalidOperation("The requested .NET Data Provider's implementation does not have an Instance field of a System.Data.Common.DbProviderFactory derived type.");
			}
			if (!field.FieldType.IsSubclassOf(typeof(DbProviderFactory)))
			{
				throw ADP.InvalidOperation("The requested .NET Data Provider's implementation does not have an Instance field of a System.Data.Common.DbProviderFactory derived type.");
			}
			object value = field.GetValue(null);
			if (value == null)
			{
				throw ADP.InvalidOperation("The requested .NET Data Provider's implementation does not have an Instance field of a System.Data.Common.DbProviderFactory derived type.");
			}
			return (DbProviderFactory)value;
		}

		// Token: 0x06002B51 RID: 11089 RVA: 0x000BF77C File Offset: 0x000BD97C
		public static void RegisterFactory(string providerInvariantName, Type providerFactoryClass)
		{
			DbProviderFactories.RegisterFactory(providerInvariantName, DbProviderFactories.GetFactoryInstance(providerFactoryClass));
		}

		// Token: 0x06002B52 RID: 11090 RVA: 0x000BF78A File Offset: 0x000BD98A
		public static void RegisterFactory(string providerInvariantName, DbProviderFactory factory)
		{
			ADP.CheckArgumentLength(providerInvariantName, "providerInvariantName");
			ADP.CheckArgumentNull(factory, "factory");
			DbProviderFactories._registeredFactories[providerInvariantName] = new DbProviderFactories.ProviderRegistration(factory.GetType().AssemblyQualifiedName, factory);
		}

		// Token: 0x06002B53 RID: 11091 RVA: 0x000BF7C0 File Offset: 0x000BD9C0
		public static bool UnregisterFactory(string providerInvariantName)
		{
			DbProviderFactories.ProviderRegistration providerRegistration;
			return !string.IsNullOrWhiteSpace(providerInvariantName) && DbProviderFactories._registeredFactories.TryRemove(providerInvariantName, out providerRegistration);
		}

		// Token: 0x04001A63 RID: 6755
		private const string AssemblyQualifiedName = "AssemblyQualifiedName";

		// Token: 0x04001A64 RID: 6756
		private const string Instance = "Instance";

		// Token: 0x04001A65 RID: 6757
		private const string InvariantName = "InvariantName";

		// Token: 0x04001A66 RID: 6758
		private const string Name = "Name";

		// Token: 0x04001A67 RID: 6759
		private const string Description = "Description";

		// Token: 0x04001A68 RID: 6760
		private const string InstanceFieldName = "Instance";

		// Token: 0x04001A69 RID: 6761
		private static ConcurrentDictionary<string, DbProviderFactories.ProviderRegistration> _registeredFactories = new ConcurrentDictionary<string, DbProviderFactories.ProviderRegistration>();

		// Token: 0x04001A6A RID: 6762
		private static ConnectionState _initState;

		// Token: 0x04001A6B RID: 6763
		private static DataTable _providerTable;

		// Token: 0x04001A6C RID: 6764
		private static object _lockobj = new object();

		// Token: 0x02000384 RID: 900
		private struct ProviderRegistration
		{
			// Token: 0x06002B55 RID: 11093 RVA: 0x000BF7FA File Offset: 0x000BD9FA
			internal ProviderRegistration(string factoryTypeAssemblyQualifiedName, DbProviderFactory factoryInstance)
			{
				this.FactoryTypeAssemblyQualifiedName = factoryTypeAssemblyQualifiedName;
				this.FactoryInstance = factoryInstance;
			}

			// Token: 0x17000730 RID: 1840
			// (get) Token: 0x06002B56 RID: 11094 RVA: 0x000BF80A File Offset: 0x000BDA0A
			internal readonly string FactoryTypeAssemblyQualifiedName { get; }

			// Token: 0x17000731 RID: 1841
			// (get) Token: 0x06002B57 RID: 11095 RVA: 0x000BF812 File Offset: 0x000BDA12
			internal readonly DbProviderFactory FactoryInstance { get; }
		}
	}
}
