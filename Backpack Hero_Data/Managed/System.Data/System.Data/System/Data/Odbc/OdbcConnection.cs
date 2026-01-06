using System;
using System.ComponentModel;
using System.Data.Common;
using System.Data.ProviderBase;
using System.EnterpriseServices;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Transactions;
using Unity;

namespace System.Data.Odbc
{
	/// <summary>Represents an open connection to a data source. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000287 RID: 647
	public sealed class OdbcConnection : DbConnection, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcConnection" /> class with the specified connection string.</summary>
		/// <param name="connectionString">The connection used to open the data source. </param>
		// Token: 0x06001BCD RID: 7117 RVA: 0x00089B32 File Offset: 0x00087D32
		public OdbcConnection(string connectionString)
			: this()
		{
			this.ConnectionString = connectionString;
		}

		// Token: 0x06001BCE RID: 7118 RVA: 0x00089B41 File Offset: 0x00087D41
		private OdbcConnection(OdbcConnection connection)
			: this()
		{
			this.CopyFrom(connection);
			this._connectionTimeout = connection._connectionTimeout;
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06001BCF RID: 7119 RVA: 0x00089B5C File Offset: 0x00087D5C
		// (set) Token: 0x06001BD0 RID: 7120 RVA: 0x00089B64 File Offset: 0x00087D64
		internal OdbcConnectionHandle ConnectionHandle
		{
			get
			{
				return this._connectionHandle;
			}
			set
			{
				this._connectionHandle = value;
			}
		}

		/// <summary>Gets or sets the string used to open a data source.</summary>
		/// <returns>The ODBC driver connection string that includes settings, such as the data source name, needed to establish the initial connection. The default value is an empty string (""). The maximum length is 1024 characters.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06001BD1 RID: 7121 RVA: 0x00089B6D File Offset: 0x00087D6D
		// (set) Token: 0x06001BD2 RID: 7122 RVA: 0x00089B75 File Offset: 0x00087D75
		public override string ConnectionString
		{
			get
			{
				return this.ConnectionString_Get();
			}
			set
			{
				this.ConnectionString_Set(value);
			}
		}

		/// <summary>Gets or sets the time to wait while trying to establish a connection before terminating the attempt and generating an error.</summary>
		/// <returns>The time in seconds to wait for a connection to open. The default value is 15 seconds.</returns>
		/// <exception cref="T:System.ArgumentException">The value set is less than 0. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06001BD3 RID: 7123 RVA: 0x00089B7E File Offset: 0x00087D7E
		// (set) Token: 0x06001BD4 RID: 7124 RVA: 0x00089B86 File Offset: 0x00087D86
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(15)]
		public new int ConnectionTimeout
		{
			get
			{
				return this._connectionTimeout;
			}
			set
			{
				if (value < 0)
				{
					throw ODBC.NegativeArgument();
				}
				if (this.IsOpen)
				{
					throw ODBC.CantSetPropertyOnOpenConnection();
				}
				this._connectionTimeout = value;
			}
		}

		/// <summary>Gets the name of the current database or the database to be used after a connection is opened.</summary>
		/// <returns>The name of the current database. The default value is an empty string ("") until the connection is opened.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06001BD5 RID: 7125 RVA: 0x00089BA7 File Offset: 0x00087DA7
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string Database
		{
			get
			{
				if (this.IsOpen && !this.ProviderInfo.NoCurrentCatalog)
				{
					return this.GetConnectAttrString(ODBC32.SQL_ATTR.CURRENT_CATALOG);
				}
				return string.Empty;
			}
		}

		/// <summary>Gets the server name or file name of the data source.</summary>
		/// <returns>The server name or file name of the data source. The default value is an empty string ("") until the connection is opened.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06001BD6 RID: 7126 RVA: 0x00089BCC File Offset: 0x00087DCC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override string DataSource
		{
			get
			{
				if (this.IsOpen)
				{
					return this.GetInfoStringUnhandled(ODBC32.SQL_INFO.SERVER_NAME, true);
				}
				return string.Empty;
			}
		}

		/// <summary>Gets a string that contains the version of the server to which the client is connected.</summary>
		/// <returns>The version of the connected server.</returns>
		/// <exception cref="T:System.InvalidOperationException">The connection is closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06001BD7 RID: 7127 RVA: 0x00089BE5 File Offset: 0x00087DE5
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override string ServerVersion
		{
			get
			{
				return this.InnerConnection.ServerVersion;
			}
		}

		/// <summary>Gets the current state of the connection.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Data.ConnectionState" /> values. The default is Closed.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06001BD8 RID: 7128 RVA: 0x00089BF2 File Offset: 0x00087DF2
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override ConnectionState State
		{
			get
			{
				return this.InnerConnection.State;
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001BD9 RID: 7129 RVA: 0x00089BFF File Offset: 0x00087DFF
		internal OdbcConnectionPoolGroupProviderInfo ProviderInfo
		{
			get
			{
				return (OdbcConnectionPoolGroupProviderInfo)this.PoolGroup.ProviderInfo;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001BDA RID: 7130 RVA: 0x00089C11 File Offset: 0x00087E11
		internal ConnectionState InternalState
		{
			get
			{
				return this.State | this._extraState;
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001BDB RID: 7131 RVA: 0x00089C20 File Offset: 0x00087E20
		internal bool IsOpen
		{
			get
			{
				return this.InnerConnection is OdbcConnectionOpen;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001BDC RID: 7132 RVA: 0x00089C30 File Offset: 0x00087E30
		// (set) Token: 0x06001BDD RID: 7133 RVA: 0x00089C59 File Offset: 0x00087E59
		internal OdbcTransaction LocalTransaction
		{
			get
			{
				OdbcTransaction odbcTransaction = null;
				if (this._weakTransaction != null)
				{
					odbcTransaction = (OdbcTransaction)this._weakTransaction.Target;
				}
				return odbcTransaction;
			}
			set
			{
				this._weakTransaction = null;
				if (value != null)
				{
					this._weakTransaction = new WeakReference(value);
				}
			}
		}

		/// <summary>Gets the name of the ODBC driver specified for the current connection.</summary>
		/// <returns>The name of the ODBC driver. This typically is the DLL name (for example, Sqlsrv32.dll). The default value is an empty string ("") until the connection is opened.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001BDE RID: 7134 RVA: 0x00089C71 File Offset: 0x00087E71
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Driver
		{
			get
			{
				if (this.IsOpen)
				{
					if (this.ProviderInfo.DriverName == null)
					{
						this.ProviderInfo.DriverName = this.GetInfoStringUnhandled(ODBC32.SQL_INFO.DRIVER_NAME);
					}
					return this.ProviderInfo.DriverName;
				}
				return ADP.StrEmpty;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001BDF RID: 7135 RVA: 0x00089CAC File Offset: 0x00087EAC
		internal bool IsV3Driver
		{
			get
			{
				if (this.ProviderInfo.DriverVersion == null)
				{
					this.ProviderInfo.DriverVersion = this.GetInfoStringUnhandled(ODBC32.SQL_INFO.DRIVER_ODBC_VER);
					if (this.ProviderInfo.DriverVersion != null && this.ProviderInfo.DriverVersion.Length >= 2)
					{
						try
						{
							this.ProviderInfo.IsV3Driver = int.Parse(this.ProviderInfo.DriverVersion.Substring(0, 2), CultureInfo.InvariantCulture) >= 3;
							goto IL_0095;
						}
						catch (FormatException ex)
						{
							this.ProviderInfo.IsV3Driver = false;
							ADP.TraceExceptionWithoutRethrow(ex);
							goto IL_0095;
						}
					}
					this.ProviderInfo.DriverVersion = "";
				}
				IL_0095:
				return this.ProviderInfo.IsV3Driver;
			}
		}

		/// <summary>Occurs when the ODBC driver sends a warning or an informational message.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06001BE0 RID: 7136 RVA: 0x00089D6C File Offset: 0x00087F6C
		// (remove) Token: 0x06001BE1 RID: 7137 RVA: 0x00089D85 File Offset: 0x00087F85
		public event OdbcInfoMessageEventHandler InfoMessage
		{
			add
			{
				this._infoMessageEventHandler = (OdbcInfoMessageEventHandler)Delegate.Combine(this._infoMessageEventHandler, value);
			}
			remove
			{
				this._infoMessageEventHandler = (OdbcInfoMessageEventHandler)Delegate.Remove(this._infoMessageEventHandler, value);
			}
		}

		// Token: 0x06001BE2 RID: 7138 RVA: 0x00089DA0 File Offset: 0x00087FA0
		internal char EscapeChar(string method)
		{
			this.CheckState(method);
			if (!this.ProviderInfo.HasEscapeChar)
			{
				string infoStringUnhandled = this.GetInfoStringUnhandled(ODBC32.SQL_INFO.SEARCH_PATTERN_ESCAPE);
				this.ProviderInfo.EscapeChar = ((infoStringUnhandled.Length == 1) ? infoStringUnhandled[0] : this.QuoteChar(method)[0]);
			}
			return this.ProviderInfo.EscapeChar;
		}

		// Token: 0x06001BE3 RID: 7139 RVA: 0x00089E00 File Offset: 0x00088000
		internal string QuoteChar(string method)
		{
			this.CheckState(method);
			if (!this.ProviderInfo.HasQuoteChar)
			{
				string infoStringUnhandled = this.GetInfoStringUnhandled(ODBC32.SQL_INFO.IDENTIFIER_QUOTE_CHAR);
				this.ProviderInfo.QuoteChar = ((1 == infoStringUnhandled.Length) ? infoStringUnhandled : "\0");
			}
			return this.ProviderInfo.QuoteChar;
		}

		/// <summary>Starts a transaction at the data source.</summary>
		/// <returns>An object representing the new transaction.</returns>
		/// <exception cref="T:System.InvalidOperationException">A transaction is currently active. Parallel transactions are not supported. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001BE4 RID: 7140 RVA: 0x00089E51 File Offset: 0x00088051
		public new OdbcTransaction BeginTransaction()
		{
			return this.BeginTransaction(IsolationLevel.Unspecified);
		}

		/// <summary>Starts a transaction at the data source with the specified <see cref="T:System.Data.IsolationLevel" /> value.</summary>
		/// <returns>An object representing the new transaction.</returns>
		/// <param name="isolevel">The transaction isolation level for this connection. If you do not specify an isolation level, the default isolation level for the driver is used. </param>
		/// <exception cref="T:System.InvalidOperationException">A transaction is currently active. Parallel transactions are not supported. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001BE5 RID: 7141 RVA: 0x00089E5A File Offset: 0x0008805A
		public new OdbcTransaction BeginTransaction(IsolationLevel isolevel)
		{
			return (OdbcTransaction)this.InnerConnection.BeginTransaction(isolevel);
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x00089E70 File Offset: 0x00088070
		private void RollbackDeadTransaction()
		{
			WeakReference weakTransaction = this._weakTransaction;
			if (weakTransaction != null && !weakTransaction.IsAlive)
			{
				this._weakTransaction = null;
				this.ConnectionHandle.CompleteTransaction(1);
			}
		}

		/// <summary>Changes the current database associated with an open <see cref="T:System.Data.Odbc.OdbcConnection" />.</summary>
		/// <param name="value">The database name. </param>
		/// <exception cref="T:System.ArgumentException">The database name is not valid. </exception>
		/// <exception cref="T:System.InvalidOperationException">The connection is not open. </exception>
		/// <exception cref="T:System.Data.Odbc.OdbcException">Cannot change the database. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001BE7 RID: 7143 RVA: 0x00089EA3 File Offset: 0x000880A3
		public override void ChangeDatabase(string value)
		{
			this.InnerConnection.ChangeDatabase(value);
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x00089EB4 File Offset: 0x000880B4
		internal void CheckState(string method)
		{
			ConnectionState internalState = this.InternalState;
			if (ConnectionState.Open != internalState)
			{
				throw ADP.OpenConnectionRequired(method, internalState);
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.ICloneable.Clone" />.</summary>
		/// <returns>A new <see cref="T:System.Object" /> that is a copy of this instance.</returns>
		// Token: 0x06001BE9 RID: 7145 RVA: 0x00089ED4 File Offset: 0x000880D4
		object ICloneable.Clone()
		{
			return new OdbcConnection(this);
		}

		// Token: 0x06001BEA RID: 7146 RVA: 0x00089EDC File Offset: 0x000880DC
		internal bool ConnectionIsAlive(Exception innerException)
		{
			if (this.IsOpen)
			{
				if (!this.ProviderInfo.NoConnectionDead)
				{
					int connectAttr = this.GetConnectAttr(ODBC32.SQL_ATTR.CONNECTION_DEAD, ODBC32.HANDLER.IGNORE);
					if (1 == connectAttr)
					{
						this.Close();
						throw ADP.ConnectionIsDisabled(innerException);
					}
				}
				return true;
			}
			return false;
		}

		/// <summary>Creates and returns an <see cref="T:System.Data.Odbc.OdbcCommand" /> object associated with the <see cref="T:System.Data.Odbc.OdbcConnection" />.</summary>
		/// <returns>An <see cref="T:System.Data.Odbc.OdbcCommand" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001BEB RID: 7147 RVA: 0x00089F1F File Offset: 0x0008811F
		public new OdbcCommand CreateCommand()
		{
			return new OdbcCommand(string.Empty, this);
		}

		// Token: 0x06001BEC RID: 7148 RVA: 0x00089F2C File Offset: 0x0008812C
		internal OdbcStatementHandle CreateStatementHandle()
		{
			return new OdbcStatementHandle(this.ConnectionHandle);
		}

		/// <summary>Closes the connection to the data source. </summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001BED RID: 7149 RVA: 0x00089F3C File Offset: 0x0008813C
		public override void Close()
		{
			this.InnerConnection.CloseConnection(this, this.ConnectionFactory);
			OdbcConnectionHandle connectionHandle = this._connectionHandle;
			if (connectionHandle != null)
			{
				this._connectionHandle = null;
				WeakReference weakTransaction = this._weakTransaction;
				if (weakTransaction != null)
				{
					this._weakTransaction = null;
					IDisposable disposable = weakTransaction.Target as OdbcTransaction;
					if (disposable != null && weakTransaction.IsAlive)
					{
						disposable.Dispose();
					}
				}
				connectionHandle.Dispose();
			}
		}

		// Token: 0x06001BEE RID: 7150 RVA: 0x000094D4 File Offset: 0x000076D4
		private void DisposeMe(bool disposing)
		{
		}

		// Token: 0x06001BEF RID: 7151 RVA: 0x00089FA0 File Offset: 0x000881A0
		internal string GetConnectAttrString(ODBC32.SQL_ATTR attribute)
		{
			string text = "";
			int num = 0;
			byte[] array = new byte[100];
			OdbcConnectionHandle connectionHandle = this.ConnectionHandle;
			if (connectionHandle != null)
			{
				ODBC32.RetCode retCode = connectionHandle.GetConnectionAttribute(attribute, array, out num);
				if (array.Length + 2 <= num)
				{
					array = new byte[num + 2];
					retCode = connectionHandle.GetConnectionAttribute(attribute, array, out num);
				}
				if (retCode == ODBC32.RetCode.SUCCESS || ODBC32.RetCode.SUCCESS_WITH_INFO == retCode)
				{
					text = (BitConverter.IsLittleEndian ? Encoding.Unicode : Encoding.BigEndianUnicode).GetString(array, 0, Math.Min(num, array.Length));
				}
				else if (retCode == ODBC32.RetCode.ERROR)
				{
					string diagSqlState = this.GetDiagSqlState();
					if ("HYC00" == diagSqlState || "HY092" == diagSqlState || "IM001" == diagSqlState)
					{
						this.FlagUnsupportedConnectAttr(attribute);
					}
				}
			}
			return text;
		}

		// Token: 0x06001BF0 RID: 7152 RVA: 0x0008A064 File Offset: 0x00088264
		internal int GetConnectAttr(ODBC32.SQL_ATTR attribute, ODBC32.HANDLER handler)
		{
			int num = -1;
			int num2 = 0;
			byte[] array = new byte[4];
			OdbcConnectionHandle connectionHandle = this.ConnectionHandle;
			if (connectionHandle != null)
			{
				ODBC32.RetCode connectionAttribute = connectionHandle.GetConnectionAttribute(attribute, array, out num2);
				if (connectionAttribute == ODBC32.RetCode.SUCCESS || ODBC32.RetCode.SUCCESS_WITH_INFO == connectionAttribute)
				{
					num = BitConverter.ToInt32(array, 0);
				}
				else
				{
					if (connectionAttribute == ODBC32.RetCode.ERROR)
					{
						string diagSqlState = this.GetDiagSqlState();
						if ("HYC00" == diagSqlState || "HY092" == diagSqlState || "IM001" == diagSqlState)
						{
							this.FlagUnsupportedConnectAttr(attribute);
						}
					}
					if (handler == ODBC32.HANDLER.THROW)
					{
						this.HandleError(connectionHandle, connectionAttribute);
					}
				}
			}
			return num;
		}

		// Token: 0x06001BF1 RID: 7153 RVA: 0x0008A0F4 File Offset: 0x000882F4
		private string GetDiagSqlState()
		{
			string text;
			this.ConnectionHandle.GetDiagnosticField(out text);
			return text;
		}

		// Token: 0x06001BF2 RID: 7154 RVA: 0x0008A110 File Offset: 0x00088310
		internal ODBC32.RetCode GetInfoInt16Unhandled(ODBC32.SQL_INFO info, out short resultValue)
		{
			byte[] array = new byte[2];
			ODBC32.RetCode info2 = this.ConnectionHandle.GetInfo1(info, array);
			resultValue = BitConverter.ToInt16(array, 0);
			return info2;
		}

		// Token: 0x06001BF3 RID: 7155 RVA: 0x0008A13C File Offset: 0x0008833C
		internal ODBC32.RetCode GetInfoInt32Unhandled(ODBC32.SQL_INFO info, out int resultValue)
		{
			byte[] array = new byte[4];
			ODBC32.RetCode info2 = this.ConnectionHandle.GetInfo1(info, array);
			resultValue = BitConverter.ToInt32(array, 0);
			return info2;
		}

		// Token: 0x06001BF4 RID: 7156 RVA: 0x0008A168 File Offset: 0x00088368
		private int GetInfoInt32Unhandled(ODBC32.SQL_INFO infotype)
		{
			byte[] array = new byte[4];
			this.ConnectionHandle.GetInfo1(infotype, array);
			return BitConverter.ToInt32(array, 0);
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x0008A191 File Offset: 0x00088391
		internal string GetInfoStringUnhandled(ODBC32.SQL_INFO info)
		{
			return this.GetInfoStringUnhandled(info, false);
		}

		// Token: 0x06001BF6 RID: 7158 RVA: 0x0008A19C File Offset: 0x0008839C
		private string GetInfoStringUnhandled(ODBC32.SQL_INFO info, bool handleError)
		{
			string text = null;
			short num = 0;
			byte[] array = new byte[100];
			OdbcConnectionHandle connectionHandle = this.ConnectionHandle;
			if (connectionHandle != null)
			{
				ODBC32.RetCode retCode = connectionHandle.GetInfo2(info, array, out num);
				if (array.Length < (int)(num - 2))
				{
					array = new byte[(int)(num + 2)];
					retCode = connectionHandle.GetInfo2(info, array, out num);
				}
				if (retCode == ODBC32.RetCode.SUCCESS || retCode == ODBC32.RetCode.SUCCESS_WITH_INFO)
				{
					text = (BitConverter.IsLittleEndian ? Encoding.Unicode : Encoding.BigEndianUnicode).GetString(array, 0, Math.Min((int)num, array.Length));
				}
				else if (handleError)
				{
					this.HandleError(this.ConnectionHandle, retCode);
				}
			}
			else if (handleError)
			{
				text = "";
			}
			return text;
		}

		// Token: 0x06001BF7 RID: 7159 RVA: 0x0008A234 File Offset: 0x00088434
		internal Exception HandleErrorNoThrow(OdbcHandle hrHandle, ODBC32.RetCode retcode)
		{
			if (retcode != ODBC32.RetCode.SUCCESS)
			{
				if (retcode != ODBC32.RetCode.SUCCESS_WITH_INFO)
				{
					OdbcException ex = OdbcException.CreateException(ODBC32.GetDiagErrors(null, hrHandle, retcode), retcode);
					if (ex != null)
					{
						ex.Errors.SetSource(this.Driver);
					}
					this.ConnectionIsAlive(ex);
					return ex;
				}
				if (this._infoMessageEventHandler != null)
				{
					OdbcErrorCollection diagErrors = ODBC32.GetDiagErrors(null, hrHandle, retcode);
					diagErrors.SetSource(this.Driver);
					this.OnInfoMessage(new OdbcInfoMessageEventArgs(diagErrors));
				}
			}
			return null;
		}

		// Token: 0x06001BF8 RID: 7160 RVA: 0x0008A2A4 File Offset: 0x000884A4
		internal void HandleError(OdbcHandle hrHandle, ODBC32.RetCode retcode)
		{
			Exception ex = this.HandleErrorNoThrow(hrHandle, retcode);
			if (retcode > ODBC32.RetCode.SUCCESS_WITH_INFO)
			{
				throw ex;
			}
		}

		/// <summary>Opens a connection to a data source with the property settings specified by the <see cref="P:System.Data.Odbc.OdbcConnection.ConnectionString" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The functionality of this method is unsupported in the base class and must be implemented in a derived class instead. </exception>
		// Token: 0x06001BF9 RID: 7161 RVA: 0x0008A2C0 File Offset: 0x000884C0
		public override void Open()
		{
			try
			{
				this.InnerConnection.OpenConnection(this, this.ConnectionFactory);
			}
			catch (DllNotFoundException ex) when (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				throw new DllNotFoundException("Dependency unixODBC with minimum version 2.3.1 is required." + Environment.NewLine + ex.Message);
			}
			if (ADP.NeedManualEnlistment())
			{
				this.EnlistTransaction(Transaction.Current);
			}
		}

		// Token: 0x06001BFA RID: 7162 RVA: 0x0008A340 File Offset: 0x00088540
		private void OnInfoMessage(OdbcInfoMessageEventArgs args)
		{
			if (this._infoMessageEventHandler != null)
			{
				try
				{
					this._infoMessageEventHandler(this, args);
				}
				catch (Exception ex)
				{
					if (!ADP.IsCatchableOrSecurityExceptionType(ex))
					{
						throw;
					}
					ADP.TraceExceptionWithoutRethrow(ex);
				}
			}
		}

		/// <summary>Indicates that the ODBC Driver Manager environment handle can be released when the last underlying connection is released.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001BFB RID: 7163 RVA: 0x0008A388 File Offset: 0x00088588
		public static void ReleaseObjectPool()
		{
			OdbcEnvironment.ReleaseObjectPool();
		}

		// Token: 0x06001BFC RID: 7164 RVA: 0x0008A390 File Offset: 0x00088590
		internal OdbcTransaction SetStateExecuting(string method, OdbcTransaction transaction)
		{
			if (this._weakTransaction != null)
			{
				OdbcTransaction odbcTransaction = this._weakTransaction.Target as OdbcTransaction;
				if (transaction != odbcTransaction)
				{
					if (transaction == null)
					{
						throw ADP.TransactionRequired(method);
					}
					if (this != transaction.Connection)
					{
						throw ADP.TransactionConnectionMismatch();
					}
					transaction = null;
				}
			}
			else if (transaction != null)
			{
				if (transaction.Connection != null)
				{
					throw ADP.TransactionConnectionMismatch();
				}
				transaction = null;
			}
			ConnectionState connectionState = this.InternalState;
			if (ConnectionState.Open != connectionState)
			{
				this.NotifyWeakReference(1);
				connectionState = this.InternalState;
				if (ConnectionState.Open != connectionState)
				{
					if ((ConnectionState.Fetching & connectionState) != ConnectionState.Closed)
					{
						throw ADP.OpenReaderExists();
					}
					throw ADP.OpenConnectionRequired(method, connectionState);
				}
			}
			return transaction;
		}

		// Token: 0x06001BFD RID: 7165 RVA: 0x0008A420 File Offset: 0x00088620
		internal void SetSupportedType(ODBC32.SQL_TYPE sqltype)
		{
			ODBC32.SQL_CVT sql_CVT;
			switch (sqltype)
			{
			case ODBC32.SQL_TYPE.WLONGVARCHAR:
				sql_CVT = ODBC32.SQL_CVT.WLONGVARCHAR;
				break;
			case ODBC32.SQL_TYPE.WVARCHAR:
				sql_CVT = ODBC32.SQL_CVT.WVARCHAR;
				break;
			case ODBC32.SQL_TYPE.WCHAR:
				sql_CVT = ODBC32.SQL_CVT.WCHAR;
				break;
			default:
				if (sqltype != ODBC32.SQL_TYPE.NUMERIC)
				{
					return;
				}
				sql_CVT = ODBC32.SQL_CVT.NUMERIC;
				break;
			}
			this.ProviderInfo.TestedSQLTypes |= (int)sql_CVT;
			this.ProviderInfo.SupportedSQLTypes |= (int)sql_CVT;
		}

		// Token: 0x06001BFE RID: 7166 RVA: 0x0008A48C File Offset: 0x0008868C
		internal void FlagRestrictedSqlBindType(ODBC32.SQL_TYPE sqltype)
		{
			ODBC32.SQL_CVT sql_CVT;
			if (sqltype != ODBC32.SQL_TYPE.NUMERIC)
			{
				if (sqltype != ODBC32.SQL_TYPE.DECIMAL)
				{
					return;
				}
				sql_CVT = ODBC32.SQL_CVT.DECIMAL;
			}
			else
			{
				sql_CVT = ODBC32.SQL_CVT.NUMERIC;
			}
			this.ProviderInfo.RestrictedSQLBindTypes |= (int)sql_CVT;
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x0008A4BB File Offset: 0x000886BB
		internal void FlagUnsupportedConnectAttr(ODBC32.SQL_ATTR Attribute)
		{
			if (Attribute == ODBC32.SQL_ATTR.CURRENT_CATALOG)
			{
				this.ProviderInfo.NoCurrentCatalog = true;
				return;
			}
			if (Attribute != ODBC32.SQL_ATTR.CONNECTION_DEAD)
			{
				return;
			}
			this.ProviderInfo.NoConnectionDead = true;
		}

		// Token: 0x06001C00 RID: 7168 RVA: 0x0008A4E4 File Offset: 0x000886E4
		internal void FlagUnsupportedStmtAttr(ODBC32.SQL_ATTR Attribute)
		{
			if (Attribute == ODBC32.SQL_ATTR.QUERY_TIMEOUT)
			{
				this.ProviderInfo.NoQueryTimeout = true;
				return;
			}
			if (Attribute == ODBC32.SQL_ATTR.SQL_COPT_SS_TXN_ISOLATION)
			{
				this.ProviderInfo.NoSqlSoptSSHiddenColumns = true;
				return;
			}
			if (Attribute != (ODBC32.SQL_ATTR)1228)
			{
				return;
			}
			this.ProviderInfo.NoSqlSoptSSNoBrowseTable = true;
		}

		// Token: 0x06001C01 RID: 7169 RVA: 0x0008A520 File Offset: 0x00088720
		internal void FlagUnsupportedColAttr(ODBC32.SQL_DESC v3FieldId, ODBC32.SQL_COLUMN v2FieldId)
		{
			if (this.IsV3Driver && v3FieldId == (ODBC32.SQL_DESC)1212)
			{
				this.ProviderInfo.NoSqlCASSColumnKey = true;
			}
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x0008A540 File Offset: 0x00088740
		internal bool SQLGetFunctions(ODBC32.SQL_API odbcFunction)
		{
			OdbcConnectionHandle connectionHandle = this.ConnectionHandle;
			if (connectionHandle != null)
			{
				short num;
				ODBC32.RetCode functions = connectionHandle.GetFunctions(odbcFunction, out num);
				if (functions != ODBC32.RetCode.SUCCESS)
				{
					this.HandleError(connectionHandle, functions);
				}
				return num != 0;
			}
			throw ODBC.ConnectionClosed();
		}

		// Token: 0x06001C03 RID: 7171 RVA: 0x0008A57C File Offset: 0x0008877C
		internal bool TestTypeSupport(ODBC32.SQL_TYPE sqltype)
		{
			ODBC32.SQL_CONVERT sql_CONVERT;
			ODBC32.SQL_CVT sql_CVT;
			switch (sqltype)
			{
			case ODBC32.SQL_TYPE.WLONGVARCHAR:
				sql_CONVERT = ODBC32.SQL_CONVERT.LONGVARCHAR;
				sql_CVT = ODBC32.SQL_CVT.WLONGVARCHAR;
				break;
			case ODBC32.SQL_TYPE.WVARCHAR:
				sql_CONVERT = ODBC32.SQL_CONVERT.VARCHAR;
				sql_CVT = ODBC32.SQL_CVT.WVARCHAR;
				break;
			case ODBC32.SQL_TYPE.WCHAR:
				sql_CONVERT = ODBC32.SQL_CONVERT.CHAR;
				sql_CVT = ODBC32.SQL_CVT.WCHAR;
				break;
			default:
				if (sqltype != ODBC32.SQL_TYPE.NUMERIC)
				{
					return false;
				}
				sql_CONVERT = ODBC32.SQL_CONVERT.NUMERIC;
				sql_CVT = ODBC32.SQL_CVT.NUMERIC;
				break;
			}
			if ((this.ProviderInfo.TestedSQLTypes & (int)sql_CVT) == 0)
			{
				int num = this.GetInfoInt32Unhandled((ODBC32.SQL_INFO)sql_CONVERT);
				num &= (int)sql_CVT;
				this.ProviderInfo.TestedSQLTypes |= (int)sql_CVT;
				this.ProviderInfo.SupportedSQLTypes |= num;
			}
			return (this.ProviderInfo.SupportedSQLTypes & (int)sql_CVT) != 0;
		}

		// Token: 0x06001C04 RID: 7172 RVA: 0x0008A620 File Offset: 0x00088820
		internal bool TestRestrictedSqlBindType(ODBC32.SQL_TYPE sqltype)
		{
			ODBC32.SQL_CVT sql_CVT;
			if (sqltype != ODBC32.SQL_TYPE.NUMERIC)
			{
				if (sqltype != ODBC32.SQL_TYPE.DECIMAL)
				{
					return false;
				}
				sql_CVT = ODBC32.SQL_CVT.DECIMAL;
			}
			else
			{
				sql_CVT = ODBC32.SQL_CVT.NUMERIC;
			}
			return (this.ProviderInfo.RestrictedSQLBindTypes & (int)sql_CVT) != 0;
		}

		// Token: 0x06001C05 RID: 7173 RVA: 0x0008A651 File Offset: 0x00088851
		protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
		{
			DbTransaction dbTransaction = this.InnerConnection.BeginTransaction(isolationLevel);
			GC.KeepAlive(this);
			return dbTransaction;
		}

		// Token: 0x06001C06 RID: 7174 RVA: 0x0008A668 File Offset: 0x00088868
		internal OdbcTransaction Open_BeginTransaction(IsolationLevel isolevel)
		{
			this.CheckState("BeginTransaction");
			this.RollbackDeadTransaction();
			if (this._weakTransaction != null && this._weakTransaction.IsAlive)
			{
				throw ADP.ParallelTransactionsNotSupported(this);
			}
			if (isolevel <= IsolationLevel.ReadUncommitted)
			{
				if (isolevel == IsolationLevel.Unspecified)
				{
					goto IL_0082;
				}
				if (isolevel == IsolationLevel.Chaos)
				{
					throw ODBC.NotSupportedIsolationLevel(isolevel);
				}
				if (isolevel == IsolationLevel.ReadUncommitted)
				{
					goto IL_0082;
				}
			}
			else if (isolevel <= IsolationLevel.RepeatableRead)
			{
				if (isolevel == IsolationLevel.ReadCommitted || isolevel == IsolationLevel.RepeatableRead)
				{
					goto IL_0082;
				}
			}
			else if (isolevel == IsolationLevel.Serializable || isolevel == IsolationLevel.Snapshot)
			{
				goto IL_0082;
			}
			throw ADP.InvalidIsolationLevel(isolevel);
			IL_0082:
			OdbcConnectionHandle connectionHandle = this.ConnectionHandle;
			ODBC32.RetCode retCode = connectionHandle.BeginTransaction(ref isolevel);
			if (retCode == ODBC32.RetCode.ERROR)
			{
				this.HandleError(connectionHandle, retCode);
			}
			OdbcTransaction odbcTransaction = new OdbcTransaction(this, isolevel, connectionHandle);
			this._weakTransaction = new WeakReference(odbcTransaction);
			return odbcTransaction;
		}

		// Token: 0x06001C07 RID: 7175 RVA: 0x0008A72C File Offset: 0x0008892C
		internal void Open_ChangeDatabase(string value)
		{
			this.CheckState("ChangeDatabase");
			if (value == null || value.Trim().Length == 0)
			{
				throw ADP.EmptyDatabaseName();
			}
			if (1024 < value.Length * 2 + 2)
			{
				throw ADP.DatabaseNameTooLong();
			}
			this.RollbackDeadTransaction();
			OdbcConnectionHandle connectionHandle = this.ConnectionHandle;
			ODBC32.RetCode retCode = connectionHandle.SetConnectionAttribute3(ODBC32.SQL_ATTR.CURRENT_CATALOG, value, checked(value.Length * 2));
			if (retCode != ODBC32.RetCode.SUCCESS)
			{
				this.HandleError(connectionHandle, retCode);
			}
		}

		// Token: 0x06001C08 RID: 7176 RVA: 0x0008A79B File Offset: 0x0008899B
		internal string Open_GetServerVersion()
		{
			return this.GetInfoStringUnhandled(ODBC32.SQL_INFO.DBMS_VER, true);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcConnection" /> class.</summary>
		// Token: 0x06001C09 RID: 7177 RVA: 0x0008A7A6 File Offset: 0x000889A6
		public OdbcConnection()
		{
			GC.SuppressFinalize(this);
			this._innerConnection = DbConnectionClosedNeverOpened.SingletonInstance;
		}

		// Token: 0x06001C0A RID: 7178 RVA: 0x0008A7C8 File Offset: 0x000889C8
		private void CopyFrom(OdbcConnection connection)
		{
			ADP.CheckArgumentNull(connection, "connection");
			this._userConnectionOptions = connection.UserConnectionOptions;
			this._poolGroup = connection.PoolGroup;
			if (DbConnectionClosedNeverOpened.SingletonInstance == connection._innerConnection)
			{
				this._innerConnection = DbConnectionClosedNeverOpened.SingletonInstance;
				return;
			}
			this._innerConnection = DbConnectionClosedPreviouslyOpened.SingletonInstance;
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06001C0B RID: 7179 RVA: 0x0008A81C File Offset: 0x00088A1C
		internal int CloseCount
		{
			get
			{
				return this._closeCount;
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06001C0C RID: 7180 RVA: 0x0008A824 File Offset: 0x00088A24
		internal DbConnectionFactory ConnectionFactory
		{
			get
			{
				return OdbcConnection.s_connectionFactory;
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001C0D RID: 7181 RVA: 0x0008A82C File Offset: 0x00088A2C
		internal DbConnectionOptions ConnectionOptions
		{
			get
			{
				DbConnectionPoolGroup poolGroup = this.PoolGroup;
				if (poolGroup == null)
				{
					return null;
				}
				return poolGroup.ConnectionOptions;
			}
		}

		// Token: 0x06001C0E RID: 7182 RVA: 0x0008A84C File Offset: 0x00088A4C
		private string ConnectionString_Get()
		{
			bool shouldHidePassword = this.InnerConnection.ShouldHidePassword;
			DbConnectionOptions userConnectionOptions = this.UserConnectionOptions;
			if (userConnectionOptions == null)
			{
				return "";
			}
			return userConnectionOptions.UsersConnectionString(shouldHidePassword);
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x0008A87C File Offset: 0x00088A7C
		private void ConnectionString_Set(string value)
		{
			DbConnectionPoolKey dbConnectionPoolKey = new DbConnectionPoolKey(value);
			this.ConnectionString_Set(dbConnectionPoolKey);
		}

		// Token: 0x06001C10 RID: 7184 RVA: 0x0008A898 File Offset: 0x00088A98
		private void ConnectionString_Set(DbConnectionPoolKey key)
		{
			DbConnectionOptions dbConnectionOptions = null;
			DbConnectionPoolGroup connectionPoolGroup = this.ConnectionFactory.GetConnectionPoolGroup(key, null, ref dbConnectionOptions);
			DbConnectionInternal innerConnection = this.InnerConnection;
			bool flag = innerConnection.AllowSetConnectionString;
			if (flag)
			{
				flag = this.SetInnerConnectionFrom(DbConnectionClosedBusy.SingletonInstance, innerConnection);
				if (flag)
				{
					this._userConnectionOptions = dbConnectionOptions;
					this._poolGroup = connectionPoolGroup;
					this._innerConnection = DbConnectionClosedNeverOpened.SingletonInstance;
				}
			}
			if (!flag)
			{
				throw ADP.OpenConnectionPropertySet("ConnectionString", innerConnection.State);
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001C11 RID: 7185 RVA: 0x0008A905 File Offset: 0x00088B05
		internal DbConnectionInternal InnerConnection
		{
			get
			{
				return this._innerConnection;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001C12 RID: 7186 RVA: 0x0008A90D File Offset: 0x00088B0D
		// (set) Token: 0x06001C13 RID: 7187 RVA: 0x0008A915 File Offset: 0x00088B15
		internal DbConnectionPoolGroup PoolGroup
		{
			get
			{
				return this._poolGroup;
			}
			set
			{
				this._poolGroup = value;
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001C14 RID: 7188 RVA: 0x0008A91E File Offset: 0x00088B1E
		internal DbConnectionOptions UserConnectionOptions
		{
			get
			{
				return this._userConnectionOptions;
			}
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x0008A928 File Offset: 0x00088B28
		internal void Abort(Exception e)
		{
			DbConnectionInternal innerConnection = this._innerConnection;
			if (ConnectionState.Open == innerConnection.State)
			{
				Interlocked.CompareExchange<DbConnectionInternal>(ref this._innerConnection, DbConnectionClosedPreviouslyOpened.SingletonInstance, innerConnection);
				innerConnection.DoomThisConnection();
			}
		}

		// Token: 0x06001C16 RID: 7190 RVA: 0x0008A95D File Offset: 0x00088B5D
		internal void AddWeakReference(object value, int tag)
		{
			this.InnerConnection.AddWeakReference(value, tag);
		}

		// Token: 0x06001C17 RID: 7191 RVA: 0x0008A96C File Offset: 0x00088B6C
		protected override DbCommand CreateDbCommand()
		{
			DbCommand dbCommand = this.ConnectionFactory.ProviderFactory.CreateCommand();
			dbCommand.Connection = this;
			return dbCommand;
		}

		// Token: 0x06001C18 RID: 7192 RVA: 0x0008A985 File Offset: 0x00088B85
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._userConnectionOptions = null;
				this._poolGroup = null;
				this.Close();
			}
			this.DisposeMe(disposing);
			base.Dispose(disposing);
		}

		/// <summary>Returns schema information for the data source of this <see cref="T:System.Data.Odbc.OdbcConnection" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that contains schema information.</returns>
		// Token: 0x06001C19 RID: 7193 RVA: 0x0005B07C File Offset: 0x0005927C
		public override DataTable GetSchema()
		{
			return this.GetSchema(DbMetaDataCollectionNames.MetaDataCollections, null);
		}

		/// <summary>Returns schema information for the data source of this <see cref="T:System.Data.Odbc.OdbcConnection" /> using the specified name for the schema name.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that contains schema information.</returns>
		/// <param name="collectionName">Specifies the name of the schema to return.</param>
		// Token: 0x06001C1A RID: 7194 RVA: 0x0005B08A File Offset: 0x0005928A
		public override DataTable GetSchema(string collectionName)
		{
			return this.GetSchema(collectionName, null);
		}

		/// <summary>Returns schema information for the data source of this <see cref="T:System.Data.Odbc.OdbcConnection" /> using the specified string for the schema name and the specified string array for the restriction values.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that contains schema information.</returns>
		/// <param name="collectionName">Specifies the name of the schema to return.</param>
		/// <param name="restrictionValues">Specifies a set of restriction values for the requested schema.</param>
		// Token: 0x06001C1B RID: 7195 RVA: 0x0008A9AC File Offset: 0x00088BAC
		public override DataTable GetSchema(string collectionName, string[] restrictionValues)
		{
			return this.InnerConnection.GetSchema(this.ConnectionFactory, this.PoolGroup, this, collectionName, restrictionValues);
		}

		// Token: 0x06001C1C RID: 7196 RVA: 0x0008A9C8 File Offset: 0x00088BC8
		internal void NotifyWeakReference(int message)
		{
			this.InnerConnection.NotifyWeakReference(message);
		}

		// Token: 0x06001C1D RID: 7197 RVA: 0x0008A9D8 File Offset: 0x00088BD8
		internal void PermissionDemand()
		{
			DbConnectionPoolGroup poolGroup = this.PoolGroup;
			DbConnectionOptions dbConnectionOptions = ((poolGroup != null) ? poolGroup.ConnectionOptions : null);
			if (dbConnectionOptions == null || dbConnectionOptions.IsEmpty)
			{
				throw ADP.NoConnectionString();
			}
			DbConnectionOptions userConnectionOptions = this.UserConnectionOptions;
		}

		// Token: 0x06001C1E RID: 7198 RVA: 0x0008AA11 File Offset: 0x00088C11
		internal void RemoveWeakReference(object value)
		{
			this.InnerConnection.RemoveWeakReference(value);
		}

		// Token: 0x06001C1F RID: 7199 RVA: 0x0008AA20 File Offset: 0x00088C20
		internal void SetInnerConnectionEvent(DbConnectionInternal to)
		{
			ConnectionState connectionState = this._innerConnection.State & ConnectionState.Open;
			ConnectionState connectionState2 = to.State & ConnectionState.Open;
			if (connectionState != connectionState2 && connectionState2 == ConnectionState.Closed)
			{
				this._closeCount++;
			}
			this._innerConnection = to;
			if (connectionState == ConnectionState.Closed && ConnectionState.Open == connectionState2)
			{
				this.OnStateChange(DbConnectionInternal.StateChangeOpen);
				return;
			}
			if (ConnectionState.Open == connectionState && connectionState2 == ConnectionState.Closed)
			{
				this.OnStateChange(DbConnectionInternal.StateChangeClosed);
				return;
			}
			if (connectionState != connectionState2)
			{
				this.OnStateChange(new StateChangeEventArgs(connectionState, connectionState2));
			}
		}

		// Token: 0x06001C20 RID: 7200 RVA: 0x0008AA97 File Offset: 0x00088C97
		internal bool SetInnerConnectionFrom(DbConnectionInternal to, DbConnectionInternal from)
		{
			return from == Interlocked.CompareExchange<DbConnectionInternal>(ref this._innerConnection, to, from);
		}

		// Token: 0x06001C21 RID: 7201 RVA: 0x0008AAA9 File Offset: 0x00088CA9
		internal void SetInnerConnectionTo(DbConnectionInternal to)
		{
			this._innerConnection = to;
		}

		/// <summary>Enlists in the specified transaction as a distributed transaction.</summary>
		/// <param name="transaction">A reference to an existing <see cref="T:System.EnterpriseServices.ITransaction" /> in which to enlist.</param>
		// Token: 0x06001C23 RID: 7203 RVA: 0x0000E24C File Offset: 0x0000C44C
		public void EnlistDistributedTransaction(ITransaction transaction)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001543 RID: 5443
		private int _connectionTimeout = 15;

		// Token: 0x04001544 RID: 5444
		private OdbcInfoMessageEventHandler _infoMessageEventHandler;

		// Token: 0x04001545 RID: 5445
		private WeakReference _weakTransaction;

		// Token: 0x04001546 RID: 5446
		private OdbcConnectionHandle _connectionHandle;

		// Token: 0x04001547 RID: 5447
		private ConnectionState _extraState;

		// Token: 0x04001548 RID: 5448
		private static readonly DbConnectionFactory s_connectionFactory = OdbcConnectionFactory.SingletonInstance;

		// Token: 0x04001549 RID: 5449
		private DbConnectionOptions _userConnectionOptions;

		// Token: 0x0400154A RID: 5450
		private DbConnectionPoolGroup _poolGroup;

		// Token: 0x0400154B RID: 5451
		private DbConnectionInternal _innerConnection;

		// Token: 0x0400154C RID: 5452
		private int _closeCount;
	}
}
