using System;
using System.Data.Common;

namespace System.Data.Sql
{
	/// <summary>Represents a request for notification for a given command. </summary>
	// Token: 0x02000125 RID: 293
	public sealed class SqlNotificationRequest
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Data.Sql.SqlNotificationRequest" /> class with default values.</summary>
		// Token: 0x06000FE3 RID: 4067 RVA: 0x0004F238 File Offset: 0x0004D438
		public SqlNotificationRequest()
			: this(null, null, 0)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Data.Sql.SqlNotificationRequest" /> class with a user-defined string that identifies a particular notification request, the name of a predefined SQL Server 2005 Service Broker service name, and the time-out period, measured in seconds.</summary>
		/// <param name="userData">A string that contains an application-specific identifier for this notification. It is not used by the notifications infrastructure, but it allows you to associate notifications with the application state. The value indicated in this parameter is included in the Service Broker queue message. </param>
		/// <param name="options">A string that contains the Service Broker service name where notification messages are posted, and it must include a database name or a Service Broker instance GUID that restricts the scope of the service name lookup to a particular database.For more information about the format of the <paramref name="options" /> parameter, see <see cref="P:System.Data.Sql.SqlNotificationRequest.Options" />.</param>
		/// <param name="timeout">The time, in seconds, to wait for a notification message. </param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="options" /> parameter is NULL. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="options" /> or <paramref name="userData" /> parameter is longer than uint16.MaxValue or the value in the <paramref name="timeout" /> parameter is less than zero. </exception>
		// Token: 0x06000FE4 RID: 4068 RVA: 0x0004F243 File Offset: 0x0004D443
		public SqlNotificationRequest(string userData, string options, int timeout)
		{
			this.UserData = userData;
			this.Timeout = timeout;
			this.Options = options;
		}

		/// <summary>Gets or sets the SQL Server Service Broker service name where notification messages are posted.</summary>
		/// <returns>string that contains the SQL Server 2005 Service Broker service name where notification messages are posted and the database or service broker instance GUID to scope the server name lookup.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value is NULL. </exception>
		/// <exception cref="T:System.ArgumentException">The value is longer than uint16.MaxValue. </exception>
		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000FE5 RID: 4069 RVA: 0x0004F260 File Offset: 0x0004D460
		// (set) Token: 0x06000FE6 RID: 4070 RVA: 0x0004F268 File Offset: 0x0004D468
		public string Options
		{
			get
			{
				return this._options;
			}
			set
			{
				if (value != null && 65535 < value.Length)
				{
					throw ADP.ArgumentOutOfRange(string.Empty, "Options");
				}
				this._options = value;
			}
		}

		/// <summary>Gets or sets a value that specifies how long SQL Server waits for a change to occur before the operation times out.</summary>
		/// <returns>A signed integer value that specifies, in seconds, how long SQL Server waits for a change to occur before the operation times out.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than zero. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000FE7 RID: 4071 RVA: 0x0004F291 File Offset: 0x0004D491
		// (set) Token: 0x06000FE8 RID: 4072 RVA: 0x0004F299 File Offset: 0x0004D499
		public int Timeout
		{
			get
			{
				return this._timeout;
			}
			set
			{
				if (0 > value)
				{
					throw ADP.ArgumentOutOfRange(string.Empty, "Timeout");
				}
				this._timeout = value;
			}
		}

		/// <summary>Gets or sets an application-specific identifier for this notification.</summary>
		/// <returns>A string value of the application-specific identifier for this notification.</returns>
		/// <exception cref="T:System.ArgumentException">The value is longer than uint16.MaxValue. </exception>
		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000FE9 RID: 4073 RVA: 0x0004F2B6 File Offset: 0x0004D4B6
		// (set) Token: 0x06000FEA RID: 4074 RVA: 0x0004F2BE File Offset: 0x0004D4BE
		public string UserData
		{
			get
			{
				return this._userData;
			}
			set
			{
				if (value != null && 65535 < value.Length)
				{
					throw ADP.ArgumentOutOfRange(string.Empty, "UserData");
				}
				this._userData = value;
			}
		}

		// Token: 0x04000A16 RID: 2582
		private string _userData;

		// Token: 0x04000A17 RID: 2583
		private string _options;

		// Token: 0x04000A18 RID: 2584
		private int _timeout;
	}
}
